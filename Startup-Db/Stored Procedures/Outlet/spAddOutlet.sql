CREATE PROCEDURE [dbo].[spAddOutlet]
(
	@Id nvarchar(128),
	@Name nvarchar(128),
	@OutletCode nvarchar(128),
	@Address nvarchar(128),
	@Suspended bit,
	@Company_Id  nvarchar(128),
	@Group_Id nvarchar(128)
)
AS
Begin
	Insert into Outlets(Id, Name, OutletCode, Address, Suspended, Company_Id,Group_Id)
    Values (@Id,@Name,@OutletCode, @Address,@Suspended,@Company_Id,@Group_Id)  
END