USE [master]
GO
/****** Object:  Database [VehicleTracking]    Script Date: 23/11/2017 16:06:32 ******/
CREATE DATABASE [VehicleTracking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VehicleTracking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\VehicleTracking.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'VehicleTracking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\VehicleTracking_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [VehicleTracking] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VehicleTracking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VehicleTracking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VehicleTracking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VehicleTracking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VehicleTracking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VehicleTracking] SET ARITHABORT OFF 
GO
ALTER DATABASE [VehicleTracking] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [VehicleTracking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VehicleTracking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VehicleTracking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VehicleTracking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VehicleTracking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VehicleTracking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VehicleTracking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VehicleTracking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VehicleTracking] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VehicleTracking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VehicleTracking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VehicleTracking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VehicleTracking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VehicleTracking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VehicleTracking] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [VehicleTracking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VehicleTracking] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VehicleTracking] SET  MULTI_USER 
GO
ALTER DATABASE [VehicleTracking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VehicleTracking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VehicleTracking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VehicleTracking] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [VehicleTracking] SET DELAYED_DURABILITY = DISABLED 
GO
USE [VehicleTracking]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Base64Image]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Base64Image](
	[Id] [uniqueidentifier] NOT NULL,
	[Base64EncodedImage] [nvarchar](max) NULL,
	[Damage_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Base64Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Batches]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batches](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[IdUser_Id] [uniqueidentifier] NULL,
	[Transport_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Batches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Damages]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Damages](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Inspection_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Damages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FlowItems]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlowItems](
	[Id] [uniqueidentifier] NOT NULL,
	[StepNumber] [int] NOT NULL,
	[FlowStep_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.FlowItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FlowSteps]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlowSteps](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.FlowSteps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HistoricVehicles]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoricVehicles](
	[Id] [uniqueidentifier] NOT NULL,
	[Brand] [nvarchar](max) NULL,
	[Model] [nvarchar](max) NULL,
	[Year] [int] NOT NULL,
	[Color] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Vin] [nvarchar](max) NULL,
	[CurrentLocation] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_dbo.HistoricVehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Inspections]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inspections](
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[IdLocation_Id] [uniqueidentifier] NULL,
	[IdUser_Id] [uniqueidentifier] NULL,
	[IdVehicle_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Inspections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Locations]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Locations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transports]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transports](
	[Id] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[IdUser_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Transports] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[Phone] [int] NOT NULL,
	[Password] [nvarchar](max) NULL,
	[IdRole_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [uniqueidentifier] NOT NULL,
	[Brand] [nvarchar](max) NULL,
	[Model] [nvarchar](max) NULL,
	[Year] [int] NOT NULL,
	[Color] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Vin] [nvarchar](max) NULL,
	[CurrentLocation] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Price] [int] NOT NULL,
	[Batch_Id] [uniqueidentifier] NULL,
	[Zone_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Zones]    Script Date: 23/11/2017 16:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zones](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsSubZone] [bit] NOT NULL,
	[MaxCapacity] [int] NOT NULL,
	[FlowStep_Id] [uniqueidentifier] NULL,
	[Zone_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Zones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_Damage_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_Damage_Id] ON [dbo].[Base64Image]
(
	[Damage_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdUser_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_IdUser_Id] ON [dbo].[Batches]
(
	[IdUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transport_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_Transport_Id] ON [dbo].[Batches]
(
	[Transport_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Inspection_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_Inspection_Id] ON [dbo].[Damages]
(
	[Inspection_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FlowStep_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_FlowStep_Id] ON [dbo].[FlowItems]
(
	[FlowStep_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdLocation_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_IdLocation_Id] ON [dbo].[Inspections]
(
	[IdLocation_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdUser_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_IdUser_Id] ON [dbo].[Inspections]
(
	[IdUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdVehicle_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_IdVehicle_Id] ON [dbo].[Inspections]
(
	[IdVehicle_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdUser_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_IdUser_Id] ON [dbo].[Transports]
(
	[IdUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdRole_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_IdRole_Id] ON [dbo].[Users]
(
	[IdRole_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Batch_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_Batch_Id] ON [dbo].[Vehicles]
(
	[Batch_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Zone_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_Zone_Id] ON [dbo].[Vehicles]
(
	[Zone_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FlowStep_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_FlowStep_Id] ON [dbo].[Zones]
(
	[FlowStep_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Zone_Id]    Script Date: 23/11/2017 16:06:32 ******/
CREATE NONCLUSTERED INDEX [IX_Zone_Id] ON [dbo].[Zones]
(
	[Zone_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Base64Image]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Base64Image_dbo.Damages_Damage_Id] FOREIGN KEY([Damage_Id])
REFERENCES [dbo].[Damages] ([Id])
GO
ALTER TABLE [dbo].[Base64Image] CHECK CONSTRAINT [FK_dbo.Base64Image_dbo.Damages_Damage_Id]
GO
ALTER TABLE [dbo].[Batches]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Batches_dbo.Transports_Transport_Id] FOREIGN KEY([Transport_Id])
REFERENCES [dbo].[Transports] ([Id])
GO
ALTER TABLE [dbo].[Batches] CHECK CONSTRAINT [FK_dbo.Batches_dbo.Transports_Transport_Id]
GO
ALTER TABLE [dbo].[Batches]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Batches_dbo.Users_IdUser_Id] FOREIGN KEY([IdUser_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Batches] CHECK CONSTRAINT [FK_dbo.Batches_dbo.Users_IdUser_Id]
GO
ALTER TABLE [dbo].[Damages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Damages_dbo.Inspections_Inspection_Id] FOREIGN KEY([Inspection_Id])
REFERENCES [dbo].[Inspections] ([Id])
GO
ALTER TABLE [dbo].[Damages] CHECK CONSTRAINT [FK_dbo.Damages_dbo.Inspections_Inspection_Id]
GO
ALTER TABLE [dbo].[FlowItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FlowItems_dbo.FlowSteps_FlowStep_Id] FOREIGN KEY([FlowStep_Id])
REFERENCES [dbo].[FlowSteps] ([Id])
GO
ALTER TABLE [dbo].[FlowItems] CHECK CONSTRAINT [FK_dbo.FlowItems_dbo.FlowSteps_FlowStep_Id]
GO
ALTER TABLE [dbo].[Inspections]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Inspections_dbo.Locations_IdLocation_Id] FOREIGN KEY([IdLocation_Id])
REFERENCES [dbo].[Locations] ([Id])
GO
ALTER TABLE [dbo].[Inspections] CHECK CONSTRAINT [FK_dbo.Inspections_dbo.Locations_IdLocation_Id]
GO
ALTER TABLE [dbo].[Inspections]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Inspections_dbo.Users_IdUser_Id] FOREIGN KEY([IdUser_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Inspections] CHECK CONSTRAINT [FK_dbo.Inspections_dbo.Users_IdUser_Id]
GO
ALTER TABLE [dbo].[Inspections]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Inspections_dbo.Vehicles_IdVehicle_Id] FOREIGN KEY([IdVehicle_Id])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[Inspections] CHECK CONSTRAINT [FK_dbo.Inspections_dbo.Vehicles_IdVehicle_Id]
GO
ALTER TABLE [dbo].[Transports]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transports_dbo.Users_IdUser_Id] FOREIGN KEY([IdUser_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Transports] CHECK CONSTRAINT [FK_dbo.Transports_dbo.Users_IdUser_Id]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.Roles_IdRole_Id] FOREIGN KEY([IdRole_Id])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.Roles_IdRole_Id]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Vehicles_dbo.Batches_Batch_Id] FOREIGN KEY([Batch_Id])
REFERENCES [dbo].[Batches] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_dbo.Vehicles_dbo.Batches_Batch_Id]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Vehicles_dbo.Zones_Zone_Id] FOREIGN KEY([Zone_Id])
REFERENCES [dbo].[Zones] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_dbo.Vehicles_dbo.Zones_Zone_Id]
GO
ALTER TABLE [dbo].[Zones]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Zones_dbo.FlowSteps_FlowStep_Id] FOREIGN KEY([FlowStep_Id])
REFERENCES [dbo].[FlowSteps] ([Id])
GO
ALTER TABLE [dbo].[Zones] CHECK CONSTRAINT [FK_dbo.Zones_dbo.FlowSteps_FlowStep_Id]
GO
ALTER TABLE [dbo].[Zones]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Zones_dbo.Zones_Zone_Id] FOREIGN KEY([Zone_Id])
REFERENCES [dbo].[Zones] ([Id])
GO
ALTER TABLE [dbo].[Zones] CHECK CONSTRAINT [FK_dbo.Zones_dbo.Zones_Zone_Id]
GO
USE [master]
GO
ALTER DATABASE [VehicleTracking] SET  READ_WRITE 
GO
