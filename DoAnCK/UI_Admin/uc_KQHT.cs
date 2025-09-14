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
    public partial class uc_KQHT: UserControl
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dt;
        public uc_KQHT()
        {
            InitializeComponent();
        }

        private void uc_KQHT_Load(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                dt = frmAdmin.getData("SELECT * FROM v_ChiTietHocPhan_Detail;");
                if (dt != null)
                {
                    gcDanhSachSV.DataSource = dt;
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

            var changes = dt.GetChanges();
            if (changes == null)
            {
                MessageBox.Show("Không có thay đổi nào để lưu!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaSV, MaMH, DiemGK, DiemCK FROM ChiTietHocPhan", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);

                da.Update(dt);
                dt.Clear();
                da.Fill(dt);
            }

            dt.AcceptChanges();
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
                dt = frmAdmin.getData("SELECT * FROM v_ChiTietHocPhan_Detail");
                gcDanhSachSV.DataSource = dt;

                MessageBox.Show("Dữ liệu đã được khôi phục về trạng thái trước khi thay đổi.",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }
    }
}
