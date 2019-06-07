CREATE PROCEDURE [dbo].[spUpdateUser]
(
	@Id nvarchar(128),
	@Name nvarchar(128),
	@Email nvarchar(128),
	@Password nvarchar(128),
	@Suspended bit,
	@EmailVerified  bit,
	@OutletGroup_Id nvarchar(128)
)
AS
Begin
	Update Users 
	set Id = @Id,
	Name = @Name,
	Email = @Email,
	Password = @Password,
	Suspended = @Suspended,
	EmailVerified = @EmailVerified,
	OutletGroup_Id = @OutletGroup_Id
    WHERE Id = @Id
END