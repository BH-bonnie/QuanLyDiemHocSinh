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
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;

namespace DoAnCK.UI_Admin
{
    public partial class uc_DSGD: UserControl, IRefreshable
    {
        private string connStr;
        private int maHocKyNamHoc;
        private bool isLoading = false;


        private DataTable dt;
        public uc_DSGD()
        {
            InitializeComponent();
            connStr = frmAdmin.ConnString;

        }

        public void RefreshData()
        {
           

            LoadLHP();
          
            gvDanhSachSV.OptionsBehavior.Editable = false;

            gvDanhSachSV.RefreshData();


        }
        private void LoadLHP()
        {
            isLoading = true;

            string queryNamHoc = "EXEC sp_DanhSachHocKyNamHoc;";
            DataTable dtNamHoc = frmAdmin.getData(queryNamHoc);

            if (dtNamHoc != null && dtNamHoc.Rows.Count > 0)
            {
                dtNamHoc.Columns.Add("HK_NamHoc", typeof(string));
                foreach (DataRow row in dtNamHoc.Rows)
                {
                    row["HK_NamHoc"] = $"HK{row["HocKy"]} - {row["NamHoc"]}";
                }

                cbbNamHoc.DataSource = dtNamHoc;
                cbbNamHoc.DisplayMember = "HK_NamHoc";
                cbbNamHoc.ValueMember = "MaHocKyNamHoc";
                cbbNamHoc.SelectedIndex = 0;

                maHocKyNamHoc = Convert.ToInt32(cbbNamHoc.SelectedValue);

                string queryLopHocPhan = $"SELECT * FROM dbo.fn_LopHocPhanTheoNamHoc({maHocKyNamHoc})";
                DataTable dt = frmAdmin.getData(queryLopHocPhan);


                gcDanhSachSV.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    gvDanhSachSV.FocusedRowHandle = 0;
                   
                }


            }
        }


        private void cbbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbNamHoc.SelectedItem == null)
                return;

            DataRowView drv = cbbNamHoc.SelectedItem as DataRowView;
            if (drv == null)
                return;
            btnHuy.Enabled = (cbbNamHoc.SelectedIndex == 0);
            barEditGiangVien.Enabled = (cbbNamHoc.SelectedIndex == 0);


            int maHocKyNamHoc = Convert.ToInt32(drv["MaHocKyNamHoc"]);

            string query = $"SELECT * FROM dbo.fn_LopHocPhanTheoNamHoc({maHocKyNamHoc})";
            DataTable dt = frmAdmin.getData(query);

            gcDanhSachSV.DataSource = dt;

        }
        void LoadGV(string maKhoa, string maGVcu)
        {
            string query = $"EXEC sp_GetGVTheoKhoa @MaKhoa = '{maKhoa}', @MaGVHienTai = '{maGVcu}'";
            DataTable dtGV = frmAdmin.getData(query);
    
            repoLookUpGiangVien.DataSource = null; 
            repoLookUpGiangVien.DataSource = dtGV;
            repoLookUpGiangVien.DisplayMember = "HoTenGV";
            repoLookUpGiangVien.ValueMember = "MaGV";
            repoLookUpGiangVien.NullText = "Chọn giảng viên thay thế";
            repoLookUpGiangVien.CustomDisplayText += (s, e) =>
            {
                e.DisplayText = "Chọn giảng viên thay thế";
            };
        }


        private void barEditGiangVien_EditValueChanged(object sender, EventArgs e)
        {
            if (gvDanhSachSV.FocusedRowHandle < 0) return;
            DataRow row = gvDanhSachSV.GetDataRow(gvDanhSachSV.FocusedRowHandle);
            if (row == null) return;
            string maLHP = row["MaLHP"]?.ToString();
            string maGV = barEditGiangVien.EditValue?.ToString();
            if (string.IsNullOrEmpty(maGV)) return;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn chọn giảng viên {maGV} dạy lớp {maLHP} không?",
                "Xác nhận đổi giảng viên",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                string maGVCu = row["MaGV"]?.ToString();
                string query = $"EXEC sp_ChuyenGiangVien @MaLHP = '{maLHP}', @MaHocKyNamHoc = {maHocKyNamHoc}, @MaGVNguon = '{maGVCu}', @MaGVDich = '{maGV}'";
                try
                {
                    frmAdmin.executeQuery(query);
                    MessageBox.Show($"Lớp {maLHP} đã đổi giảng viên thành công.", "Thành công");
                    LoadLHP();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đổi: " + ex.Message, "Lỗi");
                }
            }
        }

        private void gvDanhSachSV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                DataRow row = gvDanhSachSV.GetDataRow(e.FocusedRowHandle);
                string maKhoa = row["MaKhoa"]?.ToString();
                string maGVcu = row["MaGV"]?.ToString();
                LoadGV(maKhoa, maGVcu);
            }
        }

        private void repoLookUpGiangVien_Popup(object sender, EventArgs e)
        {
            if (repoLookUpGiangVien.DataSource == null || ((DataTable)repoLookUpGiangVien.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("Không có giảng viên khác để thay thế.", "Thông báo");
            }
        }

        private void barEditGiangVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            string maKhoa = gvDanhSachSV.GetRowCellValue(gvDanhSachSV.FocusedRowHandle, "MaKhoa")?.ToString();
            string maGVcu = gvDanhSachSV.GetRowCellValue(gvDanhSachSV.FocusedRowHandle, "MaGV")?.ToString();

            string query = $"EXEC sp_GetGVTheoKhoa @MaKhoa = '{maKhoa}', @MaGVHienTai = '{maGVcu}'";
            DataTable dtGV = frmAdmin.getData(query);

            if (dtGV == null || dtGV.Rows.Count == 0)
            {
                MessageBox.Show("Không có giảng viên thay thế!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                repoLookUpGiangVien.DataSource = null; 
                repoLookUpGiangVien.NullText = "Chọn giảng viên thay thế";
                return;
            }

        }

        private void btnHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            int[] selectedRows = gvDanhSachSV.GetSelectedRows();

            if (selectedRows.Length > 0)
            {
                var result = MessageBox.Show($"Bạn có chắc chắn muốn huỷ {selectedRows.Length} lớp học phần đã chọn?",
                                             "Xác nhận huỷ",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = selectedRows.Length - 1; i >= 0; i--)
                        {
                            DataRow row = gvDanhSachSV.GetDataRow(selectedRows[i]);
                            if (row != null && row.RowState == DataRowState.Added)
                            {
                                row.Delete();
                                gvDanhSachSV.DeleteRow(selectedRows[i]);
                            }
                            else
                            {
                                string maLHP = gvDanhSachSV.GetRowCellValue(selectedRows[i], "MaLHP").ToString();

                                string query = $"EXEC sp_HuyLopHocPhan @MaLHP = '{maLHP}',  @MaHocKyNamHoc={maHocKyNamHoc}";
                                frmAdmin.executeQuery(query);

                                gvDanhSachSV.DeleteRow(selectedRows[i]);
                            }
                        }

                        MessageBox.Show("Huỷ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi huỷ lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một lớp để huỷ!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }
    }
}