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
            // gcDanhSachSV
            // 
            this.gcDanhSachSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDanhSachSV.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcDanhSachSV.Location = new System.Drawing.Point(0, 0);
            this.gcDanhSachSV.MainView = this.gvDanhSachSV;
            this.gcDanhSachSV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcDanhSachSV.Name = "gcDanhSachSV";
            this.gcDanhSachSV.Size = new System.Drawing.Size(642, 354);
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
            this.gvDanhSachSV.DetailHeight = 284;
            this.gvDanhSachSV.GridControl = this.gcDanhSachSV;
            this.gvDanhSachSV.Name = "gvDanhSachSV";
            this.gvDanhSachSV.OptionsView.AllowCellMerge = true;
            this.gvDanhSachSV.OptionsView.ShowGroupPanel = false;
            // 
            // MaMH
            // 
            this.MaMH.Caption = "MaMH";
            this.MaMH.FieldName = "MaMH";
            this.MaMH.MinWidth = 19;
            this.MaMH.Name = "MaMH";
            this.MaMH.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.MaMH.Visible = true;
            this.MaMH.VisibleIndex = 0;
            this.MaMH.Width = 70;
            // 
            // TenMH
            // 
            this.TenMH.Caption = "Tên môn học";
            this.TenMH.FieldName = "TenMH";
            this.TenMH.MinWidth = 19;
            this.TenMH.Name = "TenMH";
            this.TenMH.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.TenMH.Visible = true;
            this.TenMH.VisibleIndex = 1;
            this.TenMH.Width = 70;
            // 
            // MaLHP
            // 
            this.MaLHP.Caption = "MaLHP";
            this.MaLHP.FieldName = "MaLHP";
            this.MaLHP.MinWidth = 19;
            this.MaLHP.Name = "MaLHP";
            this.MaLHP.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.MaLHP.Visible = true;
            this.MaLHP.VisibleIndex = 2;
            this.MaLHP.Width = 70;
            // 
            // MaGV
            // 
            this.MaGV.Caption = "MaGV";
            this.MaGV.FieldName = "MaGV";
            this.MaGV.MinWidth = 19;
            this.MaGV.Name = "MaGV";
            this.MaGV.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.MaGV.Visible = true;
            this.MaGV.VisibleIndex = 3;
            this.MaGV.Width = 70;
            // 
            // HoTenGV
            // 
            this.HoTenGV.Caption = "Họ tên giảng viên";
            this.HoTenGV.FieldName = "HoTenGV";
            this.HoTenGV.MinWidth = 19;
            this.HoTenGV.Name = "HoTenGV";
            this.HoTenGV.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.HoTenGV.Visible = true;
            this.HoTenGV.VisibleIndex = 4;
            this.HoTenGV.Width = 70;
            // 
            // Email
            // 
            this.Email.Caption = "Email";
            this.Email.FieldName = "Email";
            this.Email.MinWidth = 19;
            this.Email.Name = "Email";
            this.Email.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.Email.Visible = true;
            this.Email.VisibleIndex = 5;
            this.Email.Width = 70;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.splitContainerControl1.Size = new System.Drawing.Size(642, 419);
            this.splitContainerControl1.SplitterPosition = 55;
            this.splitContainerControl1.TabIndex = 11;
            // 
            // cbbNamHoc
            // 
            this.cbbNamHoc.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbNamHoc.FormattingEnabled = true;
            this.cbbNamHoc.Location = new System.Drawing.Point(86, 19);
            this.cbbNamHoc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbbNamHoc.Name = "cbbNamHoc";
            this.cbbNamHoc.Size = new System.Drawing.Size(200, 24);
            this.cbbNamHoc.TabIndex = 3;
            this.cbbNamHoc.SelectedIndexChanged += new System.EventHandler(this.cbbNamHoc_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Năm học:";
            // 
            // uc_DSGD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "uc_DSGD";
            this.Size = new System.Drawing.Size(642, 419);
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
    }
}
