namespace DoAnCK.UI_Admin
{
    partial class uc_CT
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
            this.btnLuu = new System.Windows.Forms.Button();
            this.txtTiLeCK = new System.Windows.Forms.TextBox();
            this.txtTiLeGK = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(230, 136);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 10;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txtTiLeCK
            // 
            this.txtTiLeCK.Location = new System.Drawing.Point(205, 91);
            this.txtTiLeCK.Name = "txtTiLeCK";
            this.txtTiLeCK.ReadOnly = true;
            this.txtTiLeCK.Size = new System.Drawing.Size(100, 22);
            this.txtTiLeCK.TabIndex = 9;
            // 
            // txtTiLeGK
            // 
            this.txtTiLeGK.Location = new System.Drawing.Point(205, 53);
            this.txtTiLeGK.Name = "txtTiLeGK";
            this.txtTiLeGK.Size = new System.Drawing.Size(100, 22);
            this.txtTiLeGK.TabIndex = 8;
            this.txtTiLeGK.TextChanged += new System.EventHandler(this.txtTiLeGK_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Trọng số cuối kì";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Trọng số điểm giữa kỳ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "THIẾT LẬP";
            // 
            // uc_CT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtTiLeCK);
            this.Controls.Add(this.txtTiLeGK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "uc_CT";
            this.Size = new System.Drawing.Size(392, 205);
            this.Load += new System.EventHandler(this.uc_CT_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.TextBox txtTiLeCK;
        private System.Windows.Forms.TextBox txtTiLeGK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}
