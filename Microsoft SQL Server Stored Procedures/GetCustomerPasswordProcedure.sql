USE [IS645Project]
GO
/****** Object:  StoredProcedure [Motel].[GetCustomerPassword]    Script Date: 12/3/2024 11:28:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [Motel].[GetCustomerPassword]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Password
	FROM Motel.Customer
	WHERE @Email = Email;
END
