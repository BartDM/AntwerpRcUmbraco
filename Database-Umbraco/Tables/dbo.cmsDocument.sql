CREATE TABLE [dbo].[cmsDocument]
(
[nodeId] [int] NOT NULL,
[published] [bit] NOT NULL,
[documentUser] [int] NOT NULL,
[versionId] [uniqueidentifier] NOT NULL,
[text] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[releaseDate] [datetime] NULL,
[expireDate] [datetime] NULL,
[updateDate] [datetime] NOT NULL CONSTRAINT [DF_cmsDocument_updateDate] DEFAULT (getdate()),
[templateId] [int] NULL,
[alias] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[newest] [bit] NOT NULL CONSTRAINT [DF_cmsDocument_newest] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDocument] ADD CONSTRAINT [PK_cmsDocument] PRIMARY KEY CLUSTERED  ([versionId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDocument] ADD CONSTRAINT [IX_cmsDocument] UNIQUE NONCLUSTERED  ([nodeId], [versionId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDocument] ADD CONSTRAINT [FK_cmsDocument_cmsContent] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[cmsContent] ([nodeId])
GO
ALTER TABLE [dbo].[cmsDocument] ADD CONSTRAINT [FK_cmsDocument_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
ALTER TABLE [dbo].[cmsDocument] ADD CONSTRAINT [FK_cmsDocument_cmsTemplate] FOREIGN KEY ([templateId]) REFERENCES [dbo].[cmsTemplate] ([nodeId])
GO
