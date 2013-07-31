CREATE TABLE [dbo].[umbracoApp]
(
[sortOrder] [tinyint] NOT NULL CONSTRAINT [DF_app_sortOrder] DEFAULT ((0)),
[appAlias] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[appIcon] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[appName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[appInitWithTreeAlias] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoApp] ADD CONSTRAINT [PK_umbracoApp] PRIMARY KEY CLUSTERED  ([appAlias]) ON [PRIMARY]
GO
