--CREATE LOGIN [sponsorshipbot] WITH password='<ProvidePassword>';
--CREATE USER [sponsorshipbot] FROM LOGIN [sponsorshipbot];
--EXEC sp_addrolemember 'db_owner', 'sponsorshipbot';


DROP TABLE [dbo].[Totals]
GO
DROP TABLE [dbo].[Sponsors]
GO
DROP TABLE [dbo].[Conferences]
GO

CREATE TABLE [dbo].[Conferences]
(
	[Id] [int] NOT NULL IDENTITY (1, 1),
	[Name] [nvarchar](255) NOT NULL

	CONSTRAINT [PK_Conferences] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

INSERT INTO [dbo].[Conferences] ([Name]) VALUES ('DDDEA 2015')
INSERT INTO [dbo].[Conferences] ([Name]) VALUES ('DDDEA 2016')
GO

CREATE TABLE [dbo].[Sponsors]
(
	[Id] [int] NOT NULL IDENTITY (1, 1),
	[Name] [nvarchar](255) NOT NULL,
    [AmountPledged] [decimal](7, 2) NULL,
    [AmountReceived] [decimal](7, 2) NULL,
	[AddedBy] [nchar](255) NOT NULL,
	[ConferenceId] [int] NOT NULL

	CONSTRAINT [PK_Sponsors] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

ALTER TABLE [dbo].[Sponsors] ADD CONSTRAINT [FK_Sponsors_Conference] FOREIGN KEY([ConferenceId])
	REFERENCES [dbo].[Conferences] ([Id])
GO

CREATE TABLE [dbo].[Totals]
(
	[Id] [int] NOT NULL IDENTITY (1, 1),
    [AmountNeeded] [decimal](7, 2) NOT NULL,
    [StartingBalance] [decimal](7, 2) NOT NULL,
	[ConferenceId] [int] NOT NULL

	CONSTRAINT [PK_Totals] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

ALTER TABLE [dbo].[Sponsors] ADD CONSTRAINT [FK_Totals_Conference] FOREIGN KEY([ConferenceId])
	REFERENCES [dbo].[Conferences] ([Id])
GO
