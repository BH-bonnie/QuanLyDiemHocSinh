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
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.TextEditController.Utils;

namespace DoAnCK.UI_GV
{
    public partial class uc_DSSV : UserControl
    {
        string connStr = frmGiangVien.ConnString;


        public uc_DSSV()
        {
            InitializeComponent();
        }
        private int maHocKyNamHoc;
        private DataTable dt;
        private bool isLoading = false;
        private void uc_DSSV_Load(object sender, EventArgs e)
        {
            isLoading = true;

            string queryMaHK = "SELECT TOP 1 MaHocKyNamHoc FROM HocKyNamHoc ORDER BY MaHocKyNamHoc DESC";
            DataTable dtHK = frmGiangVien.getData(queryMaHK);
            if (dtHK != null && dtHK.Rows.Count > 0)
                maHocKyNamHoc = Convert.ToInt32(dtHK.Rows[0]["MaHocKyNamHoc"]);
            else
                MessageBox.Show("Không tìm thấy học kỳ/năm học hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            LoadMaLop();

            isLoading = false;

            LoadSinhVien();
        }
        private void LoadMaLop()
        {
            string queryMa = $@"
            SELECT DISTINCT MaLHP 
            FROM LopHocPhan 
            WHERE MaHocKyNamHoc = {maHocKyNamHoc} 
              AND MaGV = '{"GV001"}'
            ORDER BY MaLHP";

            DataTable dtMa = frmGiangVien.getData(queryMa);

            if (dtMa != null && dtMa.Rows.Count > 0)
            {
                cbbMa.DataSource = dtMa;
                cbbMa.DisplayMember = "MaLHP";
                cbbMa.ValueMember = "MaLHP";
            }
            else
            {
                cbbMa.DataSource = null;
                MessageBox.Show("Giảng viên này chưa phụ trách lớp học phần nào trong học kỳ hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadSinhVien()
        {
            if (isLoading || cbbMa.SelectedValue == null) return;

            string maLHP = cbbMa.SelectedValue.ToString();
            string query = $"SELECT * FROM fn_SinhVienTheoLopHocPhan('{maLHP}', {maHocKyNamHoc})";

            dt = frmGiangVien.getData(query);

            if (dt != null && dt.Rows.Count > 0)
                gcDanhSach.DataSource = dt;
            else
            {
                gcDanhSach.DataSource = null;
                MessageBox.Show("Không có dữ liệu sinh viên cho lớp học phần này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void cbbMa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            LoadSinhVien();
        }




        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }






        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gvDanhSach.FocusedRowHandle;
            if (rowHandle < 0) return;

            DataRow row = gvDanhSach.GetDataRow(rowHandle);
            if (row == null) return;

            string maSV = row["MaSV"].ToString();
            string maLHP = cbbMa.SelectedValue.ToString();

            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sinh viên {maSV} khỏi lớp học phần {maLHP} không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                string query = $"DELETE FROM DangKyMonHoc WHERE MaSV='{maSV}' AND MaLHP='{maLHP}'";
                try
                {
                    frmGiangVien.executeQuery(query); // gọi phương thức executeQuery đúng cách

                    // Xóa dòng khỏi DataTable cục bộ
                    dt.Rows.Remove(row);
                    gvDanhSach.RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test");
        }

        // Event handler to load classes before the dropdown is shown
        private void barListItem_GetItemData(object sender, EventArgs e)
        {
            // Check if a student is selected and current class is available
            if (gvDanhSach.FocusedRowHandle >= 0 && cbbMa.SelectedValue != null)
            {
                string maLHPHienTai = cbbMa.SelectedValue.ToString();
                string maGV = "GV001"; // or get from login context
                LoadLopChuyen(maLHPHienTai, maGV);
            }
        }

        private void LoadLopChuyen(string maLHPHienTai, string maGV)
        {
            string query = $@"
                SELECT MaLHP
                FROM LopHocPhan
                WHERE MaHocKyNamHoc = {maHocKyNamHoc}
                  AND MaGV = '{maGV}'
                  AND MaMH = (SELECT MaMH FROM LopHocPhan WHERE MaLHP = '{maLHPHienTai}')
                  AND MaLHP <> '{maLHPHienTai}'
                ORDER BY MaLHP";

            DataTable dtLop = frmGiangVien.getData(query);

            barListItem.Strings.Clear(); // Clear the list of items using the Strings property

            if (dtLop != null && dtLop.Rows.Count > 0)
            {
                foreach (DataRow row in dtLop.Rows)
                {
                    barListItem.Strings.Add(row["MaLHP"].ToString()); 
                }
            }
            else
            {
                MessageBox.Show("Không có lớp học phần khác để chuyển trong cùng môn và giảng viên.", "Thông báo");
            }
        }


        private void barListItem_ListItemClick(object sender, ListItemClickEventArgs e)
        {
            if (gvDanhSach.FocusedRowHandle < 0) return;

            DataRow row = gvDanhSach.GetDataRow(gvDanhSach.FocusedRowHandle);
            if (row == null) return;

            string maSV = row["MaSV"].ToString();
            string maLHPNguon = cbbMa.SelectedValue.ToString();  // Lớp nguồn (hiện tại)
            string maLHPDich = barListItem.Strings[e.Index];

            string maGV = "GV001";

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn chuyển sinh viên {maSV} từ lớp {maLHPNguon} sang lớp {maLHPDich} không?",
                "Xác nhận chuyển lớp",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Sử dụng UPDATE đơn giản
                    string updateQuery = "UPDATE DangKyMonHoc SET MaLHP = @MaLHPDich WHERE MaSV = @MaSV AND MaLHP = @MaLHPNguon";

                    using (SqlConnection conn = new SqlConnection(connStr))
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSV", maSV);
                        cmd.Parameters.AddWithValue("@MaLHPNguon", maLHPNguon);
                        cmd.Parameters.AddWithValue("@MaLHPDich", maLHPDich);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Đã chuyển sinh viên {maSV} từ lớp {maLHPNguon} sang lớp {maLHPDich}.", "Thành công");
                            LoadSinhVien();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy đăng ký để chuyển!", "Lỗi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chuyển lớp: " + ex.Message, "Lỗi");
                }
            }
        }

  
    }

}
