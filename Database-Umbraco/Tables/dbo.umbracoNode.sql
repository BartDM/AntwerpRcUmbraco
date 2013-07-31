CREATE TABLE [dbo].[umbracoNode]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[trashed] [bit] NOT NULL CONSTRAINT [DF_umbracoNode_trashed] DEFAULT ((0)),
[parentID] [int] NOT NULL,
[nodeUser] [int] NULL,
[level] [smallint] NOT NULL,
[path] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[sortOrder] [int] NOT NULL,
[uniqueID] [uniqueidentifier] NULL,
[text] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[nodeObjectType] [uniqueidentifier] NULL,
[createDate] [datetime] NOT NULL CONSTRAINT [DF_umbracoNode_createDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoNode] ADD CONSTRAINT [PK_structure] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_umbracoNodeObjectType] ON [dbo].[umbracoNode] ([nodeObjectType]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_umbracoNodeParentId] ON [dbo].[umbracoNode] ([parentID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoNode] ADD CONSTRAINT [FK_umbracoNode_umbracoNode] FOREIGN KEY ([parentID]) REFERENCES [dbo].[umbracoNode] ([id])
GO
