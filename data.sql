USE QL_SinhVien;
GO

INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, NoiSinh, GioiTinh, CMND_CCCD, LopSV)
VALUES
('SV001', N'Nguyễn Văn An', '2003-05-14', N'Hà Nội', N'Nam', '012345678901', 'CNTT01'),
('SV002', N'Trần Thị Mai', '2003-08-20', N'Hải Phòng', N'Nữ', '012345678902', 'CNTT01'),
('SV003', N'Lê Văn Nam', '2003-11-02', N'Hà Nam', N'Nam', '012345678903', 'CNTT01'),
('SV004', N'Phạm Thị Hoa', '2003-04-11', N'Ninh Bình', N'Nữ', '012345678904', 'CNTT01'),
('SV005', N'Đỗ Văn Bình', '2003-12-05', N'Thanh Hóa', N'Nam', '012345678905', 'CNTT01'),

('SV006', N'Nguyễn Thị Lan', '2003-07-14', N'Hà Nội', N'Nữ', '012345678906', 'CNTT02'),
('SV007', N'Hoàng Văn Minh', '2003-03-15', N'Hà Nội', N'Nam', '012345678907', 'CNTT02'),
('SV008', N'Phạm Thị Thảo', '2003-09-21', N'Hà Tĩnh', N'Nữ', '012345678908', 'CNTT02'),
('SV009', N'Trần Văn Hùng', '2003-10-10', N'Hà Nam', N'Nam', '012345678909', 'CNTT02'),
('SV010', N'Bùi Thị Hạnh', '2003-05-30', N'Hà Nội', N'Nữ', '012345678910', 'CNTT02'),

('SV011', N'Nguyễn Văn Dũng', '2003-06-12', N'Hà Nội', N'Nam', '012345678911', 'CNTT01'),
('SV012', N'Trần Thị Lệ', '2003-09-09', N'Hà Nam', N'Nữ', '012345678912', 'CNTT01'),
('SV013', N'Lê Văn Phú', '2003-01-19', N'Nghệ An', N'Nam', '012345678913', 'CNTT01'),
('SV014', N'Phạm Thị Nga', '2003-11-23', N'Hà Nội', N'Nữ', '012345678914', 'CNTT01'),
('SV015', N'Đỗ Văn Toàn', '2003-12-15', N'Nam Định', N'Nam', '012345678915', 'CNTT01'),

('SV016', N'Nguyễn Thị Thanh', '2003-07-18', N'Hà Nội', N'Nữ', '012345678916', 'CNTT02'),
('SV017', N'Hoàng Văn Thắng', '2003-08-20', N'Hà Nội', N'Nam', '012345678917', 'CNTT02'),
('SV018', N'Phạm Thị Thủy', '2003-03-12', N'Hà Tĩnh', N'Nữ', '012345678918', 'CNTT02'),
('SV019', N'Trần Văn Tài', '2003-04-25', N'Hà Nam', N'Nam', '012345678919', 'CNTT02'),
('SV020', N'Bùi Thị Quỳnh', '2003-06-11', N'Hà Nội', N'Nữ', '012345678920', 'CNTT02'),

-- Thêm tiếp cho đủ 40 SV
('SV021', N'Nguyễn Văn Hoàng', '2003-05-12', N'Hà Nội', N'Nam', '012345678921', 'CNTT01'),
('SV022', N'Trần Thị Thu', '2003-08-14', N'Hải Phòng', N'Nữ', '012345678922', 'CNTT01'),
('SV023', N'Lê Văn Quân', '2003-11-20', N'Hà Nam', N'Nam', '012345678923', 'CNTT01'),
('SV024', N'Phạm Thị Hà', '2003-04-10', N'Ninh Bình', N'Nữ', '012345678924', 'CNTT01'),
('SV025', N'Đỗ Văn Lực', '2003-12-09', N'Thanh Hóa', N'Nam', '012345678925', 'CNTT01'),

('SV026', N'Nguyễn Thị Yến', '2003-07-01', N'Hà Nội', N'Nữ', '012345678926', 'CNTT02'),
('SV027', N'Hoàng Văn Khánh', '2003-03-05', N'Hà Nội', N'Nam', '012345678927', 'CNTT02'),
('SV028', N'Phạm Thị Vân', '2003-09-15', N'Hà Tĩnh', N'Nữ', '012345678928', 'CNTT02'),
('SV029', N'Trần Văn Khôi', '2003-10-07', N'Hà Nam', N'Nam', '012345678929', 'CNTT02'),
('SV030', N'Bùi Thị Xuân', '2003-05-18', N'Hà Nội', N'Nữ', '012345678930', 'CNTT02'),

('SV031', N'Nguyễn Văn Sơn', '2003-06-20', N'Hà Nội', N'Nam', '012345678931', 'CNTT01'),
('SV032', N'Trần Thị Huyền', '2003-09-02', N'Hà Nam', N'Nữ', '012345678932', 'CNTT01'),
('SV033', N'Lê Văn Lợi', '2003-01-22', N'Nghệ An', N'Nam', '012345678933', 'CNTT01'),
('SV034', N'Phạm Thị Phương', '2003-11-25', N'Hà Nội', N'Nữ', '012345678934', 'CNTT01'),
('SV035', N'Đỗ Văn Cường', '2003-12-19', N'Nam Định', N'Nam', '012345678935', 'CNTT01'),

('SV036', N'Nguyễn Thị Hiền', '2003-07-11', N'Hà Nội', N'Nữ', '012345678936', 'CNTT02'),
('SV037', N'Hoàng Văn Kiên', '2003-08-13', N'Hà Nội', N'Nam', '012345678937', 'CNTT02'),
('SV038', N'Phạm Thị Nhung', '2003-03-18', N'Hà Tĩnh', N'Nữ', '012345678938', 'CNTT02'),
('SV039', N'Trần Văn Khánh', '2003-04-29', N'Hà Nam', N'Nam', '012345678939', 'CNTT02'),
('SV040', N'Bùi Thị Minh', '2003-06-14', N'Hà Nội', N'Nữ', '012345678940', 'CNTT02');
INSERT INTO MonHoc (MaMH, TenMH, SoTinChi)
VALUES
('MH001', N'Cơ sở dữ liệu', 3),
('MH002', N'Lập trình C#', 3),
('MH003', N'Mạng máy tính', 3);
INSERT INTO GiangVien (MaGV, HoTenGV, HocVi, Khoa, Email, DienThoai)
VALUES
('GV001', N'Nguyễn Văn A', N'Tiến sĩ', N'Công nghệ thông tin', 'vana@ute.edu.vn', '0901123456'),
('GV002', N'Trần Thị B', N'Thạc sĩ', N'Công nghệ thông tin', 'thib@ute.edu.vn', '0902123456'),
('GV003', N'Lê Văn C', N'Thạc sĩ', N'Công nghệ thông tin', 'vanc@ute.edu.vn', '0903123456');
INSERT INTO HocKyNamHoc (HocKy, NamHoc)
VALUES
(1, '2023-2024'),
(2, '2023-2024'),
(1, '2024-2025'),
(2, '2024-2025');
USE QL_SinhVien;
GO

-- ==========================
-- Lớp học phần (LopHocPhan)
-- ==========================
-- Giả sử: MaHocKyNamHoc: 1 = HK1, 2 = HK2
USE QL_SinhVien;
GO

-- ==========================
-- Lớp học phần (LopHocPhan)
-- ==========================
-- Giả sử: MaHocKyNamHoc: 1 = HK1, 2 = HK2

-- ==========================
-- Đăng ký môn học (DangKyMonHoc)
-- ==========================
-- MSSV phân tán lộn xộn, số SV mỗi lớp khác nhau

-- ==========================
-- Lớp học phần (LopHocPhan) có MaHocKyNamHoc
-- ==========================
-- ==========================
-- Thêm lớp học phần theo mã MH1, MH2, MH3...
-- ==========================
INSERT INTO LopHocPhan (MaLHP, MaMH, MaGV, MaHocKyNamHoc)
VALUES 
-- Môn MH001 → MH1
('MH1_001', 'MH001', 'GV001', 4),
('MH1_002', 'MH001', 'GV001', 4),

-- Môn MH002 → MH2
('MH2_001', 'MH002', 'GV001', 4),
('MH2_002', 'MH002', 'GV003', 4),

-- Môn MH003 → MH3
('MH3_001', 'MH003', 'GV002', 4);

-- ==========================
-- Đăng ký môn học theo lớp MH1, MH2, MH3
-- ==========================
-- Lớp MH1_001
INSERT INTO DangKyMonHoc (MaSV, MaLHP, MaHocKyNamHoc) VALUES
('SV003','MH1_001', 4),
('SV010','MH1_001', 4),
('SV001','MH1_001', 4),
('SV007','MH1_001', 4);

-- Lớp MH1_002
INSERT INTO DangKyMonHoc (MaSV, MaLHP, MaHocKyNamHoc) VALUES
('SV014','MH1_002', 4),
('SV005','MH1_002', 4),
('SV016','MH1_002', 4);

-- Lớp MH2_001
INSERT INTO DangKyMonHoc (MaSV, MaLHP, MaHocKyNamHoc) VALUES
('SV020','MH2_001', 4),
('SV008','MH2_001', 4),
('SV012','MH2_001', 4),
('SV019','MH2_001', 4),
('SV002','MH2_001', 4);

-- Lớp MH2_002
INSERT INTO DangKyMonHoc (MaSV, MaLHP, MaHocKyNamHoc) VALUES
('SV025','MH2_002', 4),
('SV027','MH2_002', 4),
('SV032','MH2_002', 4);

-- Lớp MH3_001
INSERT INTO DangKyMonHoc (MaSV, MaLHP, MaHocKyNamHoc) VALUES
('SV033','MH3_001', 4),
('SV034','MH3_001', 4),
('SV036','MH3_001', 4),
('SV035','MH3_001', 4),
('SV038','MH3_001', 4),
('SV040','MH3_001', 4),
('SV037','MH3_001', 4),
('SV039','MH3_001', 4);
