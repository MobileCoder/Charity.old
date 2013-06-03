SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Items_Create', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Items_Create]
GO

CREATE PROCEDURE [sp_Items_Create]
	@organizationId int,
	@userId int,
	@title varchar(100),
	@description varchar(255),
	@startDate DATETIME,
	@endDate DATETIME,
	@cashValue MONEY,
	@initialBid MONEY,
	@bidIncrement MONEY
AS
BEGIN
	DECLARE @status INT					SET @status = 1;
	DECLARE @purchasedBy INT			SET @purchasedBy = 0;

	INSERT INTO [Items] (OrganizationId, UserId, Title, Description, CreateDate, [StartDate], [EndDate], [CashValue], [InitialBid], [BidIncrement], [Status], PurchasedBy) VALUES (
		@organizationId,
		@userId,
		@title,
		@description,
		getdate(),
		@startDate,
		@endDate,
		@cashValue,
		@initialBid,
		@bidIncrement,
		@status,
		@purchasedBy);

	SELECT CAST(scope_identity() AS int);
END
GO