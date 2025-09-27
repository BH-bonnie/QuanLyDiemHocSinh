	USE QL_SinhVien;
	GO

IF OBJECT_ID('dbo.fn_LopHocPhanTheoNamHoc', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_LopHocPhanTheoNamHoc;
GO

CREATE FUNCTION dbo.fn_LopHocPhanTheoNamHoc
(
    @MaHocKyNamHoc INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        LHP.MaMH,
		MH.TenMH,
        LHP.MaLHP,
        GV.MaGV,
        GV.HoTenGV,
		GV.Email
    FROM LopHocPhan LHP
    INNER JOIN MonHoc MH ON LHP.MaMH = MH.MaMH
    INNER JOIN GiangVien GV ON LHP.MaGV = GV.MaGV
    INNER JOIN HocKyNamHoc HKNH ON LHP.MaHocKyNamHoc = HKNH.MaHocKyNamHoc
    WHERE LHP.MaHocKyNamHoc = @MaHocKyNamHoc AND GV.TrangThai =0              
              

);
GO
IF OBJECT_ID('dbo.v_SinhVien_Detail', 'V') IS NOT NULL
    DROP VIEW dbo.v_SinhVien_Detail;
GO

CREATE VIEW dbo.v_SinhVien_Detail
AS
SELECT 
    SV.MaSV, 
    SV.HoTen, 
    SV.NgaySinh, 
    SV.NoiSinh, 
    SV.GioiTinh,
    SV.LopSV,
    SV.TrangThai	
FROM SinhVien SV;
GO
IF OBJECT_ID('dbo.trg_UpdateDiem_KhiSVNgungHoatDong', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_UpdateDiem_KhiSVNgungHoatDong;
GO

CREATE TRIGGER dbo.trg_UpdateDiem_KhiSVNgungHoatDong
ON SinhVien
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CTHP
    SET 
        DiemGK = ISNULL(CTHP.DiemGK, 0),
        DiemCK = ISNULL(CTHP.DiemCK, 0),
        DiemTB = 0
    FROM ChiTietHocPhan CTHP
    INNER JOIN inserted i ON CTHP.MaSV = i.MaSV
    WHERE i.TrangThai = 1;
END;
GO

IF OBJECT_ID('dbo.v_GiangVien_Detail', 'V') IS NOT NULL
    DROP VIEW dbo.v_GiangVien_Detail;
GO

CREATE VIEW dbo.v_GiangVien_Detail
AS
SELECT 
    GV.MaGV,
    GV.HoTenGV,
    GV.HocVi,
    GV.Khoa,
    GV.Email,
    GV.DienThoai,
	GV.TrangThai	

FROM GiangVien GV;
GO

IF OBJECT_ID('dbo.v_MonHoc', 'V') IS NOT NULL
    DROP VIEW dbo.v_MonHoc;
GO

CREATE VIEW dbo.v_MonHoc
AS
SELECT 
    MaMH,       
    TenMH,      
    SoTinChi    
FROM MonHoc;
GO
IF OBJECT_ID('dbo.fn_DangKyMonHocTheoNamHoc', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_DangKyMonHocTheoNamHoc;
GO

CREATE FUNCTION dbo.fn_DangKyMonHocTheoNamHoc
(
    @MaHocKyNamHoc INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 


		LHP.MaMH,
		MH.TenMH,
		DKMH.MaLHP,
        DKMH.MaSV,
        SV.HoTen
    FROM DangKyMonHoc DKMH
    INNER JOIN SinhVien SV ON DKMH.MaSV = SV.MaSV
    INNER JOIN LopHocPhan LHP ON DKMH.MaLHP = LHP.MaLHP
    INNER JOIN MonHoc MH ON LHP.MaMH = MH.MaMH
    WHERE DKMH.MaHocKyNamHoc = @MaHocKyNamHoc AND SV.TrangThai = 0          
);
GO


IF OBJECT_ID('dbo.fn_DanhSachMonHoc_GiangVien', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_DanhSachMonHoc_GiangVien;
GO

CREATE FUNCTION fn_DanhSachMonHoc_GiangVien (
    @MaGV VARCHAR(10),
    @MaHocKyNamHoc INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        MH.MaMH,
        MH.TenMH,
        LHP.MaLHP
    FROM LopHocPhan LHP
    INNER JOIN MonHoc MH ON LHP.MaMH = MH.MaMH
    INNER JOIN GiangVien GV ON LHP.MaGV = GV.MaGV
    WHERE GV.MaGV = @MaGV
      AND LHP.MaHocKyNamHoc = @MaHocKyNamHoc  
);
GO






IF OBJECT_ID('dbo.fn_FormattedDate', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_FormattedDate;
GO

CREATE FUNCTION dbo.fn_FormattedDate (@Ngay DATE)
RETURNS NVARCHAR(10)
AS
BEGIN
    IF @Ngay IS NULL
        RETURN NULL;
    RETURN RIGHT('0' + CAST(DAY(@Ngay) AS VARCHAR(2)), 2) + '/' +
           RIGHT('0' + CAST(MONTH(@Ngay) AS VARCHAR(2)), 2) + '/' +
           CAST(YEAR(@Ngay) AS VARCHAR(4));
END;
GO

IF OBJECT_ID('dbo.sp_DanhSachHocKyNamHoc', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DanhSachHocKyNamHoc;
GO
CREATE PROCEDURE sp_DanhSachHocKyNamHoc
AS
BEGIN
    SELECT 
        MaHocKyNamHoc, 
        HocKy, 
        NamHoc
    FROM HocKyNamHoc
    ORDER BY MaHocKyNamHoc DESC;
END;
GO
IF OBJECT_ID('dbo.sp_GetMaHocKyMoiNhat', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetMaHocKyMoiNhat;
GO
CREATE PROCEDURE sp_GetMaHocKyMoiNhat
AS
BEGIN
    SELECT TOP 1 MaHocKyNamHoc 
    FROM HocKyNamHoc 
    ORDER BY MaHocKyNamHoc DESC;
END;
GO

IF OBJECT_ID('trg_TinhDiemTB', 'TR') IS NOT NULL
    DROP TRIGGER trg_TinhDiemTB;
GO
CREATE TRIGGER trg_TinhDiemTB

ON ChiTietHocPhan
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CTHP
    SET CTHP.DiemTB = dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK)
    FROM ChiTietHocPhan CTHP
    INNER JOIN inserted i
        ON CTHP.MaSV = i.MaSV
       AND CTHP.MaLHP = i.MaLHP
       AND CTHP.MaHocKyNamHoc = i.MaHocKyNamHoc
    WHERE i.DiemGK IS NOT NULL
      AND i.DiemCK IS NOT NULL;
END;
GO
IF OBJECT_ID('dbo.fn_TrangThaiDiemTB', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_TrangThaiDiemTB;
GO
CREATE FUNCTION fn_TrangThaiDiemTB
(
    @DiemTB FLOAT
)
RETURNS NVARCHAR(20)
AS
BEGIN
    DECLARE @TrangThai NVARCHAR(20);

    IF @DiemTB IS NULL
        SET @TrangThai = NULL;
    ELSE IF @DiemTB >= 5
        SET @TrangThai = N'Đạt';
    ELSE
        SET @TrangThai = N'Không đạt';

    RETURN @TrangThai;
END;
GO



IF OBJECT_ID('trg_AfterInsert_DangKyMonHoc', 'TR') IS NOT NULL
    DROP TRIGGER trg_AfterInsert_DangKyMonHoc;
GO

CREATE TRIGGER trg_AfterInsert_DangKyMonHoc
ON DangKyMonHoc
AFTER INSERT
AS
BEGIN
    INSERT INTO ChiTietHocPhan (MaSV, MaLHP, DiemGK, DiemCK,DiemTB, MaHocKyNamHoc)
    SELECT 
        i.MaSV,
        i.MaLHP,
        NULL,
        NULL,
		NuLL,
        i.MaHocKyNamHoc
    FROM inserted i
END;
Go

IF OBJECT_ID('dbo.fn_SinhVienTheoLopHocPhan', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_SinhVienTheoLopHocPhan;
GO


CREATE FUNCTION fn_SinhVienTheoLopHocPhan(
    @MaLHP VARCHAR(20),
    @MaHocKyNamHoc INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        SV.MaSV,
        SV.HoTen,
        dbo.fn_FormattedDate(SV.NgaySinh) AS NgaySinh,
        SV.GioiTinh
    FROM DangKyMonHoc DKMH
    INNER JOIN SinhVien SV ON DKMH.MaSV = SV.MaSV
    INNER JOIN LopHocPhan LHP ON DKMH.MaLHP = LHP.MaLHP
    WHERE LHP.MaLHP = @MaLHP
      AND LHP.MaHocKyNamHoc = @MaHocKyNamHoc
      AND SV.TrangThai = 0
);
GO

IF OBJECT_ID('dbo.fn_SinhVienVaDiemTheoLopHocPhan', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_SinhVienVaDiemTheoLopHocPhan;
GO

CREATE FUNCTION fn_SinhVienVaDiemTheoLopHocPhan
(
    @MaLHP VARCHAR(20),
    @MaHocKyNamHoc INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        SV.MaSV,
        SV.HoTen,
        dbo.fn_FormattedDate(SV.NgaySinh) AS NgaySinh,
        SV.GioiTinh,
        CTHP.DiemGK,
        CTHP.DiemCK,
        CTHP.DiemTB,
        dbo.fn_TrangThaiDiemTB(CTHP.DiemTB) AS TrangThai
    FROM DangKyMonHoc DKMH
    INNER JOIN SinhVien SV 
        ON DKMH.MaSV = SV.MaSV
    INNER JOIN LopHocPhan LHP 
        ON DKMH.MaLHP = LHP.MaLHP
       AND DKMH.MaHocKyNamHoc = LHP.MaHocKyNamHoc
    LEFT JOIN ChiTietHocPhan CTHP
        ON CTHP.MaSV = SV.MaSV
       AND CTHP.MaLHP = LHP.MaLHP
       AND CTHP.MaHocKyNamHoc = LHP.MaHocKyNamHoc
    WHERE LHP.MaLHP = @MaLHP
      AND LHP.MaHocKyNamHoc = @MaHocKyNamHoc
      AND SV.TrangThai = 0
);
GO



IF OBJECT_ID('dbo.sp_CapNhatDiemHocPhan', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CapNhatDiemHocPhan;
GO

CREATE PROCEDURE dbo.sp_CapNhatDiemHocPhan
    @MaSV VARCHAR(10),
    @MaMH VARCHAR(20),
    @MaHocKyNamHoc INT,
    @DiemGK DECIMAL(4,2),
    @DiemCK DECIMAL(4,2)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRAN;

        UPDATE CTHP
        SET DiemGK = @DiemGK,
            DiemCK = @DiemCK
        FROM ChiTietHocPhan CTHP
        INNER JOIN LopHocPhan LHP
            ON CTHP.MaLHP = LHP.MaLHP
           AND CTHP.MaHocKyNamHoc = LHP.MaHocKyNamHoc
        WHERE CTHP.MaSV = @MaSV
          AND LHP.MaMH = @MaMH
          AND LHP.MaHocKyNamHoc = @MaHocKyNamHoc;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        THROW;
    END CATCH
END
GO


IF OBJECT_ID('dbo.sp_ThongKeDiemLopHocPhan', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ThongKeDiemLopHocPhan;
GO

CREATE PROCEDURE dbo.sp_ThongKeDiemLopHocPhan
    @MaLHP VARCHAR(20),
    @MaHocKyNamHoc INT
AS
BEGIN
    SET NOCOUNT ON;

   SELECT
    COUNT(*) AS TongSoSinhVien,
    ISNULL(SUM(CASE WHEN TrangThai = N'Đạt' THEN 1 ELSE 0 END), 0) AS SoSinhVienDat,
    ISNULL(SUM(CASE WHEN TrangThai = N'Không đạt' THEN 1 ELSE 0 END), 0) AS SoSinhVienRớt,
    ISNULL(SUM(CASE WHEN TrangThai IS NULL THEN 1 ELSE 0 END), 0) AS SoSinhVienChuaCham
FROM fn_SinhVienVaDiemTheoLopHocPhan(@MaLHP, @MaHocKyNamHoc);
END
GO
IF OBJECT_ID('dbo.sp_ThongKeDiemTheoKhoangNho', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ThongKeDiemTheoKhoangNho;
GO


CREATE PROCEDURE dbo.sp_ThongKeDiemTheoKhoangNho
    @MaLHP VARCHAR(20),
    @MaHocKyNamHoc INT
AS
BEGIN
    SET NOCOUNT ON;

   SELECT
    COUNT(*) AS TongSoSinhVien,
    ISNULL(SUM(CASE WHEN DiemTB >= 0 AND DiemTB < 1 THEN 1 ELSE 0 END), 0) AS Khoang0_1,
    ISNULL(SUM(CASE WHEN DiemTB >= 1 AND DiemTB < 2 THEN 1 ELSE 0 END), 0) AS Khoang1_2,
    ISNULL(SUM(CASE WHEN DiemTB >= 2 AND DiemTB < 3 THEN 1 ELSE 0 END), 0) AS Khoang2_3,
    ISNULL(SUM(CASE WHEN DiemTB >= 3 AND DiemTB < 4 THEN 1 ELSE 0 END), 0) AS Khoang3_4,
    ISNULL(SUM(CASE WHEN DiemTB >= 4 AND DiemTB < 5 THEN 1 ELSE 0 END), 0) AS Khoang4_5,
    ISNULL(SUM(CASE WHEN DiemTB >= 5 AND DiemTB < 6 THEN 1 ELSE 0 END), 0) AS Khoang5_6,
    ISNULL(SUM(CASE WHEN DiemTB >= 6 AND DiemTB < 7 THEN 1 ELSE 0 END), 0) AS Khoang6_7,
    ISNULL(SUM(CASE WHEN DiemTB >= 7 AND DiemTB < 8 THEN 1 ELSE 0 END), 0) AS Khoang7_8,
    ISNULL(SUM(CASE WHEN DiemTB >= 8 AND DiemTB < 9 THEN 1 ELSE 0 END), 0) AS Khoang8_9,
    ISNULL(SUM(CASE WHEN DiemTB >= 9 AND DiemTB <= 10 THEN 1 ELSE 0 END), 0) AS Khoang9_10
FROM fn_SinhVienVaDiemTheoLopHocPhan(@MaLHP, @MaHocKyNamHoc)

    WHERE DiemTB IS NOT NULL;
END
GO

IF OBJECT_ID('dbo.fn_SinhVienTheoLop', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_SinhVienTheoLop;
GO
CREATE FUNCTION fn_SinhVienTheoLop (@LopSV VARCHAR(20))
RETURNS TABLE
AS
RETURN
(
    SELECT MaSV,HoTen
    FROM SinhVien
    WHERE LopSV = @LopSV
);
GO
IF OBJECT_ID('dbo.fn_QuyDoiDiemHe4', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_QuyDoiDiemHe4;
GO
CREATE  FUNCTION fn_QuyDoiDiemHe4 (@DiemHe10 DECIMAL(4,2))
RETURNS DECIMAL(3,2)
AS
BEGIN
    DECLARE @DiemHe4 DECIMAL(3,2);
	IF @DiemHe10 IS NULL
    RETURN NULL;
    IF @DiemHe10 >= 9.0 SET @DiemHe4 = 4.0;         
    ELSE IF @DiemHe10 >= 8.5 SET @DiemHe4 = 3.7;  
    ELSE IF @DiemHe10 >= 8.0 SET @DiemHe4 = 3.5;  
    ELSE IF @DiemHe10 >= 7.0 SET @DiemHe4 = 3.0;  
    ELSE IF @DiemHe10 >= 6.5 SET @DiemHe4 = 2.5;  
    ELSE IF @DiemHe10 >= 5.5 SET @DiemHe4 = 2.0;  
    ELSE IF @DiemHe10 >= 5.0 SET @DiemHe4 = 1.5;    
    ELSE IF @DiemHe10 >= 4.0 SET @DiemHe4 = 1.0;  
    ELSE SET @DiemHe4 = 0.0;                       
    RETURN @DiemHe4;
END
GO
IF OBJECT_ID('dbo.fn_QuyDoiDiemChu', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_QuyDoiDiemChu;
GO
CREATE  FUNCTION fn_QuyDoiDiemChu (@DiemHe10 DECIMAL(4,2))
RETURNS NVARCHAR(2)
AS
BEGIN
    DECLARE @Chu NVARCHAR(2);
	IF @DiemHe10 IS NULL
    RETURN NULL;


    IF @DiemHe10 >= 9.0 SET @Chu = N'A+';  
    ELSE IF @DiemHe10 >= 8.5 SET @Chu = N'A';  
    ELSE IF @DiemHe10 >= 8.0 SET @Chu = N'B+';  
    ELSE IF @DiemHe10 >= 7.0 SET @Chu = N'B';  
    ELSE IF @DiemHe10 >= 6.5 SET @Chu = N'C+';  
    ELSE IF @DiemHe10 >= 5.5 SET @Chu = N'C';  
    ELSE IF @DiemHe10 >= 5.0 SET @Chu = N'D+';  
    ELSE IF @DiemHe10 >= 4.0 SET @Chu = N'D';  
    ELSE SET @Chu = N'F';

    RETURN @Chu;
END
GO
IF OBJECT_ID('dbo.fn_TinhDiemTrungBinh', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_TinhDiemTrungBinh;
GO

CREATE FUNCTION dbo.fn_TinhDiemTrungBinh
(
    @DiemGK DECIMAL(4,2),
    @DiemCK DECIMAL(4,2)
)
RETURNS DECIMAL(4,2)
AS
BEGIN
    DECLARE @DiemTB DECIMAL(4,2);
    DECLARE @TiLeGK DECIMAL(4,2);
    DECLARE @TiLeCK DECIMAL(4,2);

    SELECT TOP 1 
           @TiLeGK = TiLeGK, 
           @TiLeCK = TiLeCK
    FROM CongThucTinhDiem
	ORDER BY Ma DESC;

    IF @TiLeGK IS NULL OR @TiLeCK IS NULL
    BEGIN
        SET @TiLeGK = 0.5;
        SET @TiLeCK = 0.5;
    END

    IF @DiemGK IS NULL OR @DiemCK IS NULL
    BEGIN
        RETURN NULL;
    END

    IF @DiemCK < 3
    BEGIN
        SET @DiemGK=0;
    END
    
    SET @DiemTB = ROUND(@DiemGK * @TiLeGK + @DiemCK * @TiLeCK, 2);


    RETURN @DiemTB;
END
GO
IF OBJECT_ID('dbo.fn_ChiTietDiemSV', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_ChiTietDiemSV;
GO

CREATE FUNCTION dbo.fn_ChiTietDiemSV(@MaSV VARCHAR(10))
RETURNS TABLE
AS
RETURN
(
    SELECT
        mh.MaMH,
        mh.TenMH,
        mh.SoTinChi,
        cthp.DiemTB AS DiemHe10,
        dbo.fn_QuyDoiDiemHe4(cthp.DiemTB) AS DiemHe4,
        dbo.fn_QuyDoiDiemChu(cthp.DiemTB) AS DiemChu,
        dbo.fn_TrangThaiDiemTB(cthp.DiemTB) AS TrangThai
    FROM DangKyMonHoc DKMH
    INNER JOIN LopHocPhan LHP
        ON DKMH.MaLHP = LHP.MaLHP
       AND DKMH.MaHocKyNamHoc = LHP.MaHocKyNamHoc
    INNER JOIN MonHoc mh
        ON LHP.MaMH = mh.MaMH
    OUTER APPLY
    (
        SELECT TOP 1 CTHP.DiemTB
        FROM ChiTietHocPhan CTHP
        WHERE CTHP.MaSV = @MaSV
          AND CTHP.MaLHP = LHP.MaLHP
          AND CTHP.MaHocKyNamHoc = LHP.MaHocKyNamHoc
        ORDER BY CTHP.DiemTB DESC
    ) AS cthp
    WHERE DKMH.MaSV = @MaSV
);
GO


IF OBJECT_ID('dbo.vw_ThongTinChiTietSV', 'V') IS NOT NULL
    DROP VIEW dbo.vw_ThongTinChiTietSV;
GO


CREATE VIEW vw_ThongTinChiTietSV AS
SELECT
    sv.MaSV,
    sv.HoTen,
    dbo.fn_FormattedDate( sv.NgaySinh) as NgaySinh,
    sv.NoiSinh,
    sv.GioiTinh,
    ISNULL(drl.Diem, 0) AS DiemRenLuyen 
FROM SinhVien sv
LEFT JOIN DiemRenLuyen drl ON sv.MaSV = drl.MaSV;
GO


IF OBJECT_ID('dbo.fn_ChiTietHocPhan', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_ChiTietHocPhan;
GO

CREATE FUNCTION dbo.fn_ChiTietHocPhan (@MaHocKyNamHoc INT)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        CTHP.MaSV,
        SV.HoTen,
        LHP.MaMH,
        MH.TenMH,
        CTHP.DiemGK,
        CTHP.DiemCK,
        CTHP.DiemTB,
        dbo.fn_TrangThaiDiemTB(CTHP.DiemTB) AS KetQua
    FROM ChiTietHocPhan CTHP
    INNER JOIN SinhVien SV 
        ON CTHP.MaSV = SV.MaSV
    INNER JOIN LopHocPhan LHP 
        ON CTHP.MaLHP = LHP.MaLHP
       AND CTHP.MaHocKyNamHoc = LHP.MaHocKyNamHoc
    INNER JOIN MonHoc MH 
        ON LHP.MaMH = MH.MaMH
    WHERE CTHP.MaHocKyNamHoc = @MaHocKyNamHoc
);
GO



IF OBJECT_ID('dbo.trg_LogDangNhap', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_LogDangNhap;
GO

CREATE TRIGGER trg_LogDangNhap
ON LogDangNhap
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- In thông báo cho tất cả bản ghi vừa thêm
    SELECT 
        'Người dùng: ' + ISNULL(TenDangNhap, 'NULL') + 
        ' | Kết quả: ' + ISNULL(KetQua, 'NULL') AS ThongBao
    FROM inserted;
END; 
Go



IF OBJECT_ID('dbo.sp_DangNhap', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DangNhap;
GO

CREATE PROCEDURE dbo.sp_DangNhap
    @TenDangNhap NVARCHAR(50),
    @MatKhau NVARCHAR(255),
    @RoleIDtam INT,                -- 1 = Admin, 2 = GiangVien (quyền người dùng chọn)
    @RoleID INT OUTPUT,            -- quyền thực của tài khoản (1 = Admin, 2 = GiangVien)
    @TrangThai BIT OUTPUT, 
    @MaGV VARCHAR(10) OUTPUT,
    @KetQua NVARCHAR(100) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RoleThuc NVARCHAR(20); -- role thực từ database

    BEGIN TRY
        -- Lấy thông tin tài khoản theo TenDangNhap
        SELECT 
            @RoleThuc = r.RoleName,
            @TrangThai = tk.TrangThai,
            @MaGV = tk.MaGV,
            @RoleID = r.Roleid
        FROM TaiKhoan tk
        LEFT JOIN Roles r ON tk.Roleid = r.Roleid
        WHERE tk.TenDangNhap = @TenDangNhap
          AND tk.MatKhau = @MatKhau;

        -- Kiểm tra tài khoản tồn tại
        IF @RoleThuc IS NULL
        BEGIN
            SET @KetQua = N'Tên đăng nhập hoặc mật khẩu không chính xác!';
            INSERT INTO LogDangNhap(TenDangNhap, KetQua) VALUES (@TenDangNhap, @KetQua);
            RETURN;
        END

        -- Kiểm tra trạng thái tài khoản
        IF @TrangThai = 0
        BEGIN
            SET @KetQua = N'Tài khoản đã bị khóa!';
            INSERT INTO LogDangNhap(TenDangNhap, KetQua) VALUES (@TenDangNhap, @KetQua);
            RETURN;
        END

        -- Kiểm tra quyền đăng nhập
        IF (@RoleIDtam = 1 AND @RoleThuc = N'Admin') OR (@RoleIDtam = 2 AND @RoleThuc = N'GiangVien')
        BEGIN
            SET @KetQua = N'Đăng nhập thành công với quyền ' + @RoleThuc;
            INSERT INTO LogDangNhap(TenDangNhap, KetQua) VALUES (@TenDangNhap, @KetQua);
        END
        ELSE
        BEGIN
            SET @KetQua = N'Tài khoản không có quyền này!';
            INSERT INTO LogDangNhap(TenDangNhap, KetQua) VALUES (@TenDangNhap, @KetQua);
            -- Reset thông tin đầu ra nếu không phù hợp quyền
            SET @RoleID = NULL;
            SET @TrangThai = NULL;
            SET @MaGV = NULL;
        END

    END TRY
    BEGIN CATCH
        SET @KetQua = N'Lỗi trong quá trình đăng nhập: ' + ERROR_MESSAGE();
        SET @RoleID = NULL;
        SET @TrangThai = NULL;
        SET @MaGV = NULL;
    END CATCH
END
GO


IF OBJECT_ID('dbo.sp_GetLopHocPhanByGV', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetLopHocPhanByGV;
GO

CREATE PROCEDURE sp_GetLopHocPhanByGV
    @MaHocKyNamHoc INT,
    @MaGV VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT MaLHP
    FROM LopHocPhan
    WHERE MaHocKyNamHoc = @MaHocKyNamHoc
      AND MaGV = @MaGV
    ORDER BY MaLHP;
END;
GO

IF OBJECT_ID('sp_XoaDangKyMonHoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_XoaDangKyMonHoc;
GO

CREATE PROCEDURE sp_XoaDangKyMonHoc
    @MaSV VARCHAR(10),
    @MaLHP VARCHAR(20),
    @MaHocKyNamHoc INT
AS
BEGIN
    SET NOCOUNT ON;
	DELETE FROM ChiTietHocPhan
    DELETE FROM DangKyMonHoc
    WHERE MaSV = @MaSV 
      AND MaLHP = @MaLHP 
      AND MaHocKyNamHoc = @MaHocKyNamHoc;
END;
GO


IF OBJECT_ID('sp_LayLopHocPhanKhac', 'P') IS NOT NULL
    DROP PROCEDURE sp_LayLopHocPhanKhac;
GO

CREATE PROCEDURE sp_LayLopHocPhanKhac
    @MaHocKyNamHoc INT,
    @MaGV VARCHAR(10),
    @MaLHPHienTai VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT MaLHP
    FROM LopHocPhan
    WHERE MaHocKyNamHoc = @MaHocKyNamHoc
      AND MaGV = @MaGV
      AND MaMH = (SELECT MaMH FROM LopHocPhan WHERE MaLHP = @MaLHPHienTai AND MaHocKyNamHoc = @MaHocKyNamHoc)
      AND MaLHP <> @MaLHPHienTai
    ORDER BY MaLHP;
END;
go
IF OBJECT_ID('sp_ChuyenLopHocPhan', 'P') IS NOT NULL
    DROP PROCEDURE sp_ChuyenLopHocPhan;
GO

CREATE PROCEDURE sp_ChuyenLopHocPhan

    @MaSV VARCHAR(10),
    @MaLHPNguon VARCHAR(20),
    @MaLHPDich VARCHAR(20),
	@MaHocKyNamHoc INT

AS
BEGIN
    SET NOCOUNT ON;

    UPDATE DangKyMonHoc
    SET MaLHP = @MaLHPDich
    WHERE MaSV = @MaSV
      AND MaLHP = @MaLHPNguon
	  AND MaHocKyNamHoc = @MaHocKyNamHoc;
END;
go
IF OBJECT_ID('dbo.fn_GetThongTinGV', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_GetThongTinGV;
GO

CREATE FUNCTION dbo.fn_GetThongTinGV(@MaGV VARCHAR(10))
RETURNS TABLE
AS
RETURN
(
    SELECT MaGV, HoTenGV, HocVi, Khoa, Email, DienThoai
    FROM GiangVien
    WHERE MaGV = @MaGV
);
GO


IF OBJECT_ID('dbo.sp_TinhTBVaTinChiDat', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_TinhTBVaTinChiDat;


GO

CREATE PROCEDURE dbo.sp_TinhTBVaTinChiDat
    @MaSV VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        AVG(CTHP.DiemTB) AS DiemTB_He10,
        AVG(dbo.fn_QuyDoiDiemHe4(CTHP.DiemTB)) AS DiemTB_He4,
        SUM(MH.SoTinChi) AS TinChiDat
    FROM ChiTietHocPhan CTHP
    INNER JOIN LopHocPhan LHP
        ON CTHP.MaLHP = LHP.MaLHP
       AND CTHP.MaHocKyNamHoc = LHP.MaHocKyNamHoc
    INNER JOIN MonHoc MH
        ON LHP.MaMH = MH.MaMH
    WHERE CTHP.MaSV = @MaSV
      AND dbo.fn_TrangThaiDiemTB(CTHP.DiemTB) = N'Đạt';

END
GO



IF OBJECT_ID('dbo.sp_LayCongThucTinhDiemMoiNhat', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_LayCongThucTinhDiemMoiNhat;
GO

CREATE PROCEDURE dbo.sp_LayCongThucTinhDiemMoiNhat
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 
        Ma,
        TiLeGK,
        TiLeCK
    FROM CongThucTinhDiem
    ORDER BY Ma DESC;
END
GO


IF OBJECT_ID('dbo.sp_ThemCongThucTinhDiem', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ThemCongThucTinhDiem;
GO

CREATE PROCEDURE sp_ThemCongThucTinhDiem
    @TiLeGK DECIMAL(3,2),
    @TiLeCK DECIMAL(3,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CongThucTinhDiem(TiLeGK, TiLeCK) 
    VALUES(@TiLeGK, @TiLeCK);
END;
GO
IF OBJECT_ID('dbo.sp_XoaGiangVien', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_XoaGiangVien;
GO

CREATE PROCEDURE dbo.sp_XoaGiangVien
    @MaGV VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
		  DELETE DKMH
        FROM DangKyMonHoc DKMH
        INNER JOIN LopHocPhan LHP ON DKMH.MaLHP = LHP.MaLHP
        WHERE LHP.MaGV = @MaGV;
		DELETE FROM LopHocPhan WHERE MaGV = @MaGV;


        DELETE FROM TaiKhoan WHERE MaGV = @MaGV;
        DELETE FROM GiangVien WHERE MaGV = @MaGV;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO


IF OBJECT_ID('dbo.sp_ThemGiangVien', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ThemGiangVien;
GO
CREATE PROCEDURE dbo.sp_ThemGiangVien
    @MaGV VARCHAR(10),
    @HoTenGV NVARCHAR(100),
    @HocVi NVARCHAR(50) = NULL,
    @Khoa NVARCHAR(50) = NULL,
    @Email NVARCHAR(100) = NULL,
    @DienThoai NVARCHAR(15) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM GiangVien WHERE MaGV = @MaGV)
        BEGIN
            RAISERROR('Mã giảng viên ''%s'' đã tồn tại!', 16, 1, @MaGV);
            RETURN;
        END

        INSERT INTO GiangVien (MaGV, HoTenGV, HocVi, Khoa, Email, DienThoai)
        VALUES (@MaGV, @HoTenGV, @HocVi, @Khoa, @Email, @DienThoai);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

         DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
         RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO
IF OBJECT_ID('dbo.sp_CapNhatGiangVien', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CapNhatGiangVien;
GO
CREATE PROCEDURE dbo.sp_CapNhatGiangVien
    @MaGV VARCHAR(10),
    @HoTenGV NVARCHAR(100),
    @HocVi NVARCHAR(50) = NULL,
    @Khoa NVARCHAR(50) = NULL,
    @Email NVARCHAR(100) = NULL,
    @DienThoai NVARCHAR(15) = NULL,
	@TrangThai BIT 

AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE GiangVien 
        SET HoTenGV = @HoTenGV,
            HocVi = @HocVi,
            Khoa = @Khoa,
            Email = @Email,
            DienThoai = @DienThoai,
			TrangThai = @TrangThai

        WHERE MaGV = @MaGV;

        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('Không tìm thấy giảng viên có mã ''%s''', 16, 1, @MaGV);
            RETURN;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

         DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
         RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

IF OBJECT_ID('dbo.sp_XoaSinhVien', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_XoaSinhVien;
GO

CREATE PROCEDURE dbo.sp_XoaSinhVien
		@MaSV VARCHAR(10)
	AS
	BEGIN
		SET NOCOUNT ON;

		BEGIN TRY
			BEGIN TRANSACTION;

			DELETE FROM SinhVien
			WHERE MaSV = @MaSV;

			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0
				ROLLBACK TRANSACTION;

			THROW;
		END CATCH
	END;
	GO



IF OBJECT_ID('dbo.sp_ThemSinhVien', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ThemSinhVien;
GO

CREATE PROCEDURE dbo.sp_ThemSinhVien
    @MaSV VARCHAR(10),
    @HoTen NVARCHAR(100),
    @LopSV VARCHAR(20),
    @NgaySinh DATE = NULL,
    @NoiSinh NVARCHAR(100) = NULL,
    @GioiTinh NVARCHAR(10) = NULL

AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        BEGIN TRANSACTION

        IF EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = @MaSV)
        BEGIN
            RAISERROR('Mã sinh viên ''%s'' đã tồn tại!', 16, 1, @MaSV)
            RETURN
        END

        INSERT INTO SinhVien (MaSV, HoTen, LopSV, NgaySinh, NoiSinh, GioiTinh)
        VALUES (@MaSV, @HoTen, @LopSV, @NgaySinh, @NoiSinh, @GioiTinh)

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
	      DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
 RAISERROR(@ErrMsg, 16, 1);

    END CATCH
END
GO

IF OBJECT_ID('dbo.sp_CapNhatSinhVien', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CapNhatSinhVien;
GO

CREATE PROCEDURE dbo.sp_CapNhatSinhVien
    @MaSV VARCHAR(10),
    @HoTen NVARCHAR(100),
    @LopSV VARCHAR(20),
    @NgaySinh DATE = NULL,
    @NoiSinh NVARCHAR(100) = NULL,
    @GioiTinh NVARCHAR(10) = NULL,
	@TrangThai BIT 


AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE SinhVien
        SET HoTen = @HoTen,
            LopSV = @LopSV,
            NgaySinh = @NgaySinh,
            NoiSinh = @NoiSinh,
            GioiTinh = @GioiTinh,
			TrangThai = @TrangThai
        WHERE MaSV = @MaSV;

        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('Không tìm thấy sinh viên có mã ''%s''', 16, 1, @MaSV);
            RETURN;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
 RAISERROR(@ErrMsg, 16, 1);;
    END CATCH
END
GO

IF OBJECT_ID('sp_TrungBinhMonHoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_TrungBinhMonHoc;
GO

CREATE PROCEDURE sp_TrungBinhMonHoc
    @MaHocKyNamHoc INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        MH.MaMH,
        MH.TenMH,
        COUNT(*) AS SoSV_Tong,
        ROUND(AVG(CTHP.DiemTB), 2) AS DiemTB,
        SUM(CASE WHEN dbo.fn_TrangThaiDiemTB(CTHP.DiemTB) = N'Đạt' THEN 1 ELSE 0 END) AS SoSV_Dat,
        SUM(CASE WHEN dbo.fn_TrangThaiDiemTB(CTHP.DiemTB) = N'Không đạt' THEN 1 ELSE 0 END) AS SoSV_Rot,
        SUM(CASE WHEN CTHP.DiemTB IS NULL THEN 1 ELSE 0 END) AS SoSV_Chuacham
    FROM ChiTietHocPhan CTHP
    INNER JOIN LopHocPhan LHP
        ON CTHP.MaLHP = LHP.MaLHP
       AND CTHP.MaHocKyNamHoc = LHP.MaHocKyNamHoc
    INNER JOIN MonHoc MH
        ON LHP.MaMH = MH.MaMH
    WHERE CTHP.MaHocKyNamHoc = @MaHocKyNamHoc
    GROUP BY MH.MaMH, MH.TenMH
    ORDER BY DiemTB DESC;

END
GO
IF OBJECT_ID('dbo.trg_KhoaTaiKhoan_KhiGVBNgungHoatDong', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_KhoaTaiKhoan_KhiGVBNgungHoatDong;
GO
CREATE TRIGGER trg_KhoaTaiKhoan_KhiGVBNgungHoatDong
ON GiangVien
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Cập nhật TrangThai của TaiKhoan khi GiangVien.TrangThai = 1
    UPDATE tk
    SET tk.TrangThai = 0   -- khóa tài khoản
    FROM TaiKhoan tk
    INNER JOIN inserted i ON tk.MaGV = i.MaGV
    WHERE i.TrangThai = 1;
END;
GO