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
IF OBJECT_ID('dbo.sp_ChuyenLopCungMonHoc', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ChuyenLopCungMonHoc;
GO
IF OBJECT_ID('dbo.sp_ChuyenLopTheoGV', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ChuyenLopTheoGV;
GO

CREATE PROCEDURE dbo.sp_ChuyenLopTheoGV
    @MaSV VARCHAR(10),
    @MaLHPHienTai VARCHAR(10),
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

        -- Kiểm tra giảng viên hiện tại có dạy môn này không
        IF NOT EXISTS (
            SELECT 1
            FROM LopHocPhan
            WHERE MaLHP = @MaLHPHienTai
              AND MaGV = @MaGV
              AND MaMH = @MaMH
        )
        BEGIN
            -- Không làm gì cả, chỉ thông báo lỗi
            THROW 50003, 'Giảng viên không được phép chuyển sinh viên trong môn này.', 1;
        END

        -- Kiểm tra sinh viên có tồn tại
        IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = @MaSV)
        BEGIN
            THROW 50001, 'Mã sinh viên không tồn tại.', 1;
        END

        -- Xóa sinh viên khỏi tất cả các lớp khác cùng môn học
        DELETE DKMH
        FROM DangKyMonHoc DKMH
        INNER JOIN LopHocPhan LHP ON DKMH.MaLHP = LHP.MaLHP
        WHERE DKMH.MaSV = @MaSV
          AND LHP.MaMH = @MaMH
          AND DKMH.MaLHP <> @MaLHPHienTai;

        -- Thêm sinh viên vào lớp hiện tại (chỉ khi thỏa điều kiện ở trên)
        IF NOT EXISTS (
            SELECT 1
            FROM DangKyMonHoc
            WHERE MaSV = @MaSV
              AND MaLHP = @MaLHPHienTai
        )
        BEGIN
            INSERT INTO DangKyMonHoc (MaSV, MaLHP, MaHocKyNamHoc)
            VALUES (@MaSV, @MaLHPHienTai, @MaHocKyNamHoc);
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

