CREATE TABLE [dbo].[umbracoUser]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[userDisabled] [bit] NOT NULL CONSTRAINT [DF_umbracoUser_userDisabled] DEFAULT ((0)),
[userNoConsole] [bit] NOT NULL CONSTRAINT [DF_umbracoUser_userNoConsole] DEFAULT ((0)),
[userType] [smallint] NOT NULL,
[startStructureID] [int] NOT NULL,
[startMediaID] [int] NULL,
[userName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[userLogin] [nvarchar] (125) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[userPassword] [nvarchar] (125) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[userEmail] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[userDefaultPermissions] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[userLanguage] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[defaultToLiveEditing] [bit] NOT NULL CONSTRAINT [DF_umbracoUser_defaultToLiveEditing] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUser] ADD CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUser] ADD CONSTRAINT [IX_umbracoUser] UNIQUE NONCLUSTERED  ([userLogin]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUser] ADD CONSTRAINT [FK_user_userType] FOREIGN KEY ([userType]) REFERENCES [dbo].[umbracoUserType] ([id])
GO
