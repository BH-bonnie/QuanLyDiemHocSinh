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
    public partial class uc_QLGV : UserControl
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dtGiangVien;
        private bool isAdding = false;
        private int editingRowHandle = -1;
        public uc_QLGV()
        {
            InitializeComponent();
        }

        private void uc_QLGV_Load(object sender, EventArgs e)
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
                dtGiangVien = frmAdmin.getData("SELECT * FROM v_GiangVien_Detail;");
                if (dtGiangVien != null)
                {
                    gcDanhSachSV.DataSource = dtGiangVien;
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

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] selectedRows = gvDanhSachSV.GetSelectedRows();

            if (selectedRows.Length > 0)
            {
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa không? ",
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
                            string maGV = gvDanhSachSV.GetRowCellValue(selectedRows[i], "MaGV").ToString();

                            string deleteQuery = "DELETE FROM GiangVien WHERE MaGV = @MaGV";
                            using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaGV", maGV);
                                cmd.ExecuteNonQuery();
                            }

                            // Xóa trên GridView
                            gvDanhSachSV.DeleteRow(selectedRows[i]);
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

            foreach (DataRow row in changes.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    string maGV = row["MaGV"].ToString().Trim();
                    string hoTen = row["HoTenGV"].ToString().Trim();

                    if (string.IsNullOrWhiteSpace(maGV) || string.IsNullOrWhiteSpace(hoTen))
                    {
                        MessageBox.Show("Mã giảng viên và Họ tên giảng viên không được để trống!");
                        return;
                    }

                    // Kiểm tra trùng trong database
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "SELECT COUNT(*) FROM GiangVien WHERE MaGV = @MaGV";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaGV", maGV);
                            int exist = (int)cmd.ExecuteScalar();
                            if (exist > 0)
                            {
                                MessageBox.Show($"Mã giảng viên '{maGV}' đã tồn tại!");
                                return;
                            }
                        }
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM GiangVien", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);

                da.Update(dtGiangVien);
            }

            dtGiangVien.AcceptChanges();
            MessageBox.Show("Đã lưu thay đổi vào CSDL!");
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            gvDanhSachSV.OptionsBehavior.Editable = false;
            btnThem.Enabled = true;

            isAdding = false;
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
                dtGiangVien.RejectChanges();
            }
            else
            {
                // Nếu đang sửa, hủy chỉnh sửa
                gvDanhSachSV.CancelUpdateCurrentRow();
                dtGiangVien.RejectChanges();
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
