CREATE PROCEDURE [dbo].[spUpdateOutlet]
(
	@Id nvarchar(128),
	@Name nvarchar(128),
	@OutletCode nvarchar(128),
	@Address nvarchar(128),
	@Suspended bit
)
As
Begin
	Update Outlets 
	set Id = @Id,
	Name = @Name,
	OutletCode = @OutletCode,
	Suspended = @Suspended,
	Address = @Address
    WHERE Id = @Id
END
