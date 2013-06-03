SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Address_Insert', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Address_Insert]
GO

CREATE PROCEDURE [sp_Address_Insert]
	@userId INT,
	@addressType INT,
	@address1 VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(100),
	@zipcode VARCHAR(20)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [Address] WHERE [UserId] = @userId and [AddressTypeId] = @addressType)
	BEGIN
		SELECT Id FROM [Address] WHERE [UserId] = @userId and [AddressTypeId] = @addressType
	END
	ELSE 
	BEGIN
		INSERT INTO [Address] ([UserId], [AddressTypeId], [Address1], [Address2], [City], [State], [Zipcode]) VALUES (
			@userId, 
			@addressType,
			@address1,
			@address2,
			@city,
			@state,
			@zipcode);
		
		SELECT CAST(scope_identity() AS int);
	END
END
GO
