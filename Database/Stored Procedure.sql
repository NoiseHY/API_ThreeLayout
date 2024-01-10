use API_BachHoaXanh
go
-- Chèn dữ liệu vào bảng khách hàng 
CREATE PROCEDURE ThemKhachHang
    @TenKH NVARCHAR(50),
    @Diachi NVARCHAR(100),
    @Sdt NVARCHAR(20),
    @Ngaysinh DATE
AS
BEGIN
    INSERT INTO KhachHang (TenKH, DiachiKH, Sdt, Ngaysinh)
    VALUES (@TenKH, @Diachi, @Sdt, @Ngaysinh)
END;
-- Xóa 
CREATE PROCEDURE XoaKhachHang
    @MaKH INT
AS
BEGIN
    DELETE FROM KhachHang WHERE MaKH = @MaKH
END;
-- Sửa 
CREATE PROCEDURE SuaKhachHang
    @MaKH INT,
    @TenKH NVARCHAR(50),
    @Diachi NVARCHAR(100),
    @Sdt NVARCHAR(20),
    @Ngaysinh DATE
AS
BEGIN
    UPDATE KhachHang
    SET TenKH = @TenKH,
        DiachiKH = @Diachi,
        Sdt = @Sdt,
        Ngaysinh = @Ngaysinh
    WHERE MaKH = @MaKH
END;
-- Get all 
CREATE PROCEDURE GetCustomersPaged
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM (
        SELECT *,
            ROW_NUMBER() OVER (ORDER BY MaKH) AS RowNum
        FROM KhachHang
    ) AS PaginatedCustomers
    WHERE RowNum BETWEEN ((@PageNumber - 1) * @PageSize + 1) AND (@PageNumber * @PageSize);
END;




-- Get by ID 
CREATE PROCEDURE LayKhachHangTheoMa
    @MaKH INT
AS
BEGIN
    SELECT * FROM KhachHang WHERE MaKH = @MaKH
END;

EXEC LayKhachHangTheoMa @MaKH = 1;

EXEC ThemKhachHang 
    @TenKH = N'Nguyen Van A',
    @Diachi = N'123 Đường ABC, Thành phố XYZ',
    @Sdt = N'0123456789',
    @Ngaysinh = '1990-01-01';

-- GetTaiKhoanInfo
CREATE PROCEDURE GetTaiKhoanInfo
    @TenTK NVARCHAR(50),
    @MkTK NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM TaiKhoan
    WHERE TenTK = @TenTK AND MkTK = @MkTK;
END;


----CREATE PROCEDURE GetTaiKhoanInfo
----AS
----BEGIN
----    SELECT TenTK, MkTK, MaPQ, MaTK
----    FROM TaiKhoan;
----END;


----Login
--CREATE PROCEDURE DangNhap
--    @TenTK NVARCHAR(50),
--    @MkTK NVARCHAR(50)
--AS
--BEGIN
--    SET NOCOUNT ON;
    
--    DECLARE @LoggedInUserID INT;

--    -- Kiểm tra xem tài khoản tồn tại và thông tin đăng nhập có đúng không
--    SELECT @LoggedInUserID = MaTK
--    FROM TaiKhoan
--    WHERE TenTK = @TenTK AND MkTK = @MkTK;

--    -- Nếu tìm thấy tài khoản và thông tin đăng nhập đúng
--    IF (@LoggedInUserID IS NOT NULL)
--    BEGIN
--        -- Trả về ID của tài khoản đã đăng nhập thành công
--        SELECT 'Đăng nhập thành công ' AS Result;
--    END
--    ELSE
--    BEGIN
--        -- Trả về thông báo lỗi nếu không tìm thấy tài khoản hoặc thông tin đăng nhập sai
--        SELECT 'Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin đăng nhập.' AS Result;
--    END
--END;

--Phân trang 
CREATE PROCEDURE GetPaginatedProducts
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

    SELECT *
    FROM SanPham
    ORDER BY MaSP
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;

EXEC GetPaginatedProducts @PageNumber = 2, @PageSize = 8;

--
--CREATE PROCEDURE GetNextPaginatedProducts
--    @PageNumber INT,
--    @PageSize INT
--AS
--BEGIN
--    SET NOCOUNT ON;

--    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

--    IF @Offset < (SELECT COUNT(*) FROM SanPham)
--    BEGIN
--        SELECT *
--        FROM SanPham
--        ORDER BY MaSP
--        OFFSET @Offset ROWS
--        FETCH NEXT @PageSize ROWS ONLY;
--    END
--    ELSE
--    BEGIN
--        PRINT 'No more records';
--    END
--END;


--EXEC GetNextPaginatedProducts @PageNumber = 3, @PageSize = 10;


--
CREATE PROCEDURE GetProductById
    @ProductId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM SanPham
    WHERE MaSP = @ProductId;
END;
--
CREATE PROCEDURE AddProduct
    @TenSP NVARCHAR(50),
    @Mota NVARCHAR(1000),
    @SoLuong INT,
    @Dongia DECIMAL(18,2),
    @MaTL INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO SanPham (TenSP, Mota, SoLuong, Dongia, MaTL)
    VALUES (@TenSP, @Mota, @SoLuong, @Dongia, @MaTL);
END;


EXEC AddProduct 
    @TenSP = N'Tên sản phẩm', 
    @Mota = N'Mô tả sản phẩm',
    @SoLuong = 10,
    @Dongia = 100.50,
    @MaTL = 1


--
CREATE PROCEDURE UpdateProduct
    @MaSP INT,
    @TenSP NVARCHAR(50),
    @Mota NVARCHAR(1000),
    @SoLuong INT,
    @Dongia DECIMAL(18,2),
    @MaTL INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE SanPham
    SET TenSP = @TenSP,
        Mota = @Mota,
        SoLuong = @SoLuong,
        Dongia = @Dongia,
        MaTL = @MaTL
    WHERE MaSP = @MaSP;
END;

--
CREATE PROCEDURE DeleteProduct
    @MaSP INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM SanPham
    WHERE MaSP = @MaSP;
END;


-- 
CREATE PROCEDURE UpdateSanPhamImg
    @MaSP INT,
    @Img NVARCHAR(max)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SanPham
    SET Img = @Img
    WHERE MaSP = @MaSP;
END;

--
CREATE PROCEDURE GetAllLoaiSP
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM LoaiSP;
END;

exec GetAllLoaiSP

--
CREATE PROCEDURE GetTenLoaiByMaLoai
    @MaLoai INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TenLoai
    FROM LoaiSP
    WHERE MaLoai = @MaLoai;
END;

--
CREATE PROCEDURE SearchProductByName
    @ProductName NVARCHAR(50),
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM (
        SELECT ROW_NUMBER() OVER (ORDER BY MaSP) AS RowNum, *
        FROM SanPham
        WHERE TenSP LIKE '%' + @ProductName + '%'
    ) AS Sub
    WHERE RowNum BETWEEN ((@PageNumber - 1) * @PageSize + 1) AND (@PageNumber * @PageSize);
END;


exec SearchProductByName 5, 1, 10  

--
CREATE PROCEDURE UpdateCustomerImage
    @MaKH INT,
    @NewImg NVARCHAR(MAX)
AS
BEGIN
    UPDATE KhachHang
    SET Img = @NewImg
    WHERE MaKH = @MaKH;
END;
--
CREATE PROCEDURE InsertRating
    @MaSP INT,
    @MaTK INT,
    @DanhGia float,
    @BinhLuan NVARCHAR(200)
AS
BEGIN
    INSERT INTO DanhGia (MaSP, MaTK, DanhGia, BinhLuan)
    VALUES (@MaSP, @MaTK, @DanhGia, @BinhLuan);
END;
--
CREATE PROCEDURE UpdateRating
    @MaDanhGia INT,
    @DanhGia float,
    @BinhLuan NVARCHAR(200)
AS
BEGIN
    UPDATE DanhGia
    SET DanhGia = @DanhGia, BinhLuan = @BinhLuan
    WHERE MaDanhGia = @MaDanhGia;
END;
--
CREATE PROCEDURE DeleteRating
    @MaDanhGia INT
AS
BEGIN
    DELETE FROM DanhGia
    WHERE MaDanhGia = @MaDanhGia;
END;
--
CREATE PROCEDURE PaginateRatings
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SELECT *
    FROM (
        SELECT ROW_NUMBER() OVER (ORDER BY ThoiGian DESC) AS RowNum, *
        FROM DanhGia
    ) AS Pagination
    WHERE RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize;
END;
--
CREATE PROCEDURE GetReviewsWithUserInfoByProductId
    @ProductId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DG.MaDanhGia, DG.MaSP, DG.MaTK, DG.DanhGia, DG.BinhLuan, DG.ThoiGian, KH.TenKH AS UserName, KH.Img AS UserImg
    FROM DanhGia DG
    INNER JOIN KhachHang KH ON DG.MaTK = KH.MaKH
    WHERE DG.MaSP = @ProductId;
END;
--
CREATE PROCEDURE GetCommentsByMaTK
    @MaTK INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DG.MaDanhGia, DG.MaSP, DG.MaTK, DG.DanhGia, DG.BinhLuan, DG.ThoiGian, SP.TenSP AS TenSanPham, SP.Img AS AnhSanPham
    FROM DanhGia DG
    INNER JOIN SanPham SP ON DG.MaSP = SP.MaSP
    WHERE DG.MaTK = @MaTK;
END;

exec GetCommentsByMaTK 3

--------- Chưa exec
CREATE PROCEDURE GetReviewsWithUserInfo2
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

    SELECT *
    FROM (
        SELECT 
            DG.MaDanhGia,
            DG.MaSP,
            DG.MaTK,
            DG.DanhGia,
            DG.BinhLuan,
            DG.ThoiGian,
            KH.MaKH,
            KH.TenKH,
            KH.Img,
            ROW_NUMBER() OVER (ORDER BY DG.MaDanhGia) AS RowNum
        FROM 
            DanhGia DG
        INNER JOIN 
            KhachHang KH ON DG.MaTK = KH.MaKH
    ) AS Sub
    WHERE 
        RowNum > @Offset AND RowNum <= (@Offset + @PageSize);
END;



-- Giỏ hàng 
CREATE PROCEDURE GetCartWithProductImgAndNameByCustomerId
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT GH.MaGiohang, GH.MaKH, GH.MaSP, GH.Dongia, GH.Thoidiemtao, SP.Img AS ProductImg, SP.TenSP AS ProductName
    FROM Giohang GH
    INNER JOIN SanPham SP ON GH.MaSP = SP.MaSP
    WHERE GH.MaKH = @CustomerId;
END;


exec GetCartWithProductImgAndNameByCustomerId 3
--
CREATE PROCEDURE GetHoaDonBanByMaKH
    @MaKH INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM HoaDonBan
    WHERE MaKH = @MaKH;
END;

exec GetHoaDonBanByMaKH 3
--
CREATE PROCEDURE DeleteHoaDonBan
    @MaHDB INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ChiTietHDBan
    WHERE MaHDB = @MaHDB;

    DELETE FROM HoaDonBan
    WHERE MaHDB = @MaHDB;
END;
--
CREATE PROCEDURE CountProductsInCartByCustomerId
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(MaGiohang) AS TotalProducts
    FROM Giohang
    WHERE MaKH = @CustomerId;
END;


exec CountProductsInCartByCustomerId 3

--
CREATE PROCEDURE AddToCart
    @CustomerId INT,
    @ProductId INT,
    @UnitPrice DECIMAL(10, 2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Giohang (MaKH, MaSP, Dongia, Thoidiemtao)
    VALUES (@CustomerId, @ProductId, @UnitPrice, GETDATE());
END;
--
CREATE PROCEDURE CheckProductInCart
    @MaKH INT,
    @MaSP INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ProductInCart BIT;

    IF EXISTS (
        SELECT 1
        FROM Giohang
        WHERE MaKH = @MaKH AND MaSP = @MaSP
    )
    BEGIN
        SET @ProductInCart = 1;
    END
    ELSE
    BEGIN
        SET @ProductInCart = 0;
    END

    SELECT @ProductInCart AS ProductInCart;
END;


exec CheckProductInCart 3, 65


--
CREATE PROCEDURE UpdateCartItem
    @CartId INT,
    @UnitPrice DECIMAL(10, 2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Giohang
    SET Dongia = @UnitPrice, Thoidiemtao = GETDATE()
    WHERE MaGiohang = @CartId;
END;
--
CREATE PROCEDURE RemoveFromCart
    @CartId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Giohang
    WHERE MaGiohang = @CartId;
END;


--
CREATE PROCEDURE AddHoaDonBan_ChiTiet
    @TongTien DECIMAL(18,2),
    @MaKH INT,
    @NgayBan DATE,
    @MaSP INT,
    @SoLuong INT,
    @Gia DECIMAL(18,2),
    @ThanhTien DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaHDB INT;

    -- Thêm Hóa Đơn Bán
    INSERT INTO HoaDonBan (Tongtien, MaKH, Ngayban)
    VALUES (@TongTien, @MaKH, @NgayBan);

    -- Lấy Mã Hóa Đơn Bán Vừa Thêm
    SET @MaHDB = SCOPE_IDENTITY();

    -- Thêm Chi Tiết Hóa Đơn Bán
    INSERT INTO ChiTietHDBan (MaHDB, MaSP, Soluong, Gia, Thanhtien)
    VALUES (@MaHDB, @MaSP, @SoLuong, @Gia, @ThanhTien);
END;

--
--CREATE PROCEDURE GetHoaDonBanChiTietByMaKH
--    @MaKH INT
--AS
--BEGIN
--    SET NOCOUNT ON;

--    SELECT HB.MaHDB, HB.Tongtien, HB.MaKH, HB.Ngayban,
--           CT.MaCTHDB, CT.MaSP, CT.Soluong, CT.Gia, CT.Thanhtien
--    FROM HoaDonBan HB
--    INNER JOIN ChiTietHDBan CT ON HB.MaHDB = CT.MaHDB
--    WHERE HB.MaKH = @MaKH;
--END;

--exec GetHoaDonBanChiTietByMaKH 3

--
CREATE TABLE TempChiTietHDBan (
    MaSP INT,
    Soluong INT,
    Gia DECIMAL(18,2),
    Thanhtien DECIMAL(18,2)
);
--
CREATE PROCEDURE AddTempChiTietHDBan
    @MaSP INT,
    @Soluong INT,
    @Gia DECIMAL(18,2),
    @Thanhtien DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO TempChiTietHDBan (MaSP, Soluong, Gia, Thanhtien)
    VALUES (@MaSP, @Soluong, @Gia, @Thanhtien);
END;

--
CREATE PROCEDURE AddHoaDonBan_ChiTiet
    @TongTien DECIMAL(18,2),
    @MaKH INT,
    @NgayBan DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaHDB INT;

    -- Thêm Hóa Đơn Bán
    INSERT INTO HoaDonBan (Tongtien, MaKH, Ngayban)
    VALUES (@TongTien, @MaKH, @NgayBan);

    -- Lấy Mã Hóa Đơn Bán Vừa Thêm
    SET @MaHDB = SCOPE_IDENTITY();

    -- Thêm Chi Tiết Hóa Đơn Bán từ Bảng Tạm
    INSERT INTO ChiTietHDBan (MaHDB, MaSP, Soluong, Gia, Thanhtien)
    SELECT @MaHDB, MaSP, Soluong, Gia, Thanhtien
    FROM TempChiTietHDBan;

    -- Xóa Dữ Liệu Tạm
    DELETE FROM TempChiTietHDBan;
END;
--
CREATE PROCEDURE GetTopHoaDonBanByMaKH
    @MaKH INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 *
    FROM HoaDonBan
    WHERE MaKH = @MaKH
    ORDER BY Ngayban DESC;
END;

exec GetTopHoaDonBanByMaKH 3 

--
CREATE PROCEDURE GetChiTietHDBanWithProductNameByMaHDB
    @MaHDB INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CTHB.*, SP.TenSP AS ProductName
    FROM ChiTietHDBan CTHB
    JOIN SanPham SP ON CTHB.MaSP = SP.MaSP
    WHERE CTHB.MaHDB = @MaHDB;
END;


exec GetChiTietHDBanWithProductNameByMaHDB 7 

--
CREATE PROCEDURE GetAccountsPaged
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM (
        SELECT *,
            ROW_NUMBER() OVER (ORDER BY MaTK) AS RowNum
        FROM TaiKhoan
    ) AS PaginatedAccounts
    WHERE RowNum BETWEEN ((@PageNumber - 1) * @PageSize + 1) AND (@PageNumber * @PageSize);
END;


--
CREATE PROCEDURE AddAccount
    @TenTK NVARCHAR(50),
    @MkTK NVARCHAR(50),
    @Email NVARCHAR(50),
    @MaPQ INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewMaKH INT;

    INSERT INTO TaiKhoan (TenTK, MkTK, Email, MaPQ)
    VALUES (@TenTK, @MkTK, @Email, @MaPQ);

    -- Kiểm tra nếu MaPQ = 3, tự động tạo bảng KhachHang và thêm mã khách hàng
    IF (@MaPQ = 3)
    BEGIN
        -- Thêm khách hàng mới
        INSERT INTO KhachHang (TenKH)
        VALUES (@TenTK);

        -- Lấy mã khách hàng vừa thêm
        SET @NewMaKH = SCOPE_IDENTITY();

        -- Cập nhật mã khách hàng trong bảng TaiKhoan
        UPDATE TaiKhoan
        SET MaKH = @NewMaKH
        WHERE MaTK = (SELECT MAX(MaTK) FROM TaiKhoan); -- Lấy mã tài khoản vừa thêm
    END;
END;




--
CREATE PROCEDURE UpdateAccount
    @MaTK INT,
    @TenTK NVARCHAR(50),
    @MkTK NVARCHAR(50),
    @Email NVARCHAR(50),
    @MaPQ INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE TaiKhoan
    SET TenTK = @TenTK, MkTK = @MkTK, Email = @Email, MaPQ = @MaPQ
    WHERE MaTK = @MaTK;
END;

--
CREATE PROCEDURE DeleteAccount
    @MaTK INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM TaiKhoan
    WHERE MaTK = @MaTK;
END;

--
CREATE PROCEDURE GetAllPermissionNames
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM PhanQuyen;
END;

exec GetAllPermissionNames
