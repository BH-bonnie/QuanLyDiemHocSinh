using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCK.UI_Admin
{
    public partial class frmThongKeAD : Form
    {
        private int currentMaHocKyNamHoc;

        public int CurrentMaHocKyNamHoc
        {
            get { return currentMaHocKyNamHoc; }
            set { currentMaHocKyNamHoc = value; }
        }

        public frmThongKeAD()
        {
            InitializeComponent();
        }
        public void SetData(int maHocKyNamHoc)
        {
            currentMaHocKyNamHoc = maHocKyNamHoc;

        }

        public frmThongKeAD(int maHocKyNamHoc)
        {
            InitializeComponent();
            currentMaHocKyNamHoc = maHocKyNamHoc; // Thêm dòng này
        }

        private void frmThongKeAD_Load(object sender, EventArgs e)
        {
            label2.Text = "currentMaHocKyNamHoc";


            if (currentMaHocKyNamHoc > 0)
            {
                LoadThongKeTrungBinhMonHoc();
            }
            
        }

        public void LoadThongKeTrungBinhMonHoc()
        {
            try
            {
                string query = $"EXEC sp_TrungBinhMonHoc @MaHocKyNamHoc = {currentMaHocKyNamHoc}";
                DataTable dt = frmAdmin.getData(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    gridControl1.DataSource = dt;
                    ConfigureGridView();


                      string queryHocKy = $"SELECT HocKy, NamHoc FROM HocKyNamHoc WHERE MaHocKyNamHoc = {currentMaHocKyNamHoc}";
                       DataTable dtHocKy = frmAdmin.getData(queryHocKy);
                       if (dtHocKy != null && dtHocKy.Rows.Count > 0)
                       {
                           DataRow rowHK = dtHocKy.Rows[0];
                           label2.Text = $"Học kỳ {rowHK["HocKy"]} - Năm học {rowHK["NamHoc"]}";
                       }
                      
                   }
                   else
                   {
                       gridControl1.DataSource = null;
                       label2.Text = "Không có dữ liệu";
                       MessageBox.Show("Không có dữ liệu thống kê cho học kỳ này.", "Thông báo",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
                }
            
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu thống kê: " + ex.Message, "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                gridControl1.DataSource = null;
                label2.Text = "Lỗi tải dữ liệu";
            }
        }

      
        private void ConfigureGridView()
        {
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;

            if (gridView1.Columns.Count > 0)
            {
                // Cột Mã môn học
                if (gridView1.Columns["MaMH"] != null)
                {
                    gridView1.Columns["MaMH"].Caption = "Mã Môn Học";
                    gridView1.Columns["MaMH"].Width = 100;
                }

                // Cột Tên môn học
                if (gridView1.Columns["TenMH"] != null)
                {
                    gridView1.Columns["TenMH"].Caption = "Tên Môn Học";
                    gridView1.Columns["TenMH"].Width = 200;
                }

                // Cột Tổng số sinh viên
                if (gridView1.Columns["SoSV_Tong"] != null)
                {
                    gridView1.Columns["SoSV_Tong"].Caption = "Tổng SV";
                    gridView1.Columns["SoSV_Tong"].Width = 80;
                    gridView1.Columns["SoSV_Tong"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns["SoSV_Tong"].DisplayFormat.FormatString = "0";
                }

                // Cột Điểm trung bình
                if (gridView1.Columns["DiemTB"] != null)
                {
                    gridView1.Columns["DiemTB"].Caption = "Điểm TB";
                    gridView1.Columns["DiemTB"].Width = 80;
                    gridView1.Columns["DiemTB"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns["DiemTB"].DisplayFormat.FormatString = "0.00";
                }

                // Cột Số sinh viên đạt
                if (gridView1.Columns["SoSV_Dat"] != null)
                {
                    gridView1.Columns["SoSV_Dat"].Caption = "SV Đạt";
                    gridView1.Columns["SoSV_Dat"].Width = 80;
                    gridView1.Columns["SoSV_Dat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns["SoSV_Dat"].DisplayFormat.FormatString = "0";
                }

                // Cột Số sinh viên rớt
                if (gridView1.Columns["SoSV_Rot"] != null)
                {
                    gridView1.Columns["SoSV_Rot"].Caption = "SV Rớt";
                    gridView1.Columns["SoSV_Rot"].Width = 80;
                    gridView1.Columns["SoSV_Rot"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns["SoSV_Rot"].DisplayFormat.FormatString = "0";
                }

                // Cột Số sinh viên chưa chấm
                if (gridView1.Columns["SoSV_Chuacham"] != null)
                {
                    gridView1.Columns["SoSV_Chuacham"].Caption = "SV Chưa Chấm";
                    gridView1.Columns["SoSV_Chuacham"].Width = 100;
                    gridView1.Columns["SoSV_Chuacham"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns["SoSV_Chuacham"].DisplayFormat.FormatString = "0";
                }

                gridView1.BestFitColumns();
            }
        }

       
    }
}
