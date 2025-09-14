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
        public void HienThiBieuDoThongKe(string maLHP, int maHocKyNamHoc)
        {
            // Lấy dữ liệu thống kê từ stored procedure
            DataTable dtThongKe = null;
            using (SqlConnection conn = new SqlConnection(frmGiangVien.ConnString))
            using (SqlCommand cmd = new SqlCommand("sp_ThongKeDiemLopHocPhan", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaLHP", maLHP);
                cmd.Parameters.AddWithValue("@MaHocKyNamHoc", maHocKyNamHoc);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtThongKe = new DataTable();
                da.Fill(dtThongKe);
            }

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
        public frmThongKe()
        {
            InitializeComponent();
        }
    }
}
