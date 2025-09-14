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
using DevExpress.XtraGrid.Views.Grid;

namespace DoAnCK.UI_Admin
{
    public partial class uc_CT: UserControl
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dt;
        public uc_CT()
        {
            InitializeComponent();
        }

        private void uc_CT_Load(object sender, EventArgs e)
        {
          

            }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {

            }
        }
    }
}
