
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/04/2024 03:29:02
-- Generated from EDMX file: C:\Users\1\source\repos\POMdb\POMdb\POM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [POMdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Meeting'
CREATE TABLE [dbo].[Meeting] (
    [Number] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Summary] nvarchar(100)  NOT NULL,
    [Status] int  NOT NULL,
    [UserId] int  NOT NULL,
    [ClientId] int  NOT NULL
);
GO

-- Creating table 'Client'
CREATE TABLE [dbo].[Client] (
    [Id] int NOT NULL,
    [Cname] nvarchar(20)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [Phone] nchar(10)  NOT NULL,
    [Email] nvarchar(30)  NOT NULL,
    [PayerId] int  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int NOT NULL,
    [Username] varchar(20)  NOT NULL,
    [Password] nchar(8)  NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [LastName] nvarchar(20)  NOT NULL,
    [Email] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'UserAccount'
CREATE TABLE [dbo].[UserAccount] (
    [Username] varchar(20)  NOT NULL,
    [DisplayName] nvarchar(max)  NULL,
    [ProfilePic] nvarchar(max)  NULL,
);
GO

-- Creating table 'Payer'
CREATE TABLE [dbo].[Payer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Pname] nvarchar(20)  NOT NULL,
    [ContactName] nvarchar(20)  NOT NULL,
    [ContactEmail] nvarchar(30)  NOT NULL,
    [TotalPayment] smallint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Number], [ClientId] in table 'Meeting'
ALTER TABLE [dbo].[Meeting]
ADD CONSTRAINT [PK_Meeting]
    PRIMARY KEY CLUSTERED ([Number], [ClientId] ASC);
GO

-- Creating primary key on [Id] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [PK_Client]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Username] in table 'UserAccount'
ALTER TABLE [dbo].[UserAccount]
ADD CONSTRAINT [PK_UserAccount]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [Id] in table 'Payer'
ALTER TABLE [dbo].[Payer]
ADD CONSTRAINT [PK_Payer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Meeting'
ALTER TABLE [dbo].[Meeting]
ADD CONSTRAINT [FK_MeetingUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MeetingUser'
CREATE INDEX [IX_FK_MeetingUser]
ON [dbo].[Meeting]
    ([UserId]);
GO

-- Creating foreign key on [PayerId] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [FK_PayerClient]
    FOREIGN KEY ([PayerId])
    REFERENCES [dbo].[Payer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PayerClient'
CREATE INDEX [IX_FK_PayerClient]
ON [dbo].[Client]
    ([PayerId]);
GO

-- Creating foreign key on [ClientId] in table 'Meeting'
ALTER TABLE [dbo].[Meeting]
ADD CONSTRAINT [FK_ClientMeeting]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[Client]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientMeeting'
CREATE INDEX [IX_FK_ClientMeeting]
ON [dbo].[Meeting]
    ([ClientId]);
GO

-- Creating foreign key on [Username] in table 'UserAccount'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_UserAccountUser]
    FOREIGN KEY ([Username])
    REFERENCES [dbo].[UserAccount]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAccountUser'
CREATE INDEX [IX_FK_UserAccountUser]
ON [dbo].[User]
    ([Username]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------