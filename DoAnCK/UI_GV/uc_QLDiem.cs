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
using DevExpress.XtraEditors.TextEditController.Utils;
using DevExpress.XtraCharts;

namespace DoAnCK.UI_GV
{
    public partial class uc_QLDiem: UserControl
    {
        public uc_QLDiem()
        {
            InitializeComponent();
            string connStr = frmGiangVien.ConnString;

        }

        private int maHocKyNamHoc;
        private DataTable dt;
        private bool isLoading = false;
        private void cbbMa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            LoadBangDiemTheoLop();
        }

        private void uc_QLDiem_Load(object sender, EventArgs e)
        {
            isLoading = true;

            string queryMaHK = "SELECT TOP 1 MaHocKyNamHoc FROM HocKyNamHoc ORDER BY MaHocKyNamHoc DESC";
            DataTable dtHK = frmGiangVien.getData(queryMaHK);
            if (dtHK != null && dtHK.Rows.Count > 0)
                maHocKyNamHoc = Convert.ToInt32(dtHK.Rows[0]["MaHocKyNamHoc"]);
            else
                MessageBox.Show("Không tìm thấy học kỳ/năm học hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            LoadMaLop();

            isLoading = false;

            LoadBangDiemTheoLop();
        }
        private void LoadMaLop()
        {
            string queryMa = $@"
            SELECT DISTINCT MaLHP 
            FROM LopHocPhan 
            WHERE MaHocKyNamHoc = {maHocKyNamHoc} 
              AND MaGV = '{"GV001"}'
            ORDER BY MaLHP";

            DataTable dtMa = frmGiangVien.getData(queryMa);

            if (dtMa != null && dtMa.Rows.Count > 0)
            {
                cbbMa.DataSource = dtMa;
                cbbMa.DisplayMember = "MaLHP";
                cbbMa.ValueMember = "MaLHP";
            }
            else
            {
                cbbMa.DataSource = null;
                MessageBox.Show("Giảng viên này chưa phụ trách lớp học phần nào trong học kỳ hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadBangDiemTheoLop()
        {
            if (isLoading || cbbMa.SelectedValue == null) return;

            string maLHP = cbbMa.SelectedValue.ToString();
            string query = $"SELECT * FROM fn_SinhVienVaDiemTheoLopHocPhan('{maLHP}', {maHocKyNamHoc})";

            dt = frmGiangVien.getData(query);

            if (dt != null && dt.Rows.Count > 0)
                gcDanhSach.DataSource = dt;
            else
            {
                gcDanhSach.DataSource = null;
                MessageBox.Show("Không có dữ liệu sinh viên cho lớp học phần này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Kết thúc chỉnh sửa trên GridView
            gvDanhSach.CloseEditor();
            gvDanhSach.UpdateCurrentRow();

            // Lấy các thay đổi trong DataTable
            DataTable dtChanges = ((DataTable)gcDanhSach.DataSource)?.GetChanges();
            if (dtChanges == null || dtChanges.Rows.Count == 0)
            {
                MessageBox.Show("Không có thay đổi nào để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maLHP = cbbMa.SelectedValue.ToString();

            using (SqlConnection conn = new SqlConnection(frmGiangVien.ConnString))
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
                        cmd.Parameters.AddWithValue("@MaLHP", maLHP);
                        cmd.Parameters.AddWithValue("@MaHocKyNamHoc", maHocKyNamHoc);
                        cmd.Parameters.AddWithValue("@DiemGK", (object)diemGK ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemCK", (object)diemCK ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload lại bảng điểm
                    LoadBangDiemTheoLop();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
       


   

        private void btnThongKe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cbbMa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLHP = cbbMa.SelectedValue.ToString();

            // Mở form thống kê
            frmThongKe frm = new frmThongKe();
            frm.HienThiBieuDoThongKe(maLHP, maHocKyNamHoc);
            frm.ShowDialog();
        }
    }
}
