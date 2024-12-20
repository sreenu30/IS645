USE [IS645Project]
GO
/****** Object:  StoredProcedure [Motel].[CreateCustomer]    Script Date: 12/3/2024 11:26:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Motel].[CreateCustomer]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50),
	@Password nvarchar(50),
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Motel.Customer (Email, FName, LName, Password)
	VALUES (@Email, @FirstName, @LastName, @Password)
END
