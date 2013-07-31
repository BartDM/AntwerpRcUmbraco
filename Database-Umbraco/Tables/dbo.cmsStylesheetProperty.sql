CREATE TABLE [dbo].[cmsStylesheetProperty]
(
[nodeId] [int] NOT NULL,
[stylesheetPropertyEditor] [bit] NULL,
[stylesheetPropertyAlias] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[stylesheetPropertyValue] [nvarchar] (400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsStylesheetProperty] ADD CONSTRAINT [PK_cmsStylesheetProperty] PRIMARY KEY CLUSTERED  ([nodeId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsStylesheetProperty] ADD CONSTRAINT [FK_cmsStylesheetProperty_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
