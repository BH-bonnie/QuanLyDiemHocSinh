using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCK.UI_GV
{
    public partial class frmThongKe : Form
    {
        private string currentMaLHP ;
        private int currentMaHocKyNamHoc;
        public string CurrentMaLHP
        {
            get { return currentMaLHP; }
            set { currentMaLHP = value; }
        }

        public int CurrentMaHocKyNamHoc
        {
            get { return currentMaHocKyNamHoc; }
            set { currentMaHocKyNamHoc = value; }
        }
        public void HienThiBieuDoThongKe(string maLHP, int maHocKyNamHoc)
        {
            if (string.IsNullOrEmpty(maLHP) || maHocKyNamHoc <= 0)
            {
                MessageBox.Show("Chưa có dữ liệu để hiển thị biểu đồ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy dữ liệu thống kê từ stored procedure bằng getData
            string query = $"EXEC sp_ThongKeDiemLopHocPhan @MaLHP = '{maLHP}', @MaHocKyNamHoc = {maHocKyNamHoc}";
            DataTable dtThongKe = frmGiangVien.getData(query);

            if (dtThongKe != null && dtThongKe.Rows.Count > 0)
            {
                DataRow row = dtThongKe.Rows[0];

                // Xóa dữ liệu cũ
                chartThongKe.Series.Clear();

                // Tạo series mới
                Series series = new Series("Thống kê", ViewType.Pie);
                series.Points.Add(new SeriesPoint("Đạt", Convert.ToInt32(row["SoSinhVienDat"])));
                series.Points.Add(new SeriesPoint("Rớt", Convert.ToInt32(row["SoSinhVienRớt"])));
                series.Points.Add(new SeriesPoint("Chưa chấm", Convert.ToInt32(row["SoSinhVienChuaCham"])));

                // Đặt màu sắc cho từng phần
                series.Points[0].Color = Color.FromArgb(76, 175, 80);    // Xanh lá
                series.Points[1].Color = Color.FromArgb(244, 67, 54);    // Đỏ
                series.Points[2].Color = Color.FromArgb(255, 193, 7);    // Vàng

                // Hiển thị nhãn và phần trăm
                series.Label.TextPattern = "{A}: {V} ({VP:P1})";
                series.Label.Font = new Font("Segoe UI", 12, FontStyle.Bold);

                // Hiệu ứng 3D (nếu muốn)
                chartThongKe.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chartThongKe.Series.Add(series);
                chartThongKe.Titles.Clear();
                chartThongKe.Titles.Add(new ChartTitle() { Text = "Thống kê kết quả lớp học phần", Font = new Font("Segoe UI", 14, FontStyle.Bold) });

                chartThongKe.Dock = DockStyle.Fill;
                chartThongKe.BackColor = Color.White;
                chartThongKe.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu thống kê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void HienThiBieuDoPhoDiem(string maLHP, int maHocKyNamHoc)
        {
            // Kiểm tra tham số đầu vào
            if (string.IsNullOrEmpty(maLHP) || maHocKyNamHoc <= 0)
            {
                MessageBox.Show("Chưa có dữ liệu để hiển thị biểu đồ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy dữ liệu phổ điểm từ stored procedure bằng getData
            string query = $"EXEC sp_ThongKeDiemTheoKhoangNho @MaLHP = '{maLHP}', @MaHocKyNamHoc = {maHocKyNamHoc}";
            DataTable dtPhoDiem = frmGiangVien.getData(query);

            if (dtPhoDiem != null && dtPhoDiem.Rows.Count > 0)
            {
                DataRow row = dtPhoDiem.Rows[0];

                // Xóa dữ liệu cũ
                chartThongKe.Series.Clear();

                // Tạo series mới cho biểu đồ cột
                Series series = new Series("Phổ điểm", ViewType.Bar);

                // Thêm các điểm dữ liệu cho từng khoảng điểm
                series.Points.Add(new SeriesPoint("0-1", Convert.ToInt32(row["Khoang0_1"])));
                series.Points.Add(new SeriesPoint("1-2", Convert.ToInt32(row["Khoang1_2"])));
                series.Points.Add(new SeriesPoint("2-3", Convert.ToInt32(row["Khoang2_3"])));
                series.Points.Add(new SeriesPoint("3-4", Convert.ToInt32(row["Khoang3_4"])));
                series.Points.Add(new SeriesPoint("4-5", Convert.ToInt32(row["Khoang4_5"])));
                series.Points.Add(new SeriesPoint("5-6", Convert.ToInt32(row["Khoang5_6"])));
                series.Points.Add(new SeriesPoint("6-7", Convert.ToInt32(row["Khoang6_7"])));
                series.Points.Add(new SeriesPoint("7-8", Convert.ToInt32(row["Khoang7_8"])));
                series.Points.Add(new SeriesPoint("8-9", Convert.ToInt32(row["Khoang8_9"])));
                series.Points.Add(new SeriesPoint("9-10", Convert.ToInt32(row["Khoang9_10"])));

                // Đặt màu sắc gradient cho các cột
                Color[] colors = {
                                Color.FromArgb(244, 67, 54),   // Đỏ đậm
                                Color.FromArgb(255, 87, 34),   // Đỏ cam
                                Color.FromArgb(255, 152, 0),   // Cam
                                Color.FromArgb(255, 193, 7),   // Vàng
                                Color.FromArgb(205, 220, 57),  // Vàng xanh
                                Color.FromArgb(139, 195, 74),  // Xanh lá nhạt
                                Color.FromArgb(76, 175, 80),   // Xanh lá
                                Color.FromArgb(0, 150, 136),   // Xanh lá đậm
                                Color.FromArgb(33, 150, 243),  // Xanh dương
                                Color.FromArgb(63, 81, 181)    // Xanh tím
                            };

                for (int i = 0; i < series.Points.Count && i < colors.Length; i++)
                {
                    series.Points[i].Color = colors[i];
                }

                // Cấu hình hiển thị
                series.Label.TextPattern = "{V}";
                series.Label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                // Thêm series vào chart
                chartThongKe.Series.Add(series);

                // Cấu hình chart
                chartThongKe.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chartThongKe.Titles.Clear();
                chartThongKe.Titles.Add(new ChartTitle()
                {
                    Text = "Phổ điểm lớp học phần",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold)
                });

                // Cấu hình trục X và Y
                if (chartThongKe.Diagram is XYDiagram diagram)
                {
                    diagram.AxisX.Title.Text = "Khoảng điểm";
                    diagram.AxisX.Title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    diagram.AxisY.Title.Text = "Số sinh viên";
                    diagram.AxisY.Title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    diagram.AxisY.WholeRange.SetMinMaxValues(0, Convert.ToInt32(row["TongSoSinhVien"]) + 1);

                }

                chartThongKe.Dock = DockStyle.Fill;
                chartThongKe.BackColor = Color.White;
                chartThongKe.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu phổ điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public frmThongKe()
        {
            InitializeComponent();


            comboBoxEdit.SelectedIndex = 0; 
        }

       

        public void SetData(string maLHP, int maHocKyNamHoc)
        {
            currentMaLHP = maLHP;
            currentMaHocKyNamHoc = maHocKyNamHoc;

            // Hiển thị chart theo lựa chọn hiện tại
            comboBoxEdit_SelectedIndexChanged(null, null);
        }

        private void comboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            // Kiểm tra nếu đã có dữ liệu để hiển thị
                if (!string.IsNullOrEmpty(currentMaLHP) && currentMaHocKyNamHoc > 0)
                {
                    switch (comboBoxEdit.SelectedIndex)
                    {
                        case 0: // "Thống kê đạt rớt"
                                 HienThiBieuDoThongKe(currentMaLHP, currentMaHocKyNamHoc);
                            break;
                        case 1: // "Thống kê phổ điểm"
                            HienThiBieuDoPhoDiem(currentMaLHP, currentMaHocKyNamHoc);
                            break;
                    }
                }
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {

        }
    }
}
