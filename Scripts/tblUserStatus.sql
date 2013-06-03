SET NOCOUNT ON

USE Charity
GO


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