SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_ItemImages_Create', 'P') IS NOT NULL
	DROP PROCEDURE [sp_ItemImages_Create]
GO

CREATE PROCEDURE [sp_ItemImages_Create]
	@itemId INT,
	@userId INT,
	@sequenceNo INT = NULL,
	@description VARCHAR(100),
	@image IMAGE
AS 
BEGIN
	IF @sequenceNo IS NULL
	BEGIN
		SELECT @sequenceNo = (MAX([SequenceNo])+1)
		FROM [ItemsImages]
		WHERE [ItemId] = @itemId;
	END

	INSERT INTO [ItemsImages] ([ItemId], [UserId], [SequenceNo], [Description], [Image]) VALUES (
		@itemId,
		@userId,
		@sequenceNo,
		@description,
		@image);

	SELECT CAST(scope_identity() AS int);
END
GO
