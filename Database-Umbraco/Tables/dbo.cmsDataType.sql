CREATE TABLE [dbo].[cmsDataType]
(
[pk] [int] NOT NULL IDENTITY(1, 1),
[nodeId] [int] NOT NULL,
[controlId] [uniqueidentifier] NOT NULL,
[dbType] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDataType] ADD CONSTRAINT [PK_cmsDataType] PRIMARY KEY CLUSTERED  ([pk]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDataType] ADD CONSTRAINT [IX_cmsDataType] UNIQUE NONCLUSTERED  ([nodeId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDataType] ADD CONSTRAINT [FK_cmsDataType_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
