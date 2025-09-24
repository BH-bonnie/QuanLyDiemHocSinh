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
    public partial class uc_QLDiem : UserControl, IRefreshable
    {
        private string MaGV;
        private string connStr;
        private bool isAdding = false;
        private int editingRowHandle = -1;
        private bool isLoading = false;

        private DataTable dt;
        public uc_QLDiem()
        {
            InitializeComponent();
            connStr = frmGiangVien.ConnString;
            MaGV = frmGiangVien.MaGV;
        }
        public uc_QLDiem(string connectionString, string maGV)
        {
            InitializeComponent();
            connStr = connectionString;
            MaGV = maGV;
        }

        private int maHocKyNamHoc;

        private void cbbMa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            LoadBangDiemTheoLop();
        }

        public void RefreshData()
        {
            isLoading = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            gvDanhSach.OptionsBehavior.Editable = false;

            string queryMaHK = "SELECT TOP 1 MaHocKyNamHoc FROM HocKyNamHoc ORDER BY MaHocKyNamHoc DESC";
            DataTable dtHK = frmGiangVien.getData(queryMaHK);
            if (dtHK != null && dtHK.Rows.Count > 0)
                maHocKyNamHoc = Convert.ToInt32(dtHK.Rows[0]["MaHocKyNamHoc"]);
            else
                MessageBox.Show("Không tìm thấy học kỳ/năm học hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            LoadMaLop();

            isLoading = false;

            LoadBangDiemTheoLop();
            gvDanhSach.RefreshData();


        }

        private void LoadMaLop()
        {
            string query = $"EXEC sp_GetLopHocPhanByGV @MaHocKyNamHoc = {maHocKyNamHoc}, @MaGV = '{MaGV}'";

            DataTable dtMa = frmGiangVien.getData(query);

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
            gvDanhSach.CloseEditor();
            gvDanhSach.UpdateCurrentRow();

            DataTable dtChanges = ((DataTable)gcDanhSach.DataSource)?.GetChanges();
            if (dtChanges == null || dtChanges.Rows.Count == 0)
            {
                MessageBox.Show("Không có thay đổi nào để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maLHP = cbbMa.SelectedValue.ToString();

            string queryMaMH = $"SELECT MaMH FROM LopHocPhan WHERE MaLHP = '{maLHP}'";
            DataTable dtMaMH = frmGiangVien.getData(queryMaMH);

            if (dtMaMH == null || dtMaMH.Rows.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy môn học cho LHP {maLHP}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maMH = dtMaMH.Rows[0]["MaMH"].ToString();

            try
            {
                foreach (DataRow row in dtChanges.Rows)
                {
                    string maSV = row["MaSV"].ToString();
                    decimal? diemGK = row["DiemGK"] != DBNull.Value ? Convert.ToDecimal(row["DiemGK"]) : (decimal?)null;
                    decimal? diemCK = row["DiemCK"] != DBNull.Value ? Convert.ToDecimal(row["DiemCK"]) : (decimal?)null;

                    if ((diemGK.HasValue && (diemGK < 0 || diemGK > 10)) ||
                        (diemCK.HasValue && (diemCK < 0 || diemCK > 10)))
                    {
                        MessageBox.Show($"Điểm của sinh viên {maSV} không hợp lệ (0-10).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string diemGKValue = diemGK.HasValue ? diemGK.Value.ToString("F2") : "NULL";
                    string diemCKValue = diemCK.HasValue ? diemCK.Value.ToString("F2") : "NULL";

                    string query = $"EXEC sp_CapNhatDiemHocPhan @MaSV = '{maSV}', @MaMH = '{maMH}', @MaHocKyNamHoc = {maHocKyNamHoc}, @DiemGK = {diemGKValue}, @DiemCK = {diemCKValue}";

                    frmGiangVien.executeQuery(query);
                }

                MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBangDiemTheoLop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            gvDanhSach.OptionsBehavior.Editable = false;
            isAdding = false;
            editingRowHandle = -1;
        }

        private void btnThongKe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

            string maLHP = cbbMa.SelectedValue.ToString();

            frmThongKe frm = new frmThongKe();
            frm.SetData(maLHP, maHocKyNamHoc);

            frm.ShowDialog();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnSua.Enabled = false;
            gvDanhSach.OptionsBehavior.Editable = true;
            editingRowHandle = gvDanhSach.FocusedRowHandle;
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gvDanhSach.FocusedRowHandle;

            if (isAdding)
            {
                if (gvDanhSach.IsNewItemRow(rowHandle) || rowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                {
                    gvDanhSach.DeleteRow(rowHandle);
                }
                else
                {
                    for (int i = gvDanhSach.RowCount - 1; i >= 0; i--)
                    {
                        DataRow row = gvDanhSach.GetDataRow(i);
                        if (row != null && row.RowState == DataRowState.Added)
                        {
                            gvDanhSach.DeleteRow(i);
                            break;
                        }
                    }
                }
                dt.RejectChanges();
            }
            else
            {
                gvDanhSach.CancelUpdateCurrentRow();
                dt.RejectChanges();
            }

            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            gvDanhSach.OptionsBehavior.Editable = false;
            isAdding = false;
            editingRowHandle = -1;

        }
    }
}
