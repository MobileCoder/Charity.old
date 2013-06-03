SET NOCOUNT ON

USE Charity
GO

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
