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
using DevExpress.XtraEditors.Mask.Design;

namespace DoAnCK.UI_Admin
{
    public partial class uc_Chitiet : UserControl, IRefreshable
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
            if (isLoading) return;
            LoadTreeView();
        }

        public void RefreshData()
        {

            isLoading = true;
            LoadLopSV();
            isLoading = false;
            LoadTreeView();
            gvDanhSach.RefreshData();   

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

        private void LoadTreeView()
        {
            if (isLoading || cbbMa.SelectedValue == null) return;

            treeViewTen.Nodes.Clear();

            string selectedLop = cbbMa.SelectedValue.ToString();

            string query = $"SELECT MaSV, HoTen FROM dbo.fn_SinhVienTheoLop('{selectedLop}')";
            DataTable dtSV = frmAdmin.getData(query);

            foreach (DataRow row in dtSV.Rows)
            {
                string nodeText = $"{row["MaSV"]} - {row["HoTen"]}";
                treeViewTen.Nodes.Add(nodeText);
            }
        }

        /*    private void TinhDiemTBVaTinChiDat(string maSV)
            {
                try
                {
                    string query = $"EXEC sp_TinhTBVaTinChiDat @MaSV = '{maSV}'";
                    DataTable dtResult = frmAdmin.getData(query);

                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        DataRow row = dtResult.Rows[0];

                        double diemTB_He10 = row["DiemTB_He10"] != DBNull.Value ?
                            Convert.ToDouble(row["DiemTB_He10"]) : 0;
                        lblDiemhe10.Text = $"{diemTB_He10:F2}"; 

                        double diemTB_He4 = row["DiemTB_He4"] != DBNull.Value ?
                            Convert.ToDouble(row["DiemTB_He4"]) : 0;
                        lblDiemhe4.Text = $"{diemTB_He4:F2}"; 

                        int tinChiDat = row["TinChiDat"] != DBNull.Value ?
                            Convert.ToInt32(row["TinChiDat"]) : 0;
                        lblSoTin.Text = $"{tinChiDat}";
                    }
                    else
                    {
                        lblDiemhe4.Text = "0.00";
                        lblDiemhe10.Text = "0.00";
                        lblSoTin.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tính điểm TB và tín chỉ: " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    lblDiemhe4.Text = "0.00";
                    lblDiemhe10.Text = "0.00";
                    lblSoTin.Text = "0";
                }
            }

            private void treeViewTen_AfterSelect(object sender, TreeViewEventArgs e)
            {
                string nodeText = e.Node.Text;
                string maSV = nodeText.Split('-')[0].Trim();

                string queryInfo = $"SELECT * FROM vw_ThongTinChiTietSV WHERE MaSV = '{maSV}'";
                DataTable dtInfo = frmAdmin.getData(queryInfo);

                if (dtInfo != null && dtInfo.Rows.Count > 0)
                {
                    DataRow row = dtInfo.Rows[0];
                    lblMa.Text = row["MaSV"].ToString();
                    lblTen.Text = row["HoTen"].ToString();
                    lblNS.Text = row["NgaySinh"].ToString();
                    lblDRL.Text = row["DiemRenLuyen"] == DBNull.Value ? "0" :
                        row["DiemRenLuyen"].ToString();
                    lblNoiSinh.Text = row["NoiSinh"].ToString();
                    lblGioiTinh.Text = row["GioiTinh"].ToString();
                }

                string query = $"SELECT * FROM fn_ChiTietDiemSV('{maSV}')";
                DataTable dtChiTiet = frmAdmin.getData(query);

                if (isLoading || cbbMa.SelectedValue == null) return;
                string selectedLop = cbbMa.SelectedValue.ToString();
                gcDanhSach.DataSource = dtChiTiet;

                TinhDiemTBVaTinChiDat(maSV);
            }*/
        private void treeViewTen_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string nodeText = e.Node.Text;
            string maSV = nodeText.Split('-')[0].Trim();

            // Lấy toàn bộ thông tin sinh viên + điểm TB + tín chỉ đạt
            string query = $"SELECT * FROM dbo.RTM_ThongTinChiTietSV('{maSV}')";
            DataTable dtResult = frmAdmin.getData(query);

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                DataRow row = dtResult.Rows[0];

                // Thông tin sinh viên
                lblMa.Text = row["MaSV"].ToString();
                lblTen.Text = row["HoTen"].ToString();
                lblNS.Text = row["NgaySinh"].ToString();
                lblNoiSinh.Text = row["NoiSinh"].ToString();
                lblGioiTinh.Text = row["GioiTinh"].ToString();
                lblDRL.Text = row["DiemRenLuyen"] == DBNull.Value ? "0" :
                    row["DiemRenLuyen"].ToString();

                double diemTB_He10 = row["DiemTB_He10"] != DBNull.Value ?
                    Convert.ToDouble(row["DiemTB_He10"]) : 0;
                lblDiemhe10.Text = $"{diemTB_He10:F2}";

                double diemTB_He4 = row["DiemTB_He4"] != DBNull.Value ?
                    Convert.ToDouble(row["DiemTB_He4"]) : 0;
                lblDiemhe4.Text = $"{diemTB_He4:F2}";

                int tinChiDat = row["TinChiDat"] != DBNull.Value ?
                    Convert.ToInt32(row["TinChiDat"]) : 0;
                lblSoTin.Text = $"{tinChiDat}";

                // Chi tiết điểm từng môn
                gcDanhSach.DataSource = dtResult; 
            }
            else
            {
                lblMa.Text = lblTen.Text = lblNS.Text = lblNoiSinh.Text = lblGioiTinh.Text = "";
                lblDRL.Text = "0";
                lblDiemhe10.Text = "0.00";
                lblDiemhe4.Text = "0.00";
                lblSoTin.Text = "0";
                gcDanhSach.DataSource = null;
            }
            string querydiem = $"SELECT * FROM fn_ChiTietDiemSV('{maSV}')";
            DataTable dtChiTiet = frmAdmin.getData(querydiem);

            if (isLoading || cbbMa.SelectedValue == null) return;
            string selectedLop = cbbMa.SelectedValue.ToString();
            gcDanhSach.DataSource = dtChiTiet;

        }

    }
}