USE [IS645Project]
GO
/****** Object:  Schema [Motel]    Script Date: 12/4/2024 1:31:04 AM ******/
CREATE SCHEMA [Motel]
GO
/****** Object:  Table [Motel].[Customer]    Script Date: 12/4/2024 1:31:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Motel].[Customer](
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FName] [nvarchar](50) NOT NULL,
	[LName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Motel].[Reservation]    Script Date: 12/4/2024 1:31:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Motel].[Reservation](
	[Email] [nvarchar](50) NOT NULL,
	[RoomNumber] [int] NOT NULL,
	[CheckInDate] [date] NULL,
	[CheckOutDate] [date] NULL,
 CONSTRAINT [PK_Reservation_1] PRIMARY KEY CLUSTERED 
(
	[Email] ASC,
	[RoomNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Motel].[Room]    Script Date: 12/4/2024 1:31:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Motel].[Room](
	[RoomNumber] [int] NOT NULL,
	[RoomType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Motel].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Customer] FOREIGN KEY([Email])
REFERENCES [Motel].[Customer] ([Email])
GO
ALTER TABLE [Motel].[Reservation] CHECK CONSTRAINT [FK_Reservation_Customer]
GO
ALTER TABLE [Motel].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Room] FOREIGN KEY([RoomNumber])
REFERENCES [Motel].[Room] ([RoomNumber])
GO
ALTER TABLE [Motel].[Reservation] CHECK CONSTRAINT [FK_Reservation_Room]
GO
ALTER TABLE [Motel].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Room] FOREIGN KEY([RoomNumber])
REFERENCES [Motel].[Room] ([RoomNumber])
GO
ALTER TABLE [Motel].[Room] CHECK CONSTRAINT [FK_Room_Room]
GO
