SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Bids_Insert', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Bids_Insert]
GO

CREATE PROCEDURE [sp_Bids_Insert]
	@itemId INT,
	@userId INT,
	@amount MONEY,
	@clientIp VARCHAR(20)
AS
BEGIN
	DECLARE @rc INT
	DECLARE @ITEM_NOT_FOUND INT			SELECT @ITEM_NOT_FOUND = -1
	DECLARE @USER_NOT_FOUND INT			SELECT @USER_NOT_FOUND = -2
	DECLARE @BID_BELOW_MIN INT			SELECT @BID_BELOW_MIN = -3;
	DECLARE @BID_TO_LOW INT				SELECT @BID_TO_LOW = -4;
	DECLARE @BELOW_MIN_INCREMENT INT	SELECT @BELOW_MIN_INCREMENT = -5;

	IF NOT EXISTS(SELECT 1 FROM [Items] WHERE [Id]=@itemId) 
	BEGIN
		SELECT @rc = @ITEM_NOT_FOUND;
	END 
	ELSE IF NOT EXISTS(SELECT 1 FROM [Users] WHERE [Id]=@userId)
	BEGIN
		SELECT @rc = @USER_NOT_FOUND;
	END
	ELSE
	BEGIN
		DECLARE @minBid MONEY;
		DECLARE @lastBid MONEY;
		DECLARE @bidIncrement MONEY;
		

		SELECT @minBid = [InitialBid], @bidIncrement = [BidIncrement] FROM [Items] WHERE [Id] = @itemId;
		SELECT @lastBid = ISNULL(MAX([Amount]), 0) FROM [Bids] WHERE [ItemId] = @itemId;

		IF @amount < @minBid
		BEGIN
			SELECT @rc = @BID_BELOW_MIN;
		END
		ELSE IF @amount <= @lastBid
		BEGIN
			SELECT @rc = @BID_TO_LOW;
		END
		ELSE IF @amount < (@minBid+@bidIncrement)
		BEGIN
			SELECT @rc = @BELOW_MIN_INCREMENT;
		END
		ELSE
		BEGIN
			INSERT INTO [Bids] ([ItemId], [UserId], [Amount], [Timestamp], [ClientIp]) VALUES (
				@itemId,
				@userId,
				@amount,
				GETDATE(),
				@clientIp)

			SELECT @rc = CAST(scope_identity() AS int);
		END
	END
	
	SELECT @rc;
END
GO
