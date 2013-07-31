CREATE TABLE [dbo].[umbracoUserType]
(
[id] [smallint] NOT NULL IDENTITY(1, 1),
[userTypeAlias] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[userTypeName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[userTypeDefaultPermissions] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUserType] ADD CONSTRAINT [PK_userType] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
