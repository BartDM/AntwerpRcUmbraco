CREATE TABLE [dbo].[cmsDataTypePreValues]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[datatypeNodeId] [int] NOT NULL,
[value] [nvarchar] (2500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[sortorder] [int] NOT NULL,
[alias] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDataTypePreValues] ADD CONSTRAINT [PK_cmsDataTypePreValues] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDataTypePreValues] ADD CONSTRAINT [FK_cmsDataTypePreValues_cmsDataType] FOREIGN KEY ([datatypeNodeId]) REFERENCES [dbo].[cmsDataType] ([nodeId])
GO
