CREATE PROCEDURE [dbo].[spAddOutletGroup]
(
	@Id nvarchar(128),
	@Name nvarchar(128),
	@Company_Id nvarchar(128)
)
As

Begin
	Insert into OutletGroup (Id,Name,Company_Id)
    Values (@Id,@Name,@Company_Id)  
END
