CREATE PROCEDURE [dbo].[spAddOutlet]
(
	@Id nvarchar(128),
	@Name nvarchar(128),
	@OutletCode nvarchar(128),
	@Address nvarchar(128),
	@Suspended bit,
	@Company_Id  nvarchar(128)
)
AS
Begin
	DECLARE @GroupId NVARCHAR(128);
	Select @GroupId =(select  OutletGroup_Id from Users where Id in(select UserId from [UserCompany(RS)] where CompanyId like @Company_Id ))

	Insert into Outlets(Id, Name, OutletCode, Address, Suspended, Company_Id,Group_Id)
    Values (@Id,@Name,@OutletCode, @Address,@Suspended,@Company_Id,@GroupId)  
END