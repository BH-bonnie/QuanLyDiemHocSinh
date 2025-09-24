using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DoAnCK.UI_Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoAnCK
{
 
    public partial class frmAdmin : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public static string ConnString { get; private set; }

        public frmAdmin()
        {

            InitializeComponent();
            ConnString = FormMain.ConnString;
            this.mainContainer.AutoScroll = true;


        }

        uc_QLSV ucQLSV;
        uc_QLGV ucQLGV;
        uc_QLMH ucQLMH;
        uc_DSGD ucDSGD;
        uc_DSDK ucDSDK;
        uc_KQHT ucKQHT;
        uc_CT ucCT;
        uc_Chitiet ucChitiet;
        uc_TaiKhoan ucTK;
        private void frmAdmin_Load(object sender, EventArgs e)
        {

        }
       
        public static DataTable getData(string query)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                using (var adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public static void executeQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void ShowControl(UserControl uc, AccordionControlElement btn)
        {
            if (!mainContainer.Controls.Contains(uc))
            {
                uc.Dock = DockStyle.Fill;
                uc.AutoSize = true;
                mainContainer.Controls.Add(uc);
            }

            uc.BringToFront();
            lblTieude.Caption = btn.Text;

            if (uc is IRefreshable refreshable)
            {
                refreshable.RefreshData();
            }
        }

        private void btnQLSV_Click(object sender, EventArgs e)
        {
            if (ucQLSV == null)
            {
                ucQLSV = new uc_QLSV();
            }
            ShowControl(ucQLSV, btnQLSV);




        }

        private void btnQLGV_Click(object sender, EventArgs e)
        {
            if (ucQLGV == null)
            {
                ucQLGV = new uc_QLGV();
            }
            ShowControl(ucQLGV, btnQLGV);

        }

        private void btnDSGD_Click(object sender, EventArgs e)
        {
            if (ucDSGD == null)
            {
                ucDSGD = new uc_DSGD();

            }
            ShowControl(ucDSGD, btnDSGD);


        }

        private void btnDSDK_Click(object sender, EventArgs e)
        {
            if (ucDSDK == null)
            {
                ucDSDK = new uc_DSDK();
            }
            ShowControl(ucDSDK, btnDSDK);

        }

        private void btnKQHT_Click(object sender, EventArgs e)
        {
            if(ucKQHT == null)
            {
                ucKQHT = new uc_KQHT();
            }
            ShowControl(ucKQHT, btnKQHT);

       


        }

        private void btnCT_Click(object sender, EventArgs e)
        {
            if (ucCT == null)
            {
                ucCT = new uc_CT();
            }
            ShowControl(ucCT, btnCT);


        }

        private void btnQLMH_Click(object sender, EventArgs e)
        {
            if (ucQLMH == null)
            {
                ucQLMH = new uc_QLMH();
            }
            ShowControl(ucQLMH, btnQLMH);

        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (ucChitiet == null)
            {
                ucChitiet = new uc_Chitiet();
            }
            ShowControl(ucChitiet, btnChiTiet);

        }
        private void btnTK_Click(object sender, EventArgs e)
        {
            if (ucTK == null)
            {
                ucTK = new uc_TaiKhoan();
            }
            ShowControl(ucTK, btnTK);


        }
        private void btnLichsu_Click(object sender, EventArgs e)
        {

        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.Close();

                FormMain formMain = new FormMain();
                formMain.Show();
            }
        }

       
    }
}


