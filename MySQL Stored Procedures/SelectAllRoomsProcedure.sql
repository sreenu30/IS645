USE [IS645Project]
GO
/****** Object:  StoredProcedure [Motel].[SelectAllRooms]    Script Date: 12/3/2024 11:28:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Motel].[SelectAllRooms] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT RoomNumber, RoomType
	FROM Motel.Room
END
