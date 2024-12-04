USE [IS645Project]
GO
/****** Object:  StoredProcedure [Motel].[GetCustomer]    Script Date: 12/3/2024 11:28:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

   

CREATE PROCEDURE [Motel].[GetRoom] 
	-- Add the parameters for the stored procedure here
	 @RoomNumber NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM Motel.Room
	WHERE RoomNumber = @RoomNumber;
END
