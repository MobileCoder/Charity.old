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