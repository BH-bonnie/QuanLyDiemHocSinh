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
    public partial class uc_QLMH: UserControl
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dt;

        public uc_QLMH()
        {
            InitializeComponent();
        }

        private void uc_QLMH_Load(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                dt = frmAdmin.getData("SELECT * FROM v_MonHoc;");
                if (dt != null)
                {
                    gcDanhSachSV.DataSource = dt;
                    btnLuu.Enabled = false;

                    gvDanhSachSV.OptionsBehavior.Editable = false;


                }
            }
        }

      

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            gvDanhSachSV.CloseEditor();
            gvDanhSachSV.UpdateCurrentRow();
            btnLuu.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = false;
            btnSua.Enabled = true;

            var changes = dt.GetChanges();
            if (changes == null)
            {
                MessageBox.Show("Không có thay đổi nào để lưu!");
               
                return;
            }
            foreach (DataRow row in changes.Rows)
            {
                if (row.Table.Columns.Contains("SoTinChi"))
                {
                    var value = row["SoTinChi"];
                    int soTinChi;
                    if (!int.TryParse(value.ToString(), out soTinChi) || soTinChi <= 0)
                    {
                        MessageBox.Show("Số tín chỉ phải là số nguyên không âm!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

                using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM MonHoc", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);

                da.Update(dt);
            }

            dt.AcceptChanges();
            MessageBox.Show("Đã lưu thay đổi vào CSDL!");
           


        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDanhSachSV.OptionsBehavior.Editable = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;

        }
    }
}
