USE [IS645Project]
GO
/****** Object:  StoredProcedure [Motel].[GetReservations]    Script Date: 12/3/2024 11:28:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [Motel].[GetReservations] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM Motel.Reservation
END
