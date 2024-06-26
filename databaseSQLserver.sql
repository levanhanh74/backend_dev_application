USE [master]
GO
/****** Object:  Database [ma_scschedules]    Script Date: 4/10/2024 9:08:00 AM ******/
CREATE DATABASE [ma_scschedules]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ma_scschedules', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.HANHSQLSERVER\MSSQL\DATA\ma_scschedules.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ma_scschedules_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.HANHSQLSERVER\MSSQL\DATA\ma_scschedules_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ma_scschedules] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ma_scschedules].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ma_scschedules] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ma_scschedules] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ma_scschedules] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ma_scschedules] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ma_scschedules] SET ARITHABORT OFF 
GO
ALTER DATABASE [ma_scschedules] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ma_scschedules] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ma_scschedules] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ma_scschedules] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ma_scschedules] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ma_scschedules] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ma_scschedules] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ma_scschedules] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ma_scschedules] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ma_scschedules] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ma_scschedules] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ma_scschedules] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ma_scschedules] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ma_scschedules] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ma_scschedules] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ma_scschedules] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ma_scschedules] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ma_scschedules] SET RECOVERY FULL 
GO
ALTER DATABASE [ma_scschedules] SET  MULTI_USER 
GO
ALTER DATABASE [ma_scschedules] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ma_scschedules] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ma_scschedules] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ma_scschedules] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ma_scschedules] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ma_scschedules] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ma_scschedules', N'ON'
GO
ALTER DATABASE [ma_scschedules] SET QUERY_STORE = ON
GO
ALTER DATABASE [ma_scschedules] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ma_scschedules]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[ad_id] [int] NOT NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[image] [nvarchar](500) NULL,
	[ro_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ad_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[category_id] [int] NOT NULL,
	[cate_name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[class_id] [int] NOT NULL,
	[class_name] [nvarchar](100) NULL,
	[teacher_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[course_id] [int] NOT NULL,
	[title] [nvarchar](100) NULL,
	[description] [nvarchar](max) NULL,
	[category_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[course_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[emp_id] [int] NOT NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[image] [nvarchar](500) NULL,
	[email] [nvarchar](100) NULL,
	[ro_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[emp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollments]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollments](
	[enm_id] [int] NOT NULL,
	[student_id] [int] NULL,
	[teacher_id] [int] NULL,
	[course_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[enm_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ro_id] [int] NOT NULL,
	[ro_name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ro_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[schedule_id] [int] NOT NULL,
	[teacher_id] [int] NULL,
	[student_id] [int] NULL,
	[course_id] [int] NULL,
	[class_id] [int] NULL,
	[start_time] [datetime] NULL,
	[end_time] [datetime] NULL,
	[date_week] [date] NULL,
	[status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[student_id] [int] NOT NULL,
	[image] [nvarchar](500) NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[email] [nvarchar](100) NULL,
	[ro_id] [int] NULL,
	[class_id] [int] NULL,
	[course_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[student_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 4/10/2024 9:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[teacher_id] [int] NOT NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[email] [nvarchar](100) NULL,
	[image] [nvarchar](500) NULL,
	[ro_id] [int] NULL,
	[course_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[teacher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD FOREIGN KEY([ro_id])
REFERENCES [dbo].[Roles] ([ro_id])
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD FOREIGN KEY([teacher_id])
REFERENCES [dbo].[Teachers] ([teacher_id])
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([category_id])
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD FOREIGN KEY([ro_id])
REFERENCES [dbo].[Roles] ([ro_id])
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD FOREIGN KEY([course_id])
REFERENCES [dbo].[Courses] ([course_id])
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD FOREIGN KEY([student_id])
REFERENCES [dbo].[Students] ([student_id])
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD FOREIGN KEY([teacher_id])
REFERENCES [dbo].[Teachers] ([teacher_id])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([class_id])
REFERENCES [dbo].[Class] ([class_id])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([course_id])
REFERENCES [dbo].[Courses] ([course_id])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([student_id])
REFERENCES [dbo].[Students] ([student_id])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([teacher_id])
REFERENCES [dbo].[Teachers] ([teacher_id])
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([class_id])
REFERENCES [dbo].[Class] ([class_id])
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([course_id])
REFERENCES [dbo].[Courses] ([course_id])
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([ro_id])
REFERENCES [dbo].[Roles] ([ro_id])
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD FOREIGN KEY([course_id])
REFERENCES [dbo].[Courses] ([course_id])
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD FOREIGN KEY([ro_id])
REFERENCES [dbo].[Roles] ([ro_id])
GO
USE [master]
GO
ALTER DATABASE [ma_scschedules] SET  READ_WRITE 
GO
