CREATE PROCEDURE [dbo].[spViewOutlet]
(
	@Company_Id  nvarchar(128)
)
as    
Begin    
    select *    
    from Outlets where Company_Id like @Company_Id
End  
