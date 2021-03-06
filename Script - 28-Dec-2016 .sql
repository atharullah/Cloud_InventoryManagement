USE [master]
GO
/****** Object:  Database [InventoryManagement]    Script Date: 12/28/2016 6:43:18 PM ******/
CREATE DATABASE [InventoryManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InventoryManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\InventoryManagement.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'InventoryManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\InventoryManagement_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [InventoryManagement] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InventoryManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InventoryManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InventoryManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InventoryManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InventoryManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InventoryManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [InventoryManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InventoryManagement] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [InventoryManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InventoryManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InventoryManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InventoryManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InventoryManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InventoryManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InventoryManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InventoryManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InventoryManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InventoryManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InventoryManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InventoryManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InventoryManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InventoryManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InventoryManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InventoryManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InventoryManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [InventoryManagement] SET  MULTI_USER 
GO
ALTER DATABASE [InventoryManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InventoryManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InventoryManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InventoryManagement] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [InventoryManagement]
GO
/****** Object:  Table [dbo].[Cheque]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cheque](
	[ChequeID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentBillNo] [varchar](200) NULL,
	[ChequeNo] [varchar](60) NULL,
	[ChequeDueDate] [date] NULL,
	[Amount] [int] NULL,
	[IssueDate] [date] NULL,
	[IsChequeClear] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](60) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](60) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_Cheque] PRIMARY KEY CLUSTERED 
(
	[ChequeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](200) NULL,
	[CustomerMobile] [varchar](20) NULL,
	[CustomerAddress] [varchar](600) NULL,
	[CustomerEmail] [varchar](100) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](200) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerType]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerType](
	[CustomerTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerTypeName] [varchar](60) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](200) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_CustomerType] PRIMARY KEY CLUSTERED 
(
	[CustomerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DevelopmentWork]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DevelopmentWork](
	[DevelopmentWorkID] [int] IDENTITY(1,1) NOT NULL,
	[SaleOrderID] [int] NULL,
	[BillNo] [varchar](60) NULL,
	[InventoryTypeID] [int] NULL,
	[ConfirmDate] [date] NULL,
	[DeliveryDate] [date] NULL,
	[TopMeasurement] [varchar](100) NULL,
	[BottomMeasurement] [varchar](100) NULL,
	[DesignPhotoUrl] [varchar](600) NULL,
	[WorkTypeID] [int] NULL,
	[MakingCost] [decimal](18, 0) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](60) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](60) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_DevelopmentWork] PRIMARY KEY CLUSTERED 
(
	[DevelopmentWorkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [varchar](700) NULL,
	[Mobile] [varchar](20) NULL,
	[Email] [varchar](60) NULL,
	[Address] [varchar](700) NULL,
	[JoiningDate] [date] NULL,
	[Detail] [varchar](700) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](60) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](60) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Expense]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Expense](
	[ExpenseID] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseBillNo] [varchar](30) NULL,
	[ExpenseDescription] [varchar](600) NULL,
	[ExpenseAmount] [int] NULL,
	[ExpenseDate] [date] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](200) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
(
	[ExpenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InventoryOrderDetail]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InventoryOrderDetail](
	[InventoryOrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryTypeID] [int] NOT NULL,
	[PurchaseRate] [decimal](18, 0) NULL,
	[Quantity] [int] NULL,
	[TotalItemsCost] [decimal](18, 0) NULL,
	[VatNo] [varchar](60) NULL,
	[SaleRate] [decimal](18, 0) NULL,
	[BarcodeID] [varchar](200) NULL,
	[InventoryOrderID] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](100) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_InventoryOrderDetail] PRIMARY KEY CLUSTERED 
(
	[InventoryOrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InventoryOrders]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InventoryOrders](
	[InventoryOrderID] [int] IDENTITY(1,1) NOT NULL,
	[OwnBillNo] [varchar](60) NULL,
	[PurchaseBillNo] [varchar](60) NULL,
	[PurchaseDate] [date] NULL,
	[BillDate] [date] NULL,
	[CustomerTypeID] [int] NULL,
	[SellerID] [int] NULL,
	[AmountPaid] [decimal](18, 0) NULL,
	[AmountPaidDate] [date] NULL,
	[TotalOrderAmount] [decimal](18, 0) NULL,
	[BalanceAmount] [decimal](18, 0) NULL,
	[Remarks] [varchar](300) NULL,
	[IsCompleted] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](100) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_InventoryOrders] PRIMARY KEY CLUSTERED 
(
	[InventoryOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InventoryTypes]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InventoryTypes](
	[InventoryTypeId] [int] IDENTITY(1,1) NOT NULL,
	[InventoryCode] [varchar](60) NOT NULL,
	[InventoryTypeName] [varchar](60) NULL,
	[InventoryCount] [bigint] NULL,
	[InventoryDescription] [varchar](200) NULL,
	[UnitName] [varchar](60) NULL,
	[VAT] [varchar](60) NULL,
	[LowStockCount] [int] NULL,
	[PurchaseRate] [decimal](18, 0) NULL,
	[SellingRate] [decimal](18, 0) NULL,
	[FastMoving] [bit] NULL,
	[RagNo] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](60) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](60) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_InventoryTypes] PRIMARY KEY CLUSTERED 
(
	[InventoryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](60) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](200) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Salary]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Salary](
	[SalaryID] [int] IDENTITY(1,1) NOT NULL,
	[WorkAssignID] [int] NULL,
	[EmployeeID] [int] NULL,
	[AmountPaid] [decimal](18, 2) NULL,
	[AmountPaidDate] [date] NULL,
	[Remarks] [varchar](600) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](60) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_Salary] PRIMARY KEY CLUSTERED 
(
	[SalaryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SaleOrderDetail]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SaleOrderDetail](
	[SaleOrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryTypeID] [int] NULL,
	[SellingRate] [decimal](18, 0) NULL,
	[Quantity] [int] NULL,
	[TotalAmount] [decimal](18, 0) NULL,
	[VAT] [varchar](60) NULL,
	[SaleOrderID] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](200) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_SaleOrderDetail] PRIMARY KEY CLUSTERED 
(
	[SaleOrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesOrder]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesOrder](
	[SaleOrderID] [int] IDENTITY(1,1) NOT NULL,
	[BillNo] [varchar](60) NULL,
	[CustomerID] [int] NULL,
	[ClassName] [varchar](60) NULL,
	[CustomerTypeID] [int] NULL,
	[PaidAmount] [decimal](18, 0) NULL,
	[RemainingAmount] [decimal](18, 0) NULL,
	[TotalCost] [decimal](18, 0) NULL,
	[SaleOrderDate] [date] NULL,
	[Profit] [decimal](18, 0) NULL,
	[Remarks] [varchar](300) NULL,
	[IsMakingRequired] [bit] NULL,
	[IsCompleted] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](60) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](60) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[SaleOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Seller]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Seller](
	[SellerID] [bigint] IDENTITY(1,1) NOT NULL,
	[SellerName] [varchar](60) NULL,
	[SellerMobile] [varchar](20) NULL,
	[SellerLandline] [varchar](20) NULL,
	[SellerEmail] [varchar](100) NULL,
	[SellerAddress] [varchar](900) NULL,
	[VATNo] [varchar](20) NULL,
	[TINNo] [varchar](20) NULL,
	[AccountNo] [varchar](30) NULL,
	[AccountHolderName] [varchar](200) NULL,
	[BankName] [varchar](200) NULL,
	[IFSCCode] [varchar](20) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](60) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](60) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_Seller] PRIMARY KEY CLUSTERED 
(
	[SellerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NULL,
	[UserName] [varchar](30) NULL,
	[UserRoleTypeID] [int] NULL,
	[Password] [varchar](20) NULL,
	[IsActive] [bit] NULL,
	[Remarks] [varchar](600) NULL,
	[CreatedBy] [varchar](60) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](60) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WorkAssign]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WorkAssign](
	[WorkAssignID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NULL,
	[BillNo] [varchar](60) NULL,
	[WorkTypeID] [int] NULL,
	[WorkCount] [int] NULL,
	[WorkAssignDate] [date] NULL,
	[ExpectedCompletionDate] [date] NULL,
	[CompletedCount] [int] NULL,
	[TotalCost] [decimal](18, 2) NULL,
	[RemainingCost] [decimal](18, 2) NULL,
	[Remarks] [varchar](600) NULL,
	[IsComplete] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](60) NULL,
	[ModifiedBy] [varchar](60) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_WorkAssign] PRIMARY KEY CLUSTERED 
(
	[WorkAssignID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WorkAssignInventoryUsed]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WorkAssignInventoryUsed](
	[WorkAssignInventoryUsedID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryTypeID] [int] NULL,
	[InventoryUsedCount] [int] NULL,
	[WorkAssignID] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](200) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_WorkAssignInventoryUsed] PRIMARY KEY CLUSTERED 
(
	[WorkAssignInventoryUsedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WorkType]    Script Date: 12/28/2016 6:43:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WorkType](
	[WorkTypeID] [int] IDENTITY(1,1) NOT NULL,
	[WorkTypeName] [varchar](60) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [varchar](200) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_WorkType] PRIMARY KEY CLUSTERED 
(
	[WorkTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [CustomerMobile], [CustomerAddress], [CustomerEmail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (11, N'Athar', N'8983209880', NULL, N'atharm621@gmail.com', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [CustomerMobile], [CustomerAddress], [CustomerEmail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (12, N'Wajahat', N'8097166689', NULL, N'', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [CustomerMobile], [CustomerAddress], [CustomerEmail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (13, N'Self', N'8983209880', NULL, N'', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [CustomerMobile], [CustomerAddress], [CustomerEmail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (14, N'Farhan', N'9769680668', N'', N'abc@gmail.com', 1, N'Athar', CAST(0x443C0B00 AS Date), N'Athar', CAST(0x443C0B00 AS Date))
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [CustomerMobile], [CustomerAddress], [CustomerEmail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (15, N'Farhan', N'9696806681', NULL, N'', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [CustomerMobile], [CustomerAddress], [CustomerEmail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (16, N'Asif', N'9773204699', NULL, N'', 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [CustomerMobile], [CustomerAddress], [CustomerEmail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (17, N'athar', N'8523697410', NULL, N'', 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[CustomerType] ON 

INSERT [dbo].[CustomerType] ([CustomerTypeID], [CustomerTypeName], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Cash', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[CustomerType] ([CustomerTypeID], [CustomerTypeName], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Credit', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CustomerType] OFF
SET IDENTITY_INSERT [dbo].[DevelopmentWork] ON 

INSERT [dbo].[DevelopmentWork] ([DevelopmentWorkID], [SaleOrderID], [BillNo], [InventoryTypeID], [ConfirmDate], [DeliveryDate], [TopMeasurement], [BottomMeasurement], [DesignPhotoUrl], [WorkTypeID], [MakingCost], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 7, N'SBL-3', 10, CAST(0x3E3C0B00 AS Date), CAST(0x473C0B00 AS Date), N'32', N'25', NULL, 1, CAST(100 AS Decimal(18, 0)), 1, N'Athar', CAST(0x433C0B00 AS Date), NULL, NULL)
INSERT [dbo].[DevelopmentWork] ([DevelopmentWorkID], [SaleOrderID], [BillNo], [InventoryTypeID], [ConfirmDate], [DeliveryDate], [TopMeasurement], [BottomMeasurement], [DesignPhotoUrl], [WorkTypeID], [MakingCost], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 9, N'SBL-8', 12, CAST(0x443C0B00 AS Date), CAST(0x483C0B00 AS Date), N'32', N'56', NULL, 1, CAST(200 AS Decimal(18, 0)), 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[DevelopmentWork] OFF
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeID], [EmployeeName], [Mobile], [Email], [Address], [JoiningDate], [Detail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Maher Shaikh', N'8983209882', N'Atharullah1@gmail.com', N'Ingoli Pura
Old Town
Badnera', CAST(0x363C0B00 AS Date), N'Badnera4', 1, N'Athar', CAST(0x423C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeID], [EmployeeName], [Mobile], [Email], [Address], [JoiningDate], [Detail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Maher', N'8983209880', N'Atharullah1@gmail.com', N'Ingoli Pura
Old Town
Badnera', CAST(0x423C0B00 AS Date), N'Test', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Employee] ([EmployeeID], [EmployeeName], [Mobile], [Email], [Address], [JoiningDate], [Detail], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'Maher', N'8983209881', N'Atharullah1@gmail.com', N'dd', CAST(0x423C0B00 AS Date), N'dd', 1, N'Athar', CAST(0x423C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[Expense] ON 

INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'SBL-01', N'Breakfast', 10, CAST(0x2F3C0B00 AS Date), NULL, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'SBL-1', N'Breakfast23', 10, CAST(0x2F3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), N'Athar', CAST(0x423C0B00 AS Date))
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'SBL-2', N'Hello', 10, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'SBL-3', N'Hey', 250, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'SBL-4', N'Hey', 250, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (6, N'SBL-5', N'Hi', 54, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (7, N'SBL-6', N'254', 10, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (8, N'SBL-7', N'hkiu', 254, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (9, N'SBL-8', N'hgg', 254, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (10, N'SBL-9', N'hjslkdj', 2554, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (11, N'SBL-10', N'jndgd', 5847, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (12, N'SBL-11', N'hjdshjds', 5847, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (13, N'SBL-12', N'54854', 25447, CAST(0x3E3C0B00 AS Date), 1, N'Athar', CAST(0x3E3C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (14, N'SBL-13', N'breakfast', 250, CAST(0x423C0B00 AS Date), 1, N'Athar', CAST(0x423C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (15, N'SBL-14', N'Lunch', 350, CAST(0x423C0B00 AS Date), 1, N'Athar', CAST(0x423C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseID], [ExpenseBillNo], [ExpenseDescription], [ExpenseAmount], [ExpenseDate], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (16, N'SBL-15', N'ggg', 256, CAST(0x443C0B00 AS Date), 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Expense] OFF
SET IDENTITY_INSERT [dbo].[InventoryOrderDetail] ON 

INSERT [dbo].[InventoryOrderDetail] ([InventoryOrderDetailID], [InventoryTypeID], [PurchaseRate], [Quantity], [TotalItemsCost], [VatNo], [SaleRate], [BarcodeID], [InventoryOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 14, CAST(34 AS Decimal(18, 0)), 30, CAST(1051 AS Decimal(18, 0)), N'3', CAST(40 AS Decimal(18, 0)), NULL, 2, 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryOrderDetail] ([InventoryOrderDetailID], [InventoryTypeID], [PurchaseRate], [Quantity], [TotalItemsCost], [VatNo], [SaleRate], [BarcodeID], [InventoryOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, 12, CAST(10 AS Decimal(18, 0)), 10, CAST(101 AS Decimal(18, 0)), N'1', CAST(15 AS Decimal(18, 0)), NULL, 2, 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryOrderDetail] ([InventoryOrderDetailID], [InventoryTypeID], [PurchaseRate], [Quantity], [TotalItemsCost], [VatNo], [SaleRate], [BarcodeID], [InventoryOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, 21, CAST(55 AS Decimal(18, 0)), 10, CAST(550 AS Decimal(18, 0)), N'', CAST(75 AS Decimal(18, 0)), NULL, 3, 1, N'Athar', CAST(0x453C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
SET IDENTITY_INSERT [dbo].[InventoryOrderDetail] OFF
SET IDENTITY_INSERT [dbo].[InventoryOrders] ON 

INSERT [dbo].[InventoryOrders] ([InventoryOrderID], [OwnBillNo], [PurchaseBillNo], [PurchaseDate], [BillDate], [CustomerTypeID], [SellerID], [AmountPaid], [AmountPaidDate], [TotalOrderAmount], [BalanceAmount], [Remarks], [IsCompleted], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'PBL--01', N'BL-006', CAST(0x443C0B00 AS Date), CAST(0x443C0B00 AS Date), 1, 4, CAST(1152 AS Decimal(18, 0)), CAST(0x443C0B00 AS Date), CAST(1152 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'', 1, 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryOrders] ([InventoryOrderID], [OwnBillNo], [PurchaseBillNo], [PurchaseDate], [BillDate], [CustomerTypeID], [SellerID], [AmountPaid], [AmountPaidDate], [TotalOrderAmount], [BalanceAmount], [Remarks], [IsCompleted], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'PBL-2', N'P-26', CAST(0x453C0B00 AS Date), CAST(0x453C0B00 AS Date), 2, 4, CAST(550 AS Decimal(18, 0)), CAST(0x453C0B00 AS Date), CAST(550 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'', 0, 1, N'Athar', CAST(0x453C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
SET IDENTITY_INSERT [dbo].[InventoryOrders] OFF
SET IDENTITY_INSERT [dbo].[InventoryTypes] ON 

INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (12, N'CP', N'Coat Pant', -40, N'', N'Peice', N'1', 0, CAST(10 AS Decimal(18, 0)), CAST(15 AS Decimal(18, 0)), 0, N'', 1, N'Athar', CAST(0x443C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (13, N'SH', N'Shoes', -70, N'', N'Peice', N'1', 0, CAST(10 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), 0, N'1', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (14, N'g2g2', N'Grossary', 30, N'work', N'2', N'3', 2, CAST(34 AS Decimal(18, 0)), CAST(40 AS Decimal(18, 0)), 1, N'dfh5255', 1, N'Athar', CAST(0x443C0B00 AS Date), N'Athar', CAST(0x443C0B00 AS Date))
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (15, N'G123', N'Grossaary', NULL, N'House stuff', N'a1234', N'3', 25, CAST(2215 AS Decimal(18, 0)), CAST(2556 AS Decimal(18, 0)), 1, N'dsfg', 1, N'Athar', CAST(0x443C0B00 AS Date), N'Athar', CAST(0x443C0B00 AS Date))
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (16, N'BP', N'Blank PaPer', -20, N'', N'Peace', N'1', 2, CAST(10 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), 1, N'1', 1, N'Athar', CAST(0x443C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (17, N'PUP', N'Push Up Paper', -10, N'', N'Peice', N'', 0, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 0, N'', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (18, N'NP', N'New Paper', NULL, N'', N'Peice', N'', 10, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 0, N'', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (19, N'LP', N'Latest Paper', NULL, N'', N'Peice', N'1', 2, CAST(10 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), 0, N'1', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (20, N'NN', N'Naan', NULL, N'', N'Peace', N'1.2', 0, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 0, N'', 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (21, N'PP', N'Pepsi', 10, N'', N'Cold Drinks', N'1', 0, CAST(55 AS Decimal(18, 0)), CAST(75 AS Decimal(18, 0)), 1, N'', 1, N'Athar', CAST(0x453C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (22, N'CC', N'CocaCola', -2, N'', N'Cold Drinks', N'', 0, CAST(45 AS Decimal(18, 0)), CAST(75 AS Decimal(18, 0)), 1, N'', 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (23, N'PS', N'Sprite', -20, N'', N'Cold Drink', N'', 0, CAST(55 AS Decimal(18, 0)), CAST(75 AS Decimal(18, 0)), 0, N'', 1, N'Athar', CAST(0x453C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (24, N'NC', N'NewCloth', NULL, N'', N'Peice', N'', 0, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 0, N'', 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (25, N'BT', N'Boot', NULL, N'', N'n', N'', 0, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 0, N'', 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (26, N'LB', N'Latest Beuty', NULL, N'', N'NU', N'', 0, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 0, N'', 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[InventoryTypes] ([InventoryTypeId], [InventoryCode], [InventoryTypeName], [InventoryCount], [InventoryDescription], [UnitName], [VAT], [LowStockCount], [PurchaseRate], [SellingRate], [FastMoving], [RagNo], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (27, N'GEN', N'General', NULL, N'', N'G', N'', 0, CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 0, N'', 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[InventoryTypes] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [RoleName], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Admin', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Role] ([RoleID], [RoleName], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Normal', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Salary] ON 

INSERT [dbo].[Salary] ([SalaryID], [WorkAssignID], [EmployeeID], [AmountPaid], [AmountPaidDate], [Remarks], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 1, 1, CAST(100.00 AS Decimal(18, 2)), CAST(0x443C0B00 AS Date), N'', NULL, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Salary] ([SalaryID], [WorkAssignID], [EmployeeID], [AmountPaid], [AmountPaidDate], [Remarks], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 2, 1, CAST(10.00 AS Decimal(18, 2)), CAST(0x453C0B00 AS Date), N'', NULL, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Salary] ([SalaryID], [WorkAssignID], [EmployeeID], [AmountPaid], [AmountPaidDate], [Remarks], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, 3, 1, CAST(100.00 AS Decimal(18, 2)), CAST(0x453C0B00 AS Date), N'', 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Salary] ([SalaryID], [WorkAssignID], [EmployeeID], [AmountPaid], [AmountPaidDate], [Remarks], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, 4, 1, CAST(100.00 AS Decimal(18, 2)), CAST(0x453C0B00 AS Date), N'', 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Salary] OFF
SET IDENTITY_INSERT [dbo].[SaleOrderDetail] ON 

INSERT [dbo].[SaleOrderDetail] ([SaleOrderDetailID], [InventoryTypeID], [SellingRate], [Quantity], [TotalAmount], [VAT], [SaleOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (9, 13, CAST(20 AS Decimal(18, 0)), 20, CAST(404 AS Decimal(18, 0)), N'1', 8, 1, N'Athar', CAST(0x443C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
INSERT [dbo].[SaleOrderDetail] ([SaleOrderDetailID], [InventoryTypeID], [SellingRate], [Quantity], [TotalAmount], [VAT], [SaleOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (10, 17, CAST(30 AS Decimal(18, 0)), 10, CAST(303 AS Decimal(18, 0)), N'1', 10, 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SaleOrderDetail] ([SaleOrderDetailID], [InventoryTypeID], [SellingRate], [Quantity], [TotalAmount], [VAT], [SaleOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (11, 16, CAST(20 AS Decimal(18, 0)), 10, CAST(202 AS Decimal(18, 0)), N'1', 10, 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SaleOrderDetail] ([SaleOrderDetailID], [InventoryTypeID], [SellingRate], [Quantity], [TotalAmount], [VAT], [SaleOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (12, 22, CAST(75 AS Decimal(18, 0)), 2, CAST(150 AS Decimal(18, 0)), N'', 12, 1, N'Athar', CAST(0x453C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
INSERT [dbo].[SaleOrderDetail] ([SaleOrderDetailID], [InventoryTypeID], [SellingRate], [Quantity], [TotalAmount], [VAT], [SaleOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (13, 16, CAST(20 AS Decimal(18, 0)), 10, CAST(202 AS Decimal(18, 0)), N'1', 13, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SaleOrderDetail] ([SaleOrderDetailID], [InventoryTypeID], [SellingRate], [Quantity], [TotalAmount], [VAT], [SaleOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (14, 12, CAST(15 AS Decimal(18, 0)), 20, CAST(303 AS Decimal(18, 0)), N'1', 14, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SaleOrderDetail] ([SaleOrderDetailID], [InventoryTypeID], [SellingRate], [Quantity], [TotalAmount], [VAT], [SaleOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (15, 13, CAST(20 AS Decimal(18, 0)), 20, CAST(404 AS Decimal(18, 0)), N'1', 14, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SaleOrderDetail] ([SaleOrderDetailID], [InventoryTypeID], [SellingRate], [Quantity], [TotalAmount], [VAT], [SaleOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (16, 12, CAST(15 AS Decimal(18, 0)), 10, CAST(152 AS Decimal(18, 0)), N'1', 14, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SaleOrderDetail] ([SaleOrderDetailID], [InventoryTypeID], [SellingRate], [Quantity], [TotalAmount], [VAT], [SaleOrderID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (17, 13, CAST(20 AS Decimal(18, 0)), 10, CAST(202 AS Decimal(18, 0)), N'1', 15, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[SaleOrderDetail] OFF
SET IDENTITY_INSERT [dbo].[SalesOrder] ON 

INSERT [dbo].[SalesOrder] ([SaleOrderID], [BillNo], [CustomerID], [ClassName], [CustomerTypeID], [PaidAmount], [RemainingAmount], [TotalCost], [SaleOrderDate], [Profit], [Remarks], [IsMakingRequired], [IsCompleted], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (8, N'SBL-01', 11, N'', 1, CAST(404 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(404 AS Decimal(18, 0)), CAST(0x443C0B00 AS Date), CAST(10 AS Decimal(18, 0)), N'', 0, 1, 1, N'Athar', CAST(0x443C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
INSERT [dbo].[SalesOrder] ([SaleOrderID], [BillNo], [CustomerID], [ClassName], [CustomerTypeID], [PaidAmount], [RemainingAmount], [TotalCost], [SaleOrderDate], [Profit], [Remarks], [IsMakingRequired], [IsCompleted], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (9, N'SBL-8', 13, N'', 1, CAST(200 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(0x443C0B00 AS Date), CAST(0 AS Decimal(18, 0)), N'', 1, 0, 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SalesOrder] ([SaleOrderID], [BillNo], [CustomerID], [ClassName], [CustomerTypeID], [PaidAmount], [RemainingAmount], [TotalCost], [SaleOrderDate], [Profit], [Remarks], [IsMakingRequired], [IsCompleted], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (10, N'SBL-9', 11, N'', 1, CAST(505 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(505 AS Decimal(18, 0)), CAST(0x443C0B00 AS Date), CAST(40 AS Decimal(18, 0)), N'', 0, 0, 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SalesOrder] ([SaleOrderID], [BillNo], [CustomerID], [ClassName], [CustomerTypeID], [PaidAmount], [RemainingAmount], [TotalCost], [SaleOrderDate], [Profit], [Remarks], [IsMakingRequired], [IsCompleted], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (12, N'SBL-10', 11, N'', 1, CAST(100 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)), CAST(150 AS Decimal(18, 0)), CAST(0x453C0B00 AS Date), CAST(30 AS Decimal(18, 0)), N'', 0, 0, 1, N'Athar', CAST(0x453C0B00 AS Date), N'Athar', CAST(0x453C0B00 AS Date))
INSERT [dbo].[SalesOrder] ([SaleOrderID], [BillNo], [CustomerID], [ClassName], [CustomerTypeID], [PaidAmount], [RemainingAmount], [TotalCost], [SaleOrderDate], [Profit], [Remarks], [IsMakingRequired], [IsCompleted], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (13, N'SBL-12', 12, N'', 1, CAST(200 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(202 AS Decimal(18, 0)), CAST(0x453C0B00 AS Date), CAST(10 AS Decimal(18, 0)), N'', 0, 0, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SalesOrder] ([SaleOrderID], [BillNo], [CustomerID], [ClassName], [CustomerTypeID], [PaidAmount], [RemainingAmount], [TotalCost], [SaleOrderDate], [Profit], [Remarks], [IsMakingRequired], [IsCompleted], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (14, N'SBL-13', 17, N'', 1, CAST(10 AS Decimal(18, 0)), CAST(849 AS Decimal(18, 0)), CAST(859 AS Decimal(18, 0)), CAST(0x453C0B00 AS Date), CAST(20 AS Decimal(18, 0)), N'', 0, 0, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[SalesOrder] ([SaleOrderID], [BillNo], [CustomerID], [ClassName], [CustomerTypeID], [PaidAmount], [RemainingAmount], [TotalCost], [SaleOrderDate], [Profit], [Remarks], [IsMakingRequired], [IsCompleted], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (15, N'SBL-14', 11, N'Ninth', 1, CAST(200 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(202 AS Decimal(18, 0)), CAST(0x453C0B00 AS Date), CAST(10 AS Decimal(18, 0)), N'', 0, 0, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[SalesOrder] OFF
SET IDENTITY_INSERT [dbo].[Seller] ON 

INSERT [dbo].[Seller] ([SellerID], [SellerName], [SellerMobile], [SellerLandline], [SellerEmail], [SellerAddress], [VATNo], [TINNo], [AccountNo], [AccountHolderName], [BankName], [IFSCCode], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Afzal Guru', N'8983209880', N'07212580811', N'', N'', N'02254', N'45587', N'3033021416', N'Nazim', N'SBI', N'SBI044', NULL, N'Athar', CAST(0x3A3C0B00 AS Date), N'Athar', CAST(0x3B3C0B00 AS Date))
INSERT [dbo].[Seller] ([SellerID], [SellerName], [SellerMobile], [SellerLandline], [SellerEmail], [SellerAddress], [VATNo], [TINNo], [AccountNo], [AccountHolderName], [BankName], [IFSCCode], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Afzal Guru', N'8983209880', N'123456789', N'', N'', N'', N'', N'', N'', N'', N'', 1, N'Athar', CAST(0x423C0B00 AS Date), N'Athar', CAST(0x443C0B00 AS Date))
INSERT [dbo].[Seller] ([SellerID], [SellerName], [SellerMobile], [SellerLandline], [SellerEmail], [SellerAddress], [VATNo], [TINNo], [AccountNo], [AccountHolderName], [BankName], [IFSCCode], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'Baidu Khan', N'8983209880', N'7214364852', N'sss@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'Athar', CAST(0x433C0B00 AS Date), NULL, NULL)
INSERT [dbo].[Seller] ([SellerID], [SellerName], [SellerMobile], [SellerLandline], [SellerEmail], [SellerAddress], [VATNo], [TINNo], [AccountNo], [AccountHolderName], [BankName], [IFSCCode], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'Farhan', N'9769680668', N'256458', N'a@b', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Seller] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [EmployeeID], [UserName], [UserRoleTypeID], [Password], [IsActive], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, NULL, N'Athar', 1, N'123', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([UserID], [EmployeeID], [UserName], [UserRoleTypeID], [Password], [IsActive], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 1, N'Atharullah', 1, N'123', 1, N'done', N'Athar', CAST(0x423C0B00 AS Date), NULL, NULL)
INSERT [dbo].[User] ([UserID], [EmployeeID], [UserName], [UserRoleTypeID], [Password], [IsActive], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, 1, N'Athar2', 1, N'123', 1, N'', N'Athar', CAST(0x423C0B00 AS Date), NULL, NULL)
INSERT [dbo].[User] ([UserID], [EmployeeID], [UserName], [UserRoleTypeID], [Password], [IsActive], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, 3, N'Sandesh', 2, N'123', 1, N'10', N'Athar', CAST(0x443C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[WorkAssign] ON 

INSERT [dbo].[WorkAssign] ([WorkAssignID], [EmployeeID], [BillNo], [WorkTypeID], [WorkCount], [WorkAssignDate], [ExpectedCompletionDate], [CompletedCount], [TotalCost], [RemainingCost], [Remarks], [IsComplete], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate]) VALUES (1, 1, N'SBL-8', 1, 200, CAST(0x443C0B00 AS Date), CAST(0x483C0B00 AS Date), 2, NULL, NULL, N'', 0, NULL, N'Athar', NULL, CAST(0x443C0B00 AS Date), NULL)
INSERT [dbo].[WorkAssign] ([WorkAssignID], [EmployeeID], [BillNo], [WorkTypeID], [WorkCount], [WorkAssignDate], [ExpectedCompletionDate], [CompletedCount], [TotalCost], [RemainingCost], [Remarks], [IsComplete], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate]) VALUES (2, 1, N'SBL-8', 1, 200, CAST(0x3E3C0B00 AS Date), CAST(0x473C0B00 AS Date), 1, NULL, NULL, N'', 0, NULL, N'Athar', NULL, CAST(0x443C0B00 AS Date), NULL)
INSERT [dbo].[WorkAssign] ([WorkAssignID], [EmployeeID], [BillNo], [WorkTypeID], [WorkCount], [WorkAssignDate], [ExpectedCompletionDate], [CompletedCount], [TotalCost], [RemainingCost], [Remarks], [IsComplete], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate]) VALUES (3, 1, N'SBL-8', 1, 100, CAST(0x453C0B00 AS Date), CAST(0x453C0B00 AS Date), 2, CAST(5000.00 AS Decimal(18, 2)), CAST(4900.00 AS Decimal(18, 2)), N'', 0, 1, N'Athar', NULL, CAST(0x453C0B00 AS Date), NULL)
INSERT [dbo].[WorkAssign] ([WorkAssignID], [EmployeeID], [BillNo], [WorkTypeID], [WorkCount], [WorkAssignDate], [ExpectedCompletionDate], [CompletedCount], [TotalCost], [RemainingCost], [Remarks], [IsComplete], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate]) VALUES (4, 1, N'SBL-8', 1, 10, CAST(0x353C0B00 AS Date), CAST(0x453C0B00 AS Date), 10, CAST(200.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), N'', 1, 1, N'Athar', NULL, CAST(0x453C0B00 AS Date), NULL)
SET IDENTITY_INSERT [dbo].[WorkAssign] OFF
SET IDENTITY_INSERT [dbo].[WorkAssignInventoryUsed] ON 

INSERT [dbo].[WorkAssignInventoryUsed] ([WorkAssignInventoryUsedID], [InventoryTypeID], [InventoryUsedCount], [WorkAssignID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 13, 20, 3, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[WorkAssignInventoryUsed] ([WorkAssignInventoryUsedID], [InventoryTypeID], [InventoryUsedCount], [WorkAssignID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 12, 10, 3, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[WorkAssignInventoryUsed] ([WorkAssignInventoryUsedID], [InventoryTypeID], [InventoryUsedCount], [WorkAssignID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, 23, 20, 4, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
INSERT [dbo].[WorkAssignInventoryUsed] ([WorkAssignInventoryUsedID], [InventoryTypeID], [InventoryUsedCount], [WorkAssignID], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, 12, 10, 4, 1, N'Athar', CAST(0x453C0B00 AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[WorkAssignInventoryUsed] OFF
SET IDENTITY_INSERT [dbo].[WorkType] ON 

INSERT [dbo].[WorkType] ([WorkTypeID], [WorkTypeName], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Making', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[WorkType] ([WorkTypeID], [WorkTypeName], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Cutting', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[WorkType] ([WorkTypeID], [WorkTypeName], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'Finishing', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[WorkType] OFF
ALTER TABLE [dbo].[Cheque] ADD  CONSTRAINT [DF_Cheque_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CustomerType] ADD  CONSTRAINT [DF_CustomerType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[DevelopmentWork] ADD  CONSTRAINT [DF_DevelopmentWork_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Expense] ADD  CONSTRAINT [DF_Expense_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[InventoryOrderDetail] ADD  CONSTRAINT [DF_InventoryOrderDetail_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[InventoryOrders] ADD  CONSTRAINT [DF_InventoryOrders_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[InventoryTypes] ADD  CONSTRAINT [DF_InventoryTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Salary] ADD  CONSTRAINT [DF_Salary_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SaleOrderDetail] ADD  CONSTRAINT [DF_SaleOrderDetail_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SalesOrder] ADD  CONSTRAINT [DF_SalesOrder_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Seller] ADD  CONSTRAINT [DF_Seller_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[WorkAssign] ADD  CONSTRAINT [DF_WorkAssign_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[WorkAssignInventoryUsed] ADD  CONSTRAINT [DF_WorkAssignInventoryUsed_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[WorkType] ADD  CONSTRAINT [DF_WorkType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
USE [master]
GO
ALTER DATABASE [InventoryManagement] SET  READ_WRITE 
GO
