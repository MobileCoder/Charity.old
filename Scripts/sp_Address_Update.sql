SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Address_Update', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Address_Update]
GO

CREATE PROCEDURE [sp_Address_Update]
	@Id INT,
	@userId INT,
	@addressType INT,
	@address1 VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(100),
	@zipcode VARCHAR(20)
AS
BEGIN
	UPDATE [Address] SET
		[UserId] = @userId,
		[AddressTypeId] = @addressType,
		[Address1] = @address1,
		[Address2] = @address2,
		[City] = @city,
		[State] = @state,
		[Zipcode] = @zipcode
	WHERE Id = @Id;
END
GO
