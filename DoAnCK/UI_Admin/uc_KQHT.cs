using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DoAnCK.UI_GV;

namespace DoAnCK.UI_Admin
{
    public partial class uc_KQHT : UserControl
    {
        private string connStr;
        private int maHocKyNamHoc;
        private bool isAdding = false;
        private int editingRowHandle = -1;
        private DataTable dt;
        private bool isLoading = false;

        public uc_KQHT()
        {
            InitializeComponent();
            connStr = frmAdmin.ConnString;
        }

        private void uc_KQHT_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            gvDanhSachSV.OptionsBehavior.Editable = false;
           

            LoadMaHKNH(); 
            LoadBangDiem(); 
        }

        private void LoadMaHKNH()
        {
            isLoading = true; 
            string queryNamHoc = @"SELECT MaHocKyNamHoc, HocKy, NamHoc FROM HocKyNamHoc ORDER BY MaHocKyNamHoc DESC";
            DataTable dtNamHoc = frmAdmin.getData(queryNamHoc);

            if (dtNamHoc != null && dtNamHoc.Rows.Count > 0)
            {
                dtNamHoc.Columns.Add("HK_NamHoc", typeof(string));
                foreach (DataRow row in dtNamHoc.Rows)
                {
                    row["HK_NamHoc"] = $"HK{row["HocKy"]} - {row["NamHoc"]}";
                }

                cbbNamHoc.DataSource = dtNamHoc;
                cbbNamHoc.DisplayMember = "HK_NamHoc";
                cbbNamHoc.ValueMember = "MaHocKyNamHoc";
                cbbNamHoc.SelectedIndex = 0; 

                maHocKyNamHoc = Convert.ToInt32(cbbNamHoc.SelectedValue);
            }
            else
            {
                MessageBox.Show("Không tìm thấy học kỳ/năm học hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            isLoading = false; 
        }

        private void LoadBangDiem()
        {
            if (isLoading || cbbNamHoc.SelectedValue == null) return;

            DataRowView drv = cbbNamHoc.SelectedItem as DataRowView;
            if (drv == null)
                return;

            maHocKyNamHoc = Convert.ToInt32(drv["MaHocKyNamHoc"]);
            string query = $"SELECT * FROM fn_ChiTietHocPhan({maHocKyNamHoc})";
            dt = frmAdmin.getData(query);

            if (dt != null && dt.Rows.Count > 0)
                gcDanhSachSV.DataSource = dt;
            else
            {
                gcDanhSachSV.DataSource = null;
                MessageBox.Show("Không có dữ liệu sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            LoadBangDiem();

            btnSua.Enabled = (cbbNamHoc.SelectedIndex == 0);
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnSua.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = true;
            editingRowHandle = gvDanhSachSV.FocusedRowHandle;
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gvDanhSachSV.FocusedRowHandle;

            if (isAdding)
            {
                if (gvDanhSachSV.IsNewItemRow(rowHandle) || rowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                {
                    gvDanhSachSV.DeleteRow(rowHandle);
                }
                else
                {
                    for (int i = gvDanhSachSV.RowCount - 1; i >= 0; i--)
                    {
                        DataRow row = gvDanhSachSV.GetDataRow(i);
                        if (row != null && row.RowState == DataRowState.Added)
                        {
                            gvDanhSachSV.DeleteRow(i);
                            break;
                        }
                    }
                }
                dt.RejectChanges();
            }
            else
            {
                gvDanhSachSV.CancelUpdateCurrentRow();
                dt.RejectChanges();
            }

            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = false;
            isAdding = false;
            editingRowHandle = -1;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDanhSachSV.CloseEditor();
            gvDanhSachSV.UpdateCurrentRow();

            DataTable dtChanges = ((DataTable)gcDanhSachSV.DataSource)?.GetChanges();
            if (dtChanges == null || dtChanges.Rows.Count == 0)
            {
                MessageBox.Show("Không có thay đổi nào để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                foreach (DataRow row in dtChanges.Rows)
                {
                    string maSV = row["MaSV"].ToString();
                    string maMH = row["MaMH"].ToString();
                    decimal? diemGK = row["DiemGK"] != DBNull.Value ? Convert.ToDecimal(row["DiemGK"]) : (decimal?)null;
                    decimal? diemCK = row["DiemCK"] != DBNull.Value ? Convert.ToDecimal(row["DiemCK"]) : (decimal?)null;



                    if ((diemGK.HasValue && (diemGK < 0 || diemGK > 10)) ||
                        (diemCK.HasValue && (diemCK < 0 || diemCK > 10)))
                    {
                        MessageBox.Show($"Điểm của sinh viên {maSV} không hợp lệ (0-10).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                     string diemGKValue = diemGK.HasValue ? diemGK.Value.ToString() : "NULL";
                    string diemCKValue = diemCK.HasValue ? diemCK.Value.ToString() : "NULL";

                    string query = $@"EXEC sp_CapNhatDiemHocPhan 
                                    @MaSV = '{maSV}', 
                                    @MaMH = '{maMH}', 
                                    @MaHocKyNamHoc = {maHocKyNamHoc}, 
                                    @DiemGK = {diemGKValue}, 
                                    @DiemCK = {diemCKValue}";

                    frmAdmin.executeQuery(query);
                }

                dt.AcceptChanges();
                MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBangDiem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = false;
            isAdding = false;
            editingRowHandle = -1;
        }

        private void btnThongKe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThongKeAD frm = new frmThongKeAD();
            frm.SetData(maHocKyNamHoc);
            frm.ShowDialog();
        }


    }
}
