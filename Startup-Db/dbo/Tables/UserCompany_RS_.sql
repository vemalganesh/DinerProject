CREATE TABLE [dbo].[UserCompany(RS)]
(
	[CompanyId] NVARCHAR(128) NOT NULL, 
    [UserId] NVARCHAR(128) NOT NULL,
	CONSTRAINT [PK_dbo.UserCompanyInfo] PRIMARY KEY CLUSTERED ([UserId] ASC, [CompanyId] ASC), 
    CONSTRAINT [FK_dbo.UserCompanyInfo_dbo.Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_dbo.UserCompanyInfo_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE,
)