CREATE TABLE [dbo].[cmsPropertyType]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[dataTypeId] [int] NOT NULL,
[contentTypeId] [int] NOT NULL,
[tabId] [int] NULL,
[Alias] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[helpText] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[sortOrder] [int] NOT NULL CONSTRAINT [DF__cmsProper__sortO__1EA48E88] DEFAULT ((0)),
[mandatory] [bit] NOT NULL CONSTRAINT [DF__cmsProper__manda__2180FB33] DEFAULT ((0)),
[validationRegExp] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsPropertyType] ADD CONSTRAINT [PK_cmsPropertyType] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsPropertyType] ADD CONSTRAINT [FK_cmsPropertyType_cmsContentType] FOREIGN KEY ([contentTypeId]) REFERENCES [dbo].[cmsContentType] ([nodeId])
GO
ALTER TABLE [dbo].[cmsPropertyType] ADD CONSTRAINT [FK_cmsPropertyType_cmsDataType] FOREIGN KEY ([dataTypeId]) REFERENCES [dbo].[cmsDataType] ([nodeId])
GO
ALTER TABLE [dbo].[cmsPropertyType] ADD CONSTRAINT [FK_cmsPropertyType_cmsTab] FOREIGN KEY ([tabId]) REFERENCES [dbo].[cmsTab] ([id])
GO
