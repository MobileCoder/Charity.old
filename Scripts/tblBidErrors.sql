SET NOCOUNT ON

USE Charity
GO

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
GO

SELECT * FROM [BidErrors]