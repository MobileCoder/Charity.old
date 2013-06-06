﻿SET NOCOUNT ON

USE Charity
GO

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