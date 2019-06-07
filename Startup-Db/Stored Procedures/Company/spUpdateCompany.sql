CREATE PROCEDURE [dbo].[spUpdateCompany]
(
	@Id nvarchar(128),
	@Name nvarchar(128),
	@Address nvarchar(128),
	@DbConnStr nvarchar(128),
	@Suspended bit,
	@ExpDate datetime
)
AS
Begin
	Update Company 
	set Id = @Id,
	Name = @Name,
	Address = @Address,
	DbConnStr = @DbConnStr,
	Suspended = @Suspended,
	ExpDate = @ExpDate
    WHERE Id = @Id
END