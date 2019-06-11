CREATE PROCEDURE [dbo].[spGetUsersInRole]
(
	@RoleName nvarchar(128)
)
AS
Begin
	SELECT u.* from Users u INNER JOIN [UserRole(RS)] ur ON ur.UserId = u.Id INNER JOIN 
	Roles r ON r.Id = ur.RoleId WHERE r.Name = @RoleName;
RETURN 0
End
