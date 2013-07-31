CREATE TABLE [dbo].[cmsPreviewXml]
(
[nodeId] [int] NOT NULL,
[versionId] [uniqueidentifier] NOT NULL,
[timestamp] [datetime] NOT NULL,
[xml] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsPreviewXml] ADD CONSTRAINT [PK_cmsContentPreviewXml] PRIMARY KEY CLUSTERED  ([nodeId], [versionId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsPreviewXml] ADD CONSTRAINT [FK_cmsPreviewXml_cmsContent] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[cmsContent] ([nodeId])
GO
ALTER TABLE [dbo].[cmsPreviewXml] ADD CONSTRAINT [FK_cmsPreviewXml_cmsContentVersion] FOREIGN KEY ([versionId]) REFERENCES [dbo].[cmsContentVersion] ([VersionId])
GO
