SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Answer];
DROP TABLE [dbo].[Question];
DROP TABLE [dbo].[Category];
DROP TABLE [dbo].[Period];
DROP TABLE [dbo].[Project];
DROP TABLE [dbo].[User];

CREATE TABLE [dbo].[User] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (250) NULL,
    [Role]     NVARCHAR (250) NULL,
    [Username] NVARCHAR (50)  NULL,
    [Password] NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO [dbo].[User] VALUES ('Admin', 'Admin', 'admin', 'admin')

CREATE TABLE [dbo].[Category] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO [dbo].[Category] ([Name]) VALUES ('QuickScan - Elicitatie')
INSERT INTO [dbo].[Category] ([Name]) VALUES ('QuickScan - Analyse')
INSERT INTO [dbo].[Category] ([Name]) VALUES ('QuickScan - Specificatie')
INSERT INTO [dbo].[Category] ([Name]) VALUES ('QuickScan - Validatie')
INSERT INTO [dbo].[Category] ([Name]) VALUES ('MaturityScan - REMS1')
INSERT INTO [dbo].[Category] ([Name]) VALUES ('MaturityScan - REMS2')
INSERT INTO [dbo].[Category] ([Name]) VALUES ('MaturityScan - REMS3')
INSERT INTO [dbo].[Category] ([Name]) VALUES ('MaturityScan - REMS4')
INSERT INTO [dbo].[Category] ([Name]) VALUES ('MaturityScan - REMS5')
INSERT INTO [dbo].[Category] ([Name]) VALUES ('MaturityScan - REMS6')


CREATE TABLE [dbo].[Period] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (50) NULL,
    [Active] BIT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Project] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Question] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Value]      VARCHAR (MAX) NULL,
    [CategoryId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CategoryId_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);

CREATE TABLE [dbo].[Answer] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [QuestionId] INT           NOT NULL,
    [PeriodId]   INT           NOT NULL,
    [Value]      INT           DEFAULT ((2)) NOT NULL,
    [UserId]     INT           NOT NULL,
    [ProjectId]  INT           DEFAULT ((1)) NOT NULL,
    [Remark]     VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PeriodId_Periods] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[Period] ([Id]),
    CONSTRAINT [FK_QuestionId_Questions] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([Id]),
    CONSTRAINT [FK_UserId_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProjectId_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);
