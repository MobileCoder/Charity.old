SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Address_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Address_Get]
GO

CREATE PROCEDURE [sp_Address_Get]
	@userId INT
AS
BEGIN
	SELECT * FROM [Address] 
	WHERE UserId = @userId

	RETURN 0;
END
GO
