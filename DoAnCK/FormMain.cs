using DoAnCK.UI_Admin;
using DoAnCK.UI_Dangnhap;
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
    public interface IRefreshable
    {
        void RefreshData();
    }
    public partial class FormMain : Form
    {
        public static string ConnString { get; private set; }
        public static int CurrentRoleID { get; private set; }

        uc_Dangnhap ucDangnhap;
        uc_Chonquyen ucChonquyen;

        public FormMain()
        {
            InitializeComponent();
            ConnString = @"Server=.;Database=QL_SinhVien;Integrated Security=true;Encrypt=false;TrustServerCertificate=true;Connection Timeout=30;";
            CurrentRoleID = 0; 
        }

        // Phương thức để cập nhật ConnString với username và password sau khi đăng nhập thành công
        public static void UpdateConnString(string username, string password)
        {
            ConnString = $@"Server=.;Database=QL_SinhVien;User ID={username};Password={password};Encrypt=false;TrustServerCertificate=true;Connection Timeout=30;";
        }

        public static void SetCurrentRole(int roleID)
        {
            CurrentRoleID = roleID;
        }

        public static void ResetConnection()
        {
            ConnString = @"Server=.;Database=QL_SinhVien;Integrated Security=true;Encrypt=false;TrustServerCertificate=true;Connection Timeout=30;";
            CurrentRoleID = 0;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (ucChonquyen == null)
            {
                ucChonquyen = new uc_Chonquyen();
                ucChonquyen.Dock = DockStyle.Fill;
                ucChonquyen.AutoSize = true;
                ucChonquyen.OnChonQuyen += UcChonquyen_OnChonQuyen;

                panelMain.Controls.Add(ucChonquyen);
                ucChonquyen.BringToFront();
            }
            else
            {
                ucChonquyen.BringToFront();
            }
        }

        private void UcChonquyen_OnChonQuyen(object sender, int role)
        {
            SetCurrentRole(role);

            if (ucDangnhap != null)
            {
                panelMain.Controls.Remove(ucDangnhap);
                ucDangnhap.Dispose();
            }

            ucDangnhap = new uc_Dangnhap(role);
            ucDangnhap.Dock = DockStyle.Fill;
            ucDangnhap.AutoSize = true;
            ucDangnhap.OnExit += UcDangnhap_OnExit;
            panelMain.Controls.Add(ucDangnhap);
            ucDangnhap.BringToFront();
        }

        private void UcDangnhap_OnExit(object sender, EventArgs e)
        {
            ResetConnection();
            ucChonquyen.BringToFront();
        }
    }
}
