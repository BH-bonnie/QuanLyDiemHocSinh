namespace DoAnCK.UI_Dangnhap
{
    partial class uc_Chonquyen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnGV = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAdmin
            // 
            this.btnAdmin.BackColor = System.Drawing.Color.Turquoise;
            this.btnAdmin.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnAdmin.ForeColor = System.Drawing.Color.White;
            this.btnAdmin.Location = new System.Drawing.Point(36, 237);
            this.btnAdmin.Margin = new System.Windows.Forms.Padding(10);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(655, 71);
            this.btnAdmin.TabIndex = 7;
            this.btnAdmin.Text = "Admin";
            this.btnAdmin.UseVisualStyleBackColor = false;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // btnGV
            // 
            this.btnGV.BackColor = System.Drawing.Color.LimeGreen;
            this.btnGV.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGV.ForeColor = System.Drawing.Color.White;
            this.btnGV.Location = new System.Drawing.Point(36, 161);
            this.btnGV.Margin = new System.Windows.Forms.Padding(10);
            this.btnGV.Name = "btnGV";
            this.btnGV.Size = new System.Drawing.Size(655, 71);
            this.btnGV.TabIndex = 6;
            this.btnGV.Text = "Giảng viên";
            this.btnGV.UseVisualStyleBackColor = false;
            this.btnGV.Click += new System.EventHandler(this.btn_GV_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(272, 91);
            this.label5.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "Đăng nhập với tư cách là";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 17F);
            this.label6.Location = new System.Drawing.Point(202, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(310, 35);
            this.label6.TabIndex = 4;
            this.label6.Text = "Chào mừng bạn trở lại ";
            // 
            // uc_Chonquyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnGV);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Name = "uc_Chonquyen";
            this.Size = new System.Drawing.Size(723, 349);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnGV;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
