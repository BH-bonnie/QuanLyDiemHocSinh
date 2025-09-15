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
                    foreach (DataRow row in changes.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            string maSV = row["MaSV"].ToString().Trim();
                            string hoTen = row["HoTen"].ToString().Trim();

                            if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(hoTen))
                            {
                                MessageBox.Show("Mã sinh viên và Họ tên không được để trống!");
                                return;
                            }

                            using (SqlConnection conn = new SqlConnection(connStr))
                            {
                                conn.Open();
                                string query = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                                    int exist = (int)cmd.ExecuteScalar();
                                    if (exist > 0)
                                    {
                                        MessageBox.Show($"Mã giảng viên '{maSV}' đã tồn tại!");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                        using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SinhVien", conn);
                        SqlCommandBuilder builder = new SqlCommandBuilder(da);

                        da.Update(dtSinhVien);
                    }

                    dtSinhVien.AcceptChanges();
                    MessageBox.Show("Đã lưu thay đổi vào CSDL!");
                    btnLuu.Enabled = false;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    gvDanhSachSV.OptionsBehavior.Editable = false;
                    btnThem.Enabled = true;

                    isAdding = false;
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
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();

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

                                // Xóa trong database
                                string deleteQuery = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                                    cmd.ExecuteNonQuery();
                                }

                                // Xóa trên GridView
                                gvDanhSachSV.DeleteRow(selectedRows[i]);
                            }
                        }
                    }

                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // Nếu là NewItemRow thì cho phép
            if (view.IsNewItemRow(rowHandle) || rowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
            {
                return;
            }

            // Lấy DataRow thực sự
            DataRow row = view.GetDataRow(rowHandle);
            if (row == null) return;

            if (isAdding)
            {
                // Cho phép sửa các dòng mới thêm (RowState == Added)
                if (row.RowState != DataRowState.Added)
                {
                    e.Cancel = true; // khóa dòng cũ
                }
            }
            else
            {
                // Ở chế độ sửa: chỉ khóa cột MaSV
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

   