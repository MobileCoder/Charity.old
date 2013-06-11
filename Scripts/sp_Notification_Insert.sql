SET NOCOUNT ON

USE Charity
GO

IF OBJECT_ID('sp_Notification_Insert', 'P') IS NOT NULL
	DROP PROCEDURE [sp_Notification_Insert]
GO

CREATE PROCEDURE [sp_Notification_Insert]
	@userId INT,
	@notificationType INT
AS
BEGIN
	INSERT INTO [Notifications] ([UserId], [NotificationType], [Timestamp]) VALUES (
		@userId,
		@notificationType,
		GETDATE());
		
	SELECT CAST(scope_identity() AS int);
END
GO
