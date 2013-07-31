CREATE TABLE [dbo].[cmsMacroProperty]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[macroPropertyHidden] [bit] NOT NULL CONSTRAINT [DF_macroProperty_macroPropertyHidden] DEFAULT ((0)),
[macroPropertyType] [smallint] NOT NULL,
[macro] [int] NOT NULL,
[macroPropertySortOrder] [tinyint] NOT NULL CONSTRAINT [DF_macroProperty_macroPropertySortOrder] DEFAULT ((0)),
[macroPropertyAlias] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[macroPropertyName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsMacroProperty] ADD CONSTRAINT [PK_macroProperty] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsMacroProperty] ADD CONSTRAINT [FK_cmsMacroProperty_cmsMacro] FOREIGN KEY ([macro]) REFERENCES [dbo].[cmsMacro] ([id])
GO
ALTER TABLE [dbo].[cmsMacroProperty] ADD CONSTRAINT [FK_umbracoMacroProperty_umbracoMacroPropertyType] FOREIGN KEY ([macroPropertyType]) REFERENCES [dbo].[cmsMacroPropertyType] ([id])
GO
