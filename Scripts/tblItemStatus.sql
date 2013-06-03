SET NOCOUNT ON

USE Charity
GO

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
