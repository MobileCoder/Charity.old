SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Users_Create', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Users_Create]
GO

CREATE PROCEDURE [sp_Users_Create]
	@organizationId int,
	@UserSecurity int,
	@email varchar(100),
	@password varchar(100),
	@firstname varchar(100),
	@lastname varchar(100),
	@status int
AS
BEGIN
	DECLARE @acceptedTerms BIT;
	SELECT @acceptedTerms = 0;

	INSERT INTO [Users] (OrganizationId, UserSecurity, Email, Password, FirstName, LastName, Status, AcceptedTerms)
	VALUES (
		@organizationId,
		@UserSecurity, 
		@email,
		@password,
		@firstname,
		@lastname,
		@status,
		@acceptedTerms);

	SELECT CAST(scope_identity() AS int);
END
GO

sp_Users_Create 1, 2, 'c.h.berry@gmail.com', '1234', 'Craig', 'Berry', 3
GO

sp_Users_Create 1, 2, 'paul_fusco@yahoo.com', '1234', 'Paul', 'Fusco', 3
GO