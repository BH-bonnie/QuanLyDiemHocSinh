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
using DevExpress.XtraEditors.Mask.Design;
using static DevExpress.Data.Helpers.FindSearchRichParser;
using DevExpress.XtraGrid.Views.Grid;


namespace DoAnCK.UI_Admin
{
    public partial class uc_QLSV : UserControl
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dtSinhVien;
        private bool isAdding = false;
        private int editingRowHandle = -1;
        public uc_QLSV()
        {
            InitializeComponent();

        }
  

        private void uc_QLSV_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            isAdding = false;
            gvDanhSachSV.OptionsBehavior.Editable = false;
            btnThem.Enabled = true;
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                dtSinhVien = frmAdmin.getData("SELECT * FROM v_SinhVien_Detail;");
                if (dtSinhVien != null)
                {
                    gcDanhSachSV.DataSource = dtSinhVien;
                    // Cho phép chọn nhiều dòng
                    gvDanhSachSV.OptionsSelection.MultiSelect = true;
                    gvDanhSachSV.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                }
            }
        }

      

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnHuy.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            isAdding = true;

            gvDanhSachSV.OptionsBehavior.Editable = true;
            gvDanhSachSV.AddNewRow();

            gvDanhSachSV.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvDanhSachSV.FocusedColumn = gvDanhSachSV.VisibleColumns[0];
            gvDanhSachSV.ShowEditor();
        }
        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDanhSachSV.CloseEditor();
            gvDanhSachSV.UpdateCurrentRow();
            var changes = dtSinhVien.GetChanges();
            if (changes == null)
            {
                MessageBox.Show("Không có thay đổi nào để lưu!");
                return;
            }

            try
            {
                foreach (DataRow row in changes.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        string maSV = row["MaSV"].ToString().Trim();
                        string hoTen = row["HoTen"].ToString().Trim();
                        string lopSV = row["LopSV"].ToString().Trim();
                        string ngaySinh = row["NgaySinh"] == DBNull.Value ? "NULL" :
                            $"'{Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd")}'";
                        string noiSinh = row["NoiSinh"]?.ToString() ?? "";
                        string gioiTinh = row["GioiTinh"]?.ToString() ?? "";
                        string cmnd = row["CMND_CCCD"]?.ToString() ?? "";

                        // Kiểm tra dữ liệu bắt buộc
                        if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(lopSV))
                        {
                            MessageBox.Show("Mã sinh viên, Họ tên, Lớp SV không được để trống!");
                            return;
                        }

                        // Gọi stored procedure để thêm sinh viên
                        string query = $@"EXEC sp_ThemSinhVien 
                                            @MaSV = '{maSV}', 
                                            @HoTen = N'{hoTen}', 
                                            @LopSV = '{lopSV}', 
                                            @NgaySinh = {ngaySinh}, 
                                            @NoiSinh = {(string.IsNullOrWhiteSpace(noiSinh) ? "NULL" : $"N'{noiSinh}'")}, 
                                            @GioiTinh = {(string.IsNullOrWhiteSpace(gioiTinh) ? "NULL" : $"N'{gioiTinh}'")}, 
                                            @CMND_CCCD = {(string.IsNullOrWhiteSpace(cmnd) ? "NULL" : $"'{cmnd}'")}";

                        frmAdmin.executeQuery(query);
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        string maSV = row["MaSV"].ToString().Trim();
                        string hoTen = row["HoTen"].ToString().Trim();
                        string lopSV = row["LopSV"].ToString().Trim();
                        string ngaySinh = row["NgaySinh"] == DBNull.Value ? "NULL" :
                            $"'{Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd")}'";
                        string noiSinh = row["NoiSinh"]?.ToString() ?? "";
                        string gioiTinh = row["GioiTinh"]?.ToString() ?? "";
                        string cmnd = row["CMND_CCCD"]?.ToString() ?? "";

                        // Kiểm tra dữ liệu bắt buộc
                        if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(lopSV))
                        {
                            MessageBox.Show("Mã sinh viên, Họ tên, Lớp SV không được để trống!");
                            return;
                        }

                        // Gọi stored procedure để cập nhật sinh viên
                        string query = $@"EXEC sp_CapNhatSinhVien 
                                            @MaSV = '{maSV}', 
                                            @HoTen = N'{hoTen}', 
                                            @LopSV = '{lopSV}', 
                                            @NgaySinh = {ngaySinh}, 
                                            @NoiSinh = {(string.IsNullOrWhiteSpace(noiSinh) ? "NULL" : $"N'{noiSinh}'")}, 
                                            @GioiTinh = {(string.IsNullOrWhiteSpace(gioiTinh) ? "NULL" : $"N'{gioiTinh}'")}, 
                                            @CMND_CCCD = {(string.IsNullOrWhiteSpace(cmnd) ? "NULL" : $"'{cmnd}'")}";

                        frmAdmin.executeQuery(query);
                    }
                }

                dtSinhVien.AcceptChanges();
                MessageBox.Show("Đã lưu thay đổi vào CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dtSinhVien = frmAdmin.getData("SELECT * FROM v_SinhVien_Detail;");
                gcDanhSachSV.DataSource = dtSinhVien;

                btnLuu.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                gvDanhSachSV.OptionsBehavior.Editable = false;
                btnThem.Enabled = true;
                isAdding = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] selectedRows = gvDanhSachSV.GetSelectedRows();

            if (selectedRows.Length > 0)
            {
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {selectedRows.Length} sinh viên đã chọn?",
                                             "Xác nhận xóa",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = selectedRows.Length - 1; i >= 0; i--)
                        {
                            DataRow row = gvDanhSachSV.GetDataRow(selectedRows[i]);
                            if (row != null && row.RowState == DataRowState.Added)
                            {
                                // Nếu là dòng mới chưa lưu DB thì chỉ cần remove khỏi DataTable
                                row.Delete();
                                gvDanhSachSV.DeleteRow(selectedRows[i]);
                            }
                            else
                            {
                                // Lấy MaSV từ hàng cũ
                                string maSV = gvDanhSachSV.GetRowCellValue(selectedRows[i], "MaSV").ToString();

                                // Sử dụng stored procedure thay vì DELETE trực tiếp
                                string query = $"EXEC sp_XoaSinhVien @MaSV = '{maSV}'";
                                frmAdmin.executeQuery(query);

                                // Xóa trên GridView
                                gvDanhSachSV.DeleteRow(selectedRows[i]);
                            }
                        }

                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sinh viên để xóa!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

            isAdding = false;
            editingRowHandle = -1;

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = false;
        }

        private void gvDanhSachSV_ShowingEditor(object sender, CancelEventArgs e)
        {

            GridView view = sender as GridView;
            int rowHandle = view.FocusedRowHandle;

            if (view.IsNewItemRow(rowHandle) || rowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
            {
                return;
            }

            DataRow row = view.GetDataRow(rowHandle);
            if (row == null) return;

            if (isAdding)
            {
                if (row.RowState != DataRowState.Added)
                {
                    e.Cancel = true; // khóa dòng cũ
                }
            }
            else
            {
                if (view.FocusedColumn != null && view.FocusedColumn.FieldName == "MaSV")
                    e.Cancel = true;
            }

        }
    


        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = true;
            editingRowHandle = gvDanhSachSV.FocusedRowHandle;

        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gvDanhSachSV.FocusedRowHandle; // Thêm dòng này

            if (isAdding)
            {
                // Nếu đang thêm mới, xóa dòng mới thêm
                if (gvDanhSachSV.IsNewItemRow(rowHandle) || rowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                {
                    gvDanhSachSV.DeleteRow(rowHandle);
                }
                else
                {
                    // Tìm và xóa dòng có RowState = Added
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

                // Hủy các thay đổi trong DataTable
                dtSinhVien.RejectChanges();
            }
            else
            {
                // Nếu đang sửa, hủy chỉnh sửa
                gvDanhSachSV.CancelUpdateCurrentRow();
                dtSinhVien.RejectChanges();
            }

            // Reset trạng thái các nút
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnHuy.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = false;
            isAdding = false;
            editingRowHandle = -1;
        }
    }
}

   