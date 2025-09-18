namespace DoAnCK.UI_Admin
{
    partial class uc_KQHT
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
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.btnQuayLai = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnThem = new DevExpress.XtraBars.BarButtonItem();
            this.btnSua = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.gcDanhSachSV = new DevExpress.XtraGrid.GridControl();
            this.gvDanhSachSV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaSV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaMH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenMH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DiemGK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DiemCK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DiemTB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KetQua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.cbbNamHoc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSachSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSachSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
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
            this.btnSua,
            this.btnXoa,
            this.btnQuayLai,
            this.btnLuu,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLuu, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnQuayLai, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnLuu
            // 
            this.btnLuu.Caption = "Lưu";
            this.btnLuu.Id = 4;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(50, 30);
            this.btnLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLuu_ItemClick);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Caption = "Hoàn tác";
            this.btnQuayLai.Id = 3;
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(50, 30);
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
            // btnThem
            // 
            this.btnThem.Id = 5;
            this.btnThem.Name = "btnThem";
            // 
            // btnSua
            // 
            this.btnSua.Caption = "Sửa";
            this.btnSua.Id = 1;
            this.btnSua.Name = "btnSua";
            // 
            // btnXoa
            // 
            this.btnXoa.Id = 6;
            this.btnXoa.Name = "btnXoa";
            // 
            // gcDanhSachSV
            // 
            this.gcDanhSachSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDanhSachSV.Location = new System.Drawing.Point(0, 0);
            this.gcDanhSachSV.MainView = this.gvDanhSachSV;
            this.gcDanhSachSV.MenuManager = this.barManager1;
            this.gcDanhSachSV.Name = "gcDanhSachSV";
            this.gcDanhSachSV.Size = new System.Drawing.Size(856, 409);
            this.gcDanhSachSV.TabIndex = 10;
            this.gcDanhSachSV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDanhSachSV});
            // 
            // gvDanhSachSV
            // 
            this.gvDanhSachSV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaSV,
            this.HoTen,
            this.MaMH,
            this.TenMH,
            this.DiemGK,
            this.DiemCK,
            this.DiemTB,
            this.KetQua});
            this.gvDanhSachSV.GridControl = this.gcDanhSachSV;
            this.gvDanhSachSV.Name = "gvDanhSachSV";
            this.gvDanhSachSV.OptionsView.ShowGroupPanel = false;
            // 
            // MaSV
            // 
            this.MaSV.Caption = "MSSV";
            this.MaSV.FieldName = "MaSV";
            this.MaSV.MinWidth = 25;
            this.MaSV.Name = "MaSV";
            this.MaSV.OptionsColumn.AllowEdit = false;
            this.MaSV.Visible = true;
            this.MaSV.VisibleIndex = 0;
            this.MaSV.Width = 109;
            // 
            // HoTen
            // 
            this.HoTen.Caption = "Họ tên";
            this.HoTen.FieldName = "HoTen";
            this.HoTen.MinWidth = 25;
            this.HoTen.Name = "HoTen";
            this.HoTen.OptionsColumn.AllowEdit = false;
            this.HoTen.Visible = true;
            this.HoTen.VisibleIndex = 1;
            this.HoTen.Width = 207;
            // 
            // MaMH
            // 
            this.MaMH.Caption = "MaMH";
            this.MaMH.FieldName = "MaMH";
            this.MaMH.MinWidth = 25;
            this.MaMH.Name = "MaMH";
            this.MaMH.OptionsColumn.AllowEdit = false;
            this.MaMH.Visible = true;
            this.MaMH.VisibleIndex = 2;
            this.MaMH.Width = 109;
            // 
            // TenMH
            // 
            this.TenMH.Caption = "Tên môn học ";
            this.TenMH.FieldName = "TenMH";
            this.TenMH.MinWidth = 25;
            this.TenMH.Name = "TenMH";
            this.TenMH.OptionsColumn.AllowEdit = false;
            this.TenMH.Visible = true;
            this.TenMH.VisibleIndex = 3;
            this.TenMH.Width = 144;
            // 
            // DiemGK
            // 
            this.DiemGK.Caption = "Điểm giữa kỳ";
            this.DiemGK.FieldName = "DiemGK";
            this.DiemGK.MinWidth = 25;
            this.DiemGK.Name = "DiemGK";
            this.DiemGK.Visible = true;
            this.DiemGK.VisibleIndex = 4;
            this.DiemGK.Width = 60;
            // 
            // DiemCK
            // 
            this.DiemCK.Caption = "Điểm cuối kỳ";
            this.DiemCK.FieldName = "DiemCK";
            this.DiemCK.MinWidth = 25;
            this.DiemCK.Name = "DiemCK";
            this.DiemCK.Visible = true;
            this.DiemCK.VisibleIndex = 5;
            this.DiemCK.Width = 62;
            // 
            // DiemTB
            // 
            this.DiemTB.Caption = "Điểm tổng";
            this.DiemTB.FieldName = "DiemTB";
            this.DiemTB.MinWidth = 25;
            this.DiemTB.Name = "DiemTB";
            this.DiemTB.OptionsColumn.AllowEdit = false;
            this.DiemTB.Visible = true;
            this.DiemTB.VisibleIndex = 6;
            this.DiemTB.Width = 65;
            // 
            // KetQua
            // 
            this.KetQua.Caption = "Kết quả";
            this.KetQua.FieldName = "KetQua";
            this.KetQua.MinWidth = 25;
            this.KetQua.Name = "KetQua";
            this.KetQua.OptionsColumn.AllowEdit = false;
            this.KetQua.Visible = true;
            this.KetQua.VisibleIndex = 7;
            this.KetQua.Width = 89;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 37);
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
            this.splitContainerControl1.Panel2.Controls.Add(this.gcDanhSachSV);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(856, 479);
            this.splitContainerControl1.SplitterPosition = 58;
            this.splitContainerControl1.TabIndex = 15;
            // 
            // cbbNamHoc
            // 
            this.cbbNamHoc.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbNamHoc.FormattingEnabled = true;
            this.cbbNamHoc.Location = new System.Drawing.Point(117, 19);
            this.cbbNamHoc.Name = "cbbNamHoc";
            this.cbbNamHoc.Size = new System.Drawing.Size(193, 27);
            this.cbbNamHoc.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Năm học:";
            // 
            // uc_KQHT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "uc_KQHT";
            this.Size = new System.Drawing.Size(856, 516);
            this.Load += new System.EventHandler(this.uc_KQHT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSachSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSachSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnThem;
        private DevExpress.XtraBars.BarButtonItem btnXoa;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.XtraBars.BarButtonItem btnQuayLai;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnSua;
        private DevExpress.XtraGrid.GridControl gcDanhSachSV;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDanhSachSV;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.ComboBox cbbNamHoc;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn MaSV;
        private DevExpress.XtraGrid.Columns.GridColumn HoTen;
        private DevExpress.XtraGrid.Columns.GridColumn MaMH;
        private DevExpress.XtraGrid.Columns.GridColumn TenMH;
        private DevExpress.XtraGrid.Columns.GridColumn DiemGK;
        private DevExpress.XtraGrid.Columns.GridColumn DiemCK;
        private DevExpress.XtraGrid.Columns.GridColumn DiemTB;
        private DevExpress.XtraGrid.Columns.GridColumn KetQua;
    }
}
