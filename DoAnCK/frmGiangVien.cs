using DevExpress.XtraBars;
using DoAnCK.UI_GV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoAnCK {
    public partial class frmGiangVien : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public static string ConnString { get; private set; }
        public static string MaGV { get; private set; } 
      public frmGiangVien(string maGV)
        {
            InitializeComponent();
            ConnString = FormMain.ConnString; 
            MaGV = maGV;
            this.mainContainer.AutoScroll = true;
        }

        uc_DSGD ucDSGD;
        uc_DSSV ucDSSV;
        uc_QLDiem ucQLD;
        uc_ThongTinGV ucTTGV;

        private void frmGiangVien_Load(object sender, EventArgs e)
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

        private void btnDSGD_Click(object sender, EventArgs e)
        {
            if (ucDSGD == null)
            {
                ucDSGD =  new uc_DSGD(FormMain.ConnString, MaGV);
                ucDSGD.Dock = DockStyle.Fill;
                ucDSGD.AutoSize = true;
                mainContainer.Controls.Add(ucDSGD);
                ucDSGD.BringToFront();
            }
            else
            {
                ucDSGD.BringToFront();
            }
        }

        private void btnDSSV_Click(object sender, EventArgs e)
        {
            if (ucDSSV == null)
            {
                ucDSSV = new uc_DSSV(FormMain.ConnString, MaGV);
                ucDSSV.Dock = DockStyle.Fill;
                ucDSSV.AutoSize = true;
                mainContainer.Controls.Add(ucDSSV);
                ucDSSV.BringToFront();
            }
            else
            {
                ucDSSV.BringToFront();
            }
        }

        private void btnQLD_Click(object sender, EventArgs e)
        {
            if (ucQLD == null)
            {
                ucQLD = new uc_QLDiem(FormMain.ConnString, MaGV);
                ucQLD.Dock = DockStyle.Fill;
                ucQLD.AutoSize = true;
                mainContainer.Controls.Add(ucQLD);
                ucQLD.BringToFront();
            }
            else
            {
                ucQLD.BringToFront();
            }
        }

        private void btnThongTin_Click(object sender, EventArgs e)
        {
            if (ucTTGV == null)
            {
                ucTTGV = new uc_ThongTinGV(FormMain.ConnString, MaGV);
                ucTTGV.Dock = DockStyle.Fill;
                ucTTGV.AutoSize = true;
                mainContainer.Controls.Add(ucTTGV);
                ucTTGV.BringToFront();
            }
            else
            {
                ucTTGV.BringToFront();
            }
            lblTieude.Caption = btnThongTin.Text;
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
