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
    public partial class uc_DSDK: UserControl
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dt;
        public uc_DSDK()
        {
            InitializeComponent();
        }

        private void uc_DSDK_Load(object sender, EventArgs e)
        {

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                dt = frmAdmin.getData("SELECT * FROM v_DangKyMonHoc_Detail;");
                if (dt != null)
                {
                    gcDanhSachSV.DataSource = dt;


                }
            }
        }
    }
}
