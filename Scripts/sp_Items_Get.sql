SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Items_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Items_Get]
GO

CREATE PROCEDURE [sp_Items_Get]
	@status INT = NULL,
	@id INT = NULL
AS
BEGIN
	SELECT 
		i.[Id],
		i.[OrganizationId],
		i.[UserId],
		i.[Title],
		i.[Description],
		i.[CreateDate],
		i.[StartDate],
		i.[EndDate],
		i.[CashValue],
		i.[InitialBid],
		i.[BidIncrement],
		i.[Status],
		i.[PurchasedBy],
		COUNT(b.[Id]) AS 'BidCount',
		ISNULL(MAX(b.[Amount]), 0) AS 'CurrentBid'
	FROM [Items] i
	LEFT OUTER JOIN [Bids] b ON b.[ItemId]=i.[Id]
	WHERE ((@id IS NULL) OR (@id IS NOT NULL AND i.id=@id))
	AND ((@status IS NULL) OR (@status IS NOT NULL AND [Status] = @status))
	AND [StartDate] <= GETDATE()
	AND [EndDate] >= GETDATE()
	GROUP BY 
		i.[Id],
		i.[OrganizationId],
		i.[UserId],
		i.[Title],
		i.[Description],
		i.[CreateDate],
		i.[StartDate],
		i.[EndDate],
		i.[CashValue],
		i.[InitialBid],
		i.[BidIncrement],
		i.[Status],
		i.[PurchasedBy];

	RETURN 0
END
GO
