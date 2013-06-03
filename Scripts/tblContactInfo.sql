﻿SET NOCOUNT ON

USE Charity
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='ContactInfo')
	DROP TABLE [ContactInfo]
GO

CREATE TABLE [ContactInfo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ContactInfoTypeId] INT NOT NULL,
	[Address] VARCHAR(100) NOT NULL
)
GO