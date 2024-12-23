﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Clients : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private string musicFolder = @"C:\Music"; // Thư mục mặc định chứa file WAV
        private string selectedFolderPath; // Khai báo biến toàn cục để lưu đường dẫn thư mục

        public Clients()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string serverAddress = txtServerAddress.Text.Trim();
                if (string.IsNullOrEmpty(serverAddress))
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ server!");
                    return;
                }

                // Kết nối đến server
                client = new TcpClient();
                client.Connect(serverAddress, 5000);
                stream = client.GetStream();

                listBoxLog.Items.Add("Đã kết nối đến server!");
                btnConnect.Enabled = false;

                // Lắng nghe lệnh từ server
                ListenToServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối: {ex.Message}");
            }
        }
        private async void ListenToServer()
        {
            var buffer = new byte[4096];
            while (true)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    ProcessServerMessage(message);
                }
                catch (Exception ex)
                {
                    listBoxLog.Items.Add($"Mất kết nối với server: {ex.Message}");
                    break;
                }
            }
        }

        private void ProcessServerMessage(string message)
        {
            if (message == "GET_FILES")
            {
                // Kiểm tra xem thư mục đã được chọn chưa
                if (!string.IsNullOrEmpty(selectedFolderPath) && Directory.Exists(selectedFolderPath))
                {
                    var files = Directory.GetFiles(selectedFolderPath, "*.wav");
                    var fileList = string.Join("|", files);

                    // Gửi danh sách file lại cho server
                    var responseMessage = Encoding.UTF8.GetBytes(fileList);
                    stream.Write(responseMessage, 0, responseMessage.Length);
                }
                else
                {
                    listBoxLog.Items.Add("Chưa chọn thư mục hoặc thư mục không hợp lệ.");
                }
            }
            else if (message == "BROWSE")
            {
                // Đảm bảo Folder Dialog mở trong UI thread
                Invoke(new Action(() =>
                {
                    using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                    {
                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            selectedFolderPath = folderDialog.SelectedPath; // Lưu đường dẫn thư mục đã chọn

                            // Gửi đường dẫn thư mục về server
                            var responseMessage = Encoding.UTF8.GetBytes($"FOLDER_SELECTED|{selectedFolderPath}");
                            stream.Write(responseMessage, 0, responseMessage.Length);

                            listBoxLog.Items.Add($"Đã chọn thư mục: {selectedFolderPath}");
                        }
                    }
                }));
            }


            else if (message.StartsWith("PLAY|"))
            {
                // Play the requested music file
                var fileName = message.Split('|')[1];
                var filePath = Path.Combine(musicFolder, fileName);
                if (File.Exists(filePath))
                {
                    var player = new SoundPlayer(filePath);
                    player.Play();

                    listBoxLog.Items.Add($"Đang chơi: {fileName}");
                }
                else
                {
                    listBoxLog.Items.Add($"File không tồn tại: {fileName}");
                }
            }
            else if (message.StartsWith("UPLOAD|"))
            {
                // Tách thông tin file
                var parts = message.Split('|');
                var fileName = parts[1];
                var fileSize = int.Parse(parts[2]);
                var filePath = Path.Combine(musicFolder, fileName);

                // Nhận nội dung file
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    var buffer = new byte[4096];
                    int totalBytesRead = 0;

                    while (totalBytesRead < fileSize)
                    {
                        int bytesRead = stream.Read(buffer, 0, Math.Min(buffer.Length, fileSize - totalBytesRead));
                        if (bytesRead == 0) break;

                        fileStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;
                    }
                }

                listBoxLog.Items.Add($"File được nhận từ server: {fileName} ({fileSize} bytes)");
            }
            else if (message.StartsWith("DELETE|"))
            {
                var fileName = message.Split('|')[1];
                var filePath = Path.Combine(musicFolder, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    listBoxLog.Items.Add($"File đã bị xóa: {fileName}");
                }
                else
                {
                    listBoxLog.Items.Add($"Không tìm thấy file: {fileName}");
                }
            }
            else if (message.StartsWith("STOP|"))
            {
                var filename = message.Split('|')[1];
                var filepath = Path.Combine(musicFolder, filename);
                if (File.Exists(filepath))
                {
                    var player = new SoundPlayer(filepath);
                    player.Stop();
                    listBoxLog.Items.Add("Nhạc đã dừng");
                }
            }
        }

    }
}
