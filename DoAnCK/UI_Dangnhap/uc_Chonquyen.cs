using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCK.UI_Dangnhap
{
    public partial class uc_Chonquyen : UserControl
    {
        public event EventHandler<string> OnChonQuyen; 

        public uc_Chonquyen()
        {
            InitializeComponent();
            
        }

        private void btn_GV_Click(object sender, EventArgs e)
        {
            OnChonQuyen?.Invoke(this, "GV");
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            OnChonQuyen?.Invoke(this, "Admin");
        }
    }
}
