namespace DoAnCK
{
    partial class frmAdmin
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
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnQLSV = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnQLGV = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnDSDK = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnDSGD = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnKQHT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnChiTiet = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement7 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnTK = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnQLMH = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnCT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            this.btnDangXuat = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnLichsu = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(294, 39);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(781, 516);
            this.mainContainer.TabIndex = 0;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1,
            this.accordionControlElement7});
            this.accordionControl1.Location = new System.Drawing.Point(0, 39);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(294, 516);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.accordionControlElement1.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnQLSV,
            this.btnQLGV,
            this.btnDSDK,
            this.btnDSGD,
            this.btnKQHT,
            this.btnChiTiet});
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "DANH MỤC";
            // 
            // btnQLSV
            // 
            this.btnQLSV.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnQLSV.Appearance.Default.Options.UseFont = true;
            this.btnQLSV.Name = "btnQLSV";
            this.btnQLSV.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnQLSV.Text = "Quản lý sinh viên";
            this.btnQLSV.Click += new System.EventHandler(this.btnQLSV_Click);
            // 
            // btnQLGV
            // 
            this.btnQLGV.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnQLGV.Appearance.Default.Options.UseFont = true;
            this.btnQLGV.Name = "btnQLGV";
            this.btnQLGV.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnQLGV.Text = "Quản lý giảng viên";
            this.btnQLGV.Click += new System.EventHandler(this.btnQLGV_Click);
            // 
            // btnDSDK
            // 
            this.btnDSDK.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnDSDK.Appearance.Default.Options.UseFont = true;
            this.btnDSDK.Name = "btnDSDK";
            this.btnDSDK.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDSDK.Text = "Danh sách sinh viên đăng ký";
            this.btnDSDK.Click += new System.EventHandler(this.btnDSDK_Click);
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
            // btnKQHT
            // 
            this.btnKQHT.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnKQHT.Appearance.Default.Options.UseFont = true;
            this.btnKQHT.Name = "btnKQHT";
            this.btnKQHT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnKQHT.Text = "Quản lý kết quả học tập";
            this.btnKQHT.Click += new System.EventHandler(this.btnKQHT_Click);
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnChiTiet.Appearance.Default.Options.UseFont = true;
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnChiTiet.Text = "Điểm chi tiết sinh viên";
            this.btnChiTiet.Click += new System.EventHandler(this.btnChiTiet_Click);
            // 
            // accordionControlElement7
            // 
            this.accordionControlElement7.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.accordionControlElement7.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement7.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnQLMH,
            this.btnCT,
            this.btnTK,
            this.btnLichsu,
            this.btnDangXuat});
            this.accordionControlElement7.Expanded = true;
            this.accordionControlElement7.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlElement7.Name = "accordionControlElement7";
            this.accordionControlElement7.Text = "HỆ THỐNG";
            // 
            // btnTK
            // 
            this.btnTK.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnTK.Appearance.Default.Options.UseFont = true;
            this.btnTK.Name = "btnTK";
            this.btnTK.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnTK.Text = "Quản lý tài khoản";
            // 
            // btnQLMH
            // 
            this.btnQLMH.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnQLMH.Appearance.Default.Options.UseFont = true;
            this.btnQLMH.Name = "btnQLMH";
            this.btnQLMH.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnQLMH.Text = "Thiết lập số tín chỉ ";
            this.btnQLMH.Click += new System.EventHandler(this.btnQLMH_Click);
            // 
            // btnCT
            // 
            this.btnCT.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCT.Appearance.Default.Options.UseFont = true;
            this.btnCT.Name = "btnCT";
            this.btnCT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnCT.Text = "Thiết lập trọng số";
            this.btnCT.Click += new System.EventHandler(this.btnCT_Click);
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1075, 39);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.Form = this;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnDangXuat.Appearance.Default.Options.UseFont = true;
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDangXuat.Text = "Đăng xuất";
            // 
            // btnLichsu
            // 
            this.btnLichsu.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnLichsu.Appearance.Default.Options.UseFont = true;
            this.btnLichsu.Name = "btnLichsu";
            this.btnLichsu.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnLichsu.Text = "Lịch sử đăng nhập";
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 555);
            this.ControlContainer = this.mainContainer;
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Name = "frmAdmin";
            this.NavigationControl = this.accordionControl1;
            this.Text = "frmAdmin";
            this.Load += new System.EventHandler(this.frmAdmin_Load);
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
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnQLSV;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnQLGV;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnDSGD;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnDSDK;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement7;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnTK;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnKQHT;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnCT;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnQLMH;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnChiTiet;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnDangXuat;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnLichsu;
    }
}