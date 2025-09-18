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
        private string connStr = FormMain.ConnString;

        private string role;

        public uc_Dangnhap(string role)
        {
            InitializeComponent();
            this.role = role;
        }

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

                    using (SqlCommand cmd = new SqlCommand("sp_DangNhap", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm parameters cho stored procedure
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                        cmd.Parameters.AddWithValue("@Role", role);

                        SqlParameter paramQuyen = new SqlParameter("@Quyen", SqlDbType.NVarChar, 20)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(paramQuyen);

                        SqlParameter paramTrangThai = new SqlParameter("@TrangThai", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(paramTrangThai);

                        SqlParameter paramMaGV = new SqlParameter("@MaGV", SqlDbType.VarChar, 10)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(paramMaGV);

                        SqlParameter paramKetQua = new SqlParameter("@KetQua", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(paramKetQua);

                        cmd.ExecuteNonQuery();

                        string ketQua = paramKetQua.Value?.ToString();
                        string quyen = paramQuyen.Value?.ToString();
                        string maGV = paramMaGV.Value?.ToString();

                        if (ketQua.Contains("không chính xác"))
                        {
                            MessageBox.Show(ketQua, "Lỗi đăng nhập",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ketQua.Contains("bị khóa"))
                        {
                            MessageBox.Show(ketQua, "Thông báo",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (ketQua.Contains("không có quyền"))
                        {
                            MessageBox.Show(ketQua, "Lỗi quyền truy cập",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ketQua.Contains("thành công"))
                        {
                            MessageBox.Show(ketQua, "Thành công",
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.FindForm().Hide();

                            if (quyen == "Admin")
                            {
                                frmAdmin adminForm = new frmAdmin();
                                adminForm.ShowDialog();
                            }
                            else if (quyen == "GiangVien")
                            {
                                frmGiangVien gvForm = new frmGiangVien(maGV);
                                gvForm.ShowDialog();
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
