CREATE PROCEDURE [dbo].[spDeleteOutlet]
(
	@Id nvarchar(128)
)
AS
Begin
	Delete from Outlets 
    WHERE Id = @Id
END