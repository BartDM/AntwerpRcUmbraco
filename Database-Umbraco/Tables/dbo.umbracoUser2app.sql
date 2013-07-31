CREATE TABLE [dbo].[umbracoUser2app]
(
[user] [int] NOT NULL,
[app] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUser2app] ADD CONSTRAINT [PK_user2app] PRIMARY KEY CLUSTERED  ([user], [app]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUser2app] ADD CONSTRAINT [FK_umbracoUser2app_umbracoUser] FOREIGN KEY ([user]) REFERENCES [dbo].[umbracoUser] ([id])
GO
