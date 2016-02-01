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

CREATE TABLE [dbo].[Role]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NULL
)

INSERT INTO [dbo].[Role] ([Name]) VALUES ('Geen')
INSERT INTO [dbo].[Role] ([Name]) VALUES ('Admin')
INSERT INTO [dbo].[Role] ([Name]) VALUES ('Business Analist')
INSERT INTO [dbo].[Role] ([Name]) VALUES ('Manager')

CREATE TABLE [dbo].[User] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (250) NULL,
    [RoleId]     INT NULL ,
    [Username] NVARCHAR (50)  NULL,
    [Password] NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_User_RoleId_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id])
);

INSERT INTO [dbo].[User] VALUES ('Admin', NULL, 'admin', 'admin123')

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


CREATE TABLE [dbo].[Project] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO Project VALUES ('-- Geen --')

CREATE TABLE [dbo].[Period] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (50) NULL,
    [Active] BIT          NOT NULL DEFAULT 0,
    [ProjectId] INT NOT NULL DEFAULT 1, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Period_ProjectId_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id])
);

INSERT INTO Period VALUES ('-- Geen --', 0, 1)

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

INSERT INTO QUESTION VALUES ('Zijn er verschillende groepen?', 1)
INSERT INTO QUESTION VALUES ('Zijn de stakeholders bekend?', 1)
INSERT INTO QUESTION VALUES ('Wie is de vertegenwoordiger van de groep?', 1)
INSERT INTO QUESTION VALUES ('Is het domein duidelijk voor de requirements?', 1)
INSERT INTO QUESTION VALUES ('Ondersteunt het systeem het bedrijfsproces?', 1)
INSERT INTO QUESTION VALUES ('Is de noodzakelijke voorwaarde van de requirements juist?', 1)
INSERT INTO QUESTION VALUES ('Is de noodzakelijke voorwaarde van de requirements volledig?', 1)
INSERT INTO QUESTION VALUES ('Is er genoeg capaciteit uit de operationele processen?', 1)
INSERT INTO QUESTION VALUES ('Biedt het systeem ondersteuning aan het bedrijfsproces?', 1)
INSERT INTO QUESTION VALUES ('Zijn de requirements geaccepteerd door de gebruikers?', 1)
INSERT INTO QUESTION VALUES ('Zijn de gebruikers betrokken geweest bij het project?', 1)
INSERT INTO QUESTION VALUES ('Zijn de wensen ge�nventariseerd?', 1)
INSERT INTO QUESTION VALUES ('Zijn de eisen ge�nventariseerd?', 1)
INSERT INTO QUESTION VALUES ('Zijn de requirements expliciet gemaakt?', 1)
INSERT INTO QUESTION VALUES ('Is de informatie analist actief betrokken geweest tijdens het proces?', 1)
INSERT INTO QUESTION VALUES ('Zijn de requirements vastgelegd in een brondocument?', 1)
INSERT INTO QUESTION VALUES ('Zijn de gedetaileerde requirements herleid van de oorspronkelijke requirements?', 1)

INSERT INTO QUESTION VALUES ('Is de essentie van een specifieke requirement vastgesteld?', 2)
INSERT INTO QUESTION VALUES ('Is de formulering van de requirements geanalyseerd en geconfirmeerd met de stakeholder?', 2)
INSERT INTO QUESTION VALUES ('Heeft de informatie analist overlegd met de stakeholder, wat de essentie is van elk requirement?', 2)
INSERT INTO QUESTION VALUES ('Is het hogere doel van een requirement vastgesteld? ', 2)
INSERT INTO QUESTION VALUES ('Zijn de requirements relevant?', 2)
INSERT INTO QUESTION VALUES ('Zijn de business requirements vastgelegd?', 2)
INSERT INTO QUESTION VALUES ('Is het vervullen van een requirement de enige zinvolle manier om het hogere requirement in te vullen?', 2)

INSERT INTO QUESTION VALUES ('Zijn de resultaten van de analyse stap vastgelegd?', 3)
INSERT INTO QUESTION VALUES ('Is er een eenduidige formulering gebruikt?', 3)
INSERT INTO QUESTION VALUES ('Zijn de use cases gemaakt?', 3)
INSERT INTO QUESTION VALUES ('Is een datamodel gemaakt?', 3)
INSERT INTO QUESTION VALUES ('Zijn de modellen gevalideerd?', 3)
INSERT INTO QUESTION VALUES ('Zijn er wijzigingen?', 3)
INSERT INTO QUESTION VALUES ('Is het document goedgekeurd door de stakeholder?', 3)

INSERT INTO QUESTION VALUES ('Zijn de requirements verwoord naar de behoeften van de gebruiker?', 4)
INSERT INTO QUESTION VALUES ('Zijn de resultaten van de specificatie gecontroleerd?', 4)
INSERT INTO QUESTION VALUES ('Zijn de desbetreffende requirements door de stakeholders beoordeeld?', 4)
INSERT INTO QUESTION VALUES ('Zijn de juiste requirements beschreven?', 4)
INSERT INTO QUESTION VALUES ('Zijn de requirements volledig?', 4)
INSERT INTO QUESTION VALUES ('Zijn de requirements consistent?', 4)

-- REMS 1 --
INSERT INTO QUESTION VALUES ('Is de probleem formulering beschreven?', 5)
INSERT INTO QUESTION VALUES ('Is het probleem begrijpelijk?', 5)
INSERT INTO QUESTION VALUES ('Begrijpt het team het probleem?', 5)
INSERT INTO QUESTION VALUES ('Zijn de stakeholders (incl. management) het eens met de probleem formulering?', 5)
INSERT INTO QUESTION VALUES ('Is het duidelijk voor het team welk probleem ze gaan oplossen?', 5)
INSERT INTO QUESTION VALUES ('Is de doelstelling gedocumenteerd?', 5)
INSERT INTO QUESTION VALUES ('Is de doelstelling gedocumenteerd in de NS business case?', 5)
INSERT INTO QUESTION VALUES ('Zijn de werkzaamheden beschreven?', 5)
INSERT INTO QUESTION VALUES ('Is het bedrijfsbelang bekend?', 5)
INSERT INTO QUESTION VALUES ('Zijn de doelstellingen beschreven in meetbare eenheden?', 5)
INSERT INTO QUESTION VALUES ('Is er een analyse gedaan naar de grondoorzaak?', 5)
INSERT INTO QUESTION VALUES ('Zijn de teamleden ervan bewust dat er een �echt probleem� wordt opgelost, en niet een symptoom of algemeen basisprobleem?', 5)
INSERT INTO QUESTION VALUES ('Is de grens van de oplossing ge�dentificeerd?', 5)
INSERT INTO QUESTION VALUES ('Zijn de overige interacties met het systeem ge�dentificeerd?', 5)
INSERT INTO QUESTION VALUES ('Is het systeem onderverdeeld in subsystemen?', 5)
INSERT INTO QUESTION VALUES ('Zo ja, zijn alle subsystemen gedefinieerd?', 5)
INSERT INTO QUESTION VALUES ('Zijn de grenzen van elk subsysteem begrepen?', 5)
INSERT INTO QUESTION VALUES ('Is er een plan voor het identificeren en tot het komen van requirements?', 5)
INSERT INTO QUESTION VALUES ('Zijn alle gebruikers van het systeem ge�dentificeerd?', 5)
INSERT INTO QUESTION VALUES ('Zijn alle stakeholders ge�dentificeerd?', 5)
INSERT INTO QUESTION VALUES ('Is er een gedegen stakeholderanalyse geweest?', 5)
INSERT INTO QUESTION VALUES ('Heb je buiten de set van de huidige gebruikers en stakeholders gekeken, bijvoorbeeld de personen die te maken hebben met de administratie, installatie, support of training?', 5)
INSERT INTO QUESTION VALUES ('Weten de teamleden dat alle stakeholders zijn ge�dentificeerd?', 5)
INSERT INTO QUESTION VALUES ('Heeft het team alle beperkingen van het systeem beschreven?', 5)
INSERT INTO QUESTION VALUES ('Heeft het team de ontwikkel beperkingen ge�dentificeerd?', 5)
INSERT INTO QUESTION VALUES ('Zijn de beperkte bronnen (zoals budget, productie kosten, politiek of contractuele requirements, systeem requirements, omgeving factoren, regulaties, personeel, software proces en tooling) in aanmerking gebracht?', 5)
INSERT INTO QUESTION VALUES ('Heb je alle actoren gevonden die interactie hebben met het systeem?', 5)
INSERT INTO QUESTION VALUES ('Is elke actor betrokken bij tenminste ��n use case?', 5)
INSERT INTO QUESTION VALUES ('Zijn er actoren die een vergelijkbare rol hebben in relatie met het systeem? Zo ja, probeer het onder ��n actor te brengen.', 5)
INSERT INTO QUESTION VALUES ('Is er een actor die het systeem op meerdere manieren gebruikt? Zo ja, dan moet je verschillende actoren beschrijven.', 5)
INSERT INTO QUESTION VALUES ('Hebben de actoren intu�tieve en beschrijfbare namen? Kunnen gebruikers en klanten de namen begrijpen?', 5)
INSERT INTO QUESTION VALUES ('Zijn de functionaliteiten van de business use case begrijpelijk van het voorgestelde systeem?', 5)
INSERT INTO QUESTION VALUES ('Is het business object model begrijpelijk voor de entiteiten in het betrokken business proces?', 5)
INSERT INTO QUESTION VALUES ('Heeft het team begrepen wat de specifieke functionaliteiten zijn van het voorgestelde systeem?', 5)



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


