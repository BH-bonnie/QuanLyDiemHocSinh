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

namespace DoAnCK.UI_Admin
{
    public partial class uc_DSGD: UserControl, IRefreshable
    {
        private string connStr;
        private int maHocKyNamHoc;
        private bool isAdding = false;
        private int editingRowHandle = -1;
        private DataTable dt;
        private bool isLoading = false;
        public uc_DSGD()
        {
            InitializeComponent();
            string connStr = frmAdmin.ConnString;

        }

        public void RefreshData()
        {

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            gvDanhSachSV.OptionsBehavior.Editable = false;

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

                int maHocKyNamHoc = Convert.ToInt32(cbbNamHoc.SelectedValue);

                string queryLopHocPhan = $"SELECT * FROM dbo.fn_LopHocPhanTheoNamHoc({maHocKyNamHoc})";
                DataTable dt = frmAdmin.getData(queryLopHocPhan);

               
                gcDanhSachSV.DataSource = dt;
               

            }
            gvDanhSachSV.RefreshData();


        }



        private void cbbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbNamHoc.SelectedItem == null)
                return;

            DataRowView drv = cbbNamHoc.SelectedItem as DataRowView;
            if (drv == null)
                return;

            int maHocKyNamHoc = Convert.ToInt32(drv["MaHocKyNamHoc"]);

            string query = $"SELECT * FROM dbo.fn_LopHocPhanTheoNamHoc({maHocKyNamHoc})";
            DataTable dt = frmAdmin.getData(query);

            gcDanhSachSV.DataSource = dt;

        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnSua.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = true;
            editingRowHandle = gvDanhSachSV.FocusedRowHandle;
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            btnHuy.Enabled = false;
            gvDanhSachSV.OptionsBehavior.Editable = false;
            isAdding = false;
            editingRowHandle = -1;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
