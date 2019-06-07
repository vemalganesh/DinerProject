CREATE PROCEDURE [dbo].[spGetUsersInRole]
(
	@RoleName nvarchar(128)
)
AS
	SELECT u.* from Users u INNER JOIN [UserRole(RS)] ur ON ur.UserId = u.Id INNER JOIN 
	roles r ON r.Id = ur.RoleId WHERE r.Name = @RoleName;
RETURN 0
