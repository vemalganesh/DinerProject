CREATE PROCEDURE [dbo].[spAddRole]
(
	@UserId nvarchar(128),
	@RoleName nvarchar(128)
)
AS
Begin
	DECLARE @RoleId NVARCHAR(128);
	Select @RoleId =(select Id from Roles where Name like @RoleName)

	Insert into [dbo].[UserRole(RS)](UserId,RoleId)
    Values (@UserId,@RoleId)  
END
