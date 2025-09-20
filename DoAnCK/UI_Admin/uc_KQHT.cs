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

namespace DoAnCK.UI_Admin
{
    public partial class uc_KQHT: UserControl
    {
        string connStr;
        private int maHocKyNamHoc;
        private bool isAdding = false;
        private int editingRowHandle = -1;
        private DataTable dt;
        private bool isLoading = false;
        public uc_KQHT()
        {
            InitializeComponent();
            string connStr = frmAdmin.ConnString;

        }

        private void uc_KQHT_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            isAdding = false;
            gvDanhSachSV.OptionsBehavior.Editable = false;
            LoadMaHKNH();

            isLoading = false;

            LoadBangDiem();


            

        }
        private void LoadMaHKNH()
        {
            string queryNamHoc = $@" SELECT MaHocKyNamHoc, HocKy, NamHoc FROM HocKyNamHoc ORDER BY MaHocKyNamHoc DESC";

            DataTable dtNamHoc = frmAdmin.getData(queryNamHoc);

            if (dtNamHoc != null && dtNamHoc.Rows.Count > 0)
            {
                dtNamHoc.Columns.Add("HK_NamHoc", typeof(string));
                foreach (DataRow row in dtNamHoc.Rows)
                {
                    row["HK_NamHoc"] = $"HK{row["HocKy"]} - {row["NamHoc"]}";
                }

                // Gán vào ComboBox
                cbbNamHoc.DataSource = dtNamHoc;
                cbbNamHoc.DisplayMember = "HK_NamHoc";
                cbbNamHoc.ValueMember = "MaHocKyNamHoc";
                maHocKyNamHoc = Convert.ToInt32(cbbNamHoc.SelectedValue);

                string querySV = $"SELECT * FROM fn_ChiTietHocPhan( {maHocKyNamHoc})";
                DataTable dt = frmAdmin.getData(querySV);
                gcDanhSachSV.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy học kỳ/năm học hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void LoadBangDiem()
        {
            if (isLoading || cbbNamHoc.SelectedValue == null) return;

            string maLHP = cbbNamHoc.SelectedValue.ToString();
            string query = $"SELECT * FROM fn_ChiTietHocPhan( {maHocKyNamHoc})";

            dt = frmAdmin.getData(query);

            if (dt != null && dt.Rows.Count > 0)
                gcDanhSachSV.DataSource = dt;
            else
            {
                gcDanhSachSV.DataSource = null;
                MessageBox.Show("Không có dữ liệu sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        

void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDanhSachSV.CloseEditor();
            gvDanhSachSV.UpdateCurrentRow();

            DataTable dtChanges = ((DataTable)gcDanhSachSV.DataSource)?.GetChanges();
            if (dtChanges == null || dtChanges.Rows.Count == 0)
            {
                MessageBox.Show("Không có thay đổi nào để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                foreach (DataRow row in dtChanges.Rows)
                {
                    string maSV = row["MaSV"].ToString();
                    string maMH = row["MaMH"].ToString();
                    decimal? diemGK = row["DiemGK"] != DBNull.Value ? Convert.ToDecimal(row["DiemGK"]) : (decimal?)null;
                    decimal? diemCK = row["DiemCK"] != DBNull.Value ? Convert.ToDecimal(row["DiemCK"]) : (decimal?)null;

                    // Kiểm tra điểm hợp lệ
                    if ((diemGK.HasValue && (diemGK < 0 || diemGK > 10)) ||
                        (diemCK.HasValue && (diemCK < 0 || diemCK > 10)))
                    {
                        MessageBox.Show($"Điểm của sinh viên {maSV} không hợp lệ (0-10).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Xây dựng câu lệnh EXEC stored procedure
                    string diemGKValue = diemGK.HasValue ? diemGK.Value.ToString() : "NULL";
                    string diemCKValue = diemCK.HasValue ? diemCK.Value.ToString() : "NULL";

                    string query = $@"EXEC sp_CapNhatDiemHocPhan 
                                    @MaSV = '{maSV}', 
                                    @MaMH = '{maMH}', 
                                    @MaHocKyNamHoc = {maHocKyNamHoc}, 
                                    @DiemGK = {diemGKValue}, 
                                    @DiemCK = {diemCKValue}";

                    frmAdmin.executeQuery(query);
                }

                dt.AcceptChanges();
                MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadBangDiem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (isLoading) return;
            LoadBangDiem();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = true;
            editingRowHandle = gvDanhSachSV.FocusedRowHandle;
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            {
                int rowHandle = gvDanhSachSV.FocusedRowHandle;

                if (isAdding)
                {
                    if (gvDanhSachSV.IsNewItemRow(rowHandle) || rowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                    {
                        gvDanhSachSV.DeleteRow(rowHandle);
                    }
                    else
                    {
                        for (int i = gvDanhSachSV.RowCount - 1; i >= 0; i--)
                        {
                            DataRow row = gvDanhSachSV.GetDataRow(i);
                            if (row != null && row.RowState == DataRowState.Added)
                            {
                                gvDanhSachSV.DeleteRow(i);
                                break;
                            }
                        }
                    }

                    dt.RejectChanges();
                }
                else
                {
                    gvDanhSachSV.CancelUpdateCurrentRow();
                    dt.RejectChanges();
                }

                btnLuu.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnHuy.Enabled = false;
                gvDanhSachSV.OptionsBehavior.Editable = false;
                isAdding = false;
                editingRowHandle = -1;
            }
        }
    }
}
