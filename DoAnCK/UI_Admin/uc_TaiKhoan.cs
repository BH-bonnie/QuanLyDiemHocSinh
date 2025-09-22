using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Customization;

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
            btnThem.Enabled = true;

            gvDanhSach.OptionsBehavior.Editable = false;
            gvDanhSach.OptionsSelection.MultiSelect = true;
            gvDanhSach.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            LoadData();
            loadGV();
            loadRole();
            loadTrangThai();
        }

        private void LoadData()
        {
            try
            {
                dtTaiKhoan = frmAdmin.getData("SELECT * FROM vw_ThongTinTaiKhoan");
                if (dtTaiKhoan != null && dtTaiKhoan.Rows.Count > 0)
                {
                    gcDanhSach.DataSource = dtTaiKhoan;

                }
                else
                {
                    MessageBox.Show("không có dữ liệu hoặc rỗng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message} StackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadRole()

        {
            lkRoleid.DataSource = frmAdmin.getData("Select * From Roles");
            lkRoleid.DisplayMember = "Rolename";
            lkRoleid.ValueMember = "Roleid";
        }
        private void loadGV()

        {
            lkMaGV.DataSource = frmAdmin.getData("Select * From GiangVien");
            lkMaGV.DisplayMember = "MaGV";
            lkMaGV.ValueMember = "MaGV";
        }
        private void loadTrangThai()

        {
            ckTrangThai.ValueChecked = true;
            ckTrangThai.ValueUnchecked = false;


        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            btnThem.Enabled = false;
            btnHuy.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            isAdding = true;


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
            gvDanhSach.OptionsBehavior.Editable = true;
            editingRowHandle = gvDanhSach.FocusedRowHandle;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] selectedRows = gvDanhSach.GetSelectedRows();

            if (selectedRows.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một tài khoản để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {selectedRows.Length} tài khoản đã chọn?",
                                         "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                for (int i = selectedRows.Length - 1; i >= 0; i--)
                {
                    DataRow row = gvDanhSach.GetDataRow(selectedRows[i]);
                    if (row != null && row.RowState == DataRowState.Added)
                    {
                        gvDanhSach.DeleteRow(selectedRows[i]);
                    }
                    else
                    {
                        // SỬA LẠI: Kiểm tra null trước khi gọi ToString()
                        object maTKValue = gvDanhSach.GetRowCellValue(selectedRows[i], "MaTK");
                        if (maTKValue == null || maTKValue == DBNull.Value)
                        {
                            MessageBox.Show($"Không tìm thấy MaTK cho dòng {selectedRows[i]}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue; // Bỏ qua dòng này và tiếp tục
                        }

                        string maTK = maTKValue.ToString();
                        frmAdmin.executeQuery($"EXEC sp_XoaTaiKhoan @MaTK = '{maTK}'");
                        gvDanhSach.DeleteRow(selectedRows[i]);
                    }
                }

                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetButtons();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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
                        string tenDangNhap = row["TenDangNhap"]?.ToString() ?? ""; 
                        string matKhau = row["MatKhau"]?.ToString() ?? "";
                        string roleId = row["Roleid"]?.ToString() ?? "";
                        string maGV = row["MaGV"]?.ToString() ?? "";
                        int trangThai = row["TrangThai"] == DBNull.Value ? 1: (Convert.ToBoolean(row["TrangThai"]) ? 1 : 0);

                        if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau) || string.IsNullOrWhiteSpace(roleId))
                        {
                            MessageBox.Show("Tên đăng nhập, mật khẩu và Role không được để trống!");
                            return;
                        }
                        
                        if (int.Parse(roleId) == 2 && string.IsNullOrWhiteSpace(maGV))
                        {
                            MessageBox.Show("Phải có mã giảng viên");
                            return;
                        }
                        

                        string query = $@"EXEC sp_ThemTaiKhoan 
                                                @TenDangNhap = N'{tenDangNhap}',
                                                @MatKhau = N'{matKhau}', 
                                                @Roleid = {roleId}, 
                                                @MaGV = {(string.IsNullOrWhiteSpace(maGV) ? "NULL" : $"'{maGV}'")}, 
                                                @TrangThai = {trangThai}";
                      
                            frmAdmin.executeQuery(query);
                       
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        string maTK = row["MaTK"].ToString();
                        string tenDangNhap = row["TenDangNhap"]?.ToString() ?? ""; 
                        string matKhau = row["MatKhau"]?.ToString() ?? "";
                        string roleId = row["Roleid"]?.ToString() ?? "";
                        string maGV = row["MaGV"]?.ToString() ?? "";
                        string trangThai = row["TrangThai"]?.ToString() ?? "";
                        if (int.Parse(roleId) == 2 && string.IsNullOrWhiteSpace(maGV))
                        {
                            MessageBox.Show("Phải có mã giảng viên");
                            return;
                        }
                        string query = $@"EXEC sp_CapNhatTaiKhoan 
                                                @MaTK = {maTK}, 
                                                @TenDangNhap = {(string.IsNullOrWhiteSpace(tenDangNhap) ? "NULL" : $"N'{tenDangNhap}'")},
                                                @MatKhau = {(string.IsNullOrWhiteSpace(matKhau) ? "NULL" : $"N'{matKhau}'")}, 
                                                @Roleid = {(string.IsNullOrWhiteSpace(roleId) ? "NULL" : roleId)}, 
                                                @MaGV = {(string.IsNullOrWhiteSpace(maGV) ? "NULL" : $"'{maGV}'")}, 
                                                @TrangThai = {(string.IsNullOrWhiteSpace(trangThai) ? "NULL" : trangThai)}";
                        frmAdmin.executeQuery(query);
                    }
                }

                dtTaiKhoan.AcceptChanges();
                MessageBox.Show("Đã lưu thay đổi vào CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                ResetButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gvDanhSach.FocusedRowHandle; // Thêm dòng này

            if (isAdding)
            {
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

                dtTaiKhoan.RejectChanges();
            }
        
            else
            {
                gvDanhSach.CancelUpdateCurrentRow();
                dtTaiKhoan.RejectChanges();
            }

             btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnHuy.Enabled = false;
            gvDanhSach.OptionsBehavior.Editable = false;
            isAdding = false;
            editingRowHandle = -1;
        }
        

        private void gvDanhSach_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
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
                    e.Cancel = true;
                }
            }
            else
            {
                if (view.FocusedColumn != null && view.FocusedColumn.FieldName == "TenDangNhap")
                    e.Cancel = true;
            }

        }

        private void ResetButtons()
        {
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            gvDanhSach.OptionsBehavior.Editable = false;
            isAdding = false;
            editingRowHandle = -1;
        }

        
    }
}
