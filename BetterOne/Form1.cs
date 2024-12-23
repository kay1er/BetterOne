using System; // Sử dụng các lớp cơ bản trong .NET.
using System.Collections.Generic; // Sử dụng lớp Dictionary để quản lý danh sách client.
using System.IO; // Sử dụng để thao tác với file và thư mục.
using System.Net; // Sử dụng để quản lý địa chỉ IP và mạng.
using System.Net.Sockets; // Sử dụng để tạo kết nối mạng qua TCP.
using System.Text; // Sử dụng để mã hóa và giải mã chuỗi.
using System.Windows.Forms; // Sử dụng để xây dựng giao diện ứng dụng Windows Forms.

namespace BetterOne
{
    public partial class Form1 : Form
    {
        TcpListener server; // Khởi tạo một server TCP để lắng nghe kết nối từ client.
        Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>(); // Lưu danh sách các client đã kết nối.

        public Form1()
        {
            InitializeComponent(); // Khởi tạo các thành phần của form.
            StartServer(); // Bắt đầu khởi chạy server.
        }

        private void StartServer()
        {
            server = new TcpListener(IPAddress.Any, 5000); // Tạo server lắng nghe tại cổng 5000.
            server.Start(); // Bắt đầu server.
            server.BeginAcceptTcpClient(ClientConnected, null); // Lắng nghe kết nối từ client.
        }

        private void ClientConnected(IAsyncResult ar)
        {
            var client = server.EndAcceptTcpClient(ar); // Chấp nhận kết nối từ client.
            var endPoint = client.Client.RemoteEndPoint.ToString(); // Lấy địa chỉ IP của client.
            clients[endPoint] = client; // Thêm client vào danh sách.

            // Cập nhật giao diện với thông tin client vừa kết nối.
            Invoke(new Action(() =>
            {
                comboBoxClients.Items.Add(endPoint); // Thêm địa chỉ client vào ComboBox.
                txtServerLog.Text += $"Client {endPoint} đã kết nối thành công {Environment.NewLine}"; // Ghi log kết nối.
            }));

            server.BeginAcceptTcpClient(ClientConnected, null); // Tiếp tục lắng nghe các kết nối mới.
        }

        private void btnGetFiles_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString(); // Lấy client được chọn trong ComboBox.
            if (selectedClient == null || !clients.ContainsKey(selectedClient))
            {
                txtServerLog.Text += $"Vui lòng chọn một client {Environment.NewLine}"; // Hiển thị thông báo nếu chưa chọn client.
                return;
            }

            var client = clients[selectedClient]; // Lấy client từ danh sách.
            var stream = client.GetStream(); // Lấy luồng dữ liệu của client.

            // Gửi yêu cầu lấy danh sách file.
            var message = Encoding.UTF8.GetBytes("GET_FILES");
            stream.Write(message, 0, message.Length); // Gửi dữ liệu tới client.

            // Nhận danh sách file từ client.
            var buffer = new byte[4096]; // Bộ đệm để lưu dữ liệu nhận về.
            int bytesRead = stream.Read(buffer, 0, buffer.Length); // Đọc dữ liệu từ luồng.
            var fileList = Encoding.UTF8.GetString(buffer, 0, bytesRead); // Chuyển dữ liệu thành chuỗi.

            // Hiển thị danh sách file trong ListBox.
            listBoxFiles.Items.Clear(); // Xóa danh sách cũ.
            foreach (var file in fileList.Split('|')) // Tách chuỗi thành danh sách file.
            {
                if (!string.IsNullOrWhiteSpace(file)) // Kiểm tra nếu file không rỗng.
                    listBoxFiles.Items.Add(file); // Thêm file vào ListBox.
            }

            txtServerLog.Text += $"Đã gửi yêu cầu hiển thị file {Environment.NewLine}"; // Ghi log.
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString(); // Lấy client được chọn.
            var selectedFile = listBoxFiles.SelectedItem?.ToString(); // Lấy file được chọn.
            if (selectedClient == null || selectedFile == null) // Kiểm tra nếu chưa chọn client hoặc file.
            {
                MessageBox.Show("Vui lòng chọn client và file để dừng!"); // Hiển thị thông báo.
                return;
            }

            var client = clients[selectedClient]; // Lấy client từ danh sách.
            var stream = client.GetStream(); // Lấy luồng dữ liệu của client.

            // Gửi yêu cầu dừng phát nhạc.
            var message = Encoding.UTF8.GetBytes($"STOP|{selectedFile}");
            stream.Write(message, 0, message.Length); // Gửi dữ liệu tới client.

            txtServerLog.Text += $"Đã gửi yêu cầu dừng nhạc {Environment.NewLine}"; // Ghi log.
        }

        private void btnPlayMusic_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString(); // Lấy client được chọn.
            var selectedFile = listBoxFiles.SelectedItem?.ToString(); // Lấy file được chọn.
            if (selectedClient == null || selectedFile == null) // Kiểm tra nếu chưa chọn client hoặc file.
            {
                txtServerLog.Text += $"Vui lòng chọn client và file để phát nhạc {Environment.NewLine}"; // Ghi log.
                return;
            }

            var client = clients[selectedClient]; // Lấy client từ danh sách.
            var stream = client.GetStream(); // Lấy luồng dữ liệu của client.

            // Gửi yêu cầu phát file nhạc.
            var message = Encoding.UTF8.GetBytes($"PLAY|{selectedFile}");
            stream.Write(message, 0, message.Length); // Gửi dữ liệu tới client.

            txtServerLog.Text += $"Đã gửi yêu cầu phát nhạc {Environment.NewLine}"; // Ghi log.
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString(); // Lấy client được chọn.
            if (selectedClient == null || !clients.ContainsKey(selectedClient)) // Kiểm tra nếu chưa chọn client.
            {
                txtServerLog.Text += $"Vui lòng chọn một client {Environment.NewLine}"; // Ghi log.
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) // Hiển thị hộp thoại chọn file.
            {
                openFileDialog.Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*"; // Chỉ hiển thị file .wav hoặc tất cả file.
                if (openFileDialog.ShowDialog() == DialogResult.OK) // Kiểm tra nếu người dùng chọn file.
                {
                    var filePath = openFileDialog.FileName; // Lấy đường dẫn file.
                    var fileName = Path.GetFileName(filePath); // Lấy tên file.
                    var fileBytes = File.ReadAllBytes(filePath); // Đọc toàn bộ nội dung file.
                    var fileSize = fileBytes.Length; // Lấy kích thước file.

                    var client = clients[selectedClient]; // Lấy client từ danh sách.
                    var stream = client.GetStream(); // Lấy luồng dữ liệu của client.

                    // Gửi lệnh upload và thông tin file.
                    var message = Encoding.UTF8.GetBytes($"UPLOAD|{fileName}|{fileSize}");
                    stream.Write(message, 0, message.Length); // Gửi thông tin file.

                    // Gửi nội dung file.
                    stream.Write(fileBytes, 0, fileSize); // Gửi toàn bộ nội dung file.

                    txtServerLog.Text += $"Gửi file thành công {Environment.NewLine}"; // Ghi log.
                }
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            var selectedClient = comboBoxClients.SelectedItem?.ToString(); // Lấy client được chọn.
            var selectedFile = listBoxFiles.SelectedItem?.ToString(); // Lấy file được chọn.
            if (selectedClient == null || selectedFile == null) // Kiểm tra nếu chưa chọn client hoặc file.
            {
                txtServerLog.Text += $"Vui lòng chọn client và file để xóa {Environment.NewLine}"; // Ghi log.
                return;
            }

            var client = clients[selectedClient]; // Lấy client từ danh sách.
            var stream = client.GetStream(); // Lấy luồng dữ liệu của client.

            // Gửi lệnh xóa file.
            var message = Encoding.UTF8.GetBytes($"DELETE|{selectedFile}");
            stream.Write(message, 0, message.Length); // Gửi dữ liệu tới client.

            txtServerLog.Text += $"Yêu cầu xóa file đã được gửi {Environment.NewLine}"; // Ghi log.
        }

        
    }
}
