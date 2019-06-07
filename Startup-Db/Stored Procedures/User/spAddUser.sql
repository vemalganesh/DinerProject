CREATE PROCEDURE [dbo].[spAddUser]
(
	@Id nvarchar(128),
	@Name nvarchar(128),
	@Email nvarchar(128),
	@Password nvarchar(128),
	@Suspended bit,
	@EmailVerified  bit

)
AS
Begin
	Insert into Users(Id, Name, Email, Password, Suspended, EmailVerified)
    Values (@Id,@Name,@Email, @Password,@Suspended,@EmailVerified)  
END