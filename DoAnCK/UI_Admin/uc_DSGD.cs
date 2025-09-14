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
    public partial class uc_DSGD: UserControl
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dt;
        public uc_DSGD()
        {
            InitializeComponent();
        }

        private void uc_QLLHP_Load(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                dt = frmAdmin.getData("SELECT * FROM v_LopHocPhan_Detail;");
                if (dt != null)
                {
                    gcDanhSachSV.DataSource = dt;
                  

                }
            }
        }
    }
}
