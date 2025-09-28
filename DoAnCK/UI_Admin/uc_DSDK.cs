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

namespace DoAnCK.UI_Admin
{
    public partial class uc_DSDK: UserControl, IRefreshable
    {


        private DataTable dt;
        private int maHocKyNamHoc;
        private bool isLoading = false;

        public uc_DSDK()
        {
            InitializeComponent();
            string connStr = frmAdmin.ConnString;

        }

        public void RefreshData()
        {

            isLoading = true;


            string queryNamHoc = "EXEC sp_DanhSachHocKyNamHoc";
            DataTable dtNamHoc = frmAdmin.getData(queryNamHoc);

            if (dtNamHoc != null && dtNamHoc.Rows.Count > 0)
            {
                dtNamHoc.Columns.Add("HK_NamHoc", typeof(string));
                foreach (DataRow row in dtNamHoc.Rows)
                {
                    row["HK_NamHoc"] = $"HK{row["HocKy"]} - {row["NamHoc"]}";
                }

                cbbNamHoc.DataSource = dtNamHoc;
                cbbNamHoc.DisplayMember = "HK_NamHoc";
                cbbNamHoc.ValueMember = "MaHocKyNamHoc";

                maHocKyNamHoc = Convert.ToInt32(cbbNamHoc.SelectedValue);

                string queryLopHocPhan = $"SELECT * FROM dbo.fn_DangKyMonHocTheoNamHoc({maHocKyNamHoc})";
                DataTable dt = frmAdmin.getData(queryLopHocPhan);

                gcDanhSachSV.DataSource = dt;
            }
            gvDanhSachSV.OptionsBehavior.Editable = false;
            gvDanhSachSV.RefreshData();



        }

        private void cbbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbNamHoc.SelectedItem == null)
                return;

            DataRowView drv = cbbNamHoc.SelectedItem as DataRowView;
            if (drv == null)
                return;

            int maHocKyNamHoc = Convert.ToInt32(drv["MaHocKyNamHoc"]);

            string query = $"SELECT * FROM dbo.fn_DangKyMonHocTheoNamHoc({maHocKyNamHoc})";
            DataTable dt = frmAdmin.getData(query);

            gcDanhSachSV.DataSource = dt;
        }
    }
}
