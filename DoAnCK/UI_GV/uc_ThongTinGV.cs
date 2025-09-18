using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DoAnCK.UI_GV
{
    public partial class uc_ThongTinGV : UserControl
    {
        private string MaGV;
        private string connStr;
        private DataTable dt;

        public uc_ThongTinGV()
        {
            InitializeComponent();
            connStr = frmGiangVien.ConnString;
            MaGV = frmGiangVien.MaGV;
        }
        public uc_ThongTinGV(string connectionString, string maGV)
        {
            InitializeComponent();
            connStr = connectionString;
            MaGV = maGV;
        }
        private void uc_ThongTinGV_Load(object sender, EventArgs e)
        {
            LoadThongTinGiangVien();
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;


        }

        private void LoadThongTinGiangVien()
        {
            try
            {
                string queryGV = $"SELECT * FROM dbo.fn_GetThongTinGV('{MaGV}')";
                DataTable dtGV = frmGiangVien.getData(queryGV);

                if (dtGV != null && dtGV.Rows.Count > 0)
                {
                    DataRow row = dtGV.Rows[0];

                    txtMa.Text = row["MaGV"].ToString();
                    txtHoten.Text = row["HoTenGV"].ToString();
                    txtHocvi.Text = row["HocVi"]?.ToString() ?? "";
                    txtKhoa.Text = row["Khoa"]?.ToString() ?? "";
                    txtEmail.Text = row["Email"]?.ToString() ?? "";
                    txtSDT.Text = row["DienThoai"]?.ToString() ?? "";
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin giảng viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }



       

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnHuy.Enabled = true;

            txtEmail.ReadOnly = false;
            txtSDT.ReadOnly = false;

        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string query = $@"EXEC sp_UpdateGiangVienContact 
                                    @MaGV = '{MaGV}',
                                    @Email = '{txtEmail.Text}',
                                    @DienThoai = '{txtSDT.Text}'";

                frmGiangVien.executeQuery(query);

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtEmail.ReadOnly = true;
                txtSDT.ReadOnly = true;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;

                btnSua.Enabled = true;
                LoadThongTinGiangVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadThongTinGiangVien();

            txtEmail.ReadOnly = true;
            txtSDT.ReadOnly = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtKhoa_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHocvi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
