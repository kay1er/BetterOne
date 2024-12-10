using System;
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
                // Lấy danh sách file WAV từ thư mục
                var files = Directory.GetFiles(musicFolder, "*.wav");
                var fileList = string.Join("|", files);

                // Gửi danh sách file về server
                var response = Encoding.UTF8.GetBytes(fileList);
                stream.Write(response, 0, response.Length);

                listBoxLog.Items.Add("Đã gửi danh sách file WAV cho server.");
            }
            else if (message.StartsWith("PLAY|"))
            {
                // Phát file nhạc được yêu cầu
                var fileName = message.Split('|')[1];
                if (File.Exists(fileName))
                {
                    var player = new SoundPlayer(fileName);
                    player.Play();

                    listBoxLog.Items.Add($"Đang phát nhạc: {Path.GetFileName(fileName)}");
                }
                else
                {
                    listBoxLog.Items.Add($"File không tồn tại: {fileName}");
                }
            }
        }
        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    musicFolder = folderDialog.SelectedPath;
                    listBoxLog.Items.Add($"Đã chọn thư mục nhạc: {musicFolder}");
                }
            }
        }
    }
}
