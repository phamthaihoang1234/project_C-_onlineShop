USE [master]
GO
/****** Object:  Database [PROJECT_PRN211]    Script Date: 3/13/2022 12:45:55 AM ******/
CREATE DATABASE [PROJECT_PRN211]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PROJECT_PRN211', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.NNGHIAA\MSSQL\DATA\PROJECT_PRN211.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PROJECT_PRN211_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.NNGHIAA\MSSQL\DATA\PROJECT_PRN211_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PROJECT_PRN211] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PROJECT_PRN211].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PROJECT_PRN211] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET ARITHABORT OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PROJECT_PRN211] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PROJECT_PRN211] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PROJECT_PRN211] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PROJECT_PRN211] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET RECOVERY FULL 
GO
ALTER DATABASE [PROJECT_PRN211] SET  MULTI_USER 
GO
ALTER DATABASE [PROJECT_PRN211] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PROJECT_PRN211] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PROJECT_PRN211] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PROJECT_PRN211] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PROJECT_PRN211] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PROJECT_PRN211] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PROJECT_PRN211', N'ON'
GO
ALTER DATABASE [PROJECT_PRN211] SET QUERY_STORE = OFF
GO
USE [PROJECT_PRN211]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[BrandID] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nchar](10) NULL,
 CONSTRAINT [PK_dbo.Brand] PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CateID] [int] IDENTITY(1,1) NOT NULL,
	[CateName] [nchar](10) NULL,
 CONSTRAINT [PK_dbo.Category] PRIMARY KEY CLUSTERED 
(
	[CateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Function]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Function](
	[FunctionCode] [varchar](50) NOT NULL,
	[FuctionName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[FunctionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NULL,
	[UserID] [int] NULL,
	[StatusID] [int] NULL,
 CONSTRAINT [PK_dbo.Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Status]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Status](
	[StatusID] [int] NOT NULL,
	[StatusName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Order_Status] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [decimal](18, 0) NULL,
	[TotalCost] [decimal](18, 0) NULL,
 CONSTRAINT [PK_dbo.OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[RoleId] [int] NOT NULL,
	[FunctionCode] [varchar](50) NOT NULL,
	[Note] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[FunctionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[Price] [decimal](18, 0) NULL,
	[Quantity] [int] NULL,
	[Description] [ntext] NULL,
	[Memory] [int] NULL,
	[NewProduct] [bit] NULL,
	[Ram] [int] NULL,
	[Image] [nvarchar](10) NULL,
	[BrandID] [int] NULL,
	[CateID] [int] NULL,
 CONSTRAINT [PK_dbo.Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[RoleName] [varchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/13/2022 12:45:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nchar](10) NULL,
	[Password] [varchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([BrandID], [BrandName]) VALUES (1, N'Asus      ')
INSERT [dbo].[Brand] ([BrandID], [BrandName]) VALUES (2, N'Dell      ')
INSERT [dbo].[Brand] ([BrandID], [BrandName]) VALUES (3, N'Msi       ')
INSERT [dbo].[Brand] ([BrandID], [BrandName]) VALUES (4, N'Lenovo    ')
INSERT [dbo].[Brand] ([BrandID], [BrandName]) VALUES (5, N'Thinkpad  ')
INSERT [dbo].[Brand] ([BrandID], [BrandName]) VALUES (6, N'Macbook   ')
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (1, N'LAPTOP    ')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (2, N'MÀN HÌNH  ')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (3, N'PHỤ KIỆN  ')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[Function] ([FunctionCode], [FuctionName]) VALUES (N'ADM1', N'ViewListProduct')
INSERT [dbo].[Function] ([FunctionCode], [FuctionName]) VALUES (N'ADM2', N'EditListProduct')
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [OrderDate], [UserID], [StatusID]) VALUES (3, CAST(N'2022-03-12T16:02:25.000' AS DateTime), 1, 2)
INSERT [dbo].[Order] ([OrderID], [OrderDate], [UserID], [StatusID]) VALUES (4, CAST(N'2022-03-12T16:26:56.000' AS DateTime), 1, 5)
INSERT [dbo].[Order] ([OrderID], [OrderDate], [UserID], [StatusID]) VALUES (5, CAST(N'2022-03-12T23:20:45.367' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
INSERT [dbo].[Order_Status] ([StatusID], [StatusName]) VALUES (1, N'Đang chờ xác nhận')
INSERT [dbo].[Order_Status] ([StatusID], [StatusName]) VALUES (2, N'Đã xác nhận')
INSERT [dbo].[Order_Status] ([StatusID], [StatusName]) VALUES (3, N'Đã huỷ')
INSERT [dbo].[Order_Status] ([StatusID], [StatusName]) VALUES (4, N'Đã thanh toán')
INSERT [dbo].[Order_Status] ([StatusID], [StatusName]) VALUES (5, N'Đang giao hàng')
GO
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [UnitPrice], [TotalCost]) VALUES (3, 2, 1, CAST(20500000 AS Decimal(18, 0)), CAST(20500000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [UnitPrice], [TotalCost]) VALUES (4, 1, 2, CAST(18500000 AS Decimal(18, 0)), CAST(37000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [UnitPrice], [TotalCost]) VALUES (4, 7, 1, CAST(2500000 AS Decimal(18, 0)), CAST(2500000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [UnitPrice], [TotalCost]) VALUES (5, 1, 1, CAST(18500000 AS Decimal(18, 0)), CAST(18500000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Permission] ([RoleId], [FunctionCode], [Note]) VALUES (1, N'ADM1', N'admin truy cap')
INSERT [dbo].[Permission] ([RoleId], [FunctionCode], [Note]) VALUES (1, N'ADM2', N'admin truy cap')
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [Description], [Memory], [NewProduct], [Ram], [Image], [BrandID], [CateID]) VALUES (1, N'Msi thin', CAST(18500000 AS Decimal(18, 0)), 10, N'may dep', 8, 1, 8, N'p1.jpg', 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [Description], [Memory], [NewProduct], [Ram], [Image], [BrandID], [CateID]) VALUES (2, N'Asus rog', CAST(20500000 AS Decimal(18, 0)), 10, N'may xin', 8, 1, 8, N'ip5.jpg', 2, 2)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [Description], [Memory], [NewProduct], [Ram], [Image], [BrandID], [CateID]) VALUES (4, N'Msi thin', CAST(18500000 AS Decimal(18, 0)), 10, N'may dep ', 8, 1, 8, N'ip5.jpg', 3, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [Description], [Memory], [NewProduct], [Ram], [Image], [BrandID], [CateID]) VALUES (6, N'Màn Hình Đồ Họa Dell Ultrasharp', CAST(11990000 AS Decimal(18, 0)), 10, N'man hinh xin', NULL, 1, NULL, N'ip5.jpg', 2, 2)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Quantity], [Description], [Memory], [NewProduct], [Ram], [Image], [BrandID], [CateID]) VALUES (7, N'man hinh asus', CAST(2500000 AS Decimal(18, 0)), 5, N'man hinh 140hz', NULL, 0, NULL, N'ip5.jpg', 1, 2)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'Mod')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (3, N'Cusomer')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (4, N'User')
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [FullName], [Email], [Phone], [Password], [Address], [RoleID]) VALUES (1, N'nguyen nghia', N'nghia123@gmail,.com', N'0248324   ', N'123', N'hanoi', 3)
INSERT [dbo].[User] ([UserID], [FullName], [Email], [Phone], [Password], [Address], [RoleID]) VALUES (2, N'hoang manh', N'manh456@gmail.com', N'0248324   ', N'123', N'hanoi', 2)
INSERT [dbo].[User] ([UserID], [FullName], [Email], [Phone], [Password], [Address], [RoleID]) VALUES (3, N'admin', N'Admin@gmail.com', N'0923231   ', N'123', N'hanoi', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
/****** Object:  Index [IX_MaUser]    Script Date: 3/13/2022 12:45:56 AM ******/
CREATE NONCLUSTERED INDEX [IX_MaUser] ON [dbo].[Order]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Madon]    Script Date: 3/13/2022 12:45:56 AM ******/
CREATE NONCLUSTERED INDEX [IX_Madon] ON [dbo].[OrderDetail]
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Masp]    Script Date: 3/13/2022 12:45:56 AM ******/
CREATE NONCLUSTERED INDEX [IX_Masp] ON [dbo].[OrderDetail]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Mahang]    Script Date: 3/13/2022 12:45:56 AM ******/
CREATE NONCLUSTERED INDEX [IX_Mahang] ON [dbo].[Product]
(
	[BrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Mahdh]    Script Date: 3/13/2022 12:45:56 AM ******/
CREATE NONCLUSTERED INDEX [IX_Mahdh] ON [dbo].[Product]
(
	[CateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.User_MaUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.User_MaUser]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Order_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Order_Status] ([StatusID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Order_Status]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderDetail_dbo.Order_Madon] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_dbo.OrderDetail_dbo.Order_Madon]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderDetail_dbo.Product_Masp] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_dbo.OrderDetail_dbo.Product_Masp]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Function1] FOREIGN KEY([FunctionCode])
REFERENCES [dbo].[Function] ([FunctionCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Function1]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Role]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Product_dbo.Brand_Mahang] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([BrandID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_dbo.Product_dbo.Brand_Mahang]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Product_dbo.Category_Mahdh] FOREIGN KEY([CateID])
REFERENCES [dbo].[Category] ([CateID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_dbo.Product_dbo.Category_Mahdh]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [PROJECT_PRN211] SET  READ_WRITE 
GO
