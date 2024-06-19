USE [master]
GO
/****** Object:  Database [PP05Tretyakov]    Script Date: 19.06.2024 19:47:19 ******/
CREATE DATABASE [PP05Tretyakov]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PP05Tretyakov', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PP05Tretyakov.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PP05Tretyakov_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PP05Tretyakov_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PP05Tretyakov] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PP05Tretyakov].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PP05Tretyakov] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET ARITHABORT OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PP05Tretyakov] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PP05Tretyakov] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PP05Tretyakov] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PP05Tretyakov] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PP05Tretyakov] SET  MULTI_USER 
GO
ALTER DATABASE [PP05Tretyakov] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PP05Tretyakov] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PP05Tretyakov] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PP05Tretyakov] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PP05Tretyakov] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PP05Tretyakov] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PP05Tretyakov', N'ON'
GO
ALTER DATABASE [PP05Tretyakov] SET QUERY_STORE = OFF
GO
USE [PP05Tretyakov]
GO
/****** Object:  Table [dbo].[Contract]    Script Date: 19.06.2024 19:47:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[Id_Contract] [int] IDENTITY(1,1) NOT NULL,
	[Number] [varchar](35) NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[End_Date] [date] NULL,
	[Amount_Contract] [decimal](18, 2) NOT NULL,
	[Wage_Fund] [decimal](5, 2) NULL,
 CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 19.06.2024 19:47:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id_Department] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](65) NOT NULL,
	[Decription] [text] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 19.06.2024 19:47:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id_Employee] [int] IDENTITY(1,1) NOT NULL,
	[FIO] [varchar](65) NOT NULL,
	[Department_Name] [nchar](65) NOT NULL,
	[Labor_Participation_Rate] [float] NOT NULL,
	[Amount_Employees_Contract] [decimal](18, 2) NOT NULL,
	[Contract_Number] [varchar](35) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id_Employee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Contract] ON 

INSERT [dbo].[Contract] ([Id_Contract], [Number], [Name], [End_Date], [Amount_Contract], [Wage_Fund]) VALUES (1, N'1', N'FSAF      ', CAST(N'2022-06-01' AS Date), CAST(1000000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[Contract] ([Id_Contract], [Number], [Name], [End_Date], [Amount_Contract], [Wage_Fund]) VALUES (2, N'2', N'dsada     ', CAST(N'2024-07-07' AS Date), CAST(2300000.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(5, 2)))
INSERT [dbo].[Contract] ([Id_Contract], [Number], [Name], [End_Date], [Amount_Contract], [Wage_Fund]) VALUES (3, N'3', N'sadasf    ', CAST(N'2024-06-06' AS Date), CAST(32133.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(5, 2)))
SET IDENTITY_INSERT [dbo].[Contract] OFF
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id_Department], [Name], [Decription]) VALUES (1, N'fsdfs                                                            ', N'dsff')
INSERT [dbo].[Department] ([Id_Department], [Name], [Decription]) VALUES (2, N'fsfsdfsad                                                        ', N'dsad')
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id_Employee], [FIO], [Department_Name], [Labor_Participation_Rate], [Amount_Employees_Contract], [Contract_Number]) VALUES (1, N'Сотрудник A', N'fsdfs                                                            ', 1, CAST(30000.00 AS Decimal(18, 2)), N'1')
INSERT [dbo].[Employee] ([Id_Employee], [FIO], [Department_Name], [Labor_Participation_Rate], [Amount_Employees_Contract], [Contract_Number]) VALUES (2, N'Сотрудник B', N'fsdfs                                                            ', 10, CAST(30000.00 AS Decimal(18, 2)), N'1')
INSERT [dbo].[Employee] ([Id_Employee], [FIO], [Department_Name], [Labor_Participation_Rate], [Amount_Employees_Contract], [Contract_Number]) VALUES (5, N'Сотрудник C', N'fsdfs                                                            ', 1, CAST(30000.00 AS Decimal(18, 2)), N'1')
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Contract1] FOREIGN KEY([Contract_Number])
REFERENCES [dbo].[Contract] ([Number])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Contract1]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department1] FOREIGN KEY([Department_Name])
REFERENCES [dbo].[Department] ([Name])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department1]
GO
USE [master]
GO
ALTER DATABASE [PP05Tretyakov] SET  READ_WRITE 
GO
