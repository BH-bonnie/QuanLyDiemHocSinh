using DoAnCK.UI_Admin;
using DoAnCK.UI_Dangnhap;
using DoAnCK.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCK
{
    public partial class FormMain : Form
    {
        public static string ConnString { get; private set; }

        uc_Dangnhap ucDangnhap;
        uc_Chonquyen ucChonquyen;

        public FormMain()
        {
            InitializeComponent();
            ConnString = @"Data Source=.;Initial Catalog=QL_SinhVien;Integrated Security=True";

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
          

            if (ucChonquyen == null)
            {
                ucChonquyen = new uc_Chonquyen();
                ucChonquyen.Dock = DockStyle.Fill;
                ucChonquyen.AutoSize = true;
                ucChonquyen.OnChonQuyen += UcChonquyen_OnChonQuyen; // Bắt sự kiện

                panelMain.Controls.Add(ucChonquyen);
                ucChonquyen.BringToFront();
            }
            else
            {
                ucChonquyen.BringToFront();
            }
        }

        private void UcChonquyen_OnChonQuyen(object sender, string role)
        {
            // Xóa cũ nếu có
            if (ucDangnhap != null)
            {
                panelMain.Controls.Remove(ucDangnhap);
                ucDangnhap.Dispose();
            }

            // Luôn tạo mới với role mới
            ucDangnhap = new uc_Dangnhap(role);
            ucDangnhap.Dock = DockStyle.Fill;
            ucDangnhap.AutoSize = true;
            ucDangnhap.OnExit += UcDangnhap_OnExit;

            panelMain.Controls.Add(ucDangnhap);
            ucDangnhap.BringToFront();
        }

        private void UcDangnhap_OnExit(object sender, EventArgs e)
        {
            ucChonquyen.BringToFront();
        }
    }
}
