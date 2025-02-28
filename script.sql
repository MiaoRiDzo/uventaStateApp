USE [master]
GO
/****** Object:  Database [stateUventa]    Script Date: 12.04.2024 12:31:47 ******/
CREATE DATABASE [stateUventa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'stateUventa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\stateUventa.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'stateUventa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\stateUventa_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [stateUventa] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [stateUventa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [stateUventa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [stateUventa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [stateUventa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [stateUventa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [stateUventa] SET ARITHABORT OFF 
GO
ALTER DATABASE [stateUventa] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [stateUventa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [stateUventa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [stateUventa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [stateUventa] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [stateUventa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [stateUventa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [stateUventa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [stateUventa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [stateUventa] SET  DISABLE_BROKER 
GO
ALTER DATABASE [stateUventa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [stateUventa] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [stateUventa] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [stateUventa] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [stateUventa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [stateUventa] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [stateUventa] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [stateUventa] SET RECOVERY FULL 
GO
ALTER DATABASE [stateUventa] SET  MULTI_USER 
GO
ALTER DATABASE [stateUventa] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [stateUventa] SET DB_CHAINING OFF 
GO
ALTER DATABASE [stateUventa] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [stateUventa] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [stateUventa] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [stateUventa] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [stateUventa] SET QUERY_STORE = OFF
GO
USE [stateUventa]
GO
/****** Object:  Table [dbo].[Appeal]    Script Date: 12.04.2024 12:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appeal](
	[ID_Appeal] [int] NOT NULL,
	[ID_Premises] [int] NULL,
	[ID_ServiceProvider] [int] NOT NULL,
	[ID_Status] [int] NOT NULL,
	[AppealMessage] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Appeal] PRIMARY KEY CLUSTERED 
(
	[ID_Appeal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Build]    Script Date: 12.04.2024 12:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Build](
	[ID_Build] [int] NOT NULL,
	[Floors] [int] NULL,
	[BuildAdress] [nvarchar](50) NULL,
 CONSTRAINT [PK_Build] PRIMARY KEY CLUSTERED 
(
	[ID_Build] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Premises]    Script Date: 12.04.2024 12:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Premises](
	[ID_Premises] [int] NOT NULL,
	[ID_State] [int] NOT NULL,
	[ID_Build] [int] NOT NULL,
	[PremisesName] [nvarchar](50) NOT NULL,
	[PremisesDescription] [nvarchar](500) NULL,
	[PremisesArea] [float] NOT NULL,
 CONSTRAINT [PK_Premises] PRIMARY KEY CLUSTERED 
(
	[ID_Premises] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12.04.2024 12:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID_Role] [int] NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID_Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceProvider]    Script Date: 12.04.2024 12:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceProvider](
	[ID_ServiceProvider] [int] NOT NULL,
	[ServiceProviderName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[INN] [nvarchar](25) NOT NULL,
	[EMail] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ServiceProvider] PRIMARY KEY CLUSTERED 
(
	[ID_ServiceProvider] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 12.04.2024 12:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[ID_State] [int] NOT NULL,
	[StateName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[ID_State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 12.04.2024 12:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[ID_Status] [int] NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[ID_Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12.04.2024 12:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID_User] [int] NOT NULL,
	[ID_Role] [int] NOT NULL,
	[ID_ServiceProvider] [int] NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [char](32) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Appeal] ([ID_Appeal], [ID_Premises], [ID_ServiceProvider], [ID_Status], [AppealMessage]) VALUES (0, 0, 1, 1, N'Тестирование более длинного сообщения')
INSERT [dbo].[Appeal] ([ID_Appeal], [ID_Premises], [ID_ServiceProvider], [ID_Status], [AppealMessage]) VALUES (2, 0, 1, 0, N'Test 3')
GO
INSERT [dbo].[Build] ([ID_Build], [Floors], [BuildAdress]) VALUES (1, 6, N'Гребнева 1')
INSERT [dbo].[Build] ([ID_Build], [Floors], [BuildAdress]) VALUES (2, 1, N'Луговая 10')
INSERT [dbo].[Build] ([ID_Build], [Floors], [BuildAdress]) VALUES (3, 3, N'Пролитарская 11А')
INSERT [dbo].[Build] ([ID_Build], [Floors], [BuildAdress]) VALUES (4, 2, N'Мальцево 11')
GO
INSERT [dbo].[Premises] ([ID_Premises], [ID_State], [ID_Build], [PremisesName], [PremisesDescription], [PremisesArea]) VALUES (0, 0, 1, N'Склад продукции', N'Основной склад организации', 10)
INSERT [dbo].[Premises] ([ID_Premises], [ID_State], [ID_Build], [PremisesName], [PremisesDescription], [PremisesArea]) VALUES (1, 5, 1, N'Офис менеджера', N'Офис менеджера компании Ювента', 12)
INSERT [dbo].[Premises] ([ID_Premises], [ID_State], [ID_Build], [PremisesName], [PremisesDescription], [PremisesArea]) VALUES (2, 2, 2, N'Магазин обуви', N'Магазин обуви без складского помещения', 32)
INSERT [dbo].[Premises] ([ID_Premises], [ID_State], [ID_Build], [PremisesName], [PremisesDescription], [PremisesArea]) VALUES (3, 0, 3, N'Администрация', NULL, 39)
GO
INSERT [dbo].[Role] ([ID_Role], [RoleName]) VALUES (0, N'Администратор')
INSERT [dbo].[Role] ([ID_Role], [RoleName]) VALUES (1, N'Менеджер')
INSERT [dbo].[Role] ([ID_Role], [RoleName]) VALUES (2, N'Координатор')
GO
INSERT [dbo].[ServiceProvider] ([ID_ServiceProvider], [ServiceProviderName], [PhoneNumber], [INN], [EMail]) VALUES (0, N'BlackPC', N'89932140233', N'027845037234', N'BPC@gmail.com')
INSERT [dbo].[ServiceProvider] ([ID_ServiceProvider], [ServiceProviderName], [PhoneNumber], [INN], [EMail]) VALUES (1, N'ЭКСПЕРТ', N'89898989', N'73454352435', N'expert@yandex.ru')
GO
INSERT [dbo].[State] ([ID_State], [StateName]) VALUES (0, N'Норма')
INSERT [dbo].[State] ([ID_State], [StateName]) VALUES (2, N'Требует внимания')
INSERT [dbo].[State] ([ID_State], [StateName]) VALUES (5, N'Проблемы с интернетом')
GO
INSERT [dbo].[Status] ([ID_Status], [StatusName]) VALUES (0, N'На рассмотрении')
INSERT [dbo].[Status] ([ID_Status], [StatusName]) VALUES (1, N'Рассмотрено')
INSERT [dbo].[Status] ([ID_Status], [StatusName]) VALUES (2, N'Обрабатывается')
INSERT [dbo].[Status] ([ID_Status], [StatusName]) VALUES (3, N'В процессе')
INSERT [dbo].[Status] ([ID_Status], [StatusName]) VALUES (4, N'Выполнено')
INSERT [dbo].[Status] ([ID_Status], [StatusName]) VALUES (5, N'Отклонено')
GO
INSERT [dbo].[User] ([ID_User], [ID_Role], [ID_ServiceProvider], [UserName], [Login], [Password]) VALUES (0, 0, NULL, N'Admin', N'adm', N'b09c600fddc573f117449b3723f23d64')
INSERT [dbo].[User] ([ID_User], [ID_Role], [ID_ServiceProvider], [UserName], [Login], [Password]) VALUES (1, 1, 0, N'Manager', N'mng', N'1d62a19ee745e1b0124db2adb0d626bb')
INSERT [dbo].[User] ([ID_User], [ID_Role], [ID_ServiceProvider], [UserName], [Login], [Password]) VALUES (2, 1, 1, N'Максим Ситиков', N'maxim-expert', N'35e94c8ba47284e9c85eb10de3e74704')
INSERT [dbo].[User] ([ID_User], [ID_Role], [ID_ServiceProvider], [UserName], [Login], [Password]) VALUES (3, 2, NULL, N'Андрей', N'coord', N'332de775a36bbfcb140e1caa06299a8a')
GO
ALTER TABLE [dbo].[Appeal]  WITH CHECK ADD  CONSTRAINT [FK_Appeal_Premises] FOREIGN KEY([ID_Premises])
REFERENCES [dbo].[Premises] ([ID_Premises])
GO
ALTER TABLE [dbo].[Appeal] CHECK CONSTRAINT [FK_Appeal_Premises]
GO
ALTER TABLE [dbo].[Appeal]  WITH CHECK ADD  CONSTRAINT [FK_Appeal_ServiceProvider] FOREIGN KEY([ID_ServiceProvider])
REFERENCES [dbo].[ServiceProvider] ([ID_ServiceProvider])
GO
ALTER TABLE [dbo].[Appeal] CHECK CONSTRAINT [FK_Appeal_ServiceProvider]
GO
ALTER TABLE [dbo].[Appeal]  WITH CHECK ADD  CONSTRAINT [FK_Appeal_Status] FOREIGN KEY([ID_Status])
REFERENCES [dbo].[Status] ([ID_Status])
GO
ALTER TABLE [dbo].[Appeal] CHECK CONSTRAINT [FK_Appeal_Status]
GO
ALTER TABLE [dbo].[Premises]  WITH CHECK ADD  CONSTRAINT [FK_Premises_Build] FOREIGN KEY([ID_Build])
REFERENCES [dbo].[Build] ([ID_Build])
GO
ALTER TABLE [dbo].[Premises] CHECK CONSTRAINT [FK_Premises_Build]
GO
ALTER TABLE [dbo].[Premises]  WITH CHECK ADD  CONSTRAINT [FK_Premises_State] FOREIGN KEY([ID_State])
REFERENCES [dbo].[State] ([ID_State])
GO
ALTER TABLE [dbo].[Premises] CHECK CONSTRAINT [FK_Premises_State]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([ID_Role])
REFERENCES [dbo].[Role] ([ID_Role])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_ServiceProvider] FOREIGN KEY([ID_ServiceProvider])
REFERENCES [dbo].[ServiceProvider] ([ID_ServiceProvider])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_ServiceProvider]
GO
USE [master]
GO
ALTER DATABASE [stateUventa] SET  READ_WRITE 
GO
