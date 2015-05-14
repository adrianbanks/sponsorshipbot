--CREATE LOGIN [sponsorshipbot] WITH password='<ProvidePassword>';
--CREATE USER [sponsorshipbot] FROM LOGIN [sponsorshipbot];
--EXEC sp_addrolemember 'db_owner', 'sponsorshipbot';

DROP TABLE [dbo].[Totals]
GO
DROP TABLE [dbo].[Sponsors]
GO

CREATE TABLE [dbo].[Totals]
(
	[Id] [int] NOT NULL IDENTITY (1, 1),
    [AmountNeeded] [decimal](7, 2) NOT NULL,
    [StartingBalance] [decimal](7, 2) NOT NULL

	CONSTRAINT [PK_Total] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

INSERT INTO [dbo].[Totals] ([AmountNeeded], [StartingBalance]) VALUES (10000.00, 2500.00)
GO

CREATE TABLE [dbo].[Sponsors]
(
	[Id] [int] NOT NULL IDENTITY (1, 1),
	[Name] [nvarchar](255) NOT NULL,
    [AmountPledged] [decimal](7, 2) NULL,
    [AmountReceived] [decimal](7, 2) NULL,
	[AddedBy] [nchar](255) NOT NULL

	CONSTRAINT [PK_Sponsors] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
