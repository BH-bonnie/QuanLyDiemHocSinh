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
        private int roleID;
        public static string TenDangNhap { get; private set; }
        public static string MatKhau { get; private set; }

        public uc_Dangnhap(int roleID)
        {
            InitializeComponent();
            this.roleID = roleID;
        }

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
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(FormMain.ConnString))
            {
                MessageBox.Show("Chuỗi kết nối chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(FormMain.ConnString))
                using (SqlCommand cmd = new SqlCommand("sp_DangNhap", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    cmd.Parameters.AddWithValue("@RoleIDtam", roleID);

                    var pRoleID = new SqlParameter("@RoleID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var pTrangThai = new SqlParameter("@TrangThai", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    var pMaGV = new SqlParameter("@MaGV", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output };
                    var pKetQua = new SqlParameter("@KetQua", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output };

                    cmd.Parameters.Add(pRoleID);
                    cmd.Parameters.Add(pTrangThai);
                    cmd.Parameters.Add(pMaGV);
                    cmd.Parameters.Add(pKetQua);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    string ketQua = pKetQua.Value?.ToString();
                    int? roleThuc = pRoleID.Value != DBNull.Value ? (int?)pRoleID.Value : null;
                    bool? trangThai = pTrangThai.Value != DBNull.Value ? (bool?)pTrangThai.Value : null;
                    string maGV = pMaGV.Value?.ToString();

                    if (roleThuc == null)
                    {
                        MessageBox.Show($"{ketQua}",
                            "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    if (trangThai == false)
                    {
                        MessageBox.Show($"{ketQua}",
                            "Tài khoản bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    FormMain.UpdateConnString(tenDangNhap, matKhau);
                    try
                    {
                        using (SqlConnection c = new SqlConnection(FormMain.ConnString))
                        {
                            c.Open();
                            MessageBox.Show("Kết nối thành công với: " + c.Database);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi kết nối: " + ex.Message);
                    }

                    TenDangNhap = tenDangNhap;
                    MatKhau = matKhau;

                    if (roleThuc == 1) // Admin
                    {
                        MessageBox.Show($"{ketQua}",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Đảm bảo frmAdmin sử dụng connection string mới
                        frmAdmin frm = new frmAdmin(maGV);
                        Form parent = this.FindForm();
                        parent.Hide();
                        frm.ShowDialog();
                        
                    }
                    else if (roleThuc == 2) // GiangVien
                    {
                        MessageBox.Show($"{ketQua}",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        frmGiangVien frm = new frmGiangVien(maGV);
                        Form parent = this.FindForm();
                        parent.Hide();
                        frm.ShowDialog();
                       
                    }
                    else
                    {
                        MessageBox.Show("Quyền không hợp lệ!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi SQL với quyền {GetRoleName(roleID)}: {ex.Message}",
                    "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetRoleName(int roleID)
        {
            return roleID == 1 ? "Admin" : roleID == 2 ? "Giảng viên" : "Không xác định";
        }
    }
}


