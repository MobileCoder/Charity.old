SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Contact_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Contact_Get]
GO

CREATE PROCEDURE [sp_Contact_Get]
	@userId INT
AS
BEGIN
	SELECT * FROM [Contact] 
	WHERE UserId = @userId

	RETURN 0;
END
GO
