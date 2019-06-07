CREATE PROCEDURE [dbo].[spIsInRole]
(
	@UserId nvarchar(128),
	@RoleName nvarchar(128)
)
AS
Begin  
	DECLARE @retVal int
    SELECT @retVal =  COUNT(*) from [UserRole(RS)] where UserId = @UserId And RoleId in (select Id
    from Roles where Name = @RoleName);

IF (@retVal > 0)
BEGIN
  SELECT 1
END
ELSE
BEGIN
    SELECT 0
END 
END