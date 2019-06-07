CREATE PROCEDURE [dbo].[spAddCompany]
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
	Insert into Company (Id,Name,Address,DbConnStr, Suspended, ExpDate)
    Values (@Id,@Name,@Address, @DbConnStr,@Suspended,@ExpDate)  
END

