	USE QL_SinhVien;
	GO
IF OBJECT_ID('dbo.v_LopHocPhan_Detail', 'V') IS NOT NULL
    DROP VIEW dbo.v_LopHocPhan_Detail;
GO
	
CREATE VIEW v_LopHocPhan_Detail AS
SELECT 
    HKNH.NamHoc,
    LHP.MaLHP,
    LHP.MaMH,
    MH.TenMH,
    LHP.MaGV,
    GV.HoTenGV,
    GV.Khoa
FROM LopHocPhan LHP
INNER JOIN MonHoc MH ON LHP.MaMH = MH.MaMH
INNER JOIN GiangVien GV ON LHP.MaGV = GV.MaGV
INNER JOIN HocKyNamHoc HKNH ON LHP.MaHocKyNamHoc = HKNH.MaHocKyNamHoc;
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
    FROM CongThucTinhDiem;

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
        SET @DiemTB = 0;
    END
    ELSE
    BEGIN
        SET @DiemTB = ROUND(@DiemGK * @TiLeGK + @DiemCK * @TiLeCK, 2);
    END

    RETURN @DiemTB;
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
    @MaLHP VARCHAR(10),
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
      AND LHP.MaLHP = @MaLHP;
END
GO
