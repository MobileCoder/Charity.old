SET NOCOUNT ON

USE Charity
GO

/* Organization Types */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='OrganizationType')
	DROP TABLE [OrganizationType]
GO

CREATE TABLE [OrganizationType]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Description] VARCHAR(100) NOT NULL
)
GO

INSERT INTO [OrganizationType] ([Id], [Description]) VALUES (1, 'Global Organization');
INSERT INTO [OrganizationType] ([Id], [Description]) VALUES (2, 'Generic Organization');

SELECT * FROM [OrganizationType]

/* Organization */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Organization')
	DROP TABLE [Organization]
GO

CREATE TABLE [Organization]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[OrganizationTypeId] INT NOT NULL,
    [Name] VARCHAR(100) NOT NULL
)
GO

INSERT INTO [Organization] ([Id], [OrganizationTypeId], [Name]) VALUES (1, 1, 'Charity Administration');

SELECT * FROM [Organization]

/* User Security Levels */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='UserSecurity')
	DROP TABLE [UserSecurity]
GO

CREATE TABLE [UserSecurity]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Description] VARCHAR(100) NOT NULL
)
GO

INSERT INTO [UserSecurity] ([Id], [Description]) VALUES (1, 'User');
INSERT INTO [UserSecurity] ([Id], [Description]) VALUES (2, 'Administrator');

SELECT * FROM [UserSecurity];

/* User Status Levels */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='UserStatus')
	DROP TABLE [UserStatus]
GO

CREATE TABLE [UserStatus]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Description] VARCHAR(100) NOT NULL
)
GO

INSERT INTO [UserStatus] ([Id], [Description]) VALUES (1, 'Active');
INSERT INTO [UserStatus] ([Id], [Description]) VALUES (2, 'Inactive');

SELECT * FROM [UserStatus];

/* Users */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Users')
	DROP TABLE [Users]
GO

CREATE TABLE [Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[OrganizationId] INT NOT NULL,
	[UserSecurity] INT NOT NULL,
	[Email] VARCHAR(100) NOT NULL,
	[Password] VARCHAR(100) NULL,
	[FirstName] VARCHAR(100) NOT NULL,
	[LastName] VARCHAR(100) NOT NULL,
	[Status] INT NOT NULL
)
GO

IF OBJECT_ID('sp_Users_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Users_Get]
GO

CREATE PROCEDURE [sp_Users_Get]
	@id int = NULL,
	@email varchar(100) = NULL
AS
BEGIN
	SELECT * FROM Users
	WHERE ((@id IS NULL) OR (@id IS NOT NULL AND id=@id))
	AND ((@email IS NULL) OR (@email IS NOT NULL AND email = @email));

	RETURN 0
END
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
	INSERT INTO [Users] (OrganizationId, UserSecurity, Email, Password, FirstName, LastName, Status)
	VALUES (
		@organizationId,
		@UserSecurity, 
		@email,
		@password,
		@firstname,
		@lastname,
		@status);

	RETURN SCOPE_IDENTITY();
END
GO

sp_Users_Create 1, 2, 'c.h.berry@gmail.com', '1234', 'Craig', 'Berry', 1
GO

sp_Users_Create 1, 2, 'paul_fusco@yahoo.com', '1234', 'Paul', 'Fusco', 1
GO

IF OBJECT_ID('sp_Users_Update', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Users_Update]
GO

CREATE PROCEDURE [sp_Users_Update]
	@id int,
	@status int = NULL,
	@password varchar(100) = NULL
AS
BEGIN
	DECLARE @_status INT
	DECLARE @_password VARCHAR(100)

	SELECT @_status=[Status], @_password=[Password] FROM [Users] WHERE [Id]=@id;

	IF @status IS NOT NULL AND @_status <> @status
		UPDATE [Users] SET [Status] = @status WHERE [Id] = @id;

	IF @password IS NOT NULL AND @_password <> @password
		UPDATE [Users] SET [Password] = @password WHERE [Id] = @id;
END
GO

sp_Users_Update 1, 2
GO

sp_Users_Get
GO

/* Item Status */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='ItemStatus')
	DROP TABLE [ItemStatus]
GO

CREATE TABLE [ItemStatus]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Description] VARCHAR(100) NOT NULL
)
GO

INSERT INTO ItemStatus (Id, Description) VALUES (1, 'Pending');
INSERT INTO ItemStatus (Id, Description) VALUES (2, 'Active');
INSERT INTO ItemStatus (Id, Description) VALUES (3, 'Inactive');
INSERT INTO ItemStatus (Id, Description) VALUES (4, 'Purchased');

SELECT * FROM [ItemStatus];

/* Item */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Items')
	DROP TABLE [Items]
GO

CREATE TABLE [Items]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[OrganizationId] INT NOT NULL,
	[UserId] INT NOT NULL,
	[Title] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(255) NOT NULL,
	[CreateDate] DATETIME NOT NULL,
	[StartDate] DATETIME NOT NULL,
	[ExpireDate] DATETIME NOT NULL,
	[Status] INT NOT NULL,
	[PurchasedBy] INT NOT NULL,
	[Amount] MONEY NOT NULL
)
GO

IF OBJECT_ID('sp_Items_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Items_Get]
GO

CREATE PROCEDURE [sp_Items_Get]
	@id int = null
AS
BEGIN
	SELECT * FROM Items
	WHERE ((@id is null) OR (@id is not null AND id=@id));

	RETURN 0
END
GO

IF OBJECT_ID('sp_Items_Create', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Items_Create]
GO

CREATE PROCEDURE [sp_Items_Create]
	@organizationId int,
	@userId int,
	@title varchar(100),
	@description varchar(255),
	@startDate DATETIME,
	@endDate DATETIME,
	@amount MONEY
AS
BEGIN
	DECLARE @status INT
	SET @status = 1;

	DECLARE @purchasedBy INT
	SET @purchasedBy = 0;

	INSERT INTO [Items] (OrganizationId, UserId, Title, Description, CreateDate, StartDate, ExpireDate, Status, PurchasedBy, Amount) VALUES (
		@organizationId,
		@userId,
		@title,
		@description,
		getdate(),
		@startDate,
		@endDate,
		@status,
		@purchasedBy,
		@amount);

	RETURN SCOPE_IDENTITY();
END
GO

sp_Items_Create 1, 1, 'Item 1', 'This is item #1', '1/1/2014', '2/1/2014', 100

IF OBJECT_ID('sp_Items_Update', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Items_Update]
GO

CREATE PROCEDURE [sp_Items_Update]
	@id int,
	@status int = NULL,
	@purchasedBy int = NULL
AS
BEGIN
	IF @purchasedBy IS NOT NULL
		UPDATE [Items] SET [PurchasedBy] = @purchasedBy, [Status] = 4 WHERE [Id] = @id;
	ELSE IF @status IS NOT NULL 
		UPDATE [Items] SET [Status] = @status WHERE [Id] = @id;
END
GO

sp_Items_Update 1, 2, 1
GO

sp_Items_Get
GO

SET NOCOUNT OFF