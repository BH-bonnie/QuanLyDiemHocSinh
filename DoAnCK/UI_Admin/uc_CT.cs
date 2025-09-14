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
using DevExpress.XtraEditors.TextEditController.Utils;

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
            string sql = "SELECT TOP 1 Ma, TiLeGK, TiLeCK FROM CongThucTinhDiem ORDER BY Ma DESC";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtTiLeGK.Text = reader["TiLeGK"].ToString();
                        txtTiLeCK.Text = reader["TiLeCK"].ToString();
                    }
                    else
                    {
                        txtTiLeGK.Text = "";
                        txtTiLeCK.Text = "";
                    }
                }
            }
        }
        
    


        
        private void btnLuu_Click(object sender, EventArgs e)
       
        {
            // Kiểm tra dữ liệu hợp lệ
            if (decimal.TryParse(txtTiLeGK.Text, out decimal tGK) &&
                decimal.TryParse(txtTiLeCK.Text, out decimal tCK) &&
                tGK >= 0 && tGK <= 1 && tCK >= 0 && tCK <= 1)
            {
                
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "INSERT INTO CongThucTinhDiem(TiLeGK, TiLeCK) VALUES(@TiLeGK, @TiLeCK)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TiLeGK", tGK);
                        cmd.Parameters.AddWithValue("@TiLeCK", tCK);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                MessageBox.Show("Lưu thành công!");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ (0–1).");
            }
        }

        private void txtTiLeGK_TextChanged(object sender, EventArgs e)
        {


          
            // Kiểm tra giá trị nhập có phải số thập phân không
            if (decimal.TryParse(txtTiLeGK.Text, out decimal tGK))
            {
                // Kiểm tra điều kiện >0 và <1
                if (tGK > 0 && tGK < 1)
                {
                    decimal tCK = 1 - tGK;
                    txtTiLeCK.Text = tCK.ToString("0.##");
                }
                else
                {
                    // Nếu không thỏa điều kiện, xóa TextBox2 hoặc thông báo
                    txtTiLeCK.Text = "";
                }
            }
            else
            {
                // Nếu không phải số
                txtTiLeCK.Text = "";
            }
        }

    }

}
