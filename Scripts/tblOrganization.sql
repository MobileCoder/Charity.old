SET NOCOUNT ON

USE Charity
GO

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
