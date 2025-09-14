using DevExpress.XtraBars;
using DoAnCK.UI;
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
    public partial class frmMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm {
        public static string ConnString { get; private set; }

       


        public frmMain()
        {
            InitializeComponent();
            ConnString = @"Data Source=.;Initial Catalog=QL_SinhVien;Integrated Security=True";
            this.mainContainer.AutoScroll = true;

      
        }
        uc_ThongTin ucThongTin;
        uc_XemDiem ucXemDiem;



        private void btn_ThongTin_Click(object sender, EventArgs e)
        {
            if (ucThongTin == null)
            {
                ucThongTin = new uc_ThongTin();
                ucThongTin.Dock = DockStyle.Top;
                ucThongTin.AutoSize = true; // tự động mở rộng theo nội dung

                mainContainer.Controls.Add(ucThongTin);
                ucThongTin.BringToFront();
            }
            else
            {
                ucThongTin.BringToFront();
            }

        }

        private void btn_XemDiem_Click(object sender, EventArgs e)
        {
            if (ucXemDiem == null)
            {
                ucXemDiem = new uc_XemDiem();
                ucXemDiem.Dock = DockStyle.Top;
                ucXemDiem.AutoSize = true; // tự động mở rộng theo nội dung

                mainContainer.Controls.Add(ucXemDiem);
                ucXemDiem.BringToFront();
            }
            else
            {
                ucXemDiem.BringToFront();
            }
        }

        private void btn_KQDKHP_Click(object sender, EventArgs e)
        {
            if (ucXemDiem == null)
            {
                ucXemDiem = new uc_XemDiem();
                ucXemDiem.Dock = DockStyle.Top;
                ucXemDiem.AutoSize = true; // tự động mở rộng theo nội dung
                mainContainer.Controls.Add(ucXemDiem);
                ucXemDiem.BringToFront();
            }
            else
            {
                ucXemDiem.BringToFront();
            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
