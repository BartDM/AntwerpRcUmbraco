CREATE TABLE [dbo].[umbracoRelationType]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[dual] [bit] NOT NULL,
[parentObjectType] [uniqueidentifier] NOT NULL,
[childObjectType] [uniqueidentifier] NOT NULL,
[name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[alias] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoRelationType] ADD CONSTRAINT [PK_umbracoRelationType] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
