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
    public partial class uc_CT: UserControl, IRefreshable
    {
        string connStr = frmAdmin.ConnString;
        private DataTable dt;
        public uc_CT()
        {
            InitializeComponent();
        }

        public void RefreshData()
        {

            try
            {
                string sql = "SELECT TOP 1 Ma, TiLeGK, TiLeCK FROM CongThucTinhDiem ORDER BY Ma DESC";
                DataTable dt = frmAdmin.getData(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtTiLeGK.Text = row["TiLeGK"].ToString();
                    txtTiLeCK.Text = row["TiLeCK"].ToString();
                }
                else
                {
                    txtTiLeGK.Text = "";
                    txtTiLeCK.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTiLeGK.Text = "";
                txtTiLeCK.Text = "";
            }

        }
        
    


        
        private void btnLuu_Click(object sender, EventArgs e)
       
        {
            if (decimal.TryParse(txtTiLeGK.Text, out decimal tGK) &&
        decimal.TryParse(txtTiLeCK.Text, out decimal tCK) &&
        tGK >= 0 && tGK <= 1 && tCK >= 0 && tCK <= 1)
            {
                try
                {
                    string query = $"EXEC sp_ThemCongThucTinhDiem @TiLeGK = {tGK}, @TiLeCK = {tCK}";
                    frmAdmin.executeQuery(query);

                    MessageBox.Show("Lưu thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ (0–1).");
            }
        }

        private void txtTiLeGK_TextChanged(object sender, EventArgs e)
        {


          
            if (decimal.TryParse(txtTiLeGK.Text, out decimal tGK))
            {
                if (tGK > 0 && tGK < 1)
                {
                    decimal tCK = 1 - tGK;
                    txtTiLeCK.Text = tCK.ToString("0.##");
                }
                else
                {
                    txtTiLeCK.Text = "";
                }
            }
            else
            {
                txtTiLeCK.Text = "";
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            RefreshData();

        }
    }

}
