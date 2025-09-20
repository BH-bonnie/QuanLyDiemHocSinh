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
IF OBJECT_ID('dbo.fn_SinhVienTheoLopHocPhan', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_SinhVienTheoLopHocPhan;
GO
-- Hàm trả về danh sách sinh viên theo lớp học phần
CREATE FUNCTION fn_SinhVienTheoLopHocPhan (
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
);
GO

IF OBJECT_ID('dbo.sp_ChuyenLopTheoGV', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ChuyenLopTheoGV;
GO

CREATE PROCEDURE dbo.sp_ChuyenLopTheoGV
    @MaSV VARCHAR(10),
    @MaLHPHienTai VARCHAR(20),
    @MaLHPMoi VARCHAR(20),
    @MaGV VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @MaMH VARCHAR(20);
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
    INSERT INTO ChiTietHocPhan (MaSV, MaMH, DiemGK, DiemCK, MaHocKyNamHoc)
    SELECT 
        i.MaSV,
        LHP.MaMH,
        NULL,
        NULL,
        i.MaHocKyNamHoc
    FROM inserted i
    JOIN LopHocPhan LHP
      ON i.MaLHP = LHP.MaLHP 
     AND i.MaHocKyNamHoc = LHP.MaHocKyNamHoc;
END;








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
        SET CTHP.DiemGK = @DiemGK,
            CTHP.DiemCK = @DiemCK
        FROM ChiTietHocPhan CTHP
        INNER JOIN LopHocPhan LHP
            ON CTHP.MaMH = LHP.MaMH
           AND CTHP.MaHocKyNamHoc = LHP.MaHocKyNamHoc
        WHERE CTHP.MaSV = @MaSV
          AND CTHP.MaMH = @MaMH
          AND CTHP.MaHocKyNamHoc = @MaHocKyNamHoc;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        -- Có thể log lỗi ra hoặc trả về mã lỗi
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
    @MaLHP VARCHAR(20),
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

go
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

CREATE PROCEDURE sp_DangNhap
    @TenDangNhap NVARCHAR(50),
    @MatKhau NVARCHAR(255),
    @Role NVARCHAR(20),
    @Quyen NVARCHAR(20) OUTPUT,
    @TrangThai BIT OUTPUT,
    @MaGV VARCHAR(10) OUTPUT,
    @KetQua NVARCHAR(100) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Lấy thông tin tài khoản
        SELECT @Quyen = Quyen, @TrangThai = TrangThai, @MaGV = MaGV
        FROM TaiKhoan
        WHERE TenDangNhap = @TenDangNhap
          AND MatKhau = @MatKhau;

        IF @Quyen IS NULL
        BEGIN
            SET @KetQua = N'Tên đăng nhập hoặc mật khẩu không chính xác!';
			INSERT INTO LogDangNhap(TenDangNhap, KetQua) 
			 VALUES (@TenDangNhap, @KetQua);
            RETURN;
			
        END

        IF @TrangThai = 0
        BEGIN
            SET @KetQua = N'Tài khoản đã bị khóa!';
			INSERT INTO LogDangNhap(TenDangNhap, KetQua) 
			VALUES (@TenDangNhap, @KetQua);
            RETURN;
        END

        IF (@Role = 'Admin' AND @Quyen = 'Admin') OR (@Role = 'GV' AND @Quyen = 'GiangVien')
        BEGIN
            SET @KetQua = N'Đăng nhập thành công với quyền ' + @Quyen;
			INSERT INTO LogDangNhap(TenDangNhap, KetQua) 
			VALUES (@TenDangNhap, @KetQua);
        END
        ELSE
        BEGIN
            SET @KetQua = N'Tài khoản này không có quyền ' + @Role + N'!';
			INSERT INTO LogDangNhap(TenDangNhap, KetQua) 
			VALUES (@TenDangNhap, @KetQua);
        END
		
    END TRY
    BEGIN CATCH
        SET @KetQua = N'Lỗi trong quá trình đăng nhập: ' + ERROR_MESSAGE();
    END CATCH
END
go
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

-- Xóa thủ tục cũ nếu có
IF OBJECT_ID('sp_XoaDangKyMonHoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_XoaDangKyMonHoc;
GO

-- Thủ tục xóa đăng ký môn học
CREATE PROCEDURE sp_XoaDangKyMonHoc
    @MaSV VARCHAR(10),
    @MaLHP VARCHAR(20),
    @MaHocKyNamHoc INT
AS
BEGIN
    SET NOCOUNT ON;

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

IF OBJECT_ID('dbo.sp_UpdateGiangVienContact', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateGiangVienContact;
GO

CREATE PROCEDURE dbo.sp_UpdateGiangVienContact
    @MaGV VARCHAR(10),
    @Email VARCHAR(100),
    @DienThoai VARCHAR(15)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE GiangVien
    SET Email = @Email,
        DienThoai = @DienThoai
    WHERE MaGV = @MaGV;
END;
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
        AVG(dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK)) AS DiemTB_He10,
        AVG(dbo.fn_QuyDoiDiemHe4(dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK))) AS DiemTB_He4,
        SUM(MH.SoTinChi) AS TinChiDat
    FROM ChiTietHocPhan CTHP
    INNER JOIN MonHoc MH ON CTHP.MaMH = MH.MaMH
    WHERE CTHP.MaSV = @MaSV
      AND dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK) >=5;
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

        THROW;
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
    @DienThoai NVARCHAR(15) = NULL
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
            DienThoai = @DienThoai
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

        THROW;
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
IF OBJECT_ID('dbo.sp_ThemSinhVien', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ThemSinhVien;
GO

CREATE PROCEDURE dbo.sp_ThemSinhVien
    @MaSV VARCHAR(10),
    @HoTen NVARCHAR(100),
    @LopSV VARCHAR(20),
    @NgaySinh DATE = NULL,
    @NoiSinh NVARCHAR(100) = NULL,
    @GioiTinh NVARCHAR(10) = NULL,
    @CMND_CCCD VARCHAR(20) = NULL
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

        INSERT INTO SinhVien (MaSV, HoTen, LopSV, NgaySinh, NoiSinh, GioiTinh, CMND_CCCD)
        VALUES (@MaSV, @HoTen, @LopSV, @NgaySinh, @NoiSinh, @GioiTinh, @CMND_CCCD)

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
        THROW
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
    @CMND_CCCD VARCHAR(20) = NULL
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
            CMND_CCCD = @CMND_CCCD
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
        THROW;
    END CATCH
END
GO
-- Xóa thủ tục cũ nếu có
IF OBJECT_ID('sp_TrungBinhMonHoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_TrungBinhMonHoc;
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
        -- Điểm trung bình chỉ tính sinh viên đã có điểm
        ROUND(AVG(CASE 
                    WHEN CTHP.DiemGK IS NOT NULL AND CTHP.DiemCK IS NOT NULL
                    THEN dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK)
                 END), 2) AS DiemTB,

       

        -- Số sinh viên đạt
        SUM(CASE 
                WHEN CTHP.DiemGK IS NOT NULL AND CTHP.DiemCK IS NOT NULL
                     AND dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK) >= 5 
                THEN 1 ELSE 0 
            END) AS SoSV_Dat,

        -- Số sinh viên rớt
        SUM(CASE 
                WHEN CTHP.DiemGK IS NOT NULL AND CTHP.DiemCK IS NOT NULL
                     AND dbo.fn_TinhDiemTrungBinh(CTHP.DiemGK, CTHP.DiemCK) < 5 
                THEN 1 ELSE 0 
            END) AS SoSV_Rot,
			 -- Số sinh viên chưa chấm
        SUM(CASE 
                WHEN CTHP.DiemGK IS NULL OR CTHP.DiemCK IS NULL THEN 1 ELSE 0 
            END) AS SoSV_Chuacham

        -- Tổng số sinh viên đăng ký môn
       
    FROM ChiTietHocPhan CTHP
    INNER JOIN MonHoc MH ON CTHP.MaMH = MH.MaMH
    WHERE CTHP.MaHocKyNamHoc = @MaHocKyNamHoc
    GROUP BY MH.MaMH, MH.TenMH
    ORDER BY DiemTB DESC;
END
GO


