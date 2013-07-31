CREATE TABLE [dbo].[umbracoAppTree]
(
[treeSilent] [bit] NOT NULL CONSTRAINT [DF_umbracoAppTree_treeSilent] DEFAULT ((0)),
[treeInitialize] [bit] NOT NULL CONSTRAINT [DF_umbracoAppTree_treeInitialize] DEFAULT ((1)),
[treeSortOrder] [tinyint] NOT NULL,
[appAlias] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[treeAlias] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[treeTitle] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[treeIconClosed] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[treeIconOpen] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[treeHandlerAssembly] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[treeHandlerType] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[action] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoAppTree] ADD CONSTRAINT [PK_umbracoAppTree] PRIMARY KEY CLUSTERED  ([appAlias], [treeAlias]) ON [PRIMARY]
GO
