namespace DoAnCK
{
    partial class frmGiangVien
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainContainer = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement7 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnThongTin = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnDSGD = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnDSSV = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnQLD = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnDangXuat = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.lblTieude = new DevExpress.XtraBars.BarHeaderItem();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(294, 53);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(6);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(781, 502);
            this.mainContainer.TabIndex = 0;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement7,
            this.accordionControlElement1,
            this.accordionControlElement4});
            this.accordionControl1.Location = new System.Drawing.Point(0, 53);
            this.accordionControl1.Margin = new System.Windows.Forms.Padding(6);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(294, 502);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement7
            // 
            this.accordionControlElement7.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.accordionControlElement7.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement7.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnThongTin});
            this.accordionControlElement7.Expanded = true;
            this.accordionControlElement7.Name = "accordionControlElement7";
            this.accordionControlElement7.Text = "TRANG CÁ NHÂN";
            // 
            // btnThongTin
            // 
            this.btnThongTin.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnThongTin.Appearance.Default.Options.UseFont = true;
            this.btnThongTin.Name = "btnThongTin";
            this.btnThongTin.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnThongTin.Text = "Thông tin cá nhân";
            this.btnThongTin.Click += new System.EventHandler(this.btnThongTin_Click);
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.accordionControlElement1.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnDSGD,
            this.btnDSSV,
            this.btnQLD});
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "DANH MỤC";
            // 
            // btnDSGD
            // 
            this.btnDSGD.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnDSGD.Appearance.Default.Options.UseFont = true;
            this.btnDSGD.Name = "btnDSGD";
            this.btnDSGD.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDSGD.Text = "Danh sách giảng dạy";
            this.btnDSGD.Click += new System.EventHandler(this.btnDSGD_Click);
            // 
            // btnDSSV
            // 
            this.btnDSSV.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnDSSV.Appearance.Default.Options.UseFont = true;
            this.btnDSSV.Name = "btnDSSV";
            this.btnDSSV.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDSSV.Text = "Danh sách sinh viên";
            this.btnDSSV.Click += new System.EventHandler(this.btnDSSV_Click);
            // 
            // btnQLD
            // 
            this.btnQLD.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnQLD.Appearance.Default.Options.UseFont = true;
            this.btnQLD.Name = "btnQLD";
            this.btnQLD.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnQLD.Text = "Quản lý điểm";
            this.btnQLD.Click += new System.EventHandler(this.btnQLD_Click);
            // 
            // accordionControlElement4
            // 
            this.accordionControlElement4.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.accordionControlElement4.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement4.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnDangXuat});
            this.accordionControlElement4.Expanded = true;
            this.accordionControlElement4.Name = "accordionControlElement4";
            this.accordionControlElement4.Text = "HỆ THỐNG";
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnDangXuat.Appearance.Default.Options.UseFont = true;
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.lblTieude});
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1075, 53);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            this.fluentDesignFormControl1.TitleItemLinks.Add(this.lblTieude);
            // 
            // lblTieude
            // 
            this.lblTieude.Appearance.FontSizeDelta = 12;
            this.lblTieude.Appearance.Options.UseFont = true;
            this.lblTieude.Caption = "Tieude";
            this.lblTieude.Id = 0;
            this.lblTieude.Name = "lblTieude";
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.Form = this;
            this.fluentFormDefaultManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.lblTieude});
            this.fluentFormDefaultManager1.MaxItemId = 1;
            // 
            // frmGiangVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 555);
            this.ControlContainer = this.mainContainer;
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Name = "frmGiangVien";
            this.NavigationControl = this.accordionControl1;
            this.Text = "Phần mềm quản lý điểm";
            this.Load += new System.EventHandler(this.frmGiangVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer mainContainer;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnDSGD;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnDSSV;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnQLD;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnDangXuat;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement7;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnThongTin;
        private DevExpress.XtraBars.BarHeaderItem lblTieude;
    }
}