
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/23/2021 18:50:59
-- Generated from EDMX file: C:\Users\ciant\Desktop\College Notes\Sem4\OOD\OOD_Project\Project\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ProjectDatabase];
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

-- Creating table 'CarTBLs'
CREATE TABLE [dbo].[CarTBLs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TopSpeed] nvarchar(max)  NOT NULL,
    [ZeroTo100] nvarchar(max)  NOT NULL,
    [Horsepower] nvarchar(max)  NOT NULL,
    [Torque] nvarchar(max)  NOT NULL,
    [MaxRpm] nvarchar(max)  NOT NULL,
    [FuelMpg] nvarchar(max)  NOT NULL,
    [ImageUrl] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ModTBLs'
CREATE TABLE [dbo].[ModTBLs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TopSpeedMod] nvarchar(max)  NOT NULL,
    [HorsepowerMod] nvarchar(max)  NOT NULL,
    [ZeroTo100Mod] nvarchar(max)  NOT NULL,
    [Index] nvarchar(max)  NOT NULL,
    [SetupName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CarTBLModTBL'
CREATE TABLE [dbo].[CarTBLModTBL] (
    [CarTBLs_Id] int  NOT NULL,
    [ModTBLs_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CarTBLs'
ALTER TABLE [dbo].[CarTBLs]
ADD CONSTRAINT [PK_CarTBLs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ModTBLs'
ALTER TABLE [dbo].[ModTBLs]
ADD CONSTRAINT [PK_ModTBLs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [CarTBLs_Id], [ModTBLs_Id] in table 'CarTBLModTBL'
ALTER TABLE [dbo].[CarTBLModTBL]
ADD CONSTRAINT [PK_CarTBLModTBL]
    PRIMARY KEY CLUSTERED ([CarTBLs_Id], [ModTBLs_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CarTBLs_Id] in table 'CarTBLModTBL'
ALTER TABLE [dbo].[CarTBLModTBL]
ADD CONSTRAINT [FK_CarTBLModTBL_CarTBL]
    FOREIGN KEY ([CarTBLs_Id])
    REFERENCES [dbo].[CarTBLs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ModTBLs_Id] in table 'CarTBLModTBL'
ALTER TABLE [dbo].[CarTBLModTBL]
ADD CONSTRAINT [FK_CarTBLModTBL_ModTBL]
    FOREIGN KEY ([ModTBLs_Id])
    REFERENCES [dbo].[ModTBLs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarTBLModTBL_ModTBL'
CREATE INDEX [IX_FK_CarTBLModTBL_ModTBL]
ON [dbo].[CarTBLModTBL]
    ([ModTBLs_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------