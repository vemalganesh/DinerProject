CREATE TABLE [dbo].[Users]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [Email] NVARCHAR(128) NULL
	CONSTRAINT AK_Email UNIQUE(Email), 
    [Name] NVARCHAR(128) NULL, 
    [Password] NVARCHAR(128) NULL, 
    [Suspended] BIT NULL, 
    [EmailVerified] BIT NULL, 
    [OutletGroup_Id] NVARCHAR(128) NULL, 
    CONSTRAINT [FK_User_OutletGroup_RS] FOREIGN KEY ([OutletGroup_Id]) REFERENCES [dbo].[OutletGroup]([Id]),
)
