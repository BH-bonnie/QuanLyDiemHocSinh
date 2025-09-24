USE QL_SinhVien;
GO

IF OBJECT_ID('dbo.ChiTietHocPhan', 'U') IS NOT NULL DROP TABLE dbo.ChiTietHocPhan;
IF OBJECT_ID('dbo.DangKyMonHoc', 'U') IS NOT NULL DROP TABLE dbo.DangKyMonHoc;
IF OBJECT_ID('dbo.DiemRenLuyen', 'U') IS NOT NULL DROP TABLE dbo.DiemRenLuyen;

IF OBJECT_ID('dbo.LopHocPhan', 'U') IS NOT NULL DROP TABLE dbo.LopHocPhan;

IF OBJECT_ID('dbo.CongThucTinhDiem', 'U') IS NOT NULL DROP TABLE dbo.CongThucTinhDiem;
IF OBJECT_ID('dbo.LogDangNhap', 'U') IS NOT NULL DROP TABLE dbo.LogDangNhap;
IF OBJECT_ID('dbo.TaiKhoan', 'U') IS NOT NULL DROP TABLE dbo.TaiKhoan;

IF OBJECT_ID('dbo.SinhVien', 'U') IS NOT NULL DROP TABLE dbo.SinhVien;
IF OBJECT_ID('dbo.Lop', 'U') IS NOT NULL DROP TABLE dbo.Lop;
IF OBJECT_ID('dbo.Khoa', 'U') IS NOT NULL DROP TABLE dbo.Khoa;
IF OBJECT_ID('dbo.MonHoc', 'U') IS NOT NULL DROP TABLE dbo.MonHoc;
IF OBJECT_ID('dbo.GiangVien', 'U') IS NOT NULL DROP TABLE dbo.GiangVien;
IF OBJECT_ID('dbo.HocKyNamHoc', 'U') IS NOT NULL DROP TABLE dbo.HocKyNamHoc;



CREATE TABLE Khoa (
	MaKhoa VARCHAR(10) PRIMARY KEY,       
	TenKhoa NVARCHAR(100) NOT NULL
);


CREATE TABLE Lop (
	LopSV  VARCHAR(20) PRIMARY KEY,
	MaKhoa VARCHAR(10) NOT NULL REFERENCES Khoa(MaKhoa)
);

CREATE TABLE SinhVien (
	MaSV       VARCHAR(10) PRIMARY KEY,
	HoTen      NVARCHAR(100) NOT NULL,
	NgaySinh   DATE,
	NoiSinh    NVARCHAR(100),
	GioiTinh   NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ')),
	CMND_CCCD  VARCHAR(20),
	LopSV      VARCHAR(20) REFERENCES Lop(LopSV),
    TrangThai BIT DEFAULT 0
);

CREATE TABLE DiemRenLuyen (
	MaSV VARCHAR(10) PRIMARY KEY REFERENCES SinhVien(MaSV),
	Diem DECIMAL(5,2) CHECK (Diem >= 0 AND Diem <= 100)
);


-- ==========================
-- Bảng MonHoc
-- ==========================
CREATE TABLE MonHoc (
	MaMH      VARCHAR(20) PRIMARY KEY,
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
	DienThoai VARCHAR(15),
	TrangThai BIT DEFAULT 0
);
		

-- Bảng HocKyNamHoc
-- ==========================
CREATE TABLE HocKyNamHoc (
    MaHocKyNamHoc INT IDENTITY(1,1) PRIMARY KEY,
    HocKy    INT NOT NULL CHECK (HocKy IN (1,2)),
    NamHoc   VARCHAR(9) NOT NULL   
);

-- ==========================
-- Bảng LopHocPhan
-- ==========================
CREATE TABLE LopHocPhan (
    MaLHP    VARCHAR(20) NOT NULL,
    MaMH     VARCHAR(20) NOT NULL  REFERENCES MonHoc(MaMH),
    MaGV     VARCHAR(10) NOT NULL REFERENCES GiangVien(MaGV),
    MaHocKyNamHoc INT NOT NULL REFERENCES HocKyNamHoc(MaHocKyNamHoc),
    PRIMARY KEY (MaLHP,MaHocKyNamHoc)
);


-- ==========================
-- Bảng DangKyMonHoc
-- ==========================
CREATE TABLE DangKyMonHoc (
    MaSV     VARCHAR(10) NOT NULL REFERENCES SinhVien(MaSV),
    MaLHP    VARCHAR(20) NOT NULL,
	MaHocKyNamHoc INT NOT NULL,
    PRIMARY KEY (MaSV, MaLHP,MaHocKyNamHoc),
    FOREIGN KEY (MaLHP,MaHocKyNamHoc) REFERENCES LopHocPhan(MaLHP,MaHocKyNamHoc)
);


-- ==========================
-- Bảng ChiTietHocPhan
-- ==========================
CREATE TABLE ChiTietHocPhan ( 
    MaSV      VARCHAR(10) NOT NULL REFERENCES SinhVien(MaSV),
    MaMH      VARCHAR(20) NOT NULL REFERENCES MonHoc(MaMH),
    MaHocKyNamHoc INT NOT NULL REFERENCES HocKyNamHoc(MaHocKyNamHoc),
    DiemGK    DECIMAL(4,2) CHECK (DiemGK BETWEEN 0 AND 10),
    DiemCK    DECIMAL(4,2) CHECK (DiemCK BETWEEN 0 AND 10),
	DiemTB    DECIMAL(4,2)
    PRIMARY KEY (MaSV, MaMH, MaHocKyNamHoc)
);


CREATE TABLE CongThucTinhDiem
(
    Ma INT IDENTITY(1,1) PRIMARY KEY,  -- Tự tăng từ 1, tăng 1
    TiLeGK DECIMAL(4,2) NOT NULL CHECK (TiLeGK >= 0 AND TiLeGK <= 1),
    TiLeCK DECIMAL(4,2) NOT NULL CHECK (TiLeCK >= 0 AND TiLeCK <= 1)
);


CREATE TABLE LogDangNhap
(
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50),
    ThoiGian DATETIME DEFAULT GETDATE(),
    KetQua NVARCHAR(100)
);  

