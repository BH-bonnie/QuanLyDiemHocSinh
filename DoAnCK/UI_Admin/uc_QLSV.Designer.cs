namespace DoAnCK.UI_Admin
{
    partial class uc_QLSV
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
            this.btnThem = new DevExpress.XtraBars.BarButtonItem();
            this.btnSua = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.btnHuy = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcDanhSachSV = new DevExpress.XtraGrid.GridControl();
            this.gvDanhSachSV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaSV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgaySinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NoiSinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GioiTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckGioiTinh = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.CMND_CCCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LopSV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLop = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.TrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bar1 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSachSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSachSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckGioiTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLop)).BeginInit();
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
            this.btnThem,
            this.btnXoa,
            this.btnLuu,
            this.btnSua,
            this.btnHuy});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 7;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnThem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSua, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnXoa, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLuu, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnHuy, true)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnThem
            // 
            this.btnThem.Caption = "Thêm";
            this.btnThem.Id = 0;
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(50, 30);
            this.btnThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThem_ItemClick);
            // 
            // btnSua
            // 
            this.btnSua.Caption = "Sửa";
            this.btnSua.Id = 5;
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(50, 30);
            this.btnSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSua_ItemClick);
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = "Xoá ";
            this.btnXoa.Id = 2;
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(50, 30);
            this.btnXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXoa_ItemClick);
            // 
            // btnLuu
            // 
            this.btnLuu.Caption = "Lưu";
            this.btnLuu.Id = 4;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(50, 30);
            this.btnLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLuu_ItemClick);
            // 
            // btnHuy
            // 
            this.btnHuy.Caption = "Huỷ";
            this.btnHuy.Id = 6;
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(50, 30);
            this.btnHuy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHuy_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlTop.Size = new System.Drawing.Size(706, 30);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 459);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlBottom.Size = new System.Drawing.Size(706, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 429);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(706, 30);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 429);
            // 
            // gcDanhSachSV
            // 
            this.gcDanhSachSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDanhSachSV.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gcDanhSachSV.Location = new System.Drawing.Point(0, 30);
            this.gcDanhSachSV.MainView = this.gvDanhSachSV;
            this.gcDanhSachSV.Margin = new System.Windows.Forms.Padding(2);
            this.gcDanhSachSV.MenuManager = this.barManager1;
            this.gcDanhSachSV.Name = "gcDanhSachSV";
            this.gcDanhSachSV.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ckGioiTinh,
            this.lkLop});
            this.gcDanhSachSV.Size = new System.Drawing.Size(706, 429);
            this.gcDanhSachSV.TabIndex = 9;
            this.gcDanhSachSV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDanhSachSV});
            // 
            // gvDanhSachSV
            // 
            this.gvDanhSachSV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaSV,
            this.HoTen,
            this.NgaySinh,
            this.NoiSinh,
            this.GioiTinh,
            this.CMND_CCCD,
            this.LopSV,
            this.TrangThai});
            this.gvDanhSachSV.DetailHeight = 284;
            this.gvDanhSachSV.GridControl = this.gcDanhSachSV;
            this.gvDanhSachSV.Name = "gvDanhSachSV";
            this.gvDanhSachSV.OptionsView.ShowGroupPanel = false;
            this.gvDanhSachSV.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvDanhSachSV_ShowingEditor);
            // 
            // MaSV
            // 
            this.MaSV.Caption = "MSSV";
            this.MaSV.FieldName = "MaSV";
            this.MaSV.MinWidth = 19;
            this.MaSV.Name = "MaSV";
            this.MaSV.Visible = true;
            this.MaSV.VisibleIndex = 0;
            this.MaSV.Width = 70;
            // 
            // HoTen
            // 
            this.HoTen.Caption = "Họ tên";
            this.HoTen.FieldName = "HoTen";
            this.HoTen.MinWidth = 19;
            this.HoTen.Name = "HoTen";
            this.HoTen.Visible = true;
            this.HoTen.VisibleIndex = 1;
            this.HoTen.Width = 249;
            // 
            // NgaySinh
            // 
            this.NgaySinh.Caption = "Ngày sinh";
            this.NgaySinh.FieldName = "NgaySinh";
            this.NgaySinh.MinWidth = 19;
            this.NgaySinh.Name = "NgaySinh";
            this.NgaySinh.Visible = true;
            this.NgaySinh.VisibleIndex = 2;
            this.NgaySinh.Width = 63;
            // 
            // NoiSinh
            // 
            this.NoiSinh.Caption = "Nơi Sinh";
            this.NoiSinh.FieldName = "NoiSinh";
            this.NoiSinh.MinWidth = 19;
            this.NoiSinh.Name = "NoiSinh";
            this.NoiSinh.Visible = true;
            this.NoiSinh.VisibleIndex = 3;
            this.NoiSinh.Width = 68;
            // 
            // GioiTinh
            // 
            this.GioiTinh.Caption = "Nữ";
            this.GioiTinh.ColumnEdit = this.ckGioiTinh;
            this.GioiTinh.FieldName = "GioiTinh";
            this.GioiTinh.MinWidth = 19;
            this.GioiTinh.Name = "GioiTinh";
            this.GioiTinh.Visible = true;
            this.GioiTinh.VisibleIndex = 4;
            this.GioiTinh.Width = 49;
            // 
            // ckGioiTinh
            // 
            this.ckGioiTinh.AutoHeight = false;
            this.ckGioiTinh.Name = "ckGioiTinh";
            // 
            // CMND_CCCD
            // 
            this.CMND_CCCD.Caption = "CMND/CCCD";
            this.CMND_CCCD.FieldName = "CMND_CCCD";
            this.CMND_CCCD.MinWidth = 19;
            this.CMND_CCCD.Name = "CMND_CCCD";
            this.CMND_CCCD.Visible = true;
            this.CMND_CCCD.VisibleIndex = 5;
            this.CMND_CCCD.Width = 112;
            // 
            // LopSV
            // 
            this.LopSV.Caption = "Lớp ";
            this.LopSV.ColumnEdit = this.lkLop;
            this.LopSV.FieldName = "LopSV";
            this.LopSV.MinWidth = 19;
            this.LopSV.Name = "LopSV";
            this.LopSV.Visible = true;
            this.LopSV.VisibleIndex = 6;
            this.LopSV.Width = 71;
            // 
            // lkLop
            // 
            this.lkLop.AutoHeight = false;
            this.lkLop.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLop.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKhoa", "Khoa", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LopSV", "Lớp", 15, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lkLop.Name = "lkLop";
            // 
            // TrangThai
            // 
            this.TrangThai.Caption = "Ngừng hoạt động";
            this.TrangThai.FieldName = "TrangThai";
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.Visible = true;
            this.TrangThai.VisibleIndex = 7;
            this.TrangThai.Width = 56;
            // 
            // bar1
            // 
            this.bar1.BarName = "Main menu";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnThem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnXoa, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLuu, true)});
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Main menu";
            // 
            // uc_QLSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcDanhSachSV);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "uc_QLSV";
            this.Size = new System.Drawing.Size(706, 459);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSachSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSachSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckGioiTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnThem;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnXoa;
        private DevExpress.XtraGrid.GridControl gcDanhSachSV;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDanhSachSV;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraGrid.Columns.GridColumn MaSV;
        private DevExpress.XtraGrid.Columns.GridColumn HoTen;
        private DevExpress.XtraGrid.Columns.GridColumn NgaySinh;
        private DevExpress.XtraGrid.Columns.GridColumn NoiSinh;
        private DevExpress.XtraGrid.Columns.GridColumn GioiTinh;
        private DevExpress.XtraGrid.Columns.GridColumn CMND_CCCD;
        private DevExpress.XtraGrid.Columns.GridColumn LopSV;
        private DevExpress.XtraBars.BarButtonItem btnSua;
        private DevExpress.XtraBars.BarButtonItem btnHuy;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckGioiTinh;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLop;
        private DevExpress.XtraGrid.Columns.GridColumn TrangThai;
    }
}
