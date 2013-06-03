SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Items_Update', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Items_Update]
GO

CREATE PROCEDURE [sp_Items_Update]
	@id int,
	@status int = NULL,
	@purchasedBy int = NULL
AS
BEGIN
	UPDATE [Items] SET 
		[PurchasedBy] = @purchasedBy, 
		[Status] = @status 
	WHERE [Id] = @id;
END
GO