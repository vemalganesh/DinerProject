CREATE PROCEDURE [dbo].[spDeleteUser]
(
	@Id nvarchar(128)
)
AS
Begin
	Delete from Users 
    WHERE Id = @Id
END