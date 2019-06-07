CREATE TABLE [dbo].[UserRole(RS)]
(
	[UserId] NVARCHAR(128) NOT NULL, 
    [RoleId] INT NOT NULL,
	CONSTRAINT [PK_dbo.UserRoles(RS)] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC), 
    CONSTRAINT [FK_dbo.UserRoles(RS)_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_dbo.UserRoles(RS)_dbo.RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id]) ON DELETE CASCADE, 
)
