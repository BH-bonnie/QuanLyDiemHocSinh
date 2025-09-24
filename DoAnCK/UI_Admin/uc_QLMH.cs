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
    public partial class uc_QLMH: UserControl, IRefreshable
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dt;

        public uc_QLMH()
        {
            InitializeComponent();
        }

        public void RefreshData()
        {

            
                dt = frmAdmin.getData("SELECT * FROM v_MonHoc;");
                if (dt != null)
                {
                    gcDanhSachSV.DataSource = dt;
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;

                    gvDanhSachSV.OptionsBehavior.Editable = false;

                }
            gvDanhSachSV.RefreshData();


        }

          
        

      

    private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDanhSachSV.CloseEditor();
            gvDanhSachSV.UpdateCurrentRow();

            var changes = dt.GetChanges();
            if (changes == null || changes.Rows.Count == 0)
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

            gvDanhSachSV.OptionsBehavior.Editable = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDanhSachSV.OptionsBehavior.Editable = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnHuy.Enabled = true;


        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dt.RejectChanges();
            gcDanhSachSV.DataSource = dt;

            gvDanhSachSV.OptionsBehavior.Editable = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
        }
    }
}
