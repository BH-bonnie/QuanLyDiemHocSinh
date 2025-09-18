namespace DoAnCK.UI_Admin
{
    partial class uc_QLMH
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
            this.components = new System.ComponentModel.Container();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnSua = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcDanhSachSV = new DevExpress.XtraGrid.GridControl();
            this.gvDanhSachSV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaMH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenMH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoTinChi = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSachSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSachSV)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnLuu,
            this.btnSua,
            this.barButtonItem1});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 8;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSua, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLuu),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnSua
            // 
            this.btnSua.Caption = "Sửa";
            this.btnSua.Id = 6;
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(50, 30);
            this.btnSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSua_ItemClick);
            // 
            // btnLuu
            // 
            this.btnLuu.Caption = "Lưu";
            this.btnLuu.Id = 4;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(50, 30);
            this.btnLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLuu_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Xuất";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.Size = new System.Drawing.Size(50, 30);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(856, 37);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 516);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(856, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 37);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 479);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(856, 37);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 479);
            // 
            // gcDanhSachSV
            // 
            this.gcDanhSachSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDanhSachSV.Location = new System.Drawing.Point(0, 37);
            this.gcDanhSachSV.MainView = this.gvDanhSachSV;
            this.gcDanhSachSV.MenuManager = this.barManager1;
            this.gcDanhSachSV.Name = "gcDanhSachSV";
            this.gcDanhSachSV.Size = new System.Drawing.Size(856, 479);
            this.gcDanhSachSV.TabIndex = 10;
            this.gcDanhSachSV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDanhSachSV});
            // 
            // gvDanhSachSV
            // 
            this.gvDanhSachSV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaMH,
            this.TenMH,
            this.SoTinChi});
            this.gvDanhSachSV.GridControl = this.gcDanhSachSV;
            this.gvDanhSachSV.Name = "gvDanhSachSV";
            this.gvDanhSachSV.OptionsView.ShowGroupPanel = false;
            // 
            // MaMH
            // 
            this.MaMH.Caption = "Mã môn học ";
            this.MaMH.FieldName = "MaMH";
            this.MaMH.MinWidth = 25;
            this.MaMH.Name = "MaMH";
            this.MaMH.OptionsColumn.AllowEdit = false;
            this.MaMH.Visible = true;
            this.MaMH.VisibleIndex = 0;
            this.MaMH.Width = 200;
            // 
            // TenMH
            // 
            this.TenMH.Caption = "Tên môn học ";
            this.TenMH.FieldName = "TenMH";
            this.TenMH.MinWidth = 25;
            this.TenMH.Name = "TenMH";
            this.TenMH.OptionsColumn.AllowEdit = false;
            this.TenMH.Visible = true;
            this.TenMH.VisibleIndex = 1;
            this.TenMH.Width = 432;
            // 
            // SoTinChi
            // 
            this.SoTinChi.Caption = "Số tín chỉ ";
            this.SoTinChi.FieldName = "SoTinChi";
            this.SoTinChi.MinWidth = 25;
            this.SoTinChi.Name = "SoTinChi";
            this.SoTinChi.Visible = true;
            this.SoTinChi.VisibleIndex = 2;
            this.SoTinChi.Width = 128;
            // 
            // uc_QLMH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcDanhSachSV);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "uc_QLMH";
            this.Size = new System.Drawing.Size(856, 516);
            this.Load += new System.EventHandler(this.uc_QLMH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSachSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSachSV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcDanhSachSV;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDanhSachSV;
        private DevExpress.XtraGrid.Columns.GridColumn MaMH;
        private DevExpress.XtraGrid.Columns.GridColumn TenMH;
        private DevExpress.XtraGrid.Columns.GridColumn SoTinChi;
        private DevExpress.XtraBars.BarButtonItem btnSua;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}
