using DevExpress.XtraBars;
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

namespace DoAnCK {
    public partial class frmAdmin : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm {
        public static string ConnString { get; private set; }

        public frmAdmin() {

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


        private void btnQLSV_Click(object sender, EventArgs e)
        {
            if (ucQLSV == null)
            {
                ucQLSV = new uc_QLSV();
                ucQLSV.Dock = DockStyle.Fill;
                ucQLSV.AutoSize = true; // tự động mở rộng theo nội dung

                mainContainer.Controls.Add(ucQLSV);
                ucQLSV.BringToFront();
            }
            else
            {
                ucQLSV.BringToFront();
            }

        }

        private void btnQLGV_Click(object sender, EventArgs e)
        {
            if (ucQLGV == null)
            {
                ucQLGV = new uc_QLGV();
                ucQLGV.Dock = DockStyle.Fill;
                ucQLGV.AutoSize = true; // tự động mở rộng theo nội dung
                mainContainer.Controls.Add(ucQLGV);
                ucQLGV.BringToFront();
            }
            else
            {
                ucQLGV.BringToFront();
            }

        }

        private void btnDSGD_Click(object sender, EventArgs e)
        {
            if (ucDSGD == null)
            {
                ucDSGD = new uc_DSGD();
                ucDSGD.Dock = DockStyle.Fill;
                ucDSGD.AutoSize = true; // tự động mở rộng theo nội dung
                mainContainer.Controls.Add(ucDSGD);
                ucDSGD.BringToFront();
            }
            else
            {
                ucDSGD.BringToFront();
            }
        }

        private void btnDSDK_Click(object sender, EventArgs e)
        {
            if (ucDSDK == null)
            {
                ucDSDK = new uc_DSDK();
                ucDSDK.Dock = DockStyle.Fill;
                ucDSDK.AutoSize = true; // tự động mở rộng theo nội dung
                mainContainer.Controls.Add(ucDSDK);
                ucDSDK.BringToFront();
            }
            else
            {
                ucDSDK.BringToFront();
            }
        }

        private void btnKQHT_Click(object sender, EventArgs e)
        {
            if (ucKQHT == null)
            {
                ucKQHT = new uc_KQHT();
                ucKQHT.Dock = DockStyle.Fill;
                ucKQHT.AutoSize = true; // tự động mở rộng theo nội dung
                mainContainer.Controls.Add(ucKQHT);
                ucKQHT.BringToFront();
            }
            else
            {
                ucKQHT.BringToFront();
            }

        }

        private void btnCT_Click(object sender, EventArgs e)
        {

            if (ucCT == null)
            {
                ucCT = new uc_CT();
                ucCT.Dock = DockStyle.Fill;
                ucCT.AutoSize = true; // tự động mở rộng theo nội dung
                mainContainer.Controls.Add(ucCT);
                ucCT.BringToFront();
            }
            else
            {
                ucCT.BringToFront();
            }
        }

        private void btnQLMH_Click(object sender, EventArgs e)
        {
            if (ucQLMH == null)
            {
                ucQLMH = new uc_QLMH();
                ucQLMH.Dock = DockStyle.Fill;
                ucQLMH.AutoSize = true; // tự động mở rộng theo nội dung
                mainContainer.Controls.Add(ucQLMH);
                ucQLMH.BringToFront();
            }
            else
            {
                ucQLMH.BringToFront();
            }

        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (ucChitiet == null)
            {
                ucChitiet = new uc_Chitiet();
                ucChitiet.Dock = DockStyle.Fill;
                ucChitiet.AutoSize = true; // tự động mở rộng theo nội dung
                mainContainer.Controls.Add(ucChitiet);
                ucChitiet.BringToFront();
            }
            else
            {
                ucChitiet.BringToFront();
            }
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

        private void btnTK_Click(object sender, EventArgs e)
        {
            if(ucTK == null)
            {
                ucTK = new uc_TaiKhoan();
                ucTK.Dock = DockStyle.Fill;
                ucTK.AutoSize = true; // tự động mở rộng theo nội dung
                mainContainer.Controls.Add(ucTK);
                ucTK.BringToFront();
            }
            else
            {
                ucTK.BringToFront();
            }
        }
    }
}
