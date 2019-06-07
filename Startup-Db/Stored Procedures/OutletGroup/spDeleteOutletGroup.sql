CREATE PROCEDURE [dbo].[spDeleteOutletGroup]
(
	@Id nvarchar(128)
)
AS
Begin
	Delete from Outlets 
    WHERE Id = @Id
END