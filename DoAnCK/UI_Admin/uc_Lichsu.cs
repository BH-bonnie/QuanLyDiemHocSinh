using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCK.UI_Admin
{
    public partial class uc_Lichsu: UserControl, IRefreshable
    {

        string connStr;
        private DataTable dt;
        public uc_Lichsu()
        {
            InitializeComponent();
            string connStr = frmAdmin.ConnString;
        }
        public void RefreshData()
        {


            string query = $"SELECT * FROM LogDangNhap";
            DataTable dt = frmAdmin.getData(query);


            gcDanhSach.DataSource = dt;
            gvDanhSach.RefreshData();



        }


    }


}
