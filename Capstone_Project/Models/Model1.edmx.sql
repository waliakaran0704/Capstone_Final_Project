
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/29/2018 15:55:05
-- Generated from EDMX file: D:\Sem 6\Capstone_Final_Project_Latest\Capstone_Project\Capstone_Project\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Capstone_Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AdminCastingDirector]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CastingDirectors] DROP CONSTRAINT [FK_AdminCastingDirector];
GO
IF OBJECT_ID(N'[dbo].[FK_LoginAdmin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Admins] DROP CONSTRAINT [FK_LoginAdmin];
GO
IF OBJECT_ID(N'[dbo].[FK_LoginCastingDirector]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CastingDirectors] DROP CONSTRAINT [FK_LoginCastingDirector];
GO
IF OBJECT_ID(N'[dbo].[FK_LoginTalent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Talents] DROP CONSTRAINT [FK_LoginTalent];
GO
IF OBJECT_ID(N'[dbo].[FK_FamilyMemberAddress_Addresses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FamilyMemberAddress] DROP CONSTRAINT [FK_FamilyMemberAddress_Addresses];
GO
IF OBJECT_ID(N'[dbo].[FK_FamilyMemberAddress_FamilyMembers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FamilyMemberAddress] DROP CONSTRAINT [FK_FamilyMemberAddress_FamilyMembers];
GO
IF OBJECT_ID(N'[dbo].[FK_TalentAddress_Addresses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TalentAddress] DROP CONSTRAINT [FK_TalentAddress_Addresses];
GO
IF OBJECT_ID(N'[dbo].[FK_TalentAddress_Talents]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TalentAddress] DROP CONSTRAINT [FK_TalentAddress_Talents];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO
IF OBJECT_ID(N'[dbo].[Admins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admins];
GO
IF OBJECT_ID(N'[dbo].[CastingDirectors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CastingDirectors];
GO
IF OBJECT_ID(N'[dbo].[FamilyMembers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FamilyMembers];
GO
IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO
IF OBJECT_ID(N'[dbo].[Talents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Talents];
GO
IF OBJECT_ID(N'[dbo].[FamilyMemberAddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FamilyMemberAddress];
GO
IF OBJECT_ID(N'[dbo].[TalentAddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TalentAddress];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [PKAddress_ID] int IDENTITY(1,1) NOT NULL,
    [MailingAddress] nvarchar(max)  NOT NULL,
    [HomeAddress] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [PKAmin_ID] int IDENTITY(1,1) NOT NULL,
    [LoginLogin_ID] int  NOT NULL
);
GO

-- Creating table 'CastingDirectors'
CREATE TABLE [dbo].[CastingDirectors] (
    [PKCD_ID] int IDENTITY(1,1) NOT NULL,
    [AdminPKAmin_ID] int  NOT NULL,
    [LoginLogin_ID] int  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FamilyMembers'
CREATE TABLE [dbo].[FamilyMembers] (
    [PK_FM_ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [HomePhoneNum] nvarchar(max)  NOT NULL,
    [CellPhoneNum] nvarchar(max)  NOT NULL,
    [BirthDate] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [Weight] nvarchar(max)  NOT NULL,
    [Height] nvarchar(max)  NOT NULL,
    [EyeColor] nvarchar(max)  NOT NULL,
    [HairColor] nvarchar(max)  NOT NULL,
    [UnionStatus] nvarchar(max)  NOT NULL,
    [SIN] nvarchar(max)  NOT NULL,
    [EthinicOriginPK_EO_ID] int  NOT NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [Login_ID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [ConfirmPassword] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Talents'
CREATE TABLE [dbo].[Talents] (
    [PKTalent_ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [HomePhoneNum] nvarchar(max)  NOT NULL,
    [CellPhoneNum] nvarchar(max)  NOT NULL,
    [BirthDate] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [Weight] float  NOT NULL,
    [Height] float  NOT NULL,
    [EyeColor] nvarchar(max)  NOT NULL,
    [HairColor] nvarchar(max)  NOT NULL,
    [UnionStatus] nvarchar(max)  NOT NULL,
    [SIN] float  NOT NULL,
    [LoginLogin_ID] int  NOT NULL,
    [EthinicOrigin] nvarchar(max)  NOT NULL,
    [CarMake] nvarchar(max)  NOT NULL,
    [CarModel] nvarchar(max)  NOT NULL,
    [CarYear] nvarchar(max)  NOT NULL,
    [CarColor] nvarchar(max)  NOT NULL,
    [ResetPaswordCode] nvarchar(max)  NULL,
    [ImagePath] nvarchar(max)  NOT NULL,
    [PostalCode] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FamilyMemberAddress'
CREATE TABLE [dbo].[FamilyMemberAddress] (
    [Addresses_PKAddress_ID] int  NOT NULL,
    [FamilyMembers_PK_FM_ID] int  NOT NULL
);
GO

-- Creating table 'TalentAddress'
CREATE TABLE [dbo].[TalentAddress] (
    [TalentAddress_Talents_PKAddress_ID] int  NOT NULL,
    [Talents_PKTalent_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [PKAddress_ID] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([PKAddress_ID] ASC);
GO

-- Creating primary key on [PKAmin_ID] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([PKAmin_ID] ASC);
GO

-- Creating primary key on [PKCD_ID] in table 'CastingDirectors'
ALTER TABLE [dbo].[CastingDirectors]
ADD CONSTRAINT [PK_CastingDirectors]
    PRIMARY KEY CLUSTERED ([PKCD_ID] ASC);
GO

-- Creating primary key on [PK_FM_ID] in table 'FamilyMembers'
ALTER TABLE [dbo].[FamilyMembers]
ADD CONSTRAINT [PK_FamilyMembers]
    PRIMARY KEY CLUSTERED ([PK_FM_ID] ASC);
GO

-- Creating primary key on [Login_ID] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([Login_ID] ASC);
GO

-- Creating primary key on [PKTalent_ID] in table 'Talents'
ALTER TABLE [dbo].[Talents]
ADD CONSTRAINT [PK_Talents]
    PRIMARY KEY CLUSTERED ([PKTalent_ID] ASC);
GO

-- Creating primary key on [Addresses_PKAddress_ID], [FamilyMembers_PK_FM_ID] in table 'FamilyMemberAddress'
ALTER TABLE [dbo].[FamilyMemberAddress]
ADD CONSTRAINT [PK_FamilyMemberAddress]
    PRIMARY KEY CLUSTERED ([Addresses_PKAddress_ID], [FamilyMembers_PK_FM_ID] ASC);
GO

-- Creating primary key on [TalentAddress_Talents_PKAddress_ID], [Talents_PKTalent_ID] in table 'TalentAddress'
ALTER TABLE [dbo].[TalentAddress]
ADD CONSTRAINT [PK_TalentAddress]
    PRIMARY KEY CLUSTERED ([TalentAddress_Talents_PKAddress_ID], [Talents_PKTalent_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AdminPKAmin_ID] in table 'CastingDirectors'
ALTER TABLE [dbo].[CastingDirectors]
ADD CONSTRAINT [FK_AdminCastingDirector]
    FOREIGN KEY ([AdminPKAmin_ID])
    REFERENCES [dbo].[Admins]
        ([PKAmin_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdminCastingDirector'
CREATE INDEX [IX_FK_AdminCastingDirector]
ON [dbo].[CastingDirectors]
    ([AdminPKAmin_ID]);
GO

-- Creating foreign key on [LoginLogin_ID] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [FK_LoginAdmin]
    FOREIGN KEY ([LoginLogin_ID])
    REFERENCES [dbo].[Logins]
        ([Login_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoginAdmin'
CREATE INDEX [IX_FK_LoginAdmin]
ON [dbo].[Admins]
    ([LoginLogin_ID]);
GO

-- Creating foreign key on [LoginLogin_ID] in table 'CastingDirectors'
ALTER TABLE [dbo].[CastingDirectors]
ADD CONSTRAINT [FK_LoginCastingDirector]
    FOREIGN KEY ([LoginLogin_ID])
    REFERENCES [dbo].[Logins]
        ([Login_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoginCastingDirector'
CREATE INDEX [IX_FK_LoginCastingDirector]
ON [dbo].[CastingDirectors]
    ([LoginLogin_ID]);
GO

-- Creating foreign key on [LoginLogin_ID] in table 'Talents'
ALTER TABLE [dbo].[Talents]
ADD CONSTRAINT [FK_LoginTalent]
    FOREIGN KEY ([LoginLogin_ID])
    REFERENCES [dbo].[Logins]
        ([Login_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoginTalent'
CREATE INDEX [IX_FK_LoginTalent]
ON [dbo].[Talents]
    ([LoginLogin_ID]);
GO

-- Creating foreign key on [Addresses_PKAddress_ID] in table 'FamilyMemberAddress'
ALTER TABLE [dbo].[FamilyMemberAddress]
ADD CONSTRAINT [FK_FamilyMemberAddress_Addresses]
    FOREIGN KEY ([Addresses_PKAddress_ID])
    REFERENCES [dbo].[Addresses]
        ([PKAddress_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FamilyMembers_PK_FM_ID] in table 'FamilyMemberAddress'
ALTER TABLE [dbo].[FamilyMemberAddress]
ADD CONSTRAINT [FK_FamilyMemberAddress_FamilyMembers]
    FOREIGN KEY ([FamilyMembers_PK_FM_ID])
    REFERENCES [dbo].[FamilyMembers]
        ([PK_FM_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FamilyMemberAddress_FamilyMembers'
CREATE INDEX [IX_FK_FamilyMemberAddress_FamilyMembers]
ON [dbo].[FamilyMemberAddress]
    ([FamilyMembers_PK_FM_ID]);
GO

-- Creating foreign key on [TalentAddress_Talents_PKAddress_ID] in table 'TalentAddress'
ALTER TABLE [dbo].[TalentAddress]
ADD CONSTRAINT [FK_TalentAddress_Addresses]
    FOREIGN KEY ([TalentAddress_Talents_PKAddress_ID])
    REFERENCES [dbo].[Addresses]
        ([PKAddress_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Talents_PKTalent_ID] in table 'TalentAddress'
ALTER TABLE [dbo].[TalentAddress]
ADD CONSTRAINT [FK_TalentAddress_Talents]
    FOREIGN KEY ([Talents_PKTalent_ID])
    REFERENCES [dbo].[Talents]
        ([PKTalent_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TalentAddress_Talents'
CREATE INDEX [IX_FK_TalentAddress_Talents]
ON [dbo].[TalentAddress]
    ([Talents_PKTalent_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------