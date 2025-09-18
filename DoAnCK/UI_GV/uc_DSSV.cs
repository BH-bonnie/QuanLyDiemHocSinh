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
        private string MaGV;
        private string connStr;
        private DataTable dt;
        public uc_DSSV()
        {
            InitializeComponent();
            connStr = frmGiangVien.ConnString;
            MaGV = frmGiangVien.MaGV;
        }
        public uc_DSSV(string connectionString, string maGV)
        {
            InitializeComponent();
            connStr = connectionString;
            MaGV = maGV;
        }
        private int maHocKyNamHoc;
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
            gvDanhSach.OptionsBehavior.Editable = false;

        }
        private void LoadMaLop()
        {
            string query = $"EXEC sp_GetLopHocPhanByGV @MaHocKyNamHoc = {maHocKyNamHoc}, @MaGV = '{MaGV}'";

            DataTable dtMa = frmGiangVien.getData(query);

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



        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gvDanhSach.FocusedRowHandle;
            if (rowHandle < 0) return;

            DataRow row = gvDanhSach.GetDataRow(rowHandle);
            if (row == null) return;

            string maSV = row["MaSV"].ToString();
            string maLHP = cbbMa.SelectedValue.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sinh viên {maSV} khỏi lớp học phần {maLHP} không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                string query = $"EXEC sp_XoaDangKyMonHoc @MaSV = '{maSV}', @MaLHP = '{maLHP}'";
                try
                {
                    frmGiangVien.executeQuery(query);
                    dt.Rows.Remove(row);
                    gvDanhSach.RefreshData();
                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

     
         private void barListItem_GetItemData(object sender, EventArgs e)
         {
             if (gvDanhSach.FocusedRowHandle >= 0 && cbbMa.SelectedValue != null)
            {
                string maLHPHienTai = cbbMa.SelectedValue.ToString();
                LoadLopChuyen(maLHPHienTai, MaGV); }
           }
       

         void LoadLopChuyen(string maLHPHienTai, string maGV)
         {
            string query = $"EXEC sp_LayLopHocPhanKhac @MaHocKyNamHoc = {maHocKyNamHoc}, @MaGV = '{maGV}', @MaLHPHienTai = '{maLHPHienTai}'";

            DataTable dtLop = frmGiangVien.getData(query);

            barListItem.Strings.Clear();

            if (dtLop != null && dtLop.Rows.Count > 0)
            {
                foreach (DataRow row in dtLop.Rows)
                {
                    barListItem.Strings.Add(row["MaLHP"].ToString());
                }
            }
            else
            {
                if (!isLoading)
                {
                    MessageBox.Show("Không có lớp học phần khác để chuyển trong cùng môn và giảng viên.", "Thông báo");
                }
            }
        }


        private void barListItem_ListItemClick(object sender, ListItemClickEventArgs e)
        {
            if (gvDanhSach.FocusedRowHandle >= 0 && cbbMa.SelectedValue != null)
            {
                string maLHPHienTai = cbbMa.SelectedValue.ToString();
                LoadLopChuyen(maLHPHienTai, MaGV);
            }
            if (gvDanhSach.FocusedRowHandle < 0) return;

            DataRow row = gvDanhSach.GetDataRow(gvDanhSach.FocusedRowHandle);
            if (row == null) return;

            string maSV = row["MaSV"].ToString();
            string maLHPNguon = cbbMa.SelectedValue.ToString();
            string maLHPDich = barListItem.Strings[e.Index];

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn chuyển sinh viên {maSV} từ lớp {maLHPNguon} sang lớp {maLHPDich} không?",
                "Xác nhận chuyển lớp",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                string query = $"EXEC sp_ChuyenLopHocPhan @MaSV = '{maSV}', @MaLHPNguon = '{maLHPNguon}', @MaLHPDich = '{maLHPDich}'";
                try
                {
                    frmGiangVien.executeQuery(query);
                    MessageBox.Show($"Đã chuyển sinh viên {maSV} từ lớp {maLHPNguon} sang lớp {maLHPDich}.", "Thành công");
                    LoadSinhVien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chuyển lớp: " + ex.Message, "Lỗi");
                }
            }
        }

      
    }

}
