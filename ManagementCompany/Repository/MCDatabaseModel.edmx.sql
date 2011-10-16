
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/16/2011 20:49:26
-- Generated from EDMX file: D:\Фриланс\Управляющая компания\GitProjects\ManagementCompany\Repository\MCDatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ManagementCompany];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BuildingsDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DateTimeImtervalsНабор] DROP CONSTRAINT [FK_BuildingsDateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsContractConsumptionHeat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeatTable] DROP CONSTRAINT [FK_BuildingsContractConsumptionHeat];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsNormativeCalculation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NormativeCalculationНабор] DROP CONSTRAINT [FK_BuildingsNormativeCalculation];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractConsumptionHeatDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeatTable] DROP CONSTRAINT [FK_ContractConsumptionHeatDateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[FK_NormativeCalculationDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NormativeCalculationНабор] DROP CONSTRAINT [FK_NormativeCalculationDateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsMeterReadings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MeterReadingsTable] DROP CONSTRAINT [FK_BuildingsMeterReadings];
GO
IF OBJECT_ID(N'[dbo].[FK_MeterReadingsDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MeterReadingsTable] DROP CONSTRAINT [FK_MeterReadingsDateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsClearing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClearingTable] DROP CONSTRAINT [FK_BuildingsClearing];
GO
IF OBJECT_ID(N'[dbo].[FK_DateTimeImtervalsClearing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClearingTable] DROP CONSTRAINT [FK_DateTimeImtervalsClearing];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BuildingsНабор]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BuildingsНабор];
GO
IF OBJECT_ID(N'[dbo].[DateTimeImtervalsНабор]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DateTimeImtervalsНабор];
GO
IF OBJECT_ID(N'[dbo].[NormativeCalculationНабор]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NormativeCalculationНабор];
GO
IF OBJECT_ID(N'[dbo].[ContractConsumptionHeatTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContractConsumptionHeatTable];
GO
IF OBJECT_ID(N'[dbo].[MeterReadingsTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MeterReadingsTable];
GO
IF OBJECT_ID(N'[dbo].[ClearingTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClearingTable];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BuildingsНабор'
CREATE TABLE [dbo].[BuildingsНабор] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [EstimateConsumptionHeat] float  NOT NULL
);
GO

-- Creating table 'DateTimeImtervalsНабор'
CREATE TABLE [dbo].[DateTimeImtervalsНабор] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [BuildingsId] int  NOT NULL
);
GO

-- Creating table 'NormativeCalculationНабор'
CREATE TABLE [dbo].[NormativeCalculationНабор] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TotalArea] float  NOT NULL,
    [CalculationArea] float  NOT NULL,
    [StandartOfHeat] float  NOT NULL,
    [ConsumptionHeatByTotalArea] float  NOT NULL,
    [ConsumptionHeatByCalculationArea] float  NOT NULL,
    [TotalNormativeHeat] float  NOT NULL,
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL
);
GO

-- Creating table 'ContractConsumptionHeatTable'
CREATE TABLE [dbo].[ContractConsumptionHeatTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AirTemperature] float  NOT NULL,
    [HeatByLoading] float  NOT NULL,
    [PeopleCount] int  NOT NULL,
    [HotWaterByNorm] float  NOT NULL,
    [TotalHeatConsumption] float  NOT NULL,
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL
);
GO

-- Creating table 'MeterReadingsTable'
CREATE TABLE [dbo].[MeterReadingsTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CurrentHeatMeterReader] float  NOT NULL,
    [CurrentWaterHeatReader] float  NOT NULL,
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL
);
GO

-- Creating table 'ClearingTable'
CREATE TABLE [dbo].[ClearingTable] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Requirements] float  NOT NULL,
    [CalculationHotWater] float  NOT NULL,
    [CalculationHot] float  NOT NULL,
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BuildingsНабор'
ALTER TABLE [dbo].[BuildingsНабор]
ADD CONSTRAINT [PK_BuildingsНабор]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DateTimeImtervalsНабор'
ALTER TABLE [dbo].[DateTimeImtervalsНабор]
ADD CONSTRAINT [PK_DateTimeImtervalsНабор]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NormativeCalculationНабор'
ALTER TABLE [dbo].[NormativeCalculationНабор]
ADD CONSTRAINT [PK_NormativeCalculationНабор]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [PK_ContractConsumptionHeatTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MeterReadingsTable'
ALTER TABLE [dbo].[MeterReadingsTable]
ADD CONSTRAINT [PK_MeterReadingsTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'ClearingTable'
ALTER TABLE [dbo].[ClearingTable]
ADD CONSTRAINT [PK_ClearingTable]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BuildingsId] in table 'DateTimeImtervalsНабор'
ALTER TABLE [dbo].[DateTimeImtervalsНабор]
ADD CONSTRAINT [FK_BuildingsDateTimeImtervals]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[BuildingsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsDateTimeImtervals'
CREATE INDEX [IX_FK_BuildingsDateTimeImtervals]
ON [dbo].[DateTimeImtervalsНабор]
    ([BuildingsId]);
GO

-- Creating foreign key on [BuildingsId] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [FK_BuildingsContractConsumptionHeat]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[BuildingsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsContractConsumptionHeat'
CREATE INDEX [IX_FK_BuildingsContractConsumptionHeat]
ON [dbo].[ContractConsumptionHeatTable]
    ([BuildingsId]);
GO

-- Creating foreign key on [BuildingsId] in table 'NormativeCalculationНабор'
ALTER TABLE [dbo].[NormativeCalculationНабор]
ADD CONSTRAINT [FK_BuildingsNormativeCalculation]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[BuildingsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsNormativeCalculation'
CREATE INDEX [IX_FK_BuildingsNormativeCalculation]
ON [dbo].[NormativeCalculationНабор]
    ([BuildingsId]);
GO

-- Creating foreign key on [DateTimeImtervals_Id] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [FK_ContractConsumptionHeatDateTimeImtervals]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervalsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractConsumptionHeatDateTimeImtervals'
CREATE INDEX [IX_FK_ContractConsumptionHeatDateTimeImtervals]
ON [dbo].[ContractConsumptionHeatTable]
    ([DateTimeImtervals_Id]);
GO

-- Creating foreign key on [DateTimeImtervals_Id] in table 'NormativeCalculationНабор'
ALTER TABLE [dbo].[NormativeCalculationНабор]
ADD CONSTRAINT [FK_NormativeCalculationDateTimeImtervals]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervalsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NormativeCalculationDateTimeImtervals'
CREATE INDEX [IX_FK_NormativeCalculationDateTimeImtervals]
ON [dbo].[NormativeCalculationНабор]
    ([DateTimeImtervals_Id]);
GO

-- Creating foreign key on [BuildingsId] in table 'MeterReadingsTable'
ALTER TABLE [dbo].[MeterReadingsTable]
ADD CONSTRAINT [FK_BuildingsMeterReadings]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[BuildingsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsMeterReadings'
CREATE INDEX [IX_FK_BuildingsMeterReadings]
ON [dbo].[MeterReadingsTable]
    ([BuildingsId]);
GO

-- Creating foreign key on [DateTimeImtervals_Id] in table 'MeterReadingsTable'
ALTER TABLE [dbo].[MeterReadingsTable]
ADD CONSTRAINT [FK_MeterReadingsDateTimeImtervals]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervalsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MeterReadingsDateTimeImtervals'
CREATE INDEX [IX_FK_MeterReadingsDateTimeImtervals]
ON [dbo].[MeterReadingsTable]
    ([DateTimeImtervals_Id]);
GO

-- Creating foreign key on [BuildingsId] in table 'ClearingTable'
ALTER TABLE [dbo].[ClearingTable]
ADD CONSTRAINT [FK_BuildingsClearing]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[BuildingsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsClearing'
CREATE INDEX [IX_FK_BuildingsClearing]
ON [dbo].[ClearingTable]
    ([BuildingsId]);
GO

-- Creating foreign key on [DateTimeImtervals_Id] in table 'ClearingTable'
ALTER TABLE [dbo].[ClearingTable]
ADD CONSTRAINT [FK_DateTimeImtervalsClearing]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervalsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DateTimeImtervalsClearing'
CREATE INDEX [IX_FK_DateTimeImtervalsClearing]
ON [dbo].[ClearingTable]
    ([DateTimeImtervals_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------