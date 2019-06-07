CREATE PROCEDURE [dbo].[spUpdateOutletGroup]
(
	@Id nvarchar(128),
	@Name nvarchar(128),
	@Company_Id nvarchar(128)
)
AS
Begin
	Update OutletGroup 
	set Id = @Id,
	Name = @Name,
	Company_Id = @Company_Id 

    WHERE Id = @Id
END
