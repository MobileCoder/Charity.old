SET NOCOUNT ON

USE Charity
GO

/* ORGANIZATION TYPES */

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

/* ORGANIZATION */

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

/* USER SECURITY LEVELS */

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

/* USER STATUS LEVELS */

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
INSERT INTO [UserStatus] ([Id], [Description]) VALUES (3, 'ActivePendingPasswordChange');

SELECT * FROM [UserStatus];

/* USERS */

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

	SELECT CAST(scope_identity() AS int);
END
GO

sp_Users_Create 1, 2, 'c.h.berry@gmail.com', '1234', 'Craig', 'Berry', 3
GO

sp_Users_Create 1, 2, 'paul_fusco@yahoo.com', '1234', 'Paul', 'Fusco', 3
GO

IF OBJECT_ID('sp_Users_Update', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Users_Update]
GO

CREATE PROCEDURE [sp_Users_Update]
	@id int,
	@status int = NULL,
	@password varchar(100),
	@firstName varchar(100),
	@lastName varchar(100)
AS
BEGIN
	UPDATE [Users] SET 
		[Status] = @status,
		[Password] = @password,
		[FirstName] = @firstName,
		[LastName] = @lastName 
	WHERE [Id] = @id; 
END
GO

sp_Users_Update 1, 2, '1234', 'Craig', 'Berry'
GO

sp_Users_Get
GO

/* ITEM STATUS */

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

/* ITEM */

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
	[EndDate] DATETIME NOT NULL,
	[CashValue] MONEY NOT NULL,
	[InitialBid] MONEY NOT NULL,
	[BidIncrement] MONEY NOT NULL,
	[Status] INT NOT NULL,
	[PurchasedBy] INT NOT NULL
)
GO

IF OBJECT_ID('sp_Items_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Items_Get]
GO

CREATE PROCEDURE [sp_Items_Get]
	@status INT = NULL,
	@id INT = NULL
AS
BEGIN
	SELECT 
		i.[Id],
		i.[OrganizationId],
		i.[UserId],
		i.[Title],
		i.[Description],
		i.[CreateDate],
		i.[StartDate],
		i.[EndDate],
		i.[CashValue],
		i.[InitialBid],
		i.[BidIncrement],
		i.[Status],
		i.[PurchasedBy],
		COUNT(b.[Id]) AS 'BidCount',
		ISNULL(MAX(b.[Amount]), 0) AS 'CurrentBid'
	FROM [Items] i
	LEFT OUTER JOIN [Bids] b ON b.[ItemId]=i.[Id]
	WHERE ((@id IS NULL) OR (@id IS NOT NULL AND i.id=@id))
	AND ((@status IS NULL) OR (@status IS NOT NULL AND [Status] = @status))
	AND [StartDate] <= GETDATE()
	AND [EndDate] >= GETDATE()
	GROUP BY 
		i.[Id],
		i.[OrganizationId],
		i.[UserId],
		i.[Title],
		i.[Description],
		i.[CreateDate],
		i.[StartDate],
		i.[EndDate],
		i.[CashValue],
		i.[InitialBid],
		i.[BidIncrement],
		i.[Status],
		i.[PurchasedBy];

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
	@cashValue MONEY,
	@initialBid MONEY,
	@bidIncrement MONEY
AS
BEGIN
	DECLARE @status INT					SET @status = 1;
	DECLARE @purchasedBy INT			SET @purchasedBy = 0;

	INSERT INTO [Items] (OrganizationId, UserId, Title, Description, CreateDate, [StartDate], [EndDate], [CashValue], [InitialBid], [BidIncrement], [Status], PurchasedBy) VALUES (
		@organizationId,
		@userId,
		@title,
		@description,
		getdate(),
		@startDate,
		@endDate,
		@cashValue,
		@initialBid,
		@bidIncrement,
		@status,
		@purchasedBy);

	SELECT CAST(scope_identity() AS int);
END
GO

sp_Items_Create 1, 1, 'Item 1', 'This is item #1', '1/1/2013', '2/1/2014', 100, 50, 5
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
 
sp_Items_Update 1, 2, 1
GO

sp_Items_Get 1
GO

/* ITEM IMAGES */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='ItemsImages')
	DROP TABLE [ItemsImages]
GO

CREATE TABLE [ItemsImages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ItemId] INT NOT NULL,
	[UserId] INT NOT NULL,
	[SequenceNo] INT NOT NULL,
	[Description] VARCHAR(100) NULL,
	[Image] IMAGE NULL
)
GO

IF OBJECT_ID('sp_ItemImages_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_ItemImages_Get]
GO

CREATE PROCEDURE [sp_ItemImages_Get]
	@itemId int
AS
BEGIN
	SELECT * FROM [ItemsImages] WHERE ItemId=@itemId ORDER BY SequenceNo;
	RETURN 0;
END
GO

IF OBJECT_ID('sp_ItemImages_Create', 'P') IS NOT NULL
	DROP PROCEDURE [sp_ItemImages_Create]
GO

CREATE PROCEDURE [sp_ItemImages_Create]
	@itemId INT,
	@userId INT,
	@sequenceNo INT = NULL,
	@description VARCHAR(100),
	@image IMAGE
AS 
BEGIN
	IF @sequenceNo IS NULL
	BEGIN
		SELECT @sequenceNo = (MAX([SequenceNo])+1)
		FROM [ItemsImages]
		WHERE [ItemId] = @itemId;
	END

	INSERT INTO [ItemsImages] ([ItemId], [UserId], [SequenceNo], [Description], [Image]) VALUES (
		@itemId,
		@userId,
		@sequenceNo,
		@description,
		@image);

	SELECT CAST(scope_identity() AS int);
END
GO

/* BIDDING ERRORS */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='BidErrors')
	DROP TABLE [BidErrors]
GO

CREATE TABLE [BidErrors]
(
	[Id] INT NOT NULL,
	[Description] VARCHAR(50)
)
GO

INSERT INTO [BidErrors] ([Id], [Description]) VALUES (0, 'Unknown Error');
INSERT INTO [BidErrors] ([Id], [Description]) VALUES (-1, 'Item not found');
INSERT INTO [BidErrors] ([Id], [Description]) VALUES (-2, 'User not found');
INSERT INTO [BidErrors] ([Id], [Description]) VALUES (-3, 'Bid below minimum bid');
INSERT INTO [BidErrors] ([Id], [Description]) VALUES (-4, 'Bid too low');
INSERT INTO [BidErrors] ([Id], [Description]) VALUES (-5, 'Bid less than minimum bid increment');

IF OBJECT_ID('sp_BidErrors_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_BidErrors_Get]
GO

CREATE PROCEDURE [sp_BidErrors_Get]
AS
BEGIN
	SELECT * FROM [BidErrors];
	RETURN 0;
END
GO

/* BIDDING */

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Bids')
	DROP TABLE [Bids]
GO

CREATE TABLE [Bids]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ItemId] INT NOT NULL,
	[UserId] INT NOT NULL,
	[Amount] MONEY NOT NULL,
	[Timestamp] DATETIME NOT NULL,
	[ClientIp] VARCHAR(20)
)
GO

IF OBJECT_ID('sp_Bids_Insert', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Bids_Insert]
GO

CREATE PROCEDURE [sp_Bids_Insert]
	@itemId INT,
	@userId INT,
	@amount MONEY,
	@clientIp VARCHAR(20)
AS
BEGIN
	DECLARE @rc INT
	DECLARE @ITEM_NOT_FOUND INT			SELECT @ITEM_NOT_FOUND = -1
	DECLARE @USER_NOT_FOUND INT			SELECT @USER_NOT_FOUND = -2
	DECLARE @BID_BELOW_MIN INT			SELECT @BID_BELOW_MIN = -3;
	DECLARE @BID_TO_LOW INT				SELECT @BID_TO_LOW = -4;
	DECLARE @BELOW_MIN_INCREMENT INT	SELECT @BELOW_MIN_INCREMENT = -5;

	IF NOT EXISTS(SELECT 1 FROM [Items] WHERE [Id]=@itemId) 
	BEGIN
		SELECT @rc = @ITEM_NOT_FOUND;
	END 
	ELSE IF NOT EXISTS(SELECT 1 FROM [Users] WHERE [Id]=@userId)
	BEGIN
		SELECT @rc = @USER_NOT_FOUND;
	END
	ELSE
	BEGIN
		DECLARE @minBid MONEY;
		DECLARE @lastBid MONEY;
		DECLARE @bidIncrement MONEY;
		

		SELECT @minBid = [InitialBid], @bidIncrement = [BidIncrement] FROM [Items] WHERE [Id] = @itemId;
		SELECT @lastBid = ISNULL(MAX([Amount]), 0) FROM [Bids] WHERE [ItemId] = @itemId;

		IF @amount < @minBid
		BEGIN
			SELECT @rc = @BID_BELOW_MIN;
		END
		ELSE IF @amount <= @lastBid
		BEGIN
			SELECT @rc = @BID_TO_LOW;
		END
		ELSE IF @amount < (@minBid+@bidIncrement)
		BEGIN
			SELECT @rc = @BELOW_MIN_INCREMENT;
		END
		ELSE
		BEGIN
			INSERT INTO [Bids] ([ItemId], [UserId], [Amount], [Timestamp], [ClientIp]) VALUES (
				@itemId,
				@userId,
				@amount,
				GETDATE(),
				@clientIp)

			SELECT @rc = CAST(scope_identity() AS int);
		END
	END
	
	SELECT @rc;
END
GO

SET NOCOUNT OFF