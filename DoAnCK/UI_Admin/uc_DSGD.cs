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
          
            string queryNamHoc = "SELECT MaHocKyNamHoc, HocKy, NamHoc FROM HocKyNamHoc ORDER BY MaHocKyNamHoc DESC";
            DataTable dtNamHoc = frmAdmin.getData(queryNamHoc);

            if (dtNamHoc != null && dtNamHoc.Rows.Count > 0)
            {
                // Thêm cột hiển thị
                dtNamHoc.Columns.Add("HK_NamHoc", typeof(string));
                foreach (DataRow row in dtNamHoc.Rows)
                {
                    row["HK_NamHoc"] = $"HK{row["HocKy"]} - {row["NamHoc"]}";
                }

                cbbNamHoc.DataSource = dtNamHoc;
                cbbNamHoc.DisplayMember = "HK_NamHoc";
                cbbNamHoc.ValueMember = "MaHocKyNamHoc";

                int maHocKyNamHoc = Convert.ToInt32(cbbNamHoc.SelectedValue);

                // 3. Câu SQL lấy lớp học phần cho năm học hiện tại
                string queryLopHocPhan = $"SELECT * FROM dbo.fn_LopHocPhanTheoNamHoc({maHocKyNamHoc})";
                DataTable dt = frmAdmin.getData(queryLopHocPhan);

                // 4. Gán vào GridControl
                gcDanhSachSV.DataSource = dt;
            }
        }

        
        private void cbbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbNamHoc.SelectedItem == null)
                return;

            // Lấy DataRowView từ SelectedItem
            DataRowView drv = cbbNamHoc.SelectedItem as DataRowView;
            if (drv == null)
                return;

            // Lấy MaHocKyNamHoc
            int maHocKyNamHoc = Convert.ToInt32(drv["MaHocKyNamHoc"]);

            // Gọi câu SQL lấy lớp học phần theo năm học
            string query = $"SELECT * FROM dbo.fn_LopHocPhanTheoNamHoc({maHocKyNamHoc})";
            DataTable dt = frmAdmin.getData(query);

            // Bind vào GridControl
            gcDanhSachSV.DataSource = dt;

        }
    }
}
