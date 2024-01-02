--use master
--go
--create database API_BachHoaXanh
--go

--use master
--go
--drop database API_BachHoaXanh

-- Sửa lỗi 1 số not null --
--Thêm email vào tài khoản, xóa cột email  ---
--Sửa bảng tài khoản. cho phép null 1 số bảng  --
--Sửa insert table ---
--Thêm img ở khách hàng 
use API_BachHoaXanh
go

-- Tạo bảng Thể loại
CREATE TABLE LoaiSP (
    MaLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(50) not null ,
    MotaLoai NVARCHAR(1000) 
);

-- Tạo bảng Sản phẩm
CREATE TABLE SanPham (
    MaSP INT IDENTITY(1,1) PRIMARY KEY,
    TenSP NVARCHAR(50) not null,
    Mota NVARCHAR(1000) ,
    SoLuong INT not null,
    Dongia DECIMAL(18,2) not null,
	MaTL INT not null,
	Img nvarchar (max),
	Foreign key (MaTL) references LoaiSP(MaLoai)
);

-- Tạo bảng Khách hàng
CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    TenKH NVARCHAR(50) not null,
    DiachiKH NVARCHAR(100) ,
    Sdt NVARCHAR(20) ,
    Ngaysinh DATE 
);

-- Tạo bảng Giảm giá
CREATE TABLE GiamGia (
    MaGG INT IDENTITY(1,1) PRIMARY KEY,
    MaVoucher NVARCHAR(20) not null,
    Ngaybatdau DATE ,
    Ngayhethan DATE ,
    Dieukien NVARCHAR(1000) 
);

-- Tạo bảng Hóa đơn bán
CREATE TABLE HoaDonBan (
    MaHDB INT IDENTITY(1,1) PRIMARY KEY,
    Tongtien DECIMAL(18,2) not null,
    MaKH INT not null,
    Ngayban DATE not null,
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);
-- Tạo bảng Chi tiết hóa đơn bán
CREATE TABLE ChiTietHDBan (
    MaCTHDB INT IDENTITY(1,1) PRIMARY KEY not null,
    MaHDB INT not null,
    MaSP INT not null,
    Soluong INT not null,
    Gia DECIMAL(18,2) not null,
    Thanhtien DECIMAL(18,2) not null,
	MaGG INT ,
	Foreign key (MaGG) references GiamGia (MaGG),
    FOREIGN KEY (MaHDB) REFERENCES HoaDonBan(MaHDB),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- Tạo bảng Nhà cung cấp
CREATE TABLE NhaCungCap (
    MaNCC INT IDENTITY(1,1) PRIMARY KEY not null,
    TenNCC NVARCHAR(50) not null,
    Diachi NVARCHAR(100) ,
    Sdt NVARCHAR(20) 
);
-- Tạo bảng Hóa đơn nhập
CREATE TABLE HoaDonNhap (
    MaHDN INT IDENTITY(1,1) PRIMARY KEY,
    Tongtien DECIMAL(18,2) not null,
    Ngayban DATE not null,
    MaNCC INT not null,
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

-- Tạo bảng Chi tiết hóa đơn nhập
CREATE TABLE ChiTietHDNhap (
    MaCTHDN INT IDENTITY(1,1) PRIMARY KEY,
    MaHDN INT not null,
    MaSP INT not null,
    Soluong INT not null,
    Gia DECIMAL(18,2) not null,
    Thanhtien DECIMAL(18,2) not null,
    FOREIGN KEY (MaHDN) REFERENCES HoaDonNhap(MaHDN),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- Tạo bảng Nhân viên
CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    TenNV NVARCHAR(50) not null,
    Ngaysinh DATE ,
    Gioitinh NVARCHAR(10) ,
    Sdt NVARCHAR(20) ,
    Diachi NVARCHAR(100) 
);

-- Tạo bảng Phân quyền
CREATE TABLE PhanQuyen (
    MaPQ INT IDENTITY(1,1) PRIMARY KEY,
    TenPQ NVARCHAR(50) not null,
    Mota NVARCHAR(100) 
)
-- Tạo bảng Tài khoản
CREATE TABLE TaiKhoan (
    MaTK INT IDENTITY(1,1) PRIMARY KEY,
    TenTK NVARCHAR(50) not null,
    MkTK NVARCHAR(50) not null,
	Email NVARCHAR(50) ,
    MaPQ INT ,
    MaKH INT ,
    MaNV INT ,
    FOREIGN KEY (MaPQ) REFERENCES PhanQuyen(MaPQ),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
--drop table TaiKhoan

-- Tạo bảng giỏ hàng
CREATE TABLE Giohang (
    MaGiohang INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT ,
    MaSP INT ,
    Dongia DECIMAL(10, 2) ,
    Thoidiemtao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);
--
CREATE TABLE DanhGia (
    MaDanhGia INT IDENTITY(1,1) PRIMARY KEY,
    MaSP INT, 
    MaTK INT,
    DanhGia INT, 
	BinhLuan NVARCHAR(200),
    ThoiGian DATETIME DEFAULT GETDATE(), 
	foreign key (MaSP) references SanPham (MaSP),
	foreign key (MaTK) references TaiKhoan (MaTK)
);
--



-- Thêm dữ liệu vào bảng Thể loại
INSERT INTO LoaiSP(TenLoai, MotaLoai) VALUES (N'Thịt heo', N'Thịt heo là loại thực phẩm phổ biến nhất tại Việt Nam, là loại nguyên liệu quen thuộc trong những bữa ăn hằng ngày. Bởi lẽ, giá thịt heo không chỉ phù hợp với điều kiện kinh tế của người Việt mà còn dễ dàng chế biến ra nhiều món ăn nhanh mà lại không tốn quá nhiều thời gian.');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Thịt bò', N'Bên cạnh thịt heo, thịt bò là một trong những loại thịt mang đến nguồn dinh dưỡng cao và luôn có mặt trong các bữa ăn trong gia đình ở khắp thế giới. Trong thịt bò có chứa rất nhiều protein và chất sắt,… đây là những chất dinh dưỡng rất tốt cho sức khỏe và sắc đẹp. Đặc biệt, đây cũng là một loại thực phẩm rất tốt dành cho người muốn giảm cân, tăng cơ bắp rất hiệu quả.');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Thịt gà', N'Thịt gà là thịt gia cầm phổ biến trên thế giới. Đây là loại thịt dễ tìm, dễ mua, dễ chế biến và còn mang lại nhiều lợi ích cho sức khỏe. Ngày nay, nhu cầu sử dụng thịt gà tăng cao nên rất nhiều thương hiệu thực phẩm nổi tiếng đã không ngừng mang đến tay người tiêu dùng đa dạng các loại thịt gà tươi ngon, chất lượng, được nuôi và chọn lọc kỹ càng. ');

INSERT INTO LoaiSP(TenLoai, MotaLoai) VALUES (N'Trái cây', N'Trái cây là một trong những loại thực phẩm cần có trong chế độ ăn uống lành mạnh, không chỉ đa dạng, có nguồn vitamin dồi dào mà trái cây còn giúp cơ thể ngăn ngừa những tác động xấu đến sức khoẻ. Vì thế, bổ sung trái cây vào khẩu phần ăn hằng ngày bên cạnh rau xanh, củ quả là điều vô cùng quan trọng. ');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Rau lá', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Củ, Quả', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Dầu ăn, nước mắm', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Nước tương', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Kem cây, kem hộp', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Bánh bao, bánh mì', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Mì ăn liền', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Hủ tiếu, miến', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Gạo các loại', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Bia, nước có cồn', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Sữa tươi', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Bột giặt', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Mì tôm ', N'');
INSERT INTO LoaiSP (TenLoai, MotaLoai) VALUES (N'Bánh gạo', N'');


-- Thêm dữ liệu vào bảng Sản phẩm 
INSERT INTO SanPham (TenSP, Mota, SoLuong, Dongia, MaTL) 
VALUES (N'5 lốc sữa lúa mạch Milo ít đường Activ Go 110ml', N'Sữa lúa mạch Milo thơm ngon từ lúa mạch, cung cấp đạm và canxi cho cơ thể. Milo từ lâu luôn là thương hiệu sữa uống lúa mạch được các bé yêu thích.', 50, 800.00, 1);
INSERT INTO SanPham (TenSP, Mota, SoLuong, Dongia, MaTL) 
VALUES (N'Mít Thái 1kg.', N'Mít là một trong những loại trái cây nhiệt đới với vị ngọt đầy lôi cuốn khiến rất nhiều người yêu thích ngay từ miếng cắn đầu tiên. Mít thái tại Bách hoá XANH được lấy từ những nguồn cung đảm bảo, không hoá chất, mang đến cho mọi người món trái cây an toàn và dinh dưỡng.', 100, 700.00, 2);
INSERT INTO SanPham (TenSP, Mota, SoLuong, Dongia, MaTL) 
VALUES (N'Gạo đặc sản Trạng Nguyên Vinh Hiển ST25 túi 5kg', N'Gạo là lương thực quan trọng, không thể thiếu trong những bữa cơm gia đình. Gạo Vinh Hiển là thương hiệu lớn với sản phẩm Gạo đặc sản trạng nguyên Vinh Hiển ST25 túi 5kg dẻo, mềm và thơm giúp ăn ngon miệng, rất hợp khẩu vị nhiều người. Túi nhỏ thích hợp cho sử dụng cá nhân hoặc dùng thử.', 30, 250.00, 3);

-- Thêm dữ liệu vào bảng Nhà cung cấp
INSERT INTO NhaCungCap (TenNCC, Diachi, Sdt) VALUES (N'Công ty ABC', N'Địa chỉ ABC', '0123456789');
INSERT INTO NhaCungCap (TenNCC, Diachi, Sdt) VALUES (N'Công ty XYZ', N'Địa chỉ XYZ', '0987654321');

-- Thêm dữ liệu vào bảng Khách hàng
INSERT INTO KhachHang (TenKH, DiachiKH,  Sdt, Ngaysinh) VALUES (N'Nguyễn Văn A', N'Địa chỉ A',  '0123456789', N'1990-01-01');
INSERT INTO KhachHang (TenKH, DiachiKH,  Sdt, Ngaysinh) VALUES (N'Trần Thị B', N'Địa chỉ B',  '0987654321', N'1995-05-05');

-- Thêm dữ liệu vào bảng Phân quyền
INSERT INTO PhanQuyen (TenPQ, Mota) VALUES (N'Quản trị viên', N'Có đầy đủ quyền truy cập và quản lý hệ thống');
INSERT INTO PhanQuyen (TenPQ, Mota) VALUES (N'Nhân viên bán hàng', N'Có quyền truy cập vào các chức năng bán hàng');

-- Thêm dữ liệu vào bảng Nhân viên
INSERT INTO NhanVien (TenNV, Ngaysinh, Gioitinh, Sdt, Diachi) VALUES (N'Nguyễn Thị C', '1992-03-15', N'Nữ', '0365478912', N'Địa chỉ C');
INSERT INTO NhanVien (TenNV, Ngaysinh, Gioitinh, Sdt, Diachi) VALUES (N'Trần Văn D', '1985-10-20', N'Nam', '0987123456', N'Địa chỉ D');

-- Thêm dữ liệu vào bảng Giảm giá
INSERT INTO GiamGia (MaVoucher, Ngaybatdau, Ngayhethan, Dieukien) VALUES (N'ABC123', '2023-01-01', '2023-06-30', N'Áp dụng cho hóa đơn trên 1000 đô');
INSERT INTO GiamGia (MaVoucher, Ngaybatdau, Ngayhethan, Dieukien) VALUES (N'XYZ456', '2023-02-15', '2023-08-31', N'Áp dụng cho sản phẩm cụ thể');


-- Thêm dữ liệu vào bảng Hóa đơn bán
INSERT INTO HoaDonBan (Tongtien, MaKH, Ngayban) VALUES (2200.00, 1, '2023-01-15');
INSERT INTO HoaDonBan (Tongtien, MaKH, Ngayban) VALUES (750.00, 2, '2023-02-20');

-- Thêm dữ liệu vào bảng Chi tiết hóa đơn bán
INSERT INTO ChiTietHDBan (MaHDB, MaSP, Soluong, Gia, Thanhtien, MaGG) 
VALUES 
    (1, 1, 2, 750.00, 1500.00, 1),
    (1, 2, 1, 700.00, 700.00, 2),
    (2, 3, 3, 250.00, 750.00, 2);

-- Thêm dữ liệu vào bảng Hóa đơn nhập
INSERT INTO HoaDonNhap (Tongtien, Ngayban, MaNCC) VALUES (3500.00, '2023-03-10', 1);
INSERT INTO HoaDonNhap (Tongtien, Ngayban, MaNCC) VALUES (6000.00, '2023-04-05', 2);

-- Thêm dữ liệu vào bảng Chi tiết hóa đơn nhập
INSERT INTO ChiTietHDNhap (MaHDN, MaSP, Soluong, Gia, Thanhtien) VALUES (1, 1, 5, 700.00, 3500.00);
INSERT INTO ChiTietHDNhap (MaHDN, MaSP, Soluong, Gia, Thanhtien) VALUES (2, 2, 10, 600.00, 6000.00);

-- Thêm dữ liệu vào bảng Tài khoản
INSERT INTO TaiKhoan (TenTK, MkTK,Email, MaPQ, MaKH, MaNV) VALUES (N'admin', '123',N'congnamhy1@gmail.com' ,1, NULL, NULL);
INSERT INTO TaiKhoan (TenTK, MkTK,Email ,MaPQ, MaKH, MaNV) VALUES (N'nvbanhang', 'nv123',N'nvbanhang@gmail.com' ,2, NULL, 1);

-- Thêm dữ liệu vào bảng Giỏ hàng 
INSERT INTO Giohang (MaKH, MaSP,Dongia)
VALUES (3, 1, 20000);

select * from KhachHang 
SELECT * FROM KhachHang FOR JSON AUTO;
--
--CREATE PROCEDURE GetNewProducts
--    @PageNumber INT,
--    @PageSize INT
--AS
--BEGIN
--    SET NOCOUNT ON;

--    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

--    SELECT *
--    FROM SanPham
--    ORDER BY MaSP DESC
--    OFFSET @Offset ROWS
--    FETCH NEXT @PageSize ROWS ONLY;
--END;

--EXEC GetNewProducts @PageNumber = 1, @PageSize = 10;

