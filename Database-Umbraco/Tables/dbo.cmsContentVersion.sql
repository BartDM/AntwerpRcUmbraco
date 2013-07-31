CREATE TABLE [dbo].[cmsContentVersion]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[ContentId] [int] NOT NULL,
[VersionId] [uniqueidentifier] NOT NULL,
[VersionDate] [datetime] NOT NULL CONSTRAINT [DF_cmsContentVersion_VersionDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentVersion] ADD CONSTRAINT [PK__cmsConte__3213E83F3C69FB99] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentVersion] ADD CONSTRAINT [IX_cmsContentVersion] UNIQUE NONCLUSTERED  ([VersionId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentVersion] ADD CONSTRAINT [FK_cmsContentVersion_cmsContent] FOREIGN KEY ([ContentId]) REFERENCES [dbo].[cmsContent] ([nodeId])
GO
