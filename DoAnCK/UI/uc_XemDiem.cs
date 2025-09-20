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


namespace DoAnCK.UI
{
    public partial class uc_XemDiem: UserControl
    {
        string connStr = FormMain.ConnString;

        public uc_XemDiem()
        {
            InitializeComponent();
        }

        private DataTable getData(string query)
        {
            using (var conn = new SqlConnection(connStr))
            {
                using (var adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
        private void uc_XemDiem_Load(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                DataTable dt = getData("Select * from SinhVien");
                if (dt != null)
                {
                    gcDanhsach.DataSource = dt; // Đảm bảo gvDanhsach là GridView

                }
            }
        }

       
    }
}
