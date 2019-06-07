CREATE PROCEDURE [dbo].[spViewRole]
(
	@UserId nvarchar(128)
)
As
Begin    
    select *    
    from Roles where Id in(select RoleId from [UserRole(RS)] where UserId = @UserId);
End
