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
        public uc_QLGV()
        {
            InitializeComponent();
        }

        private void uc_QLGV_Load(object sender, EventArgs e)
        {
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
            gvDanhSachSV.AddNewRow();

            int newRowHandle = gvDanhSachSV.FocusedRowHandle;
            gvDanhSachSV.FocusedRowHandle = newRowHandle;
            DataRow newRow = dtGiangVien.NewRow();

            gvDanhSachSV.FocusedRowHandle = gvDanhSachSV.GetRowHandle(dtGiangVien.Rows.IndexOf(newRow));

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
                    // Duyệt ngược để tránh lỗi index
                    for (int i = selectedRows.Length - 1; i >= 0; i--)
                    {
                        gvDanhSachSV.DeleteRow(selectedRows[i]);
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

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM GiangVien", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);

                da.Update(dtGiangVien);
            }

            dtGiangVien.AcceptChanges();
            MessageBox.Show("Đã lưu thay đổi vào CSDL!");
           
        }

        private void btnQuayLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn hủy các thay đổi chưa lưu?",
                                "Xác nhận",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dtGiangVien = frmAdmin.getData("SELECT * FROM v_GiangVien_Detail;");
                gcDanhSachSV.DataSource = dtGiangVien;

                MessageBox.Show("Dữ liệu đã được khôi phục về trạng thái trước khi thay đổi.",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
    }
}
