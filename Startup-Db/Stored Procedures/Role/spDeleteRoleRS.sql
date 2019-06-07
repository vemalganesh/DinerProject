CREATE PROCEDURE [dbo].[spDeleteRole]
(
	@UserId nvarchar(128),
	@RoleName nvarchar(128)
)
AS
Begin
	DECLARE @RoleId NVARCHAR(128);
	Select @RoleId from Roles where Name Like @RoleName;

	Delete from [UserRole(RS)] 
    WHERE UserId = @UserId And RoleId = @RoleId;
END
