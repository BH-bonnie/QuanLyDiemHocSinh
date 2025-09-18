using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;

namespace DoAnCK.UI_Admin
{
    public partial class uc_KQHT: UserControl
    {
        string connStr = frmAdmin.ConnString;
        private int maHocKyNamHoc;
        private DataTable dt;
        private bool isLoading = false;
        public uc_KQHT()
        {
            InitializeComponent();
        }

        private void uc_KQHT_Load(object sender, EventArgs e)
        {

            LoadMaHKNH();

            isLoading = false;

            LoadBangDiem();

            

        }
        private void LoadMaHKNH()
        {
            string queryNamHoc = $@" SELECT MaHocKyNamHoc, HocKy, NamHoc FROM HocKyNamHoc ORDER BY MaHocKyNamHoc DESC";

            DataTable dtNamHoc = frmAdmin.getData(queryNamHoc);

            if (dtNamHoc != null && dtNamHoc.Rows.Count > 0)
            {
                dtNamHoc.Columns.Add("HK_NamHoc", typeof(string));
                foreach (DataRow row in dtNamHoc.Rows)
                {
                    row["HK_NamHoc"] = $"HK{row["HocKy"]} - {row["NamHoc"]}";
                }

                // Gán vào ComboBox
                cbbNamHoc.DataSource = dtNamHoc;
                cbbNamHoc.DisplayMember = "HK_NamHoc";
                cbbNamHoc.ValueMember = "MaHocKyNamHoc";
                maHocKyNamHoc = Convert.ToInt32(cbbNamHoc.SelectedValue);

                string querySV = $"SELECT * FROM fn_ChiTietHocPhan( {maHocKyNamHoc})";
                DataTable dt = frmAdmin.getData(querySV);
                gcDanhSachSV.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy học kỳ/năm học hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void LoadBangDiem()
        {
            if (isLoading || cbbNamHoc.SelectedValue == null) return;

            string maLHP = cbbNamHoc.SelectedValue.ToString();
            string query = $"SELECT * FROM fn_ChiTietHocPhan( {maHocKyNamHoc})";

            dt = frmAdmin.getData(query);

            if (dt != null && dt.Rows.Count > 0)
                gcDanhSachSV.DataSource = dt;
            else
            {
                gcDanhSachSV.DataSource = null;
                MessageBox.Show("Không có dữ liệu sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {// Kết thúc chỉnh sửa trên GridView
            gvDanhSachSV.CloseEditor();
            gvDanhSachSV.UpdateCurrentRow();

            // Lấy các thay đổi trong DataTable
            DataTable dtChanges = ((DataTable)gcDanhSachSV.DataSource)?.GetChanges();
            if (dtChanges == null || dtChanges.Rows.Count == 0)
            {
                MessageBox.Show("Không có thay đổi nào để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SqlConnection conn = new SqlConnection(frmAdmin.ConnString))
            using (SqlCommand cmd = new SqlCommand("sp_CapNhatDiemHocPhan", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmd.Transaction = tran;

                try
                {
                    foreach (DataRow row in dtChanges.Rows)
                    {
                        // Lấy dữ liệu từ Grid
                        string maSV = row["MaSV"].ToString();
                        string maMH = row["MaMH"].ToString();
                        decimal? diemGK = row["DiemGK"] != DBNull.Value ? Convert.ToDecimal(row["DiemGK"]) : (decimal?)null;
                        decimal? diemCK = row["DiemCK"] != DBNull.Value ? Convert.ToDecimal(row["DiemCK"]) : (decimal?)null;

                        // Optional: Kiểm tra dữ liệu hợp lệ (0-10)
                        if ((diemGK.HasValue && (diemGK < 0 || diemGK > 10)) ||
                            (diemCK.HasValue && (diemCK < 0 || diemCK > 10)))
                        {
                            MessageBox.Show($"Điểm của sinh viên {maSV} không hợp lệ (0-10).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tran.Rollback();
                            return;
                        }

                        // Truyền tham số
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@MaSV", maSV);
                        cmd.Parameters.AddWithValue("@MaLHP", maMH);
                        cmd.Parameters.AddWithValue("@MaHocKyNamHoc", maHocKyNamHoc);
                        cmd.Parameters.AddWithValue("@DiemGK", (object)diemGK ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemCK", (object)diemCK ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (isLoading) return;
            LoadBangDiem();
        }

 
    }
}
