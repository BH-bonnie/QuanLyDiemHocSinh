namespace DoAnCK.UI_Admin
{
    partial class uc_DSGD
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
            this.gcDanhSachSV = new DevExpress.XtraGrid.GridControl();
            this.gvDanhSachSV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaMH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenMH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaLHP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaGV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HoTenGV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Email = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.cbbNamHoc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hScrollBar1 = new DevExpress.XtraEditors.HScrollBar();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnSua = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.btnHuy = new DevExpress.XtraBars.BarButtonItem();
            this.btnThongKe = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnThem = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSachSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSachSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDanhSachSV
            // 
            this.gcDanhSachSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDanhSachSV.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcDanhSachSV.Location = new System.Drawing.Point(0, 0);
            this.gcDanhSachSV.MainView = this.gvDanhSachSV;
            this.gcDanhSachSV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcDanhSachSV.Name = "gcDanhSachSV";
            this.gcDanhSachSV.Size = new System.Drawing.Size(856, 399);
            this.gcDanhSachSV.TabIndex = 10;
            this.gcDanhSachSV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDanhSachSV});
            // 
            // gvDanhSachSV
            // 
            this.gvDanhSachSV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaMH,
            this.TenMH,
            this.MaLHP,
            this.MaGV,
            this.HoTenGV,
            this.Email});
            this.gvDanhSachSV.GridControl = this.gcDanhSachSV;
            this.gvDanhSachSV.Name = "gvDanhSachSV";
            this.gvDanhSachSV.OptionsView.AllowCellMerge = true;
            this.gvDanhSachSV.OptionsView.ShowGroupPanel = false;
            // 
            // MaMH
            // 
            this.MaMH.Caption = "MaMH";
            this.MaMH.FieldName = "MaMH";
            this.MaMH.MinWidth = 25;
            this.MaMH.Name = "MaMH";
            this.MaMH.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.MaMH.Visible = true;
            this.MaMH.VisibleIndex = 0;
            this.MaMH.Width = 93;
            // 
            // TenMH
            // 
            this.TenMH.Caption = "Tên môn học";
            this.TenMH.FieldName = "TenMH";
            this.TenMH.MinWidth = 25;
            this.TenMH.Name = "TenMH";
            this.TenMH.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.TenMH.Visible = true;
            this.TenMH.VisibleIndex = 1;
            this.TenMH.Width = 93;
            // 
            // MaLHP
            // 
            this.MaLHP.Caption = "MaLHP";
            this.MaLHP.FieldName = "MaLHP";
            this.MaLHP.MinWidth = 25;
            this.MaLHP.Name = "MaLHP";
            this.MaLHP.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.MaLHP.Visible = true;
            this.MaLHP.VisibleIndex = 2;
            this.MaLHP.Width = 93;
            // 
            // MaGV
            // 
            this.MaGV.Caption = "MaGV";
            this.MaGV.FieldName = "MaGV";
            this.MaGV.MinWidth = 25;
            this.MaGV.Name = "MaGV";
            this.MaGV.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.MaGV.Visible = true;
            this.MaGV.VisibleIndex = 3;
            this.MaGV.Width = 93;
            // 
            // HoTenGV
            // 
            this.HoTenGV.Caption = "Họ tên giảng viên";
            this.HoTenGV.FieldName = "HoTenGV";
            this.HoTenGV.MinWidth = 25;
            this.HoTenGV.Name = "HoTenGV";
            this.HoTenGV.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.HoTenGV.Visible = true;
            this.HoTenGV.VisibleIndex = 4;
            this.HoTenGV.Width = 93;
            // 
            // Email
            // 
            this.Email.Caption = "Email";
            this.Email.FieldName = "Email";
            this.Email.MinWidth = 25;
            this.Email.Name = "Email";
            this.Email.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Email.Visible = true;
            this.Email.VisibleIndex = 5;
            this.Email.Width = 93;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 37);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.cbbNamHoc);
            this.splitContainerControl1.Panel1.Controls.Add(this.label1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.hScrollBar1);
            this.splitContainerControl1.Panel2.Controls.Add(this.gcDanhSachSV);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(856, 479);
            this.splitContainerControl1.SplitterPosition = 68;
            this.splitContainerControl1.TabIndex = 11;
            // 
            // cbbNamHoc
            // 
            this.cbbNamHoc.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbNamHoc.FormattingEnabled = true;
            this.cbbNamHoc.Location = new System.Drawing.Point(114, 23);
            this.cbbNamHoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbNamHoc.Name = "cbbNamHoc";
            this.cbbNamHoc.Size = new System.Drawing.Size(265, 27);
            this.cbbNamHoc.TabIndex = 3;
            this.cbbNamHoc.SelectedIndexChanged += new System.EventHandler(this.cbbNamHoc_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Năm học:";
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(355, 224);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(80, 21);
            this.hScrollBar1.TabIndex = 11;
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
            this.btnHuy,
            this.btnLuu,
            this.barButtonItem1,
            this.btnSua,
            this.btnThongKe});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 10;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnHuy, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnThongKe)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnSua
            // 
            this.btnSua.Caption = "Sửa";
            this.btnSua.Id = 8;
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
            // btnHuy
            // 
            this.btnHuy.Caption = "Huỷ";
            this.btnHuy.Id = 3;
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(50, 30);
            this.btnHuy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHuy_ItemClick);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Caption = "Thống kê";
            this.btnThongKe.Id = 9;
            this.btnThongKe.Name = "btnThongKe";
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
            // btnThem
            // 
            this.btnThem.Id = 5;
            this.btnThem.Name = "btnThem";
            // 
            // btnXoa
            // 
            this.btnXoa.Id = 6;
            this.btnXoa.Name = "btnXoa";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Xuất";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.Size = new System.Drawing.Size(50, 30);
            // 
            // uc_DSGD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "uc_DSGD";
            this.Size = new System.Drawing.Size(856, 516);
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSachSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSachSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDanhSachSV;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDanhSachSV;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.ComboBox cbbNamHoc;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn MaMH;
        private DevExpress.XtraGrid.Columns.GridColumn TenMH;
        private DevExpress.XtraGrid.Columns.GridColumn MaLHP;
        private DevExpress.XtraGrid.Columns.GridColumn MaGV;
        private DevExpress.XtraGrid.Columns.GridColumn HoTenGV;
        private DevExpress.XtraGrid.Columns.GridColumn Email;
        private DevExpress.XtraEditors.HScrollBar hScrollBar1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnSua;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.XtraBars.BarButtonItem btnHuy;
        private DevExpress.XtraBars.BarButtonItem btnThongKe;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnThem;
        private DevExpress.XtraBars.BarButtonItem btnXoa;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}
