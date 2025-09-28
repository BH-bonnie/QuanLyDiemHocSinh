-- Bước 1: chọn database
USE QL_SinhVien;
GO


-- Admin



-- Tạo Role trong Database
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'AdminRole')
    CREATE ROLE AdminRole;
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'GiangVienRole')
    CREATE ROLE GiangVienRole;
GO


GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON DATABASE::QL_SinhVien TO [AdminRole];
GRANT CONTROL ON SCHEMA::dbo TO [AdminRole];  -- Quyền cao hơn nếu cần
-- Quyền SELECT trên các bảng cần thiết
	GRANT SELECT ON dbo.SinhVien TO GiangVienRole;
	GRANT SELECT ON dbo.GiangVien TO GiangVienRole;
	GRANT SELECT ON dbo.MonHoc TO GiangVienRole;
	GRANT SELECT ON dbo.LopHocPhan TO GiangVienRole;
	GRANT SELECT ON dbo.HocKyNamHoc TO GiangVienRole;

	-- Quyền SELECT, UPDATE trên ChiTietHocPhan
	GRANT SELECT, UPDATE ON dbo.ChiTietHocPhan TO GiangVienRole;

	-- Quyền EXECUTE trên các stored procedure cần thiết
	GRANT EXECUTE ON dbo.sp_GetMaHocKyMoiNhat TO GiangVienRole;
	GRANT EXECUTE ON dbo.sp_DanhSachHocKyNamHoc TO GiangVienRole;

	GRANT EXECUTE ON dbo.sp_CapNhatDiemHocPhan TO GiangVienRole;
	GRANT EXECUTE ON dbo.sp_ThongKeDiemLopHocPhan TO GiangVienRole;
	GRANT EXECUTE ON dbo.sp_ThongKeDiemTheoKhoangNho TO GiangVienRole;
	GRANT EXECUTE ON dbo.sp_GetLopHocPhanByGV TO GiangVienRole;
	GRANT EXECUTE ON dbo.sp_LayLopHocPhanKhac TO GiangVienRole;
	GRANT EXECUTE ON dbo.sp_ChuyenLopHocPhan TO GiangVienRole;
	GRANT EXECUTE ON dbo.sp_CapNhatGiangVien TO GiangVienRole;
	GRANT EXECUTE ON dbo.sp_XoaDangKyMonHoc TO GiangVienRole;



	-- Quyền SELECT trên view
	GRANT SELECT ON dbo.vw_ThongTinChiTietSV TO GiangVienRole;


	-- Quyền SELECT trên Table-Valued Function
	GRANT SELECT ON dbo.fn_SinhVienVaDiemTheoLopHocPhan TO GiangVienRole;
	GRANT SELECT ON dbo.fn_GetThongTinGV TO GiangVienRole;
	GRANT SELECT ON dbo.fn_SinhVienTheoLopHocPhan TO GiangVienRole;

	GRANT SELECT ON dbo.fn_ChiTietDiemSV TO GiangVienRole;
	GRANT SELECT ON dbo.fn_DanhSachMonHoc_GiangVien TO GiangVienRole;



GO

/*SELECT 
    p.class_desc,
    p.permission_name,
    p.state_desc AS permission_state,
    o.name AS object_name,
    pr.name AS principal_name
FROM sys.database_permissions p
    LEFT JOIN sys.objects o ON p.major_id = o.object_id
    LEFT JOIN sys.database_principals pr ON p.grantee_principal_id = pr.principal_id
WHERE pr.name = 'GiangVienRole'
ORDER BY p.class_desc, o.name;
SELECT 
    sp.name AS LoginName,
    rp.name AS ServerRole
FROM sys.server_role_members rm
JOIN sys.server_principals rp ON rm.role_principal_id = rp.principal_id
JOIN sys.server_principals sp ON rm.member_principal_id = sp.principal_id
WHERE sp.name = 'gv2';
EXEC sp_helpuser;  -- Xem toàn bộ user và role
*/




IF OBJECT_ID('vw_ThongTinTaiKhoan', 'V') IS NOT NULL
    DROP VIEW vw_ThongTinTaiKhoan;
GO

CREATE VIEW vw_ThongTinTaiKhoan
AS
SELECT
    tk.MaTK,         
    tk.TenDangNhap,
    tk.MatKhau,
    tk.TrangThai,
    tk.ThoiGian,
    r.Roleid,         
    tk.MaGV
FROM TaiKhoan tk
LEFT JOIN Roles r ON tk.Roleid = r.Roleid
LEFT JOIN GiangVien gv ON tk.MaGV = gv.MaGV;
GO
Select * from Roles
Select * From vw_ThongTinTaiKhoan


IF OBJECT_ID('dbo.sp_ThemTaiKhoan', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ThemTaiKhoan;
GO

CREATE PROCEDURE dbo.sp_ThemTaiKhoan
    @TenDangNhap NVARCHAR(50),
    @MatKhau NVARCHAR(255),
    @Roleid INT = NULL,
    @MaGV VARCHAR(10) = NULL,
    @TrangThai BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Kiểm tra trùng TenDangNhap
        IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap)
        BEGIN
            RAISERROR('Tên đăng nhập ''%s'' đã tồn tại!', 16, 1, @TenDangNhap);
            RETURN;
        END
		-- Kiểm tra giảng viên đã có tài khoản với vai trò này chưa
		IF EXISTS (SELECT 1 FROM TaiKhoan WHERE MaGV = @MaGV AND Roleid = @Roleid)
		BEGIN
			RAISERROR('Mã giảng viên ''%s'' đã có tài khoản với vai trò này!', 16, 1, @MaGV);
			RETURN;
		END


        -- Thêm bản ghi vào bảng TaiKhoan
        INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Roleid, MaGV, TrangThai)
        VALUES (@TenDangNhap, @MatKhau, @Roleid, @MaGV, @TrangThai);

        DECLARE @sql NVARCHAR(MAX);

        -- Tạo LOGIN cho cả Admin và GiangVien
        SET @sql = '
            IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = ''' + @TenDangNhap + ''')
            CREATE LOGIN [' + @TenDangNhap + '] WITH PASSWORD = ''' + REPLACE(@MatKhau,'''','''''') + ''';
        ';
        EXEC(@sql);
		SET @sql = 'ALTER LOGIN [' + @TenDangNhap + '] WITH DEFAULT_DATABASE = [QL_SinhVien];';
        EXEC(@sql);
        -- Tạo USER cho cả Admin và GiangVien
        SET @sql = '
            IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = ''' + @TenDangNhap + ''')
            CREATE USER [' + @TenDangNhap + '] FOR LOGIN [' + @TenDangNhap + '];
			ALTER LOGIN [' + @TenDangNhap + '] ENABLE;
        ';
		
        EXEC(@sql);
		SET @sql = 'USE QL_SinhVien; GRANT CONNECT TO [' + @TenDangNhap + '];';
        EXEC(@sql);

        IF @Roleid = 1  -- Admin
        BEGIN
            -- Gán sysadmin
            SET @sql = 'ALTER SERVER ROLE [sysadmin] ADD MEMBER [' + @TenDangNhap + '];';
            EXEC(@sql);

            -- Thêm vào AdminRole
            SET @sql = 'ALTER ROLE [AdminRole] ADD MEMBER [' + @TenDangNhap + '];';
            EXEC(@sql);
        END
        ELSE IF @Roleid = 2  -- GiangVien
        BEGIN
            -- Thêm vào GiangVienRole
            SET @sql = 'ALTER ROLE [GiangVienRole] ADD MEMBER [' + @TenDangNhap + '];';
            EXEC(@sql);
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
			 ROLLBACK TRANSACTION
         DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
         RAISERROR(@ErrMsg, 16, 1);        
    END CATCH
END
GO
-- =========================
-- XÓA TÀI KHOẢN
-- =========================

IF OBJECT_ID('dbo.sp_XoaTaiKhoanForce', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_XoaTaiKhoanForce;
GO

CREATE PROCEDURE dbo.sp_XoaTaiKhoanForce
    @MaTK INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TenDangNhap NVARCHAR(50);
    DECLARE @spid INT;
    DECLARE @sql NVARCHAR(MAX);

    -- Lấy tên login trước
    SELECT @TenDangNhap = TenDangNhap FROM TaiKhoan WHERE MaTK = @MaTK;
    IF @TenDangNhap IS NULL
    BEGIN
        RAISERROR('Không tìm thấy tài khoản có MaTK = %d', 16, 1, @MaTK);
        RETURN;
    END

    -- =====================
    -- Kill tất cả session của login (bên ngoài transaction)
    -- =====================
    DECLARE cur_sessions CURSOR FOR
        SELECT session_id
        FROM sys.dm_exec_sessions
        WHERE login_name = @TenDangNhap;

    OPEN cur_sessions;
    FETCH NEXT FROM cur_sessions INTO @spid;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @sql = 'KILL ' + CAST(@spid AS NVARCHAR(10));
        EXEC(@sql);
        FETCH NEXT FROM cur_sessions INTO @spid;
    END

    CLOSE cur_sessions;
    DEALLOCATE cur_sessions;

    -- =====================
    -- Xóa user/login/TaiKhoan trong transaction
    -- =====================
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Xóa khỏi tất cả role database
        DECLARE @sqlRemove NVARCHAR(MAX) = '';
        SELECT @sqlRemove = @sqlRemove + 'ALTER ROLE [' + r.name + '] DROP MEMBER [' + @TenDangNhap + ']; '
        FROM sys.database_role_members drm
        JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
        JOIN sys.database_principals u ON drm.member_principal_id = u.principal_id
        WHERE u.name = @TenDangNhap;

        IF @sqlRemove <> ''
        BEGIN
            EXEC(@sqlRemove);
        END

        -- Xóa user database
        IF EXISTS (SELECT * FROM sys.database_principals WHERE name = @TenDangNhap)
        BEGIN
            SET @sql = 'DROP USER [' + @TenDangNhap + ']';
            EXEC(@sql);
        END

        -- Xóa login server
        IF EXISTS (SELECT * FROM sys.server_principals WHERE name = @TenDangNhap)
        BEGIN
            SET @sql = 'DROP LOGIN [' + @TenDangNhap + ']';
            EXEC(@sql);
        END

        DELETE FROM TaiKhoan WHERE MaTK = @MaTK;

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


IF OBJECT_ID('dbo.sp_CapNhatTaiKhoan', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CapNhatTaiKhoan;
GO

CREATE PROCEDURE dbo.sp_CapNhatTaiKhoan
    @MaTK INT,
    @TenDangNhap NVARCHAR(50) = NULL,
    @MatKhau NVARCHAR(255) = NULL,
    @Roleid INT = NULL,
    @MaGV VARCHAR(10) = NULL,
    @TrangThai BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TenDangNhapCu NVARCHAR(50);
    DECLARE @RoleidCu INT;
    DECLARE @LoginName NVARCHAR(50);
    DECLARE @sql NVARCHAR(MAX);
    DECLARE @PwdSafe NVARCHAR(255);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Lấy thông tin cũ
        SELECT @TenDangNhapCu = TenDangNhap, @RoleidCu = Roleid
        FROM TaiKhoan
        WHERE MaTK = @MaTK;

        IF @TenDangNhapCu IS NULL
        BEGIN
            RAISERROR('Không tìm thấy tài khoản có MaTK = %d', 16, 1, @MaTK);
            RETURN;
        END

        -- Kiểm tra MaGV hợp lệ nếu được cung cấp
        IF @MaGV IS NOT NULL AND NOT EXISTS (SELECT 1 FROM GiangVien WHERE MaGV = @MaGV)
        BEGIN
            RAISERROR('Mã giảng viên ''%s'' không tồn tại!', 16, 1, @MaGV);
            RETURN;
        END

        -- Cập nhật bảng TaiKhoan
        UPDATE TaiKhoan
        SET TenDangNhap = ISNULL(@TenDangNhap, TenDangNhap),
            MatKhau = ISNULL(@MatKhau, MatKhau),
            Roleid = ISNULL(@Roleid, Roleid),
            MaGV = ISNULL(@MaGV, MaGV),
            TrangThai = ISNULL(@TrangThai, TrangThai)
        WHERE MaTK = @MaTK;

        SET @LoginName = ISNULL(@TenDangNhap, @TenDangNhapCu);

        -- Nếu TenDangNhap thay đổi, đổi tên login và user
        IF @TenDangNhap IS NOT NULL AND @TenDangNhap <> @TenDangNhapCu
        BEGIN
            IF EXISTS (SELECT * FROM sys.server_principals WHERE name = @TenDangNhapCu)
            BEGIN
                SET @sql = 'ALTER LOGIN ' + QUOTENAME(@TenDangNhapCu) + ' WITH NAME = ' + QUOTENAME(@LoginName) + ';';
                PRINT @sql; -- In lệnh SQL để kiểm tra
                EXEC(@sql);
            END

            IF EXISTS (SELECT * FROM sys.database_principals WHERE name = @TenDangNhapCu)
            BEGIN
                SET @sql = 'ALTER USER ' + QUOTENAME(@TenDangNhapCu) + ' WITH NAME = ' + QUOTENAME(@LoginName) + ';';
                PRINT @sql; -- In lệnh SQL để kiểm tra
                EXEC(@sql);
            END
        END

        -- Nếu MatKhau thay đổi, cập nhật login
        IF @MatKhau IS NOT NULL
        BEGIN
            SET @PwdSafe = REPLACE(@MatKhau, '''', '''''');  -- escape dấu nháy đơn
            IF EXISTS (SELECT * FROM sys.server_principals WHERE name = @LoginName)
            BEGIN
                SET @sql = 'ALTER LOGIN ' + QUOTENAME(@LoginName) + ' WITH PASSWORD = ''' + @PwdSafe + ''';';
                PRINT @sql; -- In lệnh SQL để kiểm tra
                EXEC(@sql);
            END
        END

        -- Nếu Roleid thay đổi, cập nhật role
        IF @Roleid IS NOT NULL AND @Roleid <> @RoleidCu
        BEGIN
            -- Xóa khỏi tất cả role database
            DECLARE @sqlRemove NVARCHAR(MAX) = '';
            SELECT @sqlRemove = @sqlRemove + 'ALTER ROLE ' + QUOTENAME(r.name) + ' DROP MEMBER ' + QUOTENAME(@LoginName) + '; '
            FROM sys.database_role_members drm
            JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
            JOIN sys.database_principals u ON drm.member_principal_id = u.principal_id
            WHERE u.name = @LoginName;

            IF @sqlRemove <> ''
            BEGIN
                PRINT @sqlRemove; -- In lệnh SQL để kiểm tra
                EXEC(@sqlRemove);
            END

            -- Cập nhật role mới
            IF @Roleid = 1  -- Admin
            BEGIN
                -- Gán sysadmin nếu chưa có
                IF NOT EXISTS (
                    SELECT * FROM sys.server_role_members srm
                    JOIN sys.server_principals sp ON srm.member_principal_id = sp.principal_id
                    WHERE sp.name = @LoginName AND srm.role_principal_id = SUSER_ID('sysadmin')
                )
                BEGIN
                    SET @sql = 'ALTER SERVER ROLE [sysadmin] ADD MEMBER ' + QUOTENAME(@LoginName) + ';';
                    PRINT @sql; -- In lệnh SQL để kiểm tra
                    EXEC(@sql);
                END

                SET @sql = 'ALTER ROLE [AdminRole] ADD MEMBER ' + QUOTENAME(@LoginName) + ';';
                PRINT @sql; -- In lệnh SQL để kiểm tra
                EXEC(@sql);
            END
            ELSE IF @Roleid = 2  -- GiangVien
            BEGIN
                -- Thu hồi sysadmin nếu trước đó là Admin
                IF @RoleidCu = 1
                BEGIN
                    SET @sql = 'ALTER SERVER ROLE [sysadmin] DROP MEMBER ' + QUOTENAME(@LoginName) + ';';
                    PRINT @sql; -- In lệnh SQL để kiểm tra
                    EXEC(@sql);
                END

                SET @sql = 'ALTER ROLE [GiangVienRole] ADD MEMBER ' + QUOTENAME(@LoginName) + ';';
                PRINT @sql; -- In lệnh SQL để kiểm tra
                EXEC(@sql);
            END
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SELECT 
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

USE QL_SinhVien;
SELECT name FROM sys.database_principals WHERE name = 'gv1';
