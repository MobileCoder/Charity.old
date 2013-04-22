SET NOCOUNT ON

/* Organization Types */

DROP TABLE [OrganizationType];

CREATE TABLE [OrganizationType]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Description] VARCHAR(100) NOT NULL
);

INSERT INTO [OrganizationType] ([Id], [Description]) VALUES (1, 'Global Organization');
INSERT INTO [OrganizationType] ([Id], [Description]) VALUES (2, 'Generic Organization');

SELECT * FROM [OrganizationType]

/* Organization */

DROP TABLE [Organization];

CREATE TABLE [Organization]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[OrganizationTypeId] INT NOT NULL,
    [Name] VARCHAR(100) NOT NULL
);

INSERT INTO [Organization] ([Id], [OrganizationTypeId], [Name]) VALUES (1, 1, 'Charity Administration');

SELECT * FROM [Organization]

/* User Security Levels */

DROP TABLE [UserSecurity];

CREATE TABLE [UserSecurity]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Description] VARCHAR(100) NOT NULL
);

INSERT INTO [UserSecurity] ([Id], [Description]) VALUES (1, 'User');
INSERT INTO [UserSecurity] ([Id], [Description]) VALUES (2, 'Administrator');

SELECT * FROM [UserSecurity];

/* Users */

DROP TABLE [Users];

CREATE TABLE [Users]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[OrganizationId] INT NOT NULL,
	[UserSecurityId] INT NOT NULL,
	[Email] VARCHAR(100) NOT NULL,
	[Password] VARCHAR(100) NULL,
	[FirstName] VARCHAR(100) NOT NULL,
	[LastName] VARCHAR(100) NOT NULL,
);

INSERT INTO [Users] ([Id], [OrganizationId], [UserSecurityId], [Email], [FirstName], [LastName]) VALUES (1, 1, 2, 'c.h.berry@gmail.com', 'Craig', 'Berry');

SELECT * FROM [Users]

/* Item */

DROP TABLE [Items];

CREATE TABLE [Items]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[OrganizationId] INT NOT NULL,
	[UserId] INT NOT NULL,
	[Title] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(255) NOT NULL
);

INSERT INTO [Items] ([Id], [OrganizationId], [UserId], [Title], [Description]) VALUES (1, 1, 1, 'Item 1', 'This is item #1');
INSERT INTO [Items] ([Id], [OrganizationId], [UserId], [Title], [Description]) VALUES (2, 1, 1, 'Item 2', 'This is item #2');
INSERT INTO [Items] ([Id], [OrganizationId], [UserId], [Title], [Description]) VALUES (3, 1, 1, 'Item 3', 'This is item #3');
INSERT INTO [Items] ([Id], [OrganizationId], [UserId], [Title], [Description]) VALUES (4, 1, 1, 'Item 4', 'This is item #4');
INSERT INTO [Items] ([Id], [OrganizationId], [UserId], [Title], [Description]) VALUES (5, 1, 1, 'Item 5', 'This is item #5');

SELECT * FROM [Items];

SET NOCOUNT OFF