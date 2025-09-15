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

namespace DoAnCK.UI_GV
{
    public partial class uc_DSGD : UserControl
    {
        private string connStr;
        private DataTable dt;

        public uc_DSGD()
        {
            InitializeComponent();
            connStr = frmGiangVien.ConnString;
        }

        private void uc_DSGD_Load(object sender, EventArgs e)
        {

            string queryNamHoc = "SELECT MaHocKyNamHoc, HocKy, NamHoc FROM HocKyNamHoc ORDER BY MaHocKyNamHoc DESC";
            DataTable dtNamHoc = frmGiangVien.getData(queryNamHoc);

            if (dtNamHoc != null && dtNamHoc.Rows.Count > 0)
            {
                // Thêm cột hiển thị
                dtNamHoc.Columns.Add("HK_NamHoc", typeof(string));
                foreach (DataRow row in dtNamHoc.Rows)
                {
                    row["HK_NamHoc"] = $"HK{row["HocKy"]} - {row["NamHoc"]}";
                }

                // Gán vào ComboBox
                cbbNamHoc.DataSource = dtNamHoc;
                cbbNamHoc.DisplayMember = "HK_NamHoc";
                cbbNamHoc.ValueMember = "MaHocKyNamHoc";
                int maHocKyNamHoc = Convert.ToInt32(cbbNamHoc.SelectedValue);
                string queryMonHoc = $"SELECT * FROM fn_DanhSachMonHoc_GiangVien('{"GV001"}', {maHocKyNamHoc})";
                DataTable dt = frmGiangVien.getData(queryMonHoc);
                gcDanhSach.DataSource = dt;



            }
            gvDanhSach.OptionsBehavior.Editable = false;

        }

        private void cbbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra SelectedItem != null
            if (cbbNamHoc.SelectedItem == null)
                return;

            string maGV = "GV001";

            // Lấy DataRowView từ SelectedItem
            DataRowView drv = cbbNamHoc.SelectedItem as DataRowView;
            if (drv == null)
                return;

            // Lấy MaHocKyNamHoc
            int maHocKyNamHoc = Convert.ToInt32(drv["MaHocKyNamHoc"]);

            // Gọi function SQL
            string query = $"SELECT * FROM fn_DanhSachMonHoc_GiangVien('{maGV}', {maHocKyNamHoc})";
            DataTable dt = frmGiangVien.getData(query);

            // Bind vào grid
            gcDanhSach.DataSource = dt;
        }
    }
        
}