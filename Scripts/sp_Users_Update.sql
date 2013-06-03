SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Users_Update', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Users_Update]
GO

CREATE PROCEDURE [sp_Users_Update]
	@id INT,
	@status INT = NULL,
	@password VARCHAR(100),
	@firstName VARCHAR(100),
	@lastName VARCHAR(100),
	@acceptedTerms BIT
AS
BEGIN
	UPDATE [Users] SET 
		[Status] = @status,
		[Password] = @password,
		[FirstName] = @firstName,
		[LastName] = @lastName,
		[AcceptedTerms] = @acceptedTerms
	WHERE [Id] = @id; 
END
GO