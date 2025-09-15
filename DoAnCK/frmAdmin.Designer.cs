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
            this.btnDSGD = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnDSDK = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnKQHT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement7 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement8 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnCT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            this.btnQLMH = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(270, 39);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(705, 493);
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
            this.accordionControl1.Size = new System.Drawing.Size(270, 493);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnQLSV,
            this.btnQLGV,
            this.btnQLMH,
            this.btnDSGD,
            this.btnDSDK,
            this.btnKQHT});
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "DANH MỤC";
            // 
            // btnQLSV
            // 
            this.btnQLSV.Name = "btnQLSV";
            this.btnQLSV.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnQLSV.Text = "Quản lý sinh viên";
            this.btnQLSV.Click += new System.EventHandler(this.btnQLSV_Click);
            // 
            // btnQLGV
            // 
            this.btnQLGV.Name = "btnQLGV";
            this.btnQLGV.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnQLGV.Text = "Quản lý giảng viên";
            this.btnQLGV.Click += new System.EventHandler(this.btnQLGV_Click);
            // 
            // btnDSGD
            // 
            this.btnDSGD.Name = "btnDSGD";
            this.btnDSGD.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDSGD.Text = "Danh sách giảng dạy";
            this.btnDSGD.Click += new System.EventHandler(this.btnDSGD_Click);
            // 
            // btnDSDK
            // 
            this.btnDSDK.Name = "btnDSDK";
            this.btnDSDK.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDSDK.Text = "Danh sách sinh viên đăng ký";
            this.btnDSDK.Click += new System.EventHandler(this.btnDSDK_Click);
            // 
            // btnKQHT
            // 
            this.btnKQHT.Name = "btnKQHT";
            this.btnKQHT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnKQHT.Text = "Quản lý kết quả học tập";
            this.btnKQHT.Click += new System.EventHandler(this.btnKQHT_Click);
            // 
            // accordionControlElement7
            // 
            this.accordionControlElement7.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement8,
            this.btnCT});
            this.accordionControlElement7.Expanded = true;
            this.accordionControlElement7.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlElement7.Name = "accordionControlElement7";
            this.accordionControlElement7.Text = "HỆ THỐNG";
            // 
            // accordionControlElement8
            // 
            this.accordionControlElement8.Name = "accordionControlElement8";
            this.accordionControlElement8.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement8.Text = "Quản lý tài khoản";
            // 
            // btnCT
            // 
            this.btnCT.Name = "btnCT";
            this.btnCT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnCT.Text = "Cài đặt công thức tính";
            this.btnCT.Click += new System.EventHandler(this.btnCT_Click);
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(975, 39);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.Form = this;
            // 
            // btnQLMH
            // 
            this.btnQLMH.Name = "btnQLMH";
            this.btnQLMH.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnQLMH.Text = "Quản lý môn học";
            this.btnQLMH.Click += new System.EventHandler(this.btnQLMH_Click);
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 532);
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
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement8;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnKQHT;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnCT;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnQLMH;
    }
}