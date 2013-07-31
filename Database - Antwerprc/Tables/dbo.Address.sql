CREATE TABLE [dbo].[Address]
(
[AddressId] [bigint] NOT NULL IDENTITY(1, 1),
[Street] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Nr] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Postalcode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CountryId] [bigint] NULL,
[AuditDeleted] [bit] NOT NULL CONSTRAINT [DF__Address__AuditDe__1367E606] DEFAULT ((0)),
[AuditCreatedBy] [int] NOT NULL,
[AuditCreatedOn] [datetime] NOT NULL,
[AuditModifiedBy] [int] NULL,
[AuditModifiedOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address] ADD CONSTRAINT [PK__Address__091C2AFB117F9D94] PRIMARY KEY CLUSTERED  ([AddressId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address] ADD CONSTRAINT [FK_Address_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId])
GO
