using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DoAnCK.UI_Dangnhap
{
    public partial class uc_Dangnhap : UserControl
    {
        public event EventHandler OnExit;
        private string connStr;


        private string role;

        public uc_Dangnhap(string role)
        {
            InitializeComponent();
            this.role = role;
            connStr = FormMain.ConnString;

        }

        // Nút Thoát (thêm vào Designer hoặc code)
        private Button btnThoat;

        private void uc_Dangnhap_Load(object sender, EventArgs e)
        {
           
        }

      

        private void txtThoat_Click(object sender, EventArgs e)
        {
            OnExit?.Invoke(this, EventArgs.Empty);

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    // Thêm MaGV vào SELECT
                    string query = "SELECT Quyen, TrangThai, MaGV FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool trangThai = Convert.ToBoolean(reader["TrangThai"]);
                                if (!trangThai)
                                {
                                    MessageBox.Show("Tài khoản đã bị khóa!", "Thông báo",
                                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                string quyen = reader["Quyen"].ToString();
                                string maGV = reader["MaGV"]?.ToString(); // Lấy MaGV từ database

                                

                                // Kiểm tra quyền có khớp với role được chọn không
                                if ((role == "Admin" && quyen == "Admin") ||
                                    (role == "GV" && quyen == "GiangVien"))
                                {
                                    MessageBox.Show($"Đăng nhập thành công với quyền {quyen}!", "Thành công",
                                                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Ẩn FormMain
                                    this.FindForm().Hide();

                                    // Mở form tương ứng
                                    if (quyen == "Admin")
                                    {
                                        frmAdmin adminForm = new frmAdmin();
                                        adminForm.ShowDialog();
                                    }
                                    else if (quyen == "GiangVien")
                                    {
                                        // Truyền MaGV vào constructor
                                        frmGiangVien gvForm = new frmGiangVien(maGV);
                                        gvForm.ShowDialog();
                                    }

                                    // Hiển thị lại FormMain khi đóng form chính
                                    this.FindForm().Show();
                                }
                                else
                                {
                                    MessageBox.Show($"Tài khoản này không có quyền {role}!", "Lỗi quyền truy cập",
                                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập",
                                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
