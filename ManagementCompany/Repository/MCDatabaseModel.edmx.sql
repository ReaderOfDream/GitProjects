
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/02/2012 00:33:51
-- Generated from EDMX file: D:\Фриланс\Управляющая компания\GitProjects\ManagementCompany\Repository\MCDatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE ManagementCompany;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BuildingHeatSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Buildings] DROP CONSTRAINT [FK_BuildingHeatSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingMeterReading]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MeterReadings] DROP CONSTRAINT [FK_BuildingMeterReading];
GO
IF OBJECT_ID(N'[dbo].[FK_DateTimeIntervalMeterReading]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MeterReadings] DROP CONSTRAINT [FK_DateTimeIntervalMeterReading];
GO
IF OBJECT_ID(N'[dbo].[FK_DateTimeIntervalHeatSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DateTimeIntervals] DROP CONSTRAINT [FK_DateTimeIntervalHeatSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingClearing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clearings] DROP CONSTRAINT [FK_BuildingClearing];
GO
IF OBJECT_ID(N'[dbo].[FK_DateTimeIntervalClearing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clearings] DROP CONSTRAINT [FK_DateTimeIntervalClearing];
GO
IF OBJECT_ID(N'[dbo].[FK_DateTimeIntervalContractConsumptionHeat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeats] DROP CONSTRAINT [FK_DateTimeIntervalContractConsumptionHeat];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractConsumptionHeatBuilding]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeats] DROP CONSTRAINT [FK_ContractConsumptionHeatBuilding];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractConsumptionHeatThermometerReading]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeats] DROP CONSTRAINT [FK_ContractConsumptionHeatThermometerReading];
GO
IF OBJECT_ID(N'[dbo].[FK_NormativeCalculationBuilding]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NormativeCalculations] DROP CONSTRAINT [FK_NormativeCalculationBuilding];
GO
IF OBJECT_ID(N'[dbo].[FK_DateTimeIntervalNormativeCalculation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NormativeCalculations] DROP CONSTRAINT [FK_DateTimeIntervalNormativeCalculation];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingBuildingMonthVariables]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BuildingMonthVariablesTable] DROP CONSTRAINT [FK_BuildingBuildingMonthVariables];
GO
IF OBJECT_ID(N'[dbo].[FK_DateTimeIntervalBuildingMonthVariables]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BuildingMonthVariablesTable] DROP CONSTRAINT [FK_DateTimeIntervalBuildingMonthVariables];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Buildings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Buildings];
GO
IF OBJECT_ID(N'[dbo].[DateTimeIntervals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DateTimeIntervals];
GO
IF OBJECT_ID(N'[dbo].[NormativeCalculations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NormativeCalculations];
GO
IF OBJECT_ID(N'[dbo].[ContractConsumptionHeats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContractConsumptionHeats];
GO
IF OBJECT_ID(N'[dbo].[MeterReadings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MeterReadings];
GO
IF OBJECT_ID(N'[dbo].[Clearings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clearings];
GO
IF OBJECT_ID(N'[dbo].[HeatSuppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HeatSuppliers];
GO
IF OBJECT_ID(N'[dbo].[ThermometerReadings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ThermometerReadings];
GO
IF OBJECT_ID(N'[dbo].[WholeTableSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WholeTableSet];
GO
IF OBJECT_ID(N'[dbo].[BuildingMonthVariablesTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BuildingMonthVariablesTable];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Buildings'
CREATE TABLE [dbo].[Buildings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [StandartOfHeat] float  NOT NULL,
    [TotalArea] float  NOT NULL,
    [HeatSupplier_Id] int  NOT NULL
);
GO

-- Creating table 'DateTimeIntervals'
CREATE TABLE [dbo].[DateTimeIntervals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [HeatSupplier_Id] int  NOT NULL
);
GO

-- Creating table 'NormativeCalculations'
CREATE TABLE [dbo].[NormativeCalculations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EstimateConsumptionHeat] float  NOT NULL,
    [ConsumptionHeatByTotalArea] float  NOT NULL,
    [ConsumptionHeatByCalculationArea] float  NOT NULL,
    [TotalHeatConsumption] float  NOT NULL,
    [Building_Id] int  NOT NULL,
    [DateTimeInterval_Id] int  NOT NULL
);
GO

-- Creating table 'ContractConsumptionHeats'
CREATE TABLE [dbo].[ContractConsumptionHeats] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [HeatByLoading] float  NOT NULL,
    [HotWaterByNorm] float  NOT NULL,
    [TotalHeatConsumption] float  NOT NULL,
    [DateTimeInterval_Id] int  NOT NULL,
    [Building_Id] int  NOT NULL,
    [ThermometerReading_Id] int  NOT NULL
);
GO

-- Creating table 'MeterReadings'
CREATE TABLE [dbo].[MeterReadings] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CurrentHeatMeterReader] float  NOT NULL,
    [CurrentWaterHeatReader] float  NOT NULL,
    [Building_Id] int  NOT NULL,
    [DateTimeInterval_Id] int  NOT NULL
);
GO

-- Creating table 'Clearings'
CREATE TABLE [dbo].[Clearings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Requirements] float  NOT NULL,
    [CalculationByBughaltery] float  NOT NULL,
    [CalculationHot] float  NOT NULL,
    [Building_Id] int  NOT NULL,
    [DateTimeInterval_Id] int  NOT NULL
);
GO

-- Creating table 'HeatSuppliers'
CREATE TABLE [dbo].[HeatSuppliers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ThermometerReadings'
CREATE TABLE [dbo].[ThermometerReadings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Month] int  NOT NULL,
    [Year] int  NOT NULL,
    [AirTemperature] int  NOT NULL
);
GO

-- Creating table 'WholeTableSet'
CREATE TABLE [dbo].[WholeTableSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BuildingName] nvarchar(max)  NOT NULL,
    [BuildingDescription] nvarchar(max)  NOT NULL,
    [BuildingStandartOfHeat] float  NOT NULL,
    [BuildingTotalArea] float  NOT NULL,
    [HeatSupplierName] nvarchar(max)  NOT NULL,
    [HeatSupplierDescription] nvarchar(max)  NOT NULL,
    [DateTimeIntervalStartDate] datetime  NULL,
    [DateTimeIntervalEndDate] datetime  NULL,
    [DateTimeIntervalName] nvarchar(max)  NULL,
    [ClearingRequirements] float  NOT NULL,
    [ClearingCalculationHot] float  NOT NULL,
    [ClearingCalculationByBuhgaltery] float  NOT NULL,
    [MeterReadingHeat] float  NOT NULL,
    [MeterReadingWater] float  NOT NULL,
    [ContractHeatByLoading] float  NOT NULL,
    [ContractPeopleCount] float  NOT NULL,
    [ContractHotWaterByNorm] float  NOT NULL,
    [ContractTotalHeatConsumption] float  NOT NULL,
    [NormativeCalculationArea] float  NOT NULL,
    [NormativeEstimateConsumptionHeat] float  NOT NULL,
    [NormativConsumptionHeatByTotalArea] float  NOT NULL,
    [NormativeConsumptionHeatByCalculationArea] float  NOT NULL
);
GO

-- Creating table 'BuildingMonthVariablesTable'
CREATE TABLE [dbo].[BuildingMonthVariablesTable] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CalculationArea] float  NOT NULL,
    [CountOfPeople] int  NOT NULL,
    [Building_Id] int  NOT NULL,
    [DateTimeInterval_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Buildings'
ALTER TABLE [dbo].[Buildings]
ADD CONSTRAINT [PK_Buildings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DateTimeIntervals'
ALTER TABLE [dbo].[DateTimeIntervals]
ADD CONSTRAINT [PK_DateTimeIntervals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NormativeCalculations'
ALTER TABLE [dbo].[NormativeCalculations]
ADD CONSTRAINT [PK_NormativeCalculations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'ContractConsumptionHeats'
ALTER TABLE [dbo].[ContractConsumptionHeats]
ADD CONSTRAINT [PK_ContractConsumptionHeats]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MeterReadings'
ALTER TABLE [dbo].[MeterReadings]
ADD CONSTRAINT [PK_MeterReadings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'Clearings'
ALTER TABLE [dbo].[Clearings]
ADD CONSTRAINT [PK_Clearings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HeatSuppliers'
ALTER TABLE [dbo].[HeatSuppliers]
ADD CONSTRAINT [PK_HeatSuppliers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ThermometerReadings'
ALTER TABLE [dbo].[ThermometerReadings]
ADD CONSTRAINT [PK_ThermometerReadings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WholeTableSet'
ALTER TABLE [dbo].[WholeTableSet]
ADD CONSTRAINT [PK_WholeTableSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BuildingMonthVariablesTable'
ALTER TABLE [dbo].[BuildingMonthVariablesTable]
ADD CONSTRAINT [PK_BuildingMonthVariablesTable]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [HeatSupplier_Id] in table 'Buildings'
ALTER TABLE [dbo].[Buildings]
ADD CONSTRAINT [FK_BuildingHeatSupplier]
    FOREIGN KEY ([HeatSupplier_Id])
    REFERENCES [dbo].[HeatSuppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingHeatSupplier'
CREATE INDEX [IX_FK_BuildingHeatSupplier]
ON [dbo].[Buildings]
    ([HeatSupplier_Id]);
GO

-- Creating foreign key on [Building_Id] in table 'MeterReadings'
ALTER TABLE [dbo].[MeterReadings]
ADD CONSTRAINT [FK_BuildingMeterReading]
    FOREIGN KEY ([Building_Id])
    REFERENCES [dbo].[Buildings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingMeterReading'
CREATE INDEX [IX_FK_BuildingMeterReading]
ON [dbo].[MeterReadings]
    ([Building_Id]);
GO

-- Creating foreign key on [DateTimeInterval_Id] in table 'MeterReadings'
ALTER TABLE [dbo].[MeterReadings]
ADD CONSTRAINT [FK_DateTimeIntervalMeterReading]
    FOREIGN KEY ([DateTimeInterval_Id])
    REFERENCES [dbo].[DateTimeIntervals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DateTimeIntervalMeterReading'
CREATE INDEX [IX_FK_DateTimeIntervalMeterReading]
ON [dbo].[MeterReadings]
    ([DateTimeInterval_Id]);
GO

-- Creating foreign key on [HeatSupplier_Id] in table 'DateTimeIntervals'
ALTER TABLE [dbo].[DateTimeIntervals]
ADD CONSTRAINT [FK_DateTimeIntervalHeatSupplier]
    FOREIGN KEY ([HeatSupplier_Id])
    REFERENCES [dbo].[HeatSuppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DateTimeIntervalHeatSupplier'
CREATE INDEX [IX_FK_DateTimeIntervalHeatSupplier]
ON [dbo].[DateTimeIntervals]
    ([HeatSupplier_Id]);
GO

-- Creating foreign key on [Building_Id] in table 'Clearings'
ALTER TABLE [dbo].[Clearings]
ADD CONSTRAINT [FK_BuildingClearing]
    FOREIGN KEY ([Building_Id])
    REFERENCES [dbo].[Buildings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingClearing'
CREATE INDEX [IX_FK_BuildingClearing]
ON [dbo].[Clearings]
    ([Building_Id]);
GO

-- Creating foreign key on [DateTimeInterval_Id] in table 'Clearings'
ALTER TABLE [dbo].[Clearings]
ADD CONSTRAINT [FK_DateTimeIntervalClearing]
    FOREIGN KEY ([DateTimeInterval_Id])
    REFERENCES [dbo].[DateTimeIntervals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DateTimeIntervalClearing'
CREATE INDEX [IX_FK_DateTimeIntervalClearing]
ON [dbo].[Clearings]
    ([DateTimeInterval_Id]);
GO

-- Creating foreign key on [DateTimeInterval_Id] in table 'ContractConsumptionHeats'
ALTER TABLE [dbo].[ContractConsumptionHeats]
ADD CONSTRAINT [FK_DateTimeIntervalContractConsumptionHeat]
    FOREIGN KEY ([DateTimeInterval_Id])
    REFERENCES [dbo].[DateTimeIntervals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DateTimeIntervalContractConsumptionHeat'
CREATE INDEX [IX_FK_DateTimeIntervalContractConsumptionHeat]
ON [dbo].[ContractConsumptionHeats]
    ([DateTimeInterval_Id]);
GO

-- Creating foreign key on [Building_Id] in table 'ContractConsumptionHeats'
ALTER TABLE [dbo].[ContractConsumptionHeats]
ADD CONSTRAINT [FK_ContractConsumptionHeatBuilding]
    FOREIGN KEY ([Building_Id])
    REFERENCES [dbo].[Buildings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractConsumptionHeatBuilding'
CREATE INDEX [IX_FK_ContractConsumptionHeatBuilding]
ON [dbo].[ContractConsumptionHeats]
    ([Building_Id]);
GO

-- Creating foreign key on [ThermometerReading_Id] in table 'ContractConsumptionHeats'
ALTER TABLE [dbo].[ContractConsumptionHeats]
ADD CONSTRAINT [FK_ContractConsumptionHeatThermometerReading]
    FOREIGN KEY ([ThermometerReading_Id])
    REFERENCES [dbo].[ThermometerReadings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractConsumptionHeatThermometerReading'
CREATE INDEX [IX_FK_ContractConsumptionHeatThermometerReading]
ON [dbo].[ContractConsumptionHeats]
    ([ThermometerReading_Id]);
GO

-- Creating foreign key on [Building_Id] in table 'NormativeCalculations'
ALTER TABLE [dbo].[NormativeCalculations]
ADD CONSTRAINT [FK_NormativeCalculationBuilding]
    FOREIGN KEY ([Building_Id])
    REFERENCES [dbo].[Buildings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NormativeCalculationBuilding'
CREATE INDEX [IX_FK_NormativeCalculationBuilding]
ON [dbo].[NormativeCalculations]
    ([Building_Id]);
GO

-- Creating foreign key on [DateTimeInterval_Id] in table 'NormativeCalculations'
ALTER TABLE [dbo].[NormativeCalculations]
ADD CONSTRAINT [FK_DateTimeIntervalNormativeCalculation]
    FOREIGN KEY ([DateTimeInterval_Id])
    REFERENCES [dbo].[DateTimeIntervals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DateTimeIntervalNormativeCalculation'
CREATE INDEX [IX_FK_DateTimeIntervalNormativeCalculation]
ON [dbo].[NormativeCalculations]
    ([DateTimeInterval_Id]);
GO

-- Creating foreign key on [Building_Id] in table 'BuildingMonthVariablesTable'
ALTER TABLE [dbo].[BuildingMonthVariablesTable]
ADD CONSTRAINT [FK_BuildingBuildingMonthVariables]
    FOREIGN KEY ([Building_Id])
    REFERENCES [dbo].[Buildings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingBuildingMonthVariables'
CREATE INDEX [IX_FK_BuildingBuildingMonthVariables]
ON [dbo].[BuildingMonthVariablesTable]
    ([Building_Id]);
GO

-- Creating foreign key on [DateTimeInterval_Id] in table 'BuildingMonthVariablesTable'
ALTER TABLE [dbo].[BuildingMonthVariablesTable]
ADD CONSTRAINT [FK_DateTimeIntervalBuildingMonthVariables]
    FOREIGN KEY ([DateTimeInterval_Id])
    REFERENCES [dbo].[DateTimeIntervals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DateTimeIntervalBuildingMonthVariables'
CREATE INDEX [IX_FK_DateTimeIntervalBuildingMonthVariables]
ON [dbo].[BuildingMonthVariablesTable]
    ([DateTimeInterval_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------