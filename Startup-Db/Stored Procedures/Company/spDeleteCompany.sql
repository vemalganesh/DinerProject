CREATE PROCEDURE [dbo].[spDeleteCompany]
(
	@Id nvarchar(128)
)
AS
Begin
	Delete from Company 
    WHERE Id = @Id
END