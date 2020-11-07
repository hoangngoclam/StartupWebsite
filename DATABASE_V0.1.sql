
/*Drop database STARTUP_WEBSITE*/
/*CREATE DATABASE STARTUP_WEBSITE
GO
*/
--CREATE DATABASE STARTUP_WEBSITE
--USE STARTUP_WEBSITE

--GO
/*tẠO CÁC KHUNG CHỨA CÁC BẢNG (TÊN CÁC VÙNG CHỨA BẢNG GIỐNG SƠ ĐỒ ER) */
CREATE SCHEMA LOCATION
GO
CREATE SCHEMA CUSTOMER
GO
CREATE SCHEMA PRODUCT
GO
CREATE SCHEMA CUSTOMER_PRODUCT
GO
CREATE SCHEMA [ORDER]
GO
CREATE SCHEMA ATTRIBUTE
GO
CREATE SCHEMA PRODUCT_CATEGORY
GO
CREATE SCHEMA STARTUP
GO
CREATE SCHEMA BLOG
GO
CREATE SCHEMA PROMOTION
GO
CREATE SCHEMA [SYSTEM]
GO

CREATE TABLE LOCATION.Province
(
	ProvinceId VARCHAR(6) PRIMARY KEY  NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	[Type] NVARCHAR(100),
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
)
GO

CREATE TABLE LOCATION.District
(
	DistrictId VARCHAR(6) PRIMARY KEY NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	[Type] NVARCHAR(100),
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	ProvinceId VARCHAR(6) NOT NULL,
	CONSTRAINT FKDistrict_ProvinceId FOREIGN KEY (ProvinceId) 
	REFERENCES LOCATION.Province(ProvinceId)
)
GO

CREATE TABLE LOCATION.ward
(
	WardId VARCHAR(6) PRIMARY KEY NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	[Type] NVARCHAR(100),
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	DistrictId VARCHAR(6) NOT NULL,
	CONSTRAINT FKWard_DistricId FOREIGN KEY (DistrictId) 
	REFERENCES LOCATION.District(DistrictId)
)
GO

CREATE TABLE CUSTOMER.Customer
(
	CustomerId BIGINT PRIMARY KEY IDENTITY NOT NULL,
	WardId VARCHAR(6) NULL,
	Name NVARCHAR(500) NULL,
	UserName NVARCHAR(200) NULL,
	Email VARCHAR(70) NULL,
	Password VARCHAR(500) NULL,
	FbId VARCHAR(500) NULL,
	GgId VARCHAR(500) NULL,
	DisplayName NVARCHAR(500) NULL,
	MetaTitle VARCHAR(250) NULL,
	Address NVARCHAR(700) NULL,
	BirthDay DATE NULL,
	Gender NVARCHAR(50) NOT NULL CHECK(Gender IN(N'Nam',N'Nữ',N'Giới tính thứ 3')) DEFAULT N'Nam',
	PhoneNumber VARCHAR(15) NULL,
	AvatarUrl VARCHAR(500) DEFAULT 'https://dl.dropboxusercontent.com/s/p91afpcj4x6luur/220px-User_icon_2.svg.png?dl=0',
	Description NTEXT NULL,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
	CONSTRAINT FKCustomer_WardId
	FOREIGN KEY (WardId) REFERENCES LOCATION.ward(WardId)
)
GO

INSERT CUSTOMER.Customer 
( WardId , Name , UserName , Password , FbId , GgId , DisplayName , MetaTitle , 
Address , BirthDay , Gender , PhoneNumber , AvatarUrl , Description ) 
VALUES ( '00001' , N'Trương Hoàng Ngọc Lâm' , N'lam5propropro' , 'lam123' , 'kkdjjfkdj' , 'sdfsadf' , 
N'Hoàng Lâm' , 'hoang-lam' , N'36B/2, Tự Phước, Phường 11, Đà Lạt' , CONVERT(DATETIME, '1998-05-02') , N'Nam' , '0908310267' , 
'https://scontent-hkg3-1.xx.fbcdn.net/v/t1.0-9/89002365_2563773987233567_5674685712608264192_n.jpg?_nc_cat=101&_nc_sid=85a577&_nc_oc=AQmgrTKI75u1MDJZbPCQyNIny4yrnKGGAtQzFFASupUIFahFfqrRYwh_gtourJ8ocdA&_nc_ht=scontent-hkg3-1.xx&oh=8eeaf584dd8f2ef2d64a7d784094df2e&oe=5E952C42' , N'Đẹp trai' ),
( '00001' , N'Nguyen Van A' , N'user1' , 'lam123' , 'kkdjjfkdj' , 'sdfsadf' , 
N'Hoàng Lâm' , 'hoang-lam' , N'36B/2, Tự Phước, Phường 11, Đà Lạt' , CONVERT(DATETIME, '1998-05-02') , N'Nam' , '0908310267' , 
'https://thuthuatnhanh.com/wp-content/uploads/2018/07/anh-dai-dien-dep.jpg' , N'Đẹp trai' ),
( '00001' , N'Nguyen Van B' , N'user2' , 'lam123' , 'kkdjjfkdj' , 'sdfsadf' , 
N'Hoàng Lâm' , 'hoang-lam' , N'36B/2, Tự Phước, Phường 11, Đà Lạt' , CONVERT(DATETIME, '1998-05-02') , N'Nam' , '0908310267' , 
'https://vinagamemobile.com/wp-content/uploads/2018/04/avatar-doi-fb-06.jpg' , N'Đẹp trai' ),
( '00001' , N'Nguyen Van C' , N'user3' , 'lam123' , 'kkdjjfkdj' , 'sdfsadf' , 
N'Hoàng Lâm' , 'hoang-lam' , N'36B/2, Tự Phước, Phường 11, Đà Lạt' , CONVERT(DATETIME, '1998-05-02') , N'Nam' , '0908310267' , 
'https://i.pinimg.com/236x/44/bf/4d/44bf4d17d985189dbfc909c99664123c.jpg' , N'Đẹp trai' ),
( '00001' , N'Nguyen Van D' , N'user4' , 'lam123' , 'kkdjjfkdj' , 'sdfsadf' , 
N'Hoàng Lâm' , 'hoang-lam' , N'36B/2, Tự Phước, Phường 11, Đà Lạt' , CONVERT(DATETIME, '1998-05-02') , N'Nam' , '0908310267' , 
'https://hinhnendephd.com/wp-content/uploads/2019/10/anh-avatar-dep-250x320.jpg' , N'Đẹp trai' )

GO
SELECT * FROM CUSTOMER.Customer
GO

CREATE TABLE [ORDER].[Order]
(
	OrderId BIGINT PRIMARY KEY IDENTITY NOT NULL,
	CustomerId BIGINT NOT NULL,
	PhoneNumber VARCHAR(15),
	Address NVARCHAR(700),
	Name NVARCHAR(250),
	TotalPrice BIGINT,
	Note NTEXT,
	ShipPrice BIGINT,
	[Status] NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Chưa xử lý',N'Đã xử lý',N'Đã Hủy')) DEFAULT N'Chưa xử lý',
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
	CONSTRAINT FKOrder_CustomerId FOREIGN KEY(CustomerId)
	REFERENCES CUSTOMER.Customer(CustomerId)
)
GO
/*Table 6*/
CREATE TABLE ATTRIBUTE.Attribute
(
	AttributeId INT PRIMARY KEY IDENTITY NOT NULL,
	Name NVARCHAR(500),
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
)
GO
CREATE TABLE ATTRIBUTE.SubAttribute
(
	SubAttributeId INT PRIMARY KEY IDENTITY NOT NULL,
	AttributeId INT NOT NULL,
	Name NVARCHAR(200),
	Value NVARCHAR(200),
	[Index] INT DEFAULT 1,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
	CONSTRAINT FKSubAttribute_AttributeId FOREIGN KEY (AttributeId)
	REFERENCES ATTRIBUTE.Attribute(AttributeId)
)
GO

CREATE TABLE PRODUCT_CATEGORY.ProductCategory
(
	ProductCategoryId INT PRIMARY KEY NOT NULL IDENTITY,
	ParentCategoryId INT NULL,
	Name NVARCHAR(250) NOT NULL,
	MetaTitle VARCHAR(250) ,
	DisplayOrder FLOAT,
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Ẩn',N'Hiện')) DEFAULT N'Ẩn',
	SeoTitle NVARCHAR(200),
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
	CONSTRAINT FKProductCategory_ParentCategoryId FOREIGN KEY(ParentCategoryId)
	REFERENCES PRODUCT_CATEGORY.ProductCategory(ProductCategoryId)
)
GO

INSERT  PRODUCT_CATEGORY.ProductCategory
        ( ParentCategoryId, Name, MetaTitle, DisplayOrder, Status, SeoTitle )
VALUES  ( NULL, N'Thời trang nam', 'thoi-trang-nam', 1.0, N'Hiện',
          N'Thời trang dành cho nam giới' ),
        ( 1, N'Áo Thun', 'ao-thun', 1.1, N'Hiện',
          N'Thời trang dành cho nam giới - áo thun' ),
        ( 1, N'Áo Sơ mi', 'ao-so-mi', 1.2, N'Ẩn',
          N'Thời trang dành cho nam giới - áo sơ mi' ),
        ( 1, N'Quẩn', 'quan-nam', 1.3, N'Hiện',
          N'Thời trang dành cho nam giới - Quần' ),
        ( 1, N'Quẩn', 'quan-nam', 1.1, N'Hiện',
          N'Thời trang dành cho nam giới - Quần' ),
        ( NULL, N'Điện thoại & phụ kiện', 'dien-thoai-phu-kien', 2.0, N'Hiện',
          N'Điện thoại & phụ kiện- startup website ' )
GO

CREATE TABLE STARTUP.Startup
(
	StartupId BIGINT PRIMARY KEY NOT NULL IDENTITY,
	WardId VARCHAR(6) NOT NULL,
	Name NVARCHAR(250),
	LogoUrl VARCHAR(500) DEFAULT 'https://dl.dropboxusercontent.com/s/wctezpzx2mczs9r/shop-icon.png?dl=0',
	CoverUrl VARBINARY(500),
	[Description] NTEXT,
	Slogan NVARCHAR(300),
	[Address] NVARCHAR(700),
	PhoneNumber VARCHAR(15),
	MetaTitle VARCHAR(50),
	ViewCount BIGINT DEFAULT 0,
	Email VARCHAR(70),
	SubCount BIGINT DEFAULT 0,
	SeoTitle NVARCHAR(300) DEFAULT 'Startup website',
	FanpageLink VARCHAR(500),
	YoutubeLink VARCHAR(500),
	InstagramLink VARCHAR(500),
	[Status] NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Chưa duyệt',N'Bị ẩn',N'Đang hoạt động',N'Ngừng Hoạt động',N'Bị Khóa')) DEFAULT N'Chưa duyệt',
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
	CONSTRAINT FKStartup_WardId FOREIGN KEY(WardId)
	REFERENCES LOCATION.ward(WardId)
)
GO
INSERT STARTUP.Startup ( WardId , Name, Description , Slogan , Address , PhoneNumber , FanpageLink , YoutubeLink , InstagramLink ) 
VALUES ( '00001' , N'Startup01' , N'<h1>Hello everybody</h1>' , N'Nothing can hold me back' , N'31A Trần Khánh Dư, Phường 8, Đà Lạt' , 
'0908310267' , 'https://regex101.com/codegen?language=php' , 'https://regex101.com/codegen?language=php' , 'https://regex101.com/codegen?language=php' ),
( '00001' , N'Startup02' , N'<h1>Hello everybody</h1>' , N'Nothing can hold me back' , N'31A Trần Khánh Dư, Phường 8, Đà Lạt' , 
'0908310267' , 'https://regex101.com/codegen?language=php' , 'https://regex101.com/codegen?language=php' , 'https://regex101.com/codegen?language=php' ),
( '00001' , N'Startup03' , N'<h1>Hello everybody</h1>' , N'Nothing can hold me back' , N'31A Trần Khánh Dư, Phường 8, Đà Lạt' , 
'0908310267' , 'https://regex101.com/codegen?language=php' , 'https://regex101.com/codegen?language=php' , 'https://regex101.com/codegen?language=php' ),
( '00001' , N'Startup04' , N'<h1>Hello everybody</h1>' , N'Nothing can hold me back' , N'31A Trần Khánh Dư, Phường 8, Đà Lạt' , 
'0908310267' , 'https://regex101.com/codegen?language=php' , 'https://regex101.com/codegen?language=php' , 'https://regex101.com/codegen?language=php' )

GO
/*ROUND(RAND()*10-1,0)*/
CREATE TABLE STARTUP.StartupAccount
(
	StartupAccountId INT NOT NULL PRIMARY KEY IDENTITY,
	StatupId BIGINT ,
	Name NVARCHAR(500),
	UserName NVARCHAR(200),
	Email VARCHAR(200),
	[Password] VARCHAR(250) NOT NULL,
	DisplayName NVARCHAR(500),
	[Description] NTEXT,
	AvatarUrl VARCHAR(500) DEFAULT 'https://dl.dropboxusercontent.com/s/p91afpcj4x6luur/220px-User_icon_2.svg.png?dl=0',
	PhoneNumber VARCHAR(15),
	BirthDay DATE,
	Gender NVARCHAR(50) NOT NULL CHECK(Gender IN(N'Nam',N'Nữ',N'Giới tính thứ 3')) DEFAULT N'Nam',
	[type] NVARCHAR(50) NOT NULL CHECK([type] IN(N'Quản lý',N'Người viết bài')) DEFAULT N'Người viết bài',
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Đang hoạt động',N'Bị cấm'))DEFAULT N'Đang hoạt động',
	[Address] NVARCHAR(500),
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKStartupAccount_StartupId FOREIGN KEY(StatupId)
	REFERENCES STARTUP.Startup(StartupId),
)
GO
INSERT STARTUP.StartupAccount ( StatupId , Name , UserName , Email , Password , 
DisplayName , Description , PhoneNumber , BirthDay , Address ) 
VALUES 
( 1 , N'Truong Hoang Ngoc Lam' , N'lam123' , 'hoanglam@gmail.com' , '123' , 
N'Hoang Lam' , 'cute' , '098340394' , '2/24/1998' , N'BCD Da Lat' ),
( 1 , N'Nguyen Van A' , N'Nguyen A' , 'hoanglam@gmail.com' , '123' , 
N'Nguyen A' , 'cute' , '098340394' , '2/24/1998' , N'BCD Da Lat' ),
( 1 , N'Nguyen Van B' , N'Nguyen B' , 'hoanglam@gmail.com' , '123' , 
N'Nguyen B' , 'cute' , '098340394' , '2/24/1998' , N'BCD Da Lat' ),
( 1 , N'Nguyen Van C' , N'Nguyen C' , 'hoanglam@gmail.com' , '123' , 
N'Nguyen C' , 'cute' , '098340394' , '2/24/1998' , N'BCD Da Lat' ),
( 1 , N'Nguyen Van D' , N'Nguyen D' , 'hoanglam@gmail.com' , '123' , 
N'Nguyen D' , 'cute' , '098340394' , '2/24/1998' , N'BCD Da Lat' )

GO 


CREATE TABLE BLOG.BlogCategory
(
	BlogCategoryId INT PRIMARY KEY NOT NULL IDENTITY,
	ParentId INT NULL,
	Name NVARCHAR(250) NOT NULL,
	MetaTitle VARCHAR(250) ,
	DisplayOrder FLOAT,
	SeoTitle NVARCHAR(200),
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Ẩn',N'Hiện')) DEFAULT N'Ẩn',
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
	CONSTRAINT FKBlogCategory_ParentId FOREIGN KEY(ParentId)
	REFERENCES BLOG.BlogCategory(BlogCategoryId)
)

GO

CREATE TABLE BLOG.StartupBlogCategory
(
	StartupBlogCategoryId INT PRIMARY KEY NOT NULL IDENTITY,
	StartupId BIGINT NOT NULL,
	Name NVARCHAR(250) NOT NULL,
	MetaTitle VARCHAR(250) ,
	DisplayOrder FLOAT,
	SeoTitle NVARCHAR(200),
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Ẩn',N'Hiện')) DEFAULT N'Ẩn',
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKStarupBlogCategory_StartupId FOREIGN KEY(StartupId)
	REFERENCES STARTUP.Startup(StartupId)
)

CREATE TABLE SYSTEM.SystemAccount
(
	SystemAccountId INT NOT NULL PRIMARY KEY IDENTITY,
	Name NVARCHAR(500),
	UserName NVARCHAR(200),
	Email VARCHAR(200),
	[Password] VARCHAR(250),
	DisplayName NVARCHAR(500),
	[Description] NTEXT,
	AvatarUrl VARCHAR(500),
	PhoneNumber VARCHAR(15),
	BirthDay DATE,
	Gender NVARCHAR(50) NOT NULL CHECK(Gender IN(N'Nam',N'Nữ',N'Giới tính thứ 3')) DEFAULT N'Nam',
	[type] NVARCHAR(50) NOT NULL CHECK([type] IN(N'Admin',N'Người viết bài')) DEFAULT N'Người viết bài',
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Đang hoạt động',N'Bị cấm')) DEFAULT N'Đang hoạt động',
	[Address] NVARCHAR(500),
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
)
GO 
INSERT SYSTEM.SystemAccount ( Name , UserName , Email , Password , DisplayName , 
Description , PhoneNumber , BirthDay, Address ) 
VALUES 
( N'Truong Hoang Ngoc Lam' , N'lam5pro' , 'lam5pro@gmail.com' , 'admin' , N'lam5' , 
N'Hello babe' , '092384833' , '2/23/1998' , N'Heksldf DaLat LamDong' ),
( N'Nguyen Van A' , N'nguyen A' , 'nguyenA@gmail.com' , 'admin' , N'Van A' , 
N'Hello babe' , '092384833' , '2/23/1998' , N'Heksldf DaLat LamDong' ),
( N'Nguyen Van B' , N'nguyen B' , 'nguyenA@gmail.com' , 'admin' , N'Van B' , 
N'Hello babe' , '092384833' , '2/23/1998' , N'Heksldf DaLat LamDong' ),
( N'Nguyen Van C' , N'nguyen C' , 'nguyenA@gmail.com' , 'admin' , N'Van C' , 
N'Hello babe' , '092384833' , '2/23/1998' , N'Heksldf DaLat LamDong' ),
( N'Nguyen Van D' , N'nguyen D' , 'nguyenA@gmail.com' , 'admin' , N'Van D' , 
N'Hello babe' , '092384833' , '2/23/1998' , N'Heksldf DaLat LamDong' )
go 

CREATE TABLE BLOG.Blog
(
	BlogId BIGINT PRIMARY KEY NOT NULL IDENTITY,
	StartupBlogCategoryId INT,
	BlogCategoryId INT NOT NULL,
	SystemAdminAuthId INT NULL,
	StartupAccountId INT NULL,
	MainImageUrl NVARCHAR(500) DEFAULT 'https://dl.dropboxusercontent.com/s/cei9fvfyz553w16/2335.jpg?dl=0',
	Title NVARCHAR(500),
	MetaTitle VARCHAR(400),
	SeoTitle NVARCHAR(500),
	Label NVARCHAR(500) DEFAULT N'Blogs, Kiến thức',
	Content NTEXT,
	AllowComment BIT,
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Nháp',N'Đã xuất bản',N'Ẩn')) DEFAULT N'Nháp',
	ViewCount BIGINT DEFAULT 0,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKBlog_StartupBlogCategoryId FOREIGN KEY(StartupBlogCategoryId)
	REFERENCES BLOG.StartupBlogCategory(StartupBlogCategoryId),
	CONSTRAINT FKBlog_BlogCategoryId FOREIGN KEY(BlogCategoryId)
	REFERENCES BLOG.BlogCategory(BlogCategoryId),
	CONSTRAINT FKBlog_StartupAccountId FOREIGN KEY(StartupAccountId)
	REFERENCES STARTUP.StartupAccount(StartupAccountId),
	CONSTRAINT FKBlog_SystemAdminId FOREIGN KEY(SystemAdminAuthId)
	REFERENCES SYSTEM.SystemAccount(SystemAccountId)
)
GO



CREATE TABLE PRODUCT_CATEGORY.StartupCategory
(
	StartupCategoryId INT PRIMARY KEY NOT NULL IDENTITY,
	StartupId BIGINT NULL,
	Name NVARCHAR(250) NOT NULL,
	MetaTitle VARCHAR(250) ,
	DisplayOrder FLOAT,
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Ẩn',N'Hiện')) DEFAULT N'Ẩn',
	SeoTitle NVARCHAR(200),
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKStartupCategory_StartupId FOREIGN KEY(StartupId)
	REFERENCES STARTUP.Startup(StartupId)
)
GO
INSERT PRODUCT_CATEGORY.StartupCategory ( StartupId , Name , MetaTitle , DisplayOrder , SeoTitle ) 
VALUES 
( 2 , N'Áo khoác Xanh' , 'ao-khoac-nu' , 1.0 , N'Sản phẩm áo nữ tốt nhất' ),
( 2 , N'Áo khoác Nữ' , 'ao-khoac-nu' , 1.0 , N'Sản phẩm áo nữ tốt nhất' ),
( 2 , N'Áo khoác Nam' , 'ao-khoac-nu' , 1.0 , N'Sản phẩm áo nữ tốt nhất' ),
( 2 , N'Quần Nam' , 'ao-khoac-nu' , 1.0 , N'Sản phẩm áo nữ tốt nhất' ),
( 2 , N'Quần Nữ' , 'ao-khoac-nu' , 1.0 , N'Sản phẩm áo nữ tốt nhất' )
GO

CREATE TABLE PRODUCT.Product
(
	ProductId BIGINT NOT NULL PRIMARY KEY IDENTITY,
	ProductCategoryId INT NOT NULL,
	StartupId BIGINT NOT NULL,
	AttributeId1 INT NULL,
	AttributeId2 INT NULL,
	StartupCategoryId INT NULL,
	Name NVARCHAR(500),
	metaTitle VARCHAR(200),
	SeoTitle VARCHAR(500),
	Price BIGINT DEFAULT 0,
	PromotionPrice BIGINT DEFAULT NULL,
	Quality INT DEFAULT 0,
	[Description] NTEXT,
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Nháp',N'Hiện',N'Ẩn')) DEFAULT N'Nháp',
	ViewCount BIGINT DEFAULT 0,
	KeyWord NVARCHAR(500) DEFAULT N'Sản phẩm startup, tốt',
	MainImage NVARCHAR(300) DEFAULT 'https://dl.dropboxusercontent.com/s/06f2r9oto0oyhy8/soylent.jpg?dl=0',
	isTrial BIT DEFAULT 0,
	SubImages XML,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKProduct_ProductCategoryId FOREIGN KEY(ProductCategoryId)
	REFERENCES PRODUCT_CATEGORY.ProductCategory(ProductCategoryId),
	CONSTRAINT FKProduct_StartupId FOREIGN KEY(StartupId)
	REFERENCES STARTUP.Startup(StartupId),
	CONSTRAINT FKProduct_AttributeId1 FOREIGN KEY(AttributeId1)
	REFERENCES ATTRIBUTE.Attribute(AttributeId),
	CONSTRAINT FKProduct_AttributeId2 FOREIGN KEY(AttributeId2)
	REFERENCES ATTRIBUTE.Attribute(AttributeId),
	CONSTRAINT FKProduct_StartupCategoryId FOREIGN KEY(StartupCategoryId)
	REFERENCES PRODUCT_CATEGORY.StartupCategory(StartupCategoryId)
)
GO

CREATE TABLE ATTRIBUTE.AttibuteRelationship
(
	AttributeRelationshipId INT NOT NULL PRIMARY KEY IDENTITY,
	ProductId BIGINT NOT NULL,
	SubAttributeId INT NOT NULL,
	price BIGINT DEFAULT 0,
	imageUrl VARCHAR(500),
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKAttibuteRelationship_ProductId FOREIGN KEY (ProductId)
	REFERENCES PRODUCT.Product(ProductId),
	CONSTRAINT FKAttibuteRelationship_SubAttribureId FOREIGN KEY (SubAttributeId)
	REFERENCES ATTRIBUTE.SubAttribute(SubAttributeId)
)
GO

CREATE TABLE CUSTOMER_PRODUCT.Cart
(
	CartId BIGINT NOT NULL PRIMARY KEY IDENTITY,
	ProductId BIGINT NOT NULL,
	CustomerId BIGINT NOT NULL,
	AttributeRelationshipId INT,
	Quality INT DEFAULT 1,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
	CONSTRAINT FKCart_ProductId FOREIGN KEY (ProductId)
	REFERENCES PRODUCT.Product(ProductId),
	CONSTRAINT FKCart_CustomerId FOREIGN KEY (CustomerId)
	REFERENCES CUSTOMER.Customer(CustomerId),
	CONSTRAINT FKOrderDetail_AttributeRelationshipId FOREIGN KEY (AttributeRelationshipId)
	REFERENCES ATTRIBUTE.AttibuteRelationship(AttributeRelationshipId)
)
GO

CREATE TABLE CUSTOMER_PRODUCT.ProductLiked
(
	ProductLikedId BIGINT NOT NULL PRIMARY KEY IDENTITY,
	ProductId BIGINT NOT NULL,
	CustomerId BIGINT NOT NULL,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
	CONSTRAINT FKProductLiked_ProductId FOREIGN KEY (ProductId)
	REFERENCES PRODUCT.Product(ProductId),
	CONSTRAINT FKProductLided_CustomerId FOREIGN KEY (CustomerId)
	REFERENCES CUSTOMER.Customer(CustomerId),
)
GO

CREATE TABLE PRODUCT.Gift
(
	GiftId BIGINT NOT NULL PRIMARY KEY IDENTITY,
	ProductId BIGINT,
	Name NVARCHAR(300),
	ImageUrl VARCHAR(500) DEFAULT 'https://cdn4.iconfinder.com/data/icons/standard-new-year-icons/256/Present.png',
	Quality INT DEFAULT 1,
	price BIGINT DEFAULT 0,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE NULL,
	CONSTRAINT FKProductTrial_ProductId FOREIGN KEY (ProductId)
	REFERENCES PRODUCT.Product(ProductId),
)
GO

CREATE TABLE STARTUP.StartupLiked
(
	StartupLikedId BIGINT NOT NULL PRIMARY KEY IDENTITY,
	StartupId BIGINT NOT NULL,
	CustomerId BIGINT NOT NULL,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKStartupLiked_StartupId FOREIGN KEY (startupId)
	REFERENCES STARTUP.Startup(StartupId),
	CONSTRAINT FKProductLided_CustomerId FOREIGN KEY (CustomerId)
	REFERENCES CUSTOMER.Customer(CustomerId),
)
GO

CREATE TABLE [ORDER].OrderDetail
(
	OrderDetailId BIGINT NOT NULL PRIMARY KEY IDENTITY,
	ProductId BIGINT,
	OrderId BIGINT,
	AttributeRelationshipId INT,
	Quality INT DEFAULT 1,
	TotalPrice BIGINT DEFAULT 0,
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Chờ Thanh Toán',N'Chờ lấy hàng',N'Đang giao',N'Đã giao',N'Đã hủy')) DEFAULT N'Chờ lấy hàng',
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKOrderDetail_ProductId FOREIGN KEY (ProductId)
	REFERENCES PRODUCT.Product(ProductId),
	CONSTRAINT FKOrderDetail_OrderId FOREIGN KEY (OrderId)
	REFERENCES [ORDER].[Order](OrderId),
	CONSTRAINT FKOrderDetail_AttributeRelationshipId FOREIGN KEY (AttributeRelationshipId)
	REFERENCES ATTRIBUTE.AttibuteRelationship(AttributeRelationshipId)
)
GO

CREATE TABLE [ORDER].Rate
(
	RateId BIGINT NOT NULL PRIMARY KEY IDENTITY,
	OrderDetailId BIGINT NOT NULL,
	ProductId BIGINT NOT NULL,
	ImagesUrl XML,
	Content NVARCHAR(500),
	RateNumber TINYINT DEFAULT 5,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKRate_OrderDetailId FOREIGN KEY (OrderDetailId)
	REFERENCES [ORDER].OrderDetail(OrderDetailId),
	CONSTRAINT FKRate_ProductId FOREIGN KEY (ProductId)
	REFERENCES PRODUCT.Product(ProductId)
)
GO

CREATE TABLE SYSTEM.[Log]
(
	LogId BIGINT NOT NULL PRIMARY KEY IDENTITY,
	IdUser INT NULL DEFAULT 0,
	Name NVARCHAR(500),
	[type]NVARCHAR(50) NOT NULL CHECK([Type] IN(N'Error',N'Insert',N'Update',N'Delete',N'Action')) DEFAULT N'Action', 
	SubConent NVARCHAR(500),
	DetailContent NTEXT,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
)
GO

CREATE TABLE SYSTEM.FeedBack
(
	FeedBackId INT NOT NULL PRIMARY KEY IDENTITY,
	Email VARCHAR(100),
	PhoneNumber VARCHAR(15),
	[type]NVARCHAR(50) NOT NULL CHECK([Type] IN(N'Startup',N'Customer_for_web',N'customer_for_startup')) DEFAULT N'Customer_for_web', 
	Content NTEXT,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
)
GO

CREATE TABLE SYSTEM.WebSystem
(
	WebSystemId INT NOT NULL PRIMARY KEY IDENTITY,
	Name NVARCHAR(300),
	Content NTEXT,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
)

GO

CREATE TABLE PROMOTION.Code
(
	CodeId INT NOT NULL PRIMARY KEY IDENTITY,
	StartupId BIGINT NOT NULL,
	ProductId BIGINT NOT NULL,
	PercentDiscount TINYINT,
	PriceDiscount BIGINT,
	[Status]NVARCHAR(50) NOT NULL CHECK([Status] IN(N'Chờ',N'Hiện',N'Ẩn',N'Hết hạng')) DEFAULT N'Chờ',
	EndTime DATETIME,
	StartTime DATETIME,
	CreatedAt DATE DEFAULT GETDATE(),
	UpdatedAt DATE,
	CONSTRAINT FKCode_StartupId FOREIGN KEY (StartupId)
	REFERENCES STARTUP.Startup(StartupId),
	CONSTRAINT FKCode_ProductId FOREIGN KEY (ProductId)
	REFERENCES PRODUCT.Product(ProductId)
)
GO

