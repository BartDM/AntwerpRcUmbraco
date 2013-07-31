CREATE TABLE [dbo].[cmsPropertyData]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[contentNodeId] [int] NOT NULL,
[versionId] [uniqueidentifier] NULL,
[propertytypeid] [int] NOT NULL,
[dataInt] [int] NULL,
[dataDate] [datetime] NULL,
[dataNvarchar] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[dataNtext] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsPropertyData] ADD CONSTRAINT [PK_cmsPropertyData] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_cmsPropertyData_1] ON [dbo].[cmsPropertyData] ([contentNodeId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_cmsPropertyData] ON [dbo].[cmsPropertyData] ([id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_cmsPropertyData_3] ON [dbo].[cmsPropertyData] ([propertytypeid]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_cmsPropertyData_2] ON [dbo].[cmsPropertyData] ([versionId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsPropertyData] ADD CONSTRAINT [FK_cmsPropertyData_umbracoNode] FOREIGN KEY ([contentNodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
ALTER TABLE [dbo].[cmsPropertyData] ADD CONSTRAINT [FK_cmsPropertyData_cmsPropertyType] FOREIGN KEY ([propertytypeid]) REFERENCES [dbo].[cmsPropertyType] ([id])
GO
