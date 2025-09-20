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
    public partial class uc_TaiKhoan : UserControl
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dtTaiKhoan;
        private bool isAdding = false;
        private int editingRowHandle = -1;

        public uc_TaiKhoan()
        {
            InitializeComponent();
        }

        private void uc_TaiKhoan_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            isAdding = false;

            GridView gvDanhSach = gcDanhSach.MainView as GridView;
            gvDanhSach.OptionsBehavior.Editable = false;
            btnThem.Enabled = true;

            dtTaiKhoan = frmAdmin.getData("SELECT * FROM v_TaiKhoan_Detail;");
            if (dtTaiKhoan != null)
            {
                gcDanhSach.DataSource = dtTaiKhoan;
                // Cho phép chọn nhiều dòng
                gvDanhSach.OptionsSelection.MultiSelect = true;
                gvDanhSach.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
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

            GridView gvDanhSach = gcDanhSach.MainView as GridView;

            gvDanhSach.OptionsBehavior.Editable = true;
            gvDanhSach.AddNewRow();

            gvDanhSach.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvDanhSach.FocusedColumn = gvDanhSach.VisibleColumns[0];
            gvDanhSach.ShowEditor();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;

            GridView gvDanhSach = gcDanhSach.MainView as GridView;
            gvDanhSach.OptionsBehavior.Editable = true;
            editingRowHandle = gvDanhSach.FocusedRowHandle;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView gvDanhSach = gcDanhSach.MainView as GridView;
            int[] selectedRows = gvDanhSach.GetSelectedRows();

            if (selectedRows.Length > 0)
            {
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {selectedRows.Length} tài khoản đã chọn?",
                                             "Xác nhận xóa",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = selectedRows.Length - 1; i >= 0; i--)
                        {
                            DataRow row = gvDanhSach.GetDataRow(selectedRows[i]);
                            if (row != null && row.RowState == DataRowState.Added)
                            {
                                row.Delete();
                                gvDanhSach.DeleteRow(selectedRows[i]);
                            }
                            else
                            {
                                string maTK = gvDanhSach.GetRowCellValue(selectedRows[i], "MaTK").ToString();

                                string query = $"EXEC sp_XoaTaiKhoan @MaTK = {maTK}";
                                frmAdmin.executeQuery(query);

                                gvDanhSach.DeleteRow(selectedRows[i]);
                            }
                        }

                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một tài khoản để xóa!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

            isAdding = false;
            editingRowHandle = -1;

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView gvDanhSach = gcDanhSach.MainView as GridView;
            gvDanhSach.CloseEditor();
            gvDanhSach.UpdateCurrentRow();

            var changes = dtTaiKhoan.GetChanges();
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
                        string matKhau = row["MatKhau"]?.ToString() ?? "";
                        string roleId = row["Roleid"]?.ToString() ?? "";
                        string maGV = row["MaGV"]?.ToString() ?? "";
                        string trangThai = row["TrangThai"]?.ToString() ?? "";

                        if (string.IsNullOrWhiteSpace(matKhau) || string.IsNullOrWhiteSpace(roleId))
                        {
                            MessageBox.Show("Mật khẩu và Role không được để trống!");
                            return;
                        }

                        string query = $@"EXEC sp_ThemTaiKhoan 
                                                @MatKhau = N'{matKhau}', 
                                                @Roleid = {roleId}, 
                                                @MaGV = {(string.IsNullOrWhiteSpace(maGV) ? "NULL" : $"'{maGV}'")}, 
                                                @TrangThai = {(string.IsNullOrWhiteSpace(trangThai) ? "1" : trangThai)}";

                        frmAdmin.executeQuery(query);
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        string maTK = row["MaTK"].ToString();
                        string matKhau = row["MatKhau"]?.ToString() ?? "";
                        string roleId = row["Roleid"]?.ToString() ?? "";
                        string maGV = row["MaGV"]?.ToString() ?? "";
                        string trangThai = row["TrangThai"]?.ToString() ?? "";

                        // Kiểm tra dữ liệu bắt buộc
                        if (string.IsNullOrWhiteSpace(maTK))
                        {
                            MessageBox.Show("Mã tài khoản không được để trống!");
                            return;
                        }

                        string query = $@"EXEC sp_CapNhatTaiKhoan 
                                                @MaTK = {maTK}, 
                                                @MatKhau = {(string.IsNullOrWhiteSpace(matKhau) ? "NULL" : $"N'{matKhau}'")}, 
                                                @Roleid = {(string.IsNullOrWhiteSpace(roleId) ? "NULL" : roleId)}, 
                                                @MaGV = {(string.IsNullOrWhiteSpace(maGV) ? "NULL" : $"'{maGV}'")}, 
                                                @TrangThai = {(string.IsNullOrWhiteSpace(trangThai) ? "NULL" : trangThai)}";

                        frmAdmin.executeQuery(query);
                    }
                }

                dtTaiKhoan.AcceptChanges();
                MessageBox.Show("Đã lưu thay đổi vào CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dtTaiKhoan = frmAdmin.getData("SELECT * FROM v_TaiKhoan_Detail;");
                gcDanhSach.DataSource = dtTaiKhoan;

                btnLuu.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnHuy.Enabled = false;
                gvDanhSach.OptionsBehavior.Editable = false;
                btnThem.Enabled = true;
                isAdding = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView gvDanhSach = gcDanhSach.MainView as GridView;
            int rowHandle = gvDanhSach.FocusedRowHandle;

            if (isAdding)
            {
                // Nếu đang thêm mới, xóa dòng mới thêm
                if (gvDanhSach.IsNewItemRow(rowHandle) || rowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                {
                    gvDanhSach.DeleteRow(rowHandle);
                }
                else
                {
                    // Tìm và xóa dòng có RowState = Added
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

                // Hủy các thay đổi trong DataTable
                dtTaiKhoan.RejectChanges();
            }
            else
            {
                // Nếu đang sửa, hủy chỉnh sửa
                gvDanhSach.CancelUpdateCurrentRow();
                dtTaiKhoan.RejectChanges();
            }

            // Reset trạng thái các nút
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnHuy.Enabled = false;
            gvDanhSach.OptionsBehavior.Editable = false;
            isAdding = false;
            editingRowHandle = -1;
        }

        private void gvDanhSach_ShowingEditor(object sender, CancelEventArgs e)
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
                if (view.FocusedColumn != null && view.FocusedColumn.FieldName == "MaTK")
                    e.Cancel = true;
            }
        }
    }
}
