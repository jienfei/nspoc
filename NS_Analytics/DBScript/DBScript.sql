SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Answer];
DROP TABLE [dbo].[UserPeriod];
DROP TABLE [dbo].[Question];
DROP TABLE [dbo].[Period];
DROP TABLE [dbo].[Project];
DROP TABLE [dbo].[Category];
DROP TABLE [dbo].[User];
DROP TABLE [dbo].[Role];

CREATE TABLE [dbo].[Role] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[User] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (250) NULL,
    [RoleId]   INT            NULL,
    [Username] NVARCHAR (50)  NULL,
    [Password] NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_RoleId_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
);

CREATE TABLE [dbo].[Category] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Project] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Period] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50) NULL,
    [Active]    BIT          DEFAULT ((0)) NOT NULL,
    [ProjectId] INT          DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Period_ProjectId_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

CREATE TABLE [dbo].[UserPeriod]
(
	[UserId] INT NOT NULL , 
    [PeriodId] INT NOT NULL, 
    PRIMARY KEY ([UserId], [PeriodId]), 
    CONSTRAINT [FK_UserPeriod_UserId_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_UserPeriod_PeriodId_Period] FOREIGN KEY ([PeriodId]) REFERENCES [Period]([Id]) 
)

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
    [Remark]     VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Answer_PeriodId_Period] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[Period] ([Id]),
    CONSTRAINT [FK_Answer_QuestionId_Question] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([Id]),
    CONSTRAINT [FK_Answer_UserId_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);
