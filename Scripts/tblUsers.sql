﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Email] VARCHAR(100) NOT NULL,
	[FirstName] VARCHAR(100) NOT NULL,
	[LastName] VARCHAR(100) NOT NULL
)
