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
        public uc_QLSV()
        {
            InitializeComponent();

        }
  

        private void uc_QLSV_Load(object sender, EventArgs e)
        {
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

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SinhVien", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);

                da.Update(dtSinhVien);
            }

            dtSinhVien.AcceptChanges();
            MessageBox.Show("Đã lưu thay đổi vào CSDL!");
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDanhSachSV.AddNewRow();

            int newRowHandle = gvDanhSachSV.FocusedRowHandle;
            gvDanhSachSV.FocusedRowHandle = newRowHandle;
            DataRow newRow = dtSinhVien.NewRow();

            gvDanhSachSV.FocusedRowHandle = gvDanhSachSV.GetRowHandle(dtSinhVien.Rows.IndexOf(newRow));

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
                    // Duyệt ngược để tránh lỗi index
                    for (int i = selectedRows.Length - 1; i >= 0; i--)
                    {
                        gvDanhSachSV.DeleteRow(selectedRows[i]);
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
        }

        private void btnQuayLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn hủy các thay đổi chưa lưu?",
                                "Xác nhận",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dtSinhVien = frmAdmin.getData("SELECT * FROM v_SinhVien_Detail;");
                gcDanhSachSV.DataSource = dtSinhVien;

                MessageBox.Show("Dữ liệu đã được khôi phục về trạng thái trước khi thay đổi.",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
    }
}

   