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
    public partial class uc_QLGV : UserControl, IRefreshable
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dtGiangVien;
        private bool isAdding = false;
        private int editingRowHandle = -1;
        public uc_QLGV()
        {
            InitializeComponent();
        }

        public void RefreshData()
        {
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            isAdding = false;
            gvDanhSachSV.OptionsBehavior.Editable = false;
            btnThem.Enabled = true;
          
             dtGiangVien = frmAdmin.getData("SELECT * FROM v_GiangVien_Detail;");
                if (dtGiangVien != null)
                {
                    gcDanhSachSV.DataSource = dtGiangVien;
                    gvDanhSachSV.OptionsSelection.MultiSelect = true;
                    gvDanhSachSV.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;


                }
                loadKhoa();
                gvDanhSachSV.RefreshData();  


            
        }
        private void loadKhoa()

        {
            lkKhoa.DataSource = frmAdmin.getData("Select * From Khoa");
            lkKhoa.DisplayMember = "TenKhoa";
            lkKhoa.ValueMember = "MaKhoa";
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

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] selectedRows = gvDanhSachSV.GetSelectedRows();

            if (selectedRows.Length > 0)
            {
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {selectedRows.Length} giảng viên đã chọn?",
                                             "Xác nhận xóa",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = selectedRows.Length - 1; i >= 0; i--)
                        {
                            string maGV = gvDanhSachSV.GetRowCellValue(selectedRows[i], "MaGV").ToString();

                            string query = $"EXEC sp_XoaGiangVien @MaGV = '{maGV}'";
                            frmAdmin.executeQuery(query);

                            gvDanhSachSV.DeleteRow(selectedRows[i]);
                        }

                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa giảng viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một giảng viên để xóa!",
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
            gvDanhSachSV.OptionsBehavior.Editable = false;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDanhSachSV.CloseEditor();
            gvDanhSachSV.UpdateCurrentRow();

            var changes = dtGiangVien.GetChanges();
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
                        string maGV = row["MaGV"].ToString().Trim();
                        string hoTenGV = row["HoTenGV"].ToString().Trim();
                        string hocVi = row["HocVi"]?.ToString() ?? "";
                        string makhoa = row["MaKhoa"]?.ToString() ?? "";
                        string email = row["Email"]?.ToString() ?? "";
                        string dienThoai = row["DienThoai"]?.ToString() ?? "";

                        if (string.IsNullOrWhiteSpace(maGV) || string.IsNullOrWhiteSpace(hoTenGV) || string.IsNullOrWhiteSpace(makhoa))
                        {
                            MessageBox.Show("Mã giảng viên, Họ tên giảng viên và khoa không được để trống!");
                            return;
                        }

                        // Gọi stored procedure để thêm giảng viên
                        string query = $@"EXEC sp_ThemGiangVien 
                                            @MaGV = '{maGV}', 
                                            @HoTenGV = N'{hoTenGV}', 
                                            @HocVi = {(string.IsNullOrWhiteSpace(hocVi) ? "NULL" : $"N'{hocVi}'")}, 
                                            @MaKhoa = {(string.IsNullOrWhiteSpace(makhoa) ? "NULL" : $"N'{makhoa}'")}, 
                                            @Email = {(string.IsNullOrWhiteSpace(email) ? "NULL" : $"N'{email}'")}, 
                                            @DienThoai = {(string.IsNullOrWhiteSpace(dienThoai) ? "NULL" : $"N'{dienThoai}'")}";

                        frmAdmin.executeQuery(query);
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        string maGV = row["MaGV"].ToString().Trim();
                        string hoTenGV = row["HoTenGV"].ToString().Trim();
                        string hocVi = row["HocVi"]?.ToString() ?? "";
                        string makhoa = row["MaKhoa"]?.ToString() ?? "";
                        string email = row["Email"]?.ToString() ?? "";
                        string dienThoai = row["DienThoai"]?.ToString() ?? "";
                        int trangThai = row["TrangThai"] != DBNull.Value ? Convert.ToInt32(row["TrangThai"]) : 0;


                        // Kiểm tra dữ liệu bắt buộc
                        if (string.IsNullOrWhiteSpace(maGV) || string.IsNullOrWhiteSpace(hoTenGV))
                        {
                            MessageBox.Show("Mã giảng viên và Họ tên giảng viên không được để trống!");
                            return;
                        }

                        string query = $@"EXEC sp_CapNhatGiangVien 
                        @MaGV = '{maGV}', 
                        @HoTenGV = N'{hoTenGV}', 
                        @HocVi = {(string.IsNullOrWhiteSpace(hocVi) ? "NULL" : $"N'{hocVi}'")}, 
                        @MaKhoa = {(string.IsNullOrWhiteSpace(makhoa) ? "NULL" : $"N'{makhoa}'")}, 
                        @Email = {(string.IsNullOrWhiteSpace(email) ? "NULL" : $"N'{email}'")}, 
                        @DienThoai = {(string.IsNullOrWhiteSpace(dienThoai) ? "NULL" : $"N'{dienThoai}'")},
                        @TrangThai = { trangThai} ";


                        frmAdmin.executeQuery(query);
                    }


                }

                dtGiangVien.AcceptChanges();
                MessageBox.Show("Đã lưu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dtGiangVien = frmAdmin.getData("SELECT * FROM v_GiangVien_Detail;");
                gcDanhSachSV.DataSource = dtGiangVien;

                btnLuu.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnHuy.Enabled = false;

                gvDanhSachSV.OptionsBehavior.Editable = false;
                btnThem.Enabled = true;
                isAdding = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                if (view.FocusedColumn != null && view.FocusedColumn.FieldName == "MaGV")
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

                dtGiangVien.RejectChanges();
            }
            else
            {
                gvDanhSachSV.CancelUpdateCurrentRow();
                dtGiangVien.RejectChanges();
            }

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
