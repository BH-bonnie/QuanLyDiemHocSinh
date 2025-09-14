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
    public partial class uc_KQDKMH: UserControl
    {
        string connStr = frmMain.ConnString;
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
        public uc_KQDKMH()
        {
            InitializeComponent();
        }

        private void uc_KQDKMH_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = getData(@"
            SELECT * 
            FROM v_KetQuaDangKy
            WHERE MaSV = 'SV001' AND HocKy = 1 AND NamHoc = '2025-2026';
        ");

                if (dt != null)
                {
                    gcDanhSach.DataSource = dt;  
                    gvDanhSach.PopulateColumns(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        }
    }
