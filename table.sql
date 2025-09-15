	USE QL_SinhVien;
	GO

	-- ==========================
	-- XÓA BẢNG CŨ (theo thứ tự con -> cha)
	-- ==========================
	IF OBJECT_ID('dbo.ChiTietHocPhan', 'U') IS NOT NULL DROP TABLE dbo.ChiTietHocPhan;
	IF OBJECT_ID('dbo.DangKyMonHoc', 'U') IS NOT NULL DROP TABLE dbo.DangKyMonHoc;
	IF OBJECT_ID('dbo.LopHocPhan', 'U') IS NOT NULL DROP TABLE dbo.LopHocPhan;
	IF OBJECT_ID('dbo.GiangVien', 'U') IS NOT NULL DROP TABLE dbo.GiangVien;
	IF OBJECT_ID('dbo.MonHoc', 'U') IS NOT NULL DROP TABLE dbo.MonHoc;
	IF OBJECT_ID('dbo.ThongTinLienLac', 'U') IS NOT NULL DROP TABLE dbo.ThongTinLienLac;
	IF OBJECT_ID('dbo.SinhVien', 'U') IS NOT NULL DROP TABLE dbo.SinhVien;
		IF OBJECT_ID('dbo.CongThucTinhDiem', 'U') IS NOT NULL DROP TABLE dbo.CongThucTinhDiem;
		IF OBJECT_ID('dbo.HocKyNamHoc', 'U') IS NOT NULL DROP TABLE dbo.HocKyNamHoc;
				IF OBJECT_ID('dbo.DiemRenLuyen', 'U') IS NOT NULL DROP TABLE dbo.DiemRenLuyen;



	GO

	-- ==========================
	-- Bảng SinhVien
	-- ==========================
	CREATE TABLE SinhVien (
		MaSV       VARCHAR(10) PRIMARY KEY,
		HoTen      NVARCHAR(100) NOT NULL,
		NgaySinh   DATE,
		NoiSinh    NVARCHAR(100),
		GioiTinh   NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ')),
		CMND_CCCD  VARCHAR(20),
		LopSV      VARCHAR(20)
	);

	CREATE TABLE DiemRenLuyen (
		MaSV       VARCHAR(10) PRIMARY KEY,
		MaHocKyNamHoc INT NOT NULL
	);

	-- ==========================
	-- Bảng MonHoc
	-- ==========================
	CREATE TABLE MonHoc (
		MaMH      VARCHAR(10) PRIMARY KEY,
		TenMH     NVARCHAR(100) NOT NULL,
		SoTinChi  INT NOT NULL CHECK (SoTinChi > 0)
	);

	-- ==========================
	-- Bảng GiangVien
	-- ==========================
	CREATE TABLE GiangVien (
		MaGV      VARCHAR(10) PRIMARY KEY,
		HoTenGV   NVARCHAR(100) NOT NULL,
		HocVi     NVARCHAR(50),
		Khoa      NVARCHAR(100),
		Email     VARCHAR(100),
		DienThoai VARCHAR(15)
	);


-- Bảng HocKyNamHoc
-- ==========================
CREATE TABLE HocKyNamHoc (
    MaHocKyNamHoc INT IDENTITY(1,1) PRIMARY KEY,
    HocKy    INT NOT NULL CHECK (HocKy IN (1,2)),
    NamHoc   VARCHAR(9) NOT NULL   -- ví dụ '2024-2025'
);

-- ==========================
-- Bảng LopHocPhan
-- ==========================
CREATE TABLE LopHocPhan (
    MaLHP    VARCHAR(10) NOT NULL,
    MaMH     VARCHAR(10) NOT NULL,
    MaGV     VARCHAR(10) NOT NULL,
    MaHocKyNamHoc INT NOT NULL,
    PRIMARY KEY (MaLHP)
);


-- ==========================
-- Bảng DangKyMonHoc
-- ==========================
CREATE TABLE DangKyMonHoc (
    MaSV     VARCHAR(10) NOT NULL,
    MaLHP    VARCHAR(10) NOT NULL,
	MaHocKyNamHoc INT NOT NULL,
    PRIMARY KEY (MaSV, MaLHP),
    FOREIGN KEY (MaLHP) REFERENCES LopHocPhan(MaLHP)
);


-- ==========================
-- Bảng ChiTietHocPhan
-- ==========================
CREATE TABLE ChiTietHocPhan ( 
    MaSV      VARCHAR(10) NOT NULL,
    MaMH      VARCHAR(10) NOT NULL,
    MaHocKyNamHoc INT NOT NULL,
    DiemGK    DECIMAL(4,2) CHECK (DiemGK BETWEEN 0 AND 10),
    DiemCK    DECIMAL(4,2) CHECK (DiemCK BETWEEN 0 AND 10),
    PRIMARY KEY (MaSV, MaMH, MaHocKyNamHoc),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaMH) REFERENCES MonHoc(MaMH)
);


CREATE TABLE CongThucTinhDiem
(
    Ma INT IDENTITY(1,1) PRIMARY KEY,  -- Tự tăng từ 1, tăng 1
    TiLeGK DECIMAL(4,2) NOT NULL CHECK (TiLeGK >= 0 AND TiLeGK <= 1),
    TiLeCK DECIMAL(4,2) NOT NULL CHECK (TiLeCK >= 0 AND TiLeCK <= 1),
);


	
