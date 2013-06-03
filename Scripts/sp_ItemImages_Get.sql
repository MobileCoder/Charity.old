SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_ItemImages_Get', 'P') IS NOT NULL
	DROP PROCEDURE [sp_ItemImages_Get]
GO

CREATE PROCEDURE [sp_ItemImages_Get]
	@itemId int
AS
BEGIN
	SELECT * FROM [ItemsImages] WHERE ItemId=@itemId ORDER BY SequenceNo;
	RETURN 0;
END
GO