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
    public partial class uc_Chitiet : UserControl
    {

        public uc_Chitiet()
        {
            InitializeComponent();
            string connStr = frmAdmin.ConnString;



        }
        private DataTable dt;
        private int LopSV;

        private bool isLoading = false;
        private void cbbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeViewTen.Nodes.Clear();
            if (cbbMa.SelectedValue == null) return;

            string selectedLop = cbbMa.SelectedValue.ToString();

            // Query to get both MaSV and HoTen
            string query = $"SELECT MaSV, HoTen FROM dbo.fn_SinhVienTheoLop('{selectedLop}')";

            DataTable dtSV = frmAdmin.getData(query);

            // Add each student as a node: "MaSV - HoTen"
            foreach (DataRow row in dtSV.Rows)
            {
                string nodeText = $"{row["MaSV"]} - {row["HoTen"]}";
                treeViewTen.Nodes.Add(nodeText);
            }
        }

        private void uc_Chitiet_Load(object sender, EventArgs e)
        {
            LoadLopSV();

        }
        private void LoadLopSV()
        {
            string queryMa = $@"
            SELECT DISTINCT LopSV 
            FROM Lop";
            DataTable dtMa = frmAdmin.getData(queryMa);


            cbbMa.DataSource = dtMa;
            cbbMa.DisplayMember = "LopSV";
            cbbMa.ValueMember = "LopSV";





        }

        private double TinhTrungBinhDiemDau(string tenCotDiem)
        {
            if (gcDanhSach.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                // Lọc các dòng có trạng thái "Đậu" và cột điểm không null
                var diemList = dt.AsEnumerable()
                    .Where(r => r["TrangThai"] != DBNull.Value
                                && r["TrangThai"].ToString() == "Đậu"
                                && r[tenCotDiem] != DBNull.Value
                                && double.TryParse(r[tenCotDiem].ToString(), out _))
                    .Select(r => Convert.ToDouble(r[tenCotDiem]))
                    .ToList();

                if (diemList.Count == 0) return 0;

                return diemList.Average();
            }
            return 0;
        }
        private int TinhTongTinChiDau()
        {
            if (gcDanhSach.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                var tongTinChi = dt.AsEnumerable()
                    .Where(r => r["TrangThai"] != DBNull.Value
                                && r["TrangThai"].ToString() == "Đậu"
                                && r["SoTinChi"] != DBNull.Value
                                && int.TryParse(r["SoTinChi"].ToString(), out _))
                    .Sum(r => Convert.ToInt32(r["SoTinChi"]));
                return tongTinChi;
            }
            return 0;
        }





        private void treeViewTen_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string nodeText = e.Node.Text;
            string maSV = nodeText.Split('-')[0].Trim();
            string queryInfo = $"SELECT * FROM vw_ThongTinChiTietSV WHERE MaSV = '{maSV}'";
            DataTable dtInfo = frmAdmin.getData(queryInfo);
            DataRow row = dtInfo.Rows[0];
            lblMa.Text = row["MaSV"].ToString();
            lblTen.Text = row["HoTen"].ToString();
            lblNS.Text = row["NgaySinh"] == DBNull.Value ? "" : Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy");

            // Điểm rèn luyện
            lblDRL.Text = row["DiemRenLuyen"] == DBNull.Value ? "0" : row["DiemRenLuyen"].ToString();
            lblNoiSinh.Text = row["NoiSinh"].ToString();
            lblGioiTinh.Text = row["GioiTinh"].ToString();
            
            
            // Query detailed scores for the selected student
            string query = $"SELECT * FROM dbo.fn_ChiTietDiemSV('{maSV}')";

            DataTable dtChiTiet = frmAdmin.getData(query);

            // Bind to grid control
            gcDanhSach.DataSource = dtChiTiet;

            double tbDiemHe10 = TinhTrungBinhDiemDau("DiemHe10");
            double tbDiemHe4 = TinhTrungBinhDiemDau("DiemHe4");

            lblDiemhe4.Text = $" {tbDiemHe10:F2}";
            lblDiemhe10.Text = $" {tbDiemHe4:F2}";
            int tongTinChi = TinhTongTinChiDau();
            lblSoTin.Text = $"{tongTinChi} ";

        }
    }
}