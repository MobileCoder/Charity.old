SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Users_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Users_Get]
GO

CREATE PROCEDURE [sp_Users_Get]
	@id int = NULL,
	@email varchar(100) = NULL
AS
BEGIN
	DECLARE @canBid BIT
	IF EXISTS(SELECT 1 FROM [CreditCard] WHERE UserId=@id)
	BEGIN
		SELECT @canBid = 1;
	END

	SELECT 
		ISNULL(@canBid, 0) AS 'CanBid',
		u.* 
	FROM Users u
	WHERE ((@id IS NULL) OR (@id IS NOT NULL AND id=@id))
	AND ((@email IS NULL) OR (@email IS NOT NULL AND email = @email));

	RETURN 0
END
GO
