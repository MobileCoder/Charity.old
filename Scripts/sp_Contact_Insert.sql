SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Contact_Insert', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Contact_Insert]
GO

CREATE PROCEDURE [sp_Contact_Insert]
	@userId INT,
	@contactType INT,
	@number VARCHAR(100)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [Contact] WHERE [UserId] = @userId and [ContactTypeId] = @contactType)
	BEGIN
		SELECT Id FROM [Contact] WHERE [UserId] = @userId and [ContactTypeId] = @contactType
	END
	ELSE 
	BEGIN
		INSERT INTO [Contact] ([UserId], [ContactTypeId], [Number]) VALUES (
			@userId, 
			@contactType,
			@number);
		
		SELECT CAST(scope_identity() AS int);
	END
END
GO
