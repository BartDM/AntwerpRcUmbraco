CREATE TABLE [dbo].[cmsMacroPropertyType]
(
[id] [smallint] NOT NULL IDENTITY(1, 1),
[macroPropertyTypeAlias] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[macroPropertyTypeRenderAssembly] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[macroPropertyTypeRenderType] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[macroPropertyTypeBaseType] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsMacroPropertyType] ADD CONSTRAINT [PK_macroPropertyType] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
