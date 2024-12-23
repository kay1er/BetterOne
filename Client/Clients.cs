using System; // Sử dụng các lớp cơ bản trong .NET.
using System.Collections.Generic; // Sử dụng để làm việc với các danh sách.
using System.ComponentModel; // Sử dụng cho các tính năng liên quan đến giao diện người dùng.
using System.Data; // Sử dụng để làm việc với dữ liệu trong ứng dụng.
using System.Drawing; // Sử dụng cho việc vẽ đồ họa và giao diện người dùng.
using System.Linq; // Sử dụng các phương thức LINQ cho các phép toán trên tập hợp.
using System.Media; // Sử dụng để phát nhạc.
using System.Net.Sockets; // Sử dụng để tạo kết nối TCP.
using System.Text; // Sử dụng để mã hóa và giải mã chuỗi.
using System.Threading.Tasks; // Sử dụng cho các tác vụ bất đồng bộ.
using System.Windows.Forms; // Sử dụng để xây dựng giao diện Windows Forms.

namespace Client
{
    public partial class Clients : Form
    {
        private TcpClient client; // Biến lưu trữ đối tượng TcpClient để kết nối với server.
        private NetworkStream stream; // Biến lưu trữ luồng mạng cho giao tiếp với server.
        private string musicFolder = @"C:\Music"; // Thư mục mặc định chứa các file nhạc WAV.

        public Clients()
        {
            InitializeComponent(); // Khởi tạo các thành phần của giao diện người dùng.
        }

        // Sự kiện khi nhấn nút kết nối.
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string serverAddress = txtServerAddress.Text.Trim(); // Lấy địa chỉ server từ ô nhập liệu.
                if (string.IsNullOrEmpty(serverAddress)) // Kiểm tra nếu địa chỉ server trống.
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ server!"); // Hiển thị thông báo nếu địa chỉ server trống.
                    return;
                }

                // Kết nối đến server.
                client = new TcpClient();
                client.Connect(serverAddress, 5000); // Kết nối đến server tại cổng 5000.
                stream = client.GetStream(); // Lấy luồng mạng từ kết nối.

                listBoxLog.Items.Add("Đã kết nối đến server!"); // Thêm log vào ListBox.
                btnConnect.Enabled = false; // Vô hiệu hóa nút kết nối sau khi kết nối thành công.

                // Lắng nghe lệnh từ server.
                ListenToServer(); // Gọi phương thức để lắng nghe server.
            }
            catch (Exception ex) // Xử lý lỗi khi không thể kết nối.
            {
                MessageBox.Show($"Không thể kết nối: {ex.Message}"); // Hiển thị thông báo lỗi.
            }
        }

        // Phương thức lắng nghe tin nhắn từ server.
        private async void ListenToServer()
        {
            var buffer = new byte[4096]; // Bộ đệm để lưu trữ dữ liệu nhận từ server.
            while (true)
            {
                try
                {
                    // Đọc dữ liệu từ luồng mạng.
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // Nếu không có dữ liệu, thoát khỏi vòng lặp.

                    var message = Encoding.UTF8.GetString(buffer, 0, bytesRead); // Chuyển dữ liệu byte thành chuỗi.
                    ProcessServerMessage(message); // Xử lý tin nhắn từ server.
                }
                catch (Exception ex) // Xử lý lỗi khi mất kết nối.
                {
                    listBoxLog.Items.Add($"Mất kết nối với server: {ex.Message}"); // Thêm log vào ListBox.
                    break; // Thoát khỏi vòng lặp khi có lỗi.
                }
            }
        }

        // Phương thức xử lý tin nhắn từ server.
        private void ProcessServerMessage(string message)
        {
            if (message == "GET_FILES") // Nếu server yêu cầu danh sách file.
            {
                // Lấy danh sách các file WAV trong thư mục.
                var files = Directory.GetFiles(musicFolder, "*.wav");
                var fileList = string.Join("|", files); // Nối danh sách file thành chuỗi.

                // Gửi danh sách file lại cho server.
                var response = Encoding.UTF8.GetBytes(fileList);
                stream.Write(response, 0, response.Length); // Gửi dữ liệu tới server.

                listBoxLog.Items.Add("Đã gửi danh sách nhạc tới server."); // Thêm log vào ListBox.
            }
            else if (message.StartsWith("PLAY|")) // Nếu server yêu cầu phát nhạc.
            {
                var fileName = message.Split('|')[1]; // Lấy tên file từ thông điệp.
                var filePath = Path.Combine(musicFolder, fileName); // Lấy đường dẫn đầy đủ của file nhạc.

                if (File.Exists(filePath)) // Kiểm tra nếu file tồn tại.
                {
                    var player = new SoundPlayer(filePath); // Tạo đối tượng SoundPlayer để phát nhạc.
                    player.Play(); // Phát nhạc.

                    listBoxLog.Items.Add($"Đang chơi: {fileName}"); // Thêm log vào ListBox.
                }
                else
                {
                    listBoxLog.Items.Add($"File không tồn tại: {fileName}"); // Thêm log nếu file không tồn tại.
                }
            }
            else if (message.StartsWith("UPLOAD|")) // Nếu server yêu cầu tải lên file.
            {
                var parts = message.Split('|'); // Tách thông tin file.
                var fileName = parts[1]; // Lấy tên file.
                var fileSize = int.Parse(parts[2]); // Lấy kích thước file.
                var filePath = Path.Combine(musicFolder, fileName); // Lấy đường dẫn đầy đủ của file.

                // Nhận dữ liệu file từ server và lưu vào thư mục.
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    var buffer = new byte[4096]; // Bộ đệm để nhận dữ liệu.
                    int totalBytesRead = 0; // Biến lưu trữ tổng số byte đã nhận.

                    // Nhận và ghi dữ liệu vào file cho đến khi hoàn thành.
                    while (totalBytesRead < fileSize)
                    {
                        int bytesRead = stream.Read(buffer, 0, Math.Min(buffer.Length, fileSize - totalBytesRead));
                        if (bytesRead == 0) break; // Nếu không có dữ liệu, thoát khỏi vòng lặp.

                        fileStream.Write(buffer, 0, bytesRead); // Ghi dữ liệu vào file.
                        totalBytesRead += bytesRead; // Cập nhật số byte đã nhận.
                    }
                }

                listBoxLog.Items.Add($"File được nhận từ server: {fileName} ({fileSize} bytes)"); // Thêm log vào ListBox.
            }
            else if (message.StartsWith("DELETE|")) // Nếu server yêu cầu xóa file.
            {
                var fileName = message.Split('|')[1]; // Lấy tên file cần xóa.
                var filePath = Path.Combine(musicFolder, fileName); // Lấy đường dẫn đầy đủ của file.

                if (File.Exists(filePath)) // Kiểm tra nếu file tồn tại.
                {
                    File.Delete(filePath); // Xóa file.
                    listBoxLog.Items.Add($"File đã bị xóa: {fileName}"); // Thêm log vào ListBox.
                }
                else
                {
                    listBoxLog.Items.Add($"Không tìm thấy file: {fileName}"); // Thêm log nếu không tìm thấy file.
                }
            }
            else if (message.StartsWith("STOP|")) // Nếu server yêu cầu dừng nhạc.
            {
                var filename = message.Split('|')[1]; // Lấy tên file cần dừng.
                var filepath = Path.Combine(musicFolder, filename); // Lấy đường dẫn đầy đủ của file.

                if (File.Exists(filepath)) // Kiểm tra nếu file tồn tại.
                {
                    var player = new SoundPlayer(filepath); // Tạo đối tượng SoundPlayer để dừng nhạc.
                    player.Stop(); // Dừng nhạc.
                    listBoxLog.Items.Add("Nhạc đã dừng"); // Thêm log vào ListBox.
                }
            }
        }
    }
}
