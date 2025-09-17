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
        LHP.MaGV,
        GV.HoTenGV,
		GV.Email
    FROM LopHocPhan LHP
    INNER JOIN MonHoc MH ON LHP.MaMH = MH.MaMH
    INNER JOIN GiangVien GV ON LHP.MaGV = GV.MaGV
    INNER JOIN HocKyNamHoc HKNH ON LHP.MaHocKyNamHoc = HKNH.MaHocKyNamHoc
    WHERE LHP.MaHocKyNamHoc = @MaHocKyNamHoc
);
GO

IF OBJECT_ID('dbo.v_MonHoc', 'V') IS NOT NULL
    DROP VIEW dbo.v_MonHoc;
GO

-- Tạo view
CREATE VIEW dbo.v_MonHoc
AS
SELECT 
    MaMH,       -- Mã môn học
    TenMH,      -- Tên môn học
    SoTinChi    -- Số tín chỉ
FROM MonHoc;
GO
-- Nếu đã có function cũ, drop trước
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
    WHERE DKMH.MaHocKyNamHoc = @MaHocKyNamHoc
);
GO


-- Nếu hàm đã tồn tại thì xóa trước
IF OBJECT_ID('dbo.fn_DanhSachMonHoc_GiangVien', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_DanhSachMonHoc_GiangVien;
GO

-- Tạo hàm trả về danh sách môn học giảng viên dạy theo năm học
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
    WHERE LHP.MaGV = @MaGV
      AND LHP.MaHocKyNamHoc = @MaHocKyNamHoc
);
GO



--- Nếu hàm đã tồn tại thì xóa trước
IF OBJECT_ID('dbo.fn_SinhVienTheoLopHocPhan', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_SinhVienTheoLopHocPhan;
GO

-- Tạo hàm trả về danh sách sinh viên theo lớp học phần và MaHocKyNamHoc
-- Nếu đã tồn tại thì xóa hàm
IF OBJECT_ID('dbo.fn_FormattedDate', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_FormattedDate;
GO

-- Hàm chuyển đổi ngày sang dd/MM/yyyy
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

-- Hàm trả về danh sách sinh viên theo lớp học phần
CREATE FUNCTION fn_SinhVienTheoLopHocPhan (
    @MaLHP VARCHAR(10),
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
);
GO

IF OBJECT_ID('dbo.sp_ChuyenLopTheoGV', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ChuyenLopTheoGV;
GO

CREATE PROCEDURE dbo.sp_ChuyenLopTheoGV
    @MaSV VARCHAR(10),
    @MaLHPHienTai VARCHAR(10),
    @MaLHPMoi VARCHAR(10),
    @MaGV VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @MaMH VARCHAR(10);
        DECLARE @MaHocKyNamHoc INT;

        -- Lấy thông tin lớp hiện tại
        SELECT @MaMH = MaMH,
               @MaHocKyNamHoc = MaHocKyNamHoc
        FROM LopHocPhan
        WHERE MaLHP = @MaLHPHienTai;


        -- Xóa sinh viên khỏi lớp hiện tại
        DELETE FROM DangKyMonHoc
        WHERE MaSV = @MaSV AND MaLHP = @MaLHPHienTai;

        -- Thêm sinh viên vào lớp mới (nếu chưa có)
        IF NOT EXISTS (
            SELECT 1
            FROM DangKyMonHoc
            WHERE MaSV = @MaSV
              AND MaLHP = @MaLHPMoi
        )
        BEGIN
            INSERT INTO DangKyMonHoc (MaSV, MaLHP, MaHocKyNamHoc)
            VALUES (@MaSV, @MaLHPMoi, @MaHocKyNamHoc);
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO




IF OBJECT_ID('trg_AfterInsert_DangKyMonHoc', 'TR') IS NOT NULL
    DROP TRIGGER trg_AfterInsert_DangKyMonHoc;
GO


CREATE TRIGGER trg_AfterInsert_DangKyMonHoc
ON DangKyMonHoc
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO ChiTietHocPhan(MaSV, MaMH, DiemGK, DiemCK, MaHocKyNamHoc)
    SELECT 
        i.MaSV, 
        LHP.MaMH, 
        NULL, 
        NULL,
        i.MaHocKyNamHoc      
    FROM inserted i
    INNER JOIN LopHocPhan LHP ON i.MaLHP = LHP.MaLHP
    WHERE NOT EXISTS (
        SELECT 1 
        FROM ChiTietHocPhan CTHP
        WHERE CTHP.MaSV = i.MaSV 
          AND CTHP.MaMH = LHP.MaMH
          AND CTHP.MaHocKyNamHoc = i.MaHocKyNamHoc       
    );
END
GO





IF OBJECT_ID('dbo.fn_SinhVienVaDiemTheoLopHocPhan', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_SinhVienVaDiemTheoLopHocPhan;
GO

CREATE FUNCTION fn_SinhVienVaDiemTheoLopHocPhan
(
    @MaLHP VARCHAR(10),
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
        dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK) AS DiemTB,
        CASE 
            WHEN CTHP.DiemGK IS NULL OR CTHP.DiemCK IS NULL THEN NULL
            WHEN dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK) >= 5 THEN N'Đạt'
            ELSE N'Không đạt'
        END AS TrangThai
    FROM DangKyMonHoc DKMH
    INNER JOIN SinhVien SV ON DKMH.MaSV = SV.MaSV
    INNER JOIN LopHocPhan LHP ON DKMH.MaLHP = LHP.MaLHP
    LEFT JOIN ChiTietHocPhan CTHP
        ON CTHP.MaSV = SV.MaSV
       AND CTHP.MaMH = LHP.MaMH
       AND CTHP.MaHocKyNamHoc = LHP.MaHocKyNamHoc
    WHERE LHP.MaLHP = @MaLHP
      AND LHP.MaHocKyNamHoc = @MaHocKyNamHoc
);
GO



IF OBJECT_ID('dbo.sp_CapNhatDiemHocPhan', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CapNhatDiemHocPhan;
GO
CREATE PROCEDURE dbo.sp_CapNhatDiemHocPhan
    @MaSV VARCHAR(10),
    @MaMH VARCHAR(10),
    @MaHocKyNamHoc INT,
    @DiemGK DECIMAL(4,2),
    @DiemCK DECIMAL(4,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CTHP
    SET CTHP.DiemGK = @DiemGK,
        CTHP.DiemCK = @DiemCK
    FROM ChiTietHocPhan CTHP
    INNER JOIN LopHocPhan LHP
        ON CTHP.MaMH = LHP.MaMH
       AND CTHP.MaHocKyNamHoc = LHP.MaHocKyNamHoc
    WHERE CTHP.MaSV = @MaSV
      
END
GO


IF OBJECT_ID('dbo.sp_ThongKeDiemLopHocPhan', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ThongKeDiemLopHocPhan;
GO

CREATE PROCEDURE dbo.sp_ThongKeDiemLopHocPhan
    @MaLHP VARCHAR(10),
    @MaHocKyNamHoc INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        COUNT(*) AS TongSoSinhVien,
        SUM(CASE WHEN TrangThai = N'Đạt' THEN 1 ELSE 0 END) AS SoSinhVienDat,
        SUM(CASE WHEN TrangThai = N'Không đạt' THEN 1 ELSE 0 END) AS SoSinhVienRớt,
        SUM(CASE WHEN TrangThai IS NULL THEN 1 ELSE 0 END) AS SoSinhVienChuaCham
    FROM fn_SinhVienVaDiemTheoLopHocPhan(@MaLHP, @MaHocKyNamHoc);
END
GO
IF OBJECT_ID('dbo.sp_ThongKeDiemTheoKhoangNho', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ThongKeDiemTheoKhoangNho;
GO

CREATE PROCEDURE dbo.sp_ThongKeDiemTheoKhoangNho
    @MaLHP VARCHAR(10),
    @MaHocKyNamHoc INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        COUNT(*) AS TongSoSinhVien,
        SUM(CASE WHEN DiemTB >= 0 AND DiemTB < 1 THEN 1 ELSE 0 END) AS Khoang0_1,
        SUM(CASE WHEN DiemTB >= 1 AND DiemTB < 2 THEN 1 ELSE 0 END) AS Khoang1_2,
        SUM(CASE WHEN DiemTB >= 2 AND DiemTB < 3 THEN 1 ELSE 0 END) AS Khoang2_3,
        SUM(CASE WHEN DiemTB >= 3 AND DiemTB < 4 THEN 1 ELSE 0 END) AS Khoang3_4,
        SUM(CASE WHEN DiemTB >= 4 AND DiemTB < 5 THEN 1 ELSE 0 END) AS Khoang4_5,
        SUM(CASE WHEN DiemTB >= 5 AND DiemTB < 6 THEN 1 ELSE 0 END) AS Khoang5_6,
        SUM(CASE WHEN DiemTB >= 6 AND DiemTB < 7 THEN 1 ELSE 0 END) AS Khoang6_7,
        SUM(CASE WHEN DiemTB >= 7 AND DiemTB < 8 THEN 1 ELSE 0 END) AS Khoang7_8,
        SUM(CASE WHEN DiemTB >= 8 AND DiemTB < 9 THEN 1 ELSE 0 END) AS Khoang8_9,
        SUM(CASE WHEN DiemTB >= 9 AND DiemTB <= 10 THEN 1 ELSE 0 END) AS Khoang9_10
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
GOIF OBJECT_ID('dbo.fn_QuyDoiDiemChu', 'FN') IS NOT NULL
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

    -- Lấy tỉ lệ GK, CK từ bảng công thức
    SELECT TOP 1 
           @TiLeGK = TiLeGK, 
           @TiLeCK = TiLeCK
    FROM CongThucTinhDiem
	ORDER BY Ma DESC;

    -- Nếu không có tỉ lệ, mặc định 0.5 / 0.5
    IF @TiLeGK IS NULL OR @TiLeCK IS NULL
    BEGIN
        SET @TiLeGK = 0.5;
        SET @TiLeCK = 0.5;
    END

    -- Nếu một trong hai điểm là NULL, trả về NULL
    IF @DiemGK IS NULL OR @DiemCK IS NULL
    BEGIN
        RETURN NULL;
    END

    -- Nếu điểm CK < 3 thì kết quả = 0
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
        dbo.fn_TinhDiemTrungBinh(cthp.DiemGK, cthp.DiemCK) AS DiemHe10,
        dbo.fn_QuyDoiDiemHe4(dbo.fn_TinhDiemTrungBinh(cthp.DiemGK, cthp.DiemCK)) AS DiemHe4,
        dbo.fn_QuyDoiDiemChu(dbo.fn_TinhDiemTrungBinh(cthp.DiemGK, cthp.DiemCK)) AS DiemChu,
        CASE 
            WHEN dbo.fn_TinhDiemTrungBinh(cthp.DiemGK, cthp.DiemCK) IS NULL THEN N''
            WHEN dbo.fn_TinhDiemTrungBinh(cthp.DiemGK, cthp.DiemCK) >= 5.0 THEN N'Đậu'
            ELSE N'Rớt'
        END AS TrangThai
    FROM MonHoc mh
    OUTER APPLY
    (
        SELECT TOP 1 *
        FROM ChiTietHocPhan cthp
        WHERE cthp.MaSV = @MaSV
          AND cthp.MaMH = mh.MaMH
        ORDER BY dbo.fn_TinhDiemTrungBinh(cthp.DiemGK, cthp.DiemCK) DESC
    ) AS cthp
);

IF OBJECT_ID('dbo.vw_ThongTinChiTietSV', 'V') IS NOT NULL
    DROP VIEW dbo.vw_ThongTinChiTietSV;
GO


CREATE VIEW vw_ThongTinChiTietSV AS
SELECT
    sv.MaSV,
    sv.HoTen,
    sv.NgaySinh,
    sv.NoiSinh,
    sv.GioiTinh,
    ISNULL(drl.Diem, 0) AS DiemRenLuyen -- nếu chưa có điểm rèn luyện thì trả về 0
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
        SV.HoTen ,
        CTHP.MaMH,
        MH.TenMH,
        CTHP.DiemGK,
        CTHP.DiemCK,
        dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK) AS DiemTB,
        CASE 
			WHEN CTHP.DiemGK IS NULL OR CTHP.DiemCK IS NULL THEN NULL
            WHEN dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK) >= 5 THEN N'Đạt'
            ELSE N'Không đạt'
        END AS KetQua
    FROM ChiTietHocPhan CTHP
    INNER JOIN SinhVien SV ON CTHP.MaSV = SV.MaSV
    INNER JOIN MonHoc MH ON CTHP.MaMH = MH.MaMH
    WHERE CTHP.MaHocKyNamHoc = @MaHocKyNamHoc
);
GO
SELECT * FROM fn_ChiTietHocPhan( 4)