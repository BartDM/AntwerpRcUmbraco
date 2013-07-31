CREATE TABLE [dbo].[cmsDocumentType]
(
[contentTypeNodeId] [int] NOT NULL,
[templateNodeId] [int] NOT NULL,
[IsDefault] [bit] NOT NULL CONSTRAINT [DF_cmsDocumentType_IsDefault] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDocumentType] ADD CONSTRAINT [PK_cmsDocumentType] PRIMARY KEY CLUSTERED  ([contentTypeNodeId], [templateNodeId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDocumentType] ADD CONSTRAINT [FK_cmsDocumentType_cmsContentType] FOREIGN KEY ([contentTypeNodeId]) REFERENCES [dbo].[cmsContentType] ([nodeId])
GO
ALTER TABLE [dbo].[cmsDocumentType] ADD CONSTRAINT [FK_cmsDocumentType_umbracoNode] FOREIGN KEY ([contentTypeNodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
ALTER TABLE [dbo].[cmsDocumentType] ADD CONSTRAINT [FK_cmsDocumentType_cmsTemplate] FOREIGN KEY ([templateNodeId]) REFERENCES [dbo].[cmsTemplate] ([nodeId])
GO
