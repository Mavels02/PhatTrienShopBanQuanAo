CREATE DATABASE ASM_C4
GO
USE ASM_C4
GO

CREATE TABLE Users (
    IdUser INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    _Password VARCHAR(255) NOT NULL,
    SoDienThoai VARCHAR(15),
    DiaChi NVARCHAR(255),
    ChucVu NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Categories (
    IdCategory INT PRIMARY KEY IDENTITY(1,1),
    TenCategory NVARCHAR(100) NOT NULL,
    MoTaCategory NVARCHAR(255)
);
GO
select * from DonHang
select * from ChiTietDonHang
CREATE TABLE SanPham (
    IdSanPham INT PRIMARY KEY IDENTITY(1,1),
    TenSanPham NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255),
    Gia DECIMAL(10, 2) NOT NULL,
    SoLuong INT NOT NULL,
    HinhAnh VARCHAR(255),
    IdCategory INT REFERENCES Categories(IdCategory) ON DELETE SET NULL
);
GO
select * from sanpham
select * from Categories
CREATE TABLE DonHang (
    IdDonHang INT PRIMARY KEY IDENTITY(1,1),
    IdUser INT REFERENCES Users(IdUser) ON DELETE CASCADE,
    NgayDat DATE NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL,
    TongTien DECIMAL(10, 2) NOT NULL 
);
GO

CREATE TABLE ChiTietDonHang (
    IdChiTietDonHang INT PRIMARY KEY IDENTITY(1,1),
    IdDonHang INT REFERENCES DonHang(IdDonHang) ON DELETE CASCADE,
    IdSanPham INT REFERENCES SanPham(IdSanPham),
    SoLuong INT NOT NULL,
    GiaBan DECIMAL(10, 2) NOT NULL
);
GO
select * from Users

insert into Users values
(N'AdminTVN', 'Admin@gmail.com', 'admin123', '0338582954', N'Đà Nẵng', 'Admin')

insert into Users values
(N'NhanVienTVN', 'nhanvien@gmail.com', 'nhanvien0910', '0354424554', N'Đà Nẵng', 'NV')

insert into Categories values
('Quần', 'Quần dài'),
('Áo', 'Áo'),
('Phụ Kiện', 'Phụ Kiện')

Insert into SanPham values
(N'Áo K2',N'Áo Khoác Màu Bạc',500000 , 59 ,'Images\aokhoatK2.png', 1),
(N'Váy Ngắn',N'Váy màu nâu',100000 , 35 ,'Images\ApplywCasualSimpleShorts.png', 2),
(N'Quần Ngắn',N'Quần màu vàng nhạt',50000 , 59 ,'Images\BlankTechCargoShorts.png', 2),
(N'Áo Blusas',N'Áo hồng',100000 , 59 ,'Images\Blusas-Pink.png', 1),
(N'Áo Brooklyn',N'Áo đen',150000 , 59 ,'Images\BrooklynLetterPrintTShirt.png', 1),
(N'Nón Casquette',N'Nón Casquette',200000 , 59 ,'Images\CasquetteAviateurVintage.png', 3),
(N'Cặp Compact',N'Cặp xám',300000 , 59 ,'Images\CompactCrossbodyTravelBag.png', 3),
(N'Túi xách',N'Túi xách DiagonalFemale',500000 , 59 ,'Images\DiagonalFemaleBag.png', 3),
(N'Dây nịt',N'Men_sRealLeatherRatchetDressBelt',100000 , 59 ,'Images\Men_sRealLeatherRatchetDressBelt.png', 3),
(N'Đồng hồ',N'QuartzWatch-CRRJU2271',700000 , 59 ,'Images\aokhoatK2.png', 3),
(N'Kính mát',N'ToddlerKidsRoundFrameFashionGlasses',50000 , 59 ,'Images\ToddlerKidsRoundFrameFashionGlasses.png', 3),
(N'Bao tay',N'UnlinedGlovesPunk',500000 , 59 ,'Images\UnlinedGlovesPunk.png', 3),
(N'Giày',N'StrapMaryJaneShoes',500000 , 59 ,'Images\StrapMaryJaneShoes.png', 3),
(N'Quần Cotton',N'CottonWovenTrousers',300000 , 59 ,'Images\CottonWovenTrousers.png', 2),
(N'Quần ngắn',N'MallwalWomenShorts',200000 , 59 ,'Images\MallwalWomenShorts.png', 2),
(N'Quần jean',N'WideLegsJeans',500000 , 59 ,'Images\WideLegsJeans.png', 2)


