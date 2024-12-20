USE [IS645Project]
GO
/****** Object:  StoredProcedure [Motel].[CreateReservation]    Script Date: 12/3/2024 11:26:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Motel].[CreateReservation] 
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50),
	@RoomNumber int,
	@CheckInDate date,
	@CheckOutDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Motel.Reservation (Email, RoomNumber, CheckInDate, CheckOutDate)
	VALUES (@Email, @RoomNumber, @CheckInDate, @CheckOutDate)
END
