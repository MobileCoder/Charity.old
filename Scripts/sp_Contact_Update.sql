SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Contact_Update', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Contact_Update]
GO

CREATE PROCEDURE [sp_Contact_Update]
	@Id INT,
	@userId INT,
	@contactType INT,
	@number VARCHAR(100)
AS
BEGIN
	UPDATE [Contact] SET
		[UserId] = @userId,
		[ContactTypeId] = @contactType,
		[Number] = @number
	WHERE Id = @Id;
END
GO
