
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/04/2019 13:21:17
-- Generated from EDMX file: C:\Users\WeslyAS\Source\Repos\WeslyArfanSimanjuntak\WebApplication\Repository.Application\DataModel\ModelTritsur.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_Tritsur];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ARMADA_ToSite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ARMADA] DROP CONSTRAINT [FK_ARMADA_ToSite];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetGroupUser_ToGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetGroupUser] DROP CONSTRAINT [FK_AspNetGroupUser_ToGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetGroupUser_ToUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetGroupUser] DROP CONSTRAINT [FK_AspNetGroupUser_ToUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetRoleGroup_ToGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetRoleGroup] DROP CONSTRAINT [FK_AspNetRoleGroup_ToGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetRoleGroup_ToRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetRoleGroup] DROP CONSTRAINT [FK_AspNetRoleGroup_ToRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetRoles_ToAspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetRoles] DROP CONSTRAINT [FK_AspNetRoles_ToAspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_Batch_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BATCH] DROP CONSTRAINT [FK_Batch_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_BatchMix_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BatchMix] DROP CONSTRAINT [FK_BatchMix_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_BatchMixProduct_ToBatchMix]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BatchMixProduct] DROP CONSTRAINT [FK_BatchMixProduct_ToBatchMix];
GO
IF OBJECT_ID(N'[dbo].[FK_BatchMixProduct_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BatchMixProduct] DROP CONSTRAINT [FK_BatchMixProduct_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_BatchProduct_ToBatch]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BatchProduct] DROP CONSTRAINT [FK_BatchProduct_ToBatch];
GO
IF OBJECT_ID(N'[dbo].[FK_BatchProduct_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BatchProduct] DROP CONSTRAINT [FK_BatchProduct_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_CONTRACT_ToClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CONTRACT] DROP CONSTRAINT [FK_CONTRACT_ToClient];
GO
IF OBJECT_ID(N'[dbo].[FK_CONTRACT_ToSite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CONTRACT] DROP CONSTRAINT [FK_CONTRACT_ToSite];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractProduct_ToContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractProduct] DROP CONSTRAINT [FK_ContractProduct_ToContract];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractProduct_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractProduct] DROP CONSTRAINT [FK_ContractProduct_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryOrder_ToDeliveryRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryOrder] DROP CONSTRAINT [FK_DeliveryOrder_ToDeliveryRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryRequest_ToContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryRequest] DROP CONSTRAINT [FK_DeliveryRequest_ToContract];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryRequestProductDetailTransaction_ToDeliveryRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryRequestProductDetailTransaction] DROP CONSTRAINT [FK_DeliveryRequestProductDetailTransaction_ToDeliveryRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryRequestProductDetailTransaction_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryRequestProductDetailTransaction] DROP CONSTRAINT [FK_DeliveryRequestProductDetailTransaction_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_EMPLOYEE_ToSite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EMPLOYEE] DROP CONSTRAINT [FK_EMPLOYEE_ToSite];
GO
IF OBJECT_ID(N'[dbo].[FK_FinanceBalance_ToContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinanceBalance] DROP CONSTRAINT [FK_FinanceBalance_ToContract];
GO
IF OBJECT_ID(N'[dbo].[FK_FinanceTransaction_ToContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinanceTransaction] DROP CONSTRAINT [FK_FinanceTransaction_ToContract];
GO
IF OBJECT_ID(N'[dbo].[FK_FinanceTransaction_ToTransactionCode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinanceTransaction] DROP CONSTRAINT [FK_FinanceTransaction_ToTransactionCode];
GO
IF OBJECT_ID(N'[dbo].[FK_FinanceTransactionNostro_ToFinanceTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinanceTransactionNostro] DROP CONSTRAINT [FK_FinanceTransactionNostro_ToFinanceTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_FinanceTransactionNostro_ToNostro]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinanceTransactionNostro] DROP CONSTRAINT [FK_FinanceTransactionNostro_ToNostro];
GO
IF OBJECT_ID(N'[dbo].[FK_Menu_ToMenuAspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Menu] DROP CONSTRAINT [FK_Menu_ToMenuAspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_Menu_ToMenuParent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Menu] DROP CONSTRAINT [FK_Menu_ToMenuParent];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductAdjustment_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductAdjustment] DROP CONSTRAINT [FK_ProductAdjustment_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductAdjustment_ToSite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductAdjustment] DROP CONSTRAINT [FK_ProductAdjustment_ToSite];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductConvertion_BATCH]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductConvertion] DROP CONSTRAINT [FK_ProductConvertion_BATCH];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductConvertion_PRODUCT]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductConvertion] DROP CONSTRAINT [FK_ProductConvertion_PRODUCT];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductConvertion_SITE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductConvertion] DROP CONSTRAINT [FK_ProductConvertion_SITE];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductMixing_BatchMix]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductMixing] DROP CONSTRAINT [FK_ProductMixing_BatchMix];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductMixing_PRODUCT]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductMixing] DROP CONSTRAINT [FK_ProductMixing_PRODUCT];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductMixing_SITE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductMixing] DROP CONSTRAINT [FK_ProductMixing_SITE];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductSite_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSite] DROP CONSTRAINT [FK_ProductSite_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductSite_ToSite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSite] DROP CONSTRAINT [FK_ProductSite_ToSite];
GO
IF OBJECT_ID(N'[dbo].[FK_RITASE_RELATIONS_ARMADA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RITASE] DROP CONSTRAINT [FK_RITASE_RELATIONS_ARMADA];
GO
IF OBJECT_ID(N'[dbo].[FK_RITASE_RELATIONS_BATCH]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RITASE] DROP CONSTRAINT [FK_RITASE_RELATIONS_BATCH];
GO
IF OBJECT_ID(N'[dbo].[FK_RITASE_RELATIONS_EMPLOYEE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RITASE] DROP CONSTRAINT [FK_RITASE_RELATIONS_EMPLOYEE];
GO
IF OBJECT_ID(N'[dbo].[FK_RITASE_RELATIONS_PRODUCT]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RITASE] DROP CONSTRAINT [FK_RITASE_RELATIONS_PRODUCT];
GO
IF OBJECT_ID(N'[dbo].[FK_RITASE_RELATIONS_SITE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RITASE] DROP CONSTRAINT [FK_RITASE_RELATIONS_SITE];
GO
IF OBJECT_ID(N'[dbo].[FK_RITASE_RELATIONS_SOURCE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RITASE] DROP CONSTRAINT [FK_RITASE_RELATIONS_SOURCE];
GO
IF OBJECT_ID(N'[dbo].[FK_SITE_ToSource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SITE] DROP CONSTRAINT [FK_SITE_ToSource];
GO
IF OBJECT_ID(N'[dbo].[FK_StockProduct_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StockProduct] DROP CONSTRAINT [FK_StockProduct_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionProduct_ToProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionProduct] DROP CONSTRAINT [FK_TransactionProduct_ToProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionProduct_ToProductAdjustment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionProduct] DROP CONSTRAINT [FK_TransactionProduct_ToProductAdjustment];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionProduct_ToProductMixing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionProduct] DROP CONSTRAINT [FK_TransactionProduct_ToProductMixing];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionProduct_ToProductProductConvertion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionProduct] DROP CONSTRAINT [FK_TransactionProduct_ToProductProductConvertion];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionProduct_ToRitase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionProduct] DROP CONSTRAINT [FK_TransactionProduct_ToRitase];
GO
IF OBJECT_ID(N'[dbo].[FK_UserClientMapping_ToAspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserClientMapping] DROP CONSTRAINT [FK_UserClientMapping_ToAspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_UserClientMapping_ToClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserClientMapping] DROP CONSTRAINT [FK_UserClientMapping_ToClient];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ARMADA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ARMADA];
GO
IF OBJECT_ID(N'[dbo].[AspNetGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetGroups];
GO
IF OBJECT_ID(N'[dbo].[AspNetGroupUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetGroupUser];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoleGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoleGroup];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[BATCH]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BATCH];
GO
IF OBJECT_ID(N'[dbo].[BatchMix]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BatchMix];
GO
IF OBJECT_ID(N'[dbo].[BatchMixProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BatchMixProduct];
GO
IF OBJECT_ID(N'[dbo].[BatchProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BatchProduct];
GO
IF OBJECT_ID(N'[dbo].[CLIENT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CLIENT];
GO
IF OBJECT_ID(N'[dbo].[CONTRACT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CONTRACT];
GO
IF OBJECT_ID(N'[dbo].[ContractProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContractProduct];
GO
IF OBJECT_ID(N'[dbo].[DeliveryOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliveryOrder];
GO
IF OBJECT_ID(N'[dbo].[DeliveryOrderInvoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliveryOrderInvoice];
GO
IF OBJECT_ID(N'[dbo].[DeliveryRequest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliveryRequest];
GO
IF OBJECT_ID(N'[dbo].[DeliveryRequestProductDetailTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliveryRequestProductDetailTransaction];
GO
IF OBJECT_ID(N'[dbo].[EMPLOYEE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EMPLOYEE];
GO
IF OBJECT_ID(N'[dbo].[FinanceBalance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinanceBalance];
GO
IF OBJECT_ID(N'[dbo].[FinanceTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinanceTransaction];
GO
IF OBJECT_ID(N'[dbo].[FinanceTransactionNostro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinanceTransactionNostro];
GO
IF OBJECT_ID(N'[dbo].[Menu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Menu];
GO
IF OBJECT_ID(N'[dbo].[NostroBank]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NostroBank];
GO
IF OBJECT_ID(N'[dbo].[ParameterSetup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ParameterSetup];
GO
IF OBJECT_ID(N'[dbo].[PRODUCT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PRODUCT];
GO
IF OBJECT_ID(N'[dbo].[ProductAdjustment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductAdjustment];
GO
IF OBJECT_ID(N'[dbo].[ProductConvertion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductConvertion];
GO
IF OBJECT_ID(N'[dbo].[ProductMixing]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductMixing];
GO
IF OBJECT_ID(N'[dbo].[ProductSite]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSite];
GO
IF OBJECT_ID(N'[dbo].[RITASE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RITASE];
GO
IF OBJECT_ID(N'[dbo].[SITE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SITE];
GO
IF OBJECT_ID(N'[dbo].[SOURCE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SOURCE];
GO
IF OBJECT_ID(N'[dbo].[StockProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StockProduct];
GO
IF OBJECT_ID(N'[dbo].[TableSequence]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TableSequence];
GO
IF OBJECT_ID(N'[dbo].[TransactionCode]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionCode];
GO
IF OBJECT_ID(N'[dbo].[TransactionProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionProduct];
GO
IF OBJECT_ID(N'[dbo].[UserClientMapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserClientMapping];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ARMADA'
CREATE TABLE [dbo].[ARMADA] (
    [ARMADAID] varchar(25)  NOT NULL,
    [ARMADANAME] varchar(50)  NULL,
    [ARMADACAPACITYINKG] float  NULL,
    [ARMADATOTALTIRE] int  NULL,
    [ARMADACOLOR] varchar(25)  NULL,
    [ARMADAMERK] varchar(50)  NULL,
    [SITENAME] varchar(50)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'AspNetGroups'
CREATE TABLE [dbo].[AspNetGroups] (
    [GroupName] varchar(50)  NOT NULL,
    [GroupDescription] varchar(256)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'AspNetGroupUser'
CREATE TABLE [dbo].[AspNetGroupUser] (
    [Username] varchar(50)  NOT NULL,
    [GroupName] varchar(50)  NOT NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'AspNetRoleGroup'
CREATE TABLE [dbo].[AspNetRoleGroup] (
    [RolesId] int  NOT NULL,
    [GroupName] varchar(50)  NOT NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ParentId] int  NULL,
    [Name] varchar(50)  NULL,
    [Type] varchar(50)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Username] varchar(50)  NOT NULL,
    [FullName] varchar(50)  NULL,
    [Password] varchar(256)  NULL,
    [Email] varchar(128)  NULL,
    [LastPasswordChange] datetime  NULL,
    [ErrorTried] int  NULL,
    [IsLocked] smallint  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'BATCH'
CREATE TABLE [dbo].[BATCH] (
    [BatchCode] varchar(50)  NOT NULL,
    [BatchName] varchar(50)  NULL,
    [RawProduct] varchar(25)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'BatchMix'
CREATE TABLE [dbo].[BatchMix] (
    [BatchMixCode] varchar(50)  NOT NULL,
    [BatchMixName] varchar(50)  NULL,
    [OutputProduct] varchar(25)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'BatchMixProduct'
CREATE TABLE [dbo].[BatchMixProduct] (
    [BatchMixCode] varchar(50)  NOT NULL,
    [ProductSourceId] varchar(25)  NOT NULL,
    [ProductSourcePercentage] float  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'BatchProduct'
CREATE TABLE [dbo].[BatchProduct] (
    [BatchCode] varchar(50)  NOT NULL,
    [ProductId] varchar(25)  NOT NULL,
    [ProductPercentage] float  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'CLIENT'
CREATE TABLE [dbo].[CLIENT] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClientId] varchar(50)  NOT NULL,
    [ClientName] varchar(50)  NULL,
    [ClientAddress] varchar(50)  NULL,
    [ClientType] varchar(50)  NULL,
    [ClientCompanyName] varchar(50)  NULL,
    [ClientCompanyPIC] varchar(50)  NULL,
    [ClientCompanyPICPhoneNumber] varchar(50)  NULL,
    [ClientCompanyLeaderEmailAddress] varchar(50)  NULL,
    [ClientNPWP] varchar(50)  NULL,
    [ClientPhoneNumber] varchar(50)  NULL,
    [ClientEmail] varchar(50)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL,
    [Remark] varchar(512)  NULL,
    [ClientCompanyPIC2] varchar(50)  NULL,
    [ClientCompanyPICPhoneNumber2] varchar(50)  NULL,
    [ClientCompanyLeaderEmailAddress2] varchar(50)  NULL,
    [ClientCompanyPIC3] varchar(50)  NULL,
    [ClientCompanyPICPhoneNumber3] varchar(50)  NULL,
    [ClientCompanyLeaderEmailAddress3] varchar(50)  NULL
);
GO

-- Creating table 'CONTRACT'
CREATE TABLE [dbo].[CONTRACT] (
    [ContractId] bigint IDENTITY(1,1) NOT NULL,
    [ContractNumber] varchar(50)  NULL,
    [ClientIdPK] int  NULL,
    [Value] decimal(18,0)  NULL,
    [Line] decimal(18,0)  NULL,
    [StartPeriode] datetime  NULL,
    [EffectiveDate] datetime  NULL,
    [EndPeriode] datetime  NULL,
    [MaxExpiredDR] int  NULL,
    [SiteName] varchar(50)  NULL,
    [QuantityBasedOn] varchar(50)  NULL,
    [IsusedPpn] smallint  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'ContractProduct'
CREATE TABLE [dbo].[ContractProduct] (
    [ContractId] bigint  NOT NULL,
    [ProductId] varchar(25)  NOT NULL,
    [Amount] float  NULL,
    [PricePerTon] decimal(18,3)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL,
    [DiscountPersen] decimal(5,3)  NULL,
    [MarketingFeePersen] decimal(5,3)  NULL,
    [DiscountIdr] decimal(18,3)  NULL
);
GO

-- Creating table 'DeliveryOrder'
CREATE TABLE [dbo].[DeliveryOrder] (
    [DeliveryOrderNumber] varchar(50)  NOT NULL,
    [DeliveryRequestId] bigint  NOT NULL,
    [Amount] float  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'DeliveryOrderInvoice'
CREATE TABLE [dbo].[DeliveryOrderInvoice] (
    [DeliveryOrderInvoiceNumber] varchar(50)  NOT NULL,
    [DeliveryOrderNumber] varchar(50)  NOT NULL,
    [Amount] decimal(18,2)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'DeliveryRequest'
CREATE TABLE [dbo].[DeliveryRequest] (
    [DeliveryRequestId] bigint IDENTITY(1,1) NOT NULL,
    [DeliveryRequestNumber] varchar(50)  NOT NULL,
    [ContractId] bigint  NULL,
    [DeliveryRequestTime] datetime  NULL,
    [TokenToLoad] nchar(10)  NULL,
    [IsActiveToken] nchar(10)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL,
    [Status] varchar(50)  NULL
);
GO

-- Creating table 'DeliveryRequestProductDetailTransaction'
CREATE TABLE [dbo].[DeliveryRequestProductDetailTransaction] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [DeliveryRequestId] bigint  NULL,
    [ProductId] varchar(25)  NULL,
    [Amount] float  NULL,
    [Status] varchar(25)  NULL,
    [DriverName] varchar(50)  NULL,
    [ArmadaPlatNumber] varchar(25)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'EMPLOYEE'
CREATE TABLE [dbo].[EMPLOYEE] (
    [EMPLOYEEID] varchar(25)  NOT NULL,
    [EMPLOYEENAME] varchar(50)  NULL,
    [EMPLOYEEAGE] int  NULL,
    [EMPLOYEEFULLNAME] varchar(50)  NULL,
    [EMPLOYEEJOINAT] datetime  NULL,
    [EMPLOYEESTATUS] varchar(50)  NULL,
    [EmployeeGroup] varchar(50)  NULL,
    [SiteName] varchar(50)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'FinanceBalance'
CREATE TABLE [dbo].[FinanceBalance] (
    [ContractId] bigint  NOT NULL,
    [ActualAmount] decimal(18,0)  NULL,
    [AvailableAmount] decimal(18,0)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'FinanceTransaction'
CREATE TABLE [dbo].[FinanceTransaction] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [TransactionNumber] varchar(50)  NULL,
    [ContractId] bigint  NULL,
    [Amount] decimal(18,2)  NULL,
    [TransactionCode] varchar(25)  NULL,
    [ValueDate] datetime  NULL,
    [DueDate] datetime  NULL,
    [Date] datetime  NULL,
    [ReferenceNumber] varchar(128)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'FinanceTransactionNostro'
CREATE TABLE [dbo].[FinanceTransactionNostro] (
    [CodeNostroBank] varchar(25)  NOT NULL,
    [FinanceTransactionId] bigint  NOT NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'Menu'
CREATE TABLE [dbo].[Menu] (
    [MenuId] int IDENTITY(1,1) NOT NULL,
    [MenuName] varchar(50)  NULL,
    [MenuParentId] int  NULL,
    [Sequence] int  NULL,
    [AspNetRoles] int  NULL,
    [MenuIClass] varchar(50)  NULL,
    [MenuLevel] int  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'NostroBank'
CREATE TABLE [dbo].[NostroBank] (
    [CodeNostroBank] varchar(25)  NOT NULL,
    [AccountType] nchar(10)  NULL,
    [NostroBankName] nchar(10)  NULL,
    [NostroBankDescription] nchar(10)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'ParameterSetup'
CREATE TABLE [dbo].[ParameterSetup] (
    [Name] varchar(50)  NOT NULL,
    [Value] varchar(512)  NULL,
    [DataType] varchar(20)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'PRODUCT'
CREATE TABLE [dbo].[PRODUCT] (
    [PRODUCTID] varchar(25)  NOT NULL,
    [PRODUCTNAME] varchar(50)  NULL,
    [PRODUCTDESCRIPTION] varchar(1024)  NULL,
    [PRODUCTIMAGEPATH] varchar(100)  NULL,
    [TypeMaterial] varchar(25)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'ProductAdjustment'
CREATE TABLE [dbo].[ProductAdjustment] (
    [AdjustmentId] bigint IDENTITY(1,1) NOT NULL,
    [AdjustmentNumber] varchar(50)  NOT NULL,
    [SiteName] varchar(50)  NULL,
    [ProductId] varchar(25)  NULL,
    [AdjustBy] varchar(50)  NULL,
    [Ammount] float  NULL,
    [AdjustmentTime] datetime  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'ProductConvertion'
CREATE TABLE [dbo].[ProductConvertion] (
    [ConvertionId] bigint IDENTITY(1,1) NOT NULL,
    [ConvertionNumber] varchar(50)  NOT NULL,
    [Site] varchar(50)  NULL,
    [BatchCode] varchar(50)  NULL,
    [SourceProudct] varchar(25)  NULL,
    [Ammount] float  NULL,
    [ConvertionTime] datetime  NULL,
    [ConvertBy] varchar(50)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'ProductMixing'
CREATE TABLE [dbo].[ProductMixing] (
    [ProductMixingId] bigint IDENTITY(1,1) NOT NULL,
    [ProductMixingNumber] varchar(50)  NOT NULL,
    [Site] varchar(50)  NULL,
    [BatchMixCode] varchar(50)  NULL,
    [OutcomeProudct] varchar(25)  NULL,
    [Ammount] float  NULL,
    [MixingTime] datetime  NULL,
    [MixedBy] varchar(50)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'ProductSite'
CREATE TABLE [dbo].[ProductSite] (
    [ProductId] varchar(25)  NOT NULL,
    [SiteName] varchar(50)  NOT NULL,
    [TotalStock] float  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'RITASE'
CREATE TABLE [dbo].[RITASE] (
    [RITASEID] int IDENTITY(1,1) NOT NULL,
    [RITASENUMBER] varchar(50)  NOT NULL,
    [SITE] varchar(50)  NULL,
    [PRODUCT] varchar(25)  NULL,
    [DRIVERARMADA] varchar(25)  NULL,
    [ARMADA] varchar(25)  NULL,
    [QuantityInTon] float  NULL,
    [RITASETIME] datetime  NULL,
    [RITASESTATUS] varchar(50)  NULL,
    [Remark] varchar(512)  NULL,
    [BatchCode] varchar(50)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL,
    [SOURCEID] int  NULL
);
GO

-- Creating table 'SITE'
CREATE TABLE [dbo].[SITE] (
    [SITENAME] varchar(50)  NOT NULL,
    [SITEADDRESS] varchar(100)  NULL,
    [Description] varchar(512)  NULL,
    [SourceId] int  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'SOURCE'
CREATE TABLE [dbo].[SOURCE] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [Address] varchar(256)  NULL,
    [Location] varchar(256)  NULL,
    [Description] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'StockProduct'
CREATE TABLE [dbo].[StockProduct] (
    [ProductId] varchar(25)  NOT NULL,
    [ProductStockInTon] decimal(18,0)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'TableSequence'
CREATE TABLE [dbo].[TableSequence] (
    [Id] varchar(50)  NOT NULL,
    [Year] int  NOT NULL,
    [LastSequenceNumber] bigint  NOT NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'TransactionCode'
CREATE TABLE [dbo].[TransactionCode] (
    [TransactionCode1] varchar(25)  NOT NULL,
    [TransactionName] varchar(25)  NULL,
    [CreditOrDebit] varchar(25)  NULL,
    [UserOrSystem] varchar(25)  NULL,
    [Debit] varchar(25)  NULL,
    [Credit] varchar(25)  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'TransactionProduct'
CREATE TABLE [dbo].[TransactionProduct] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [TransactionProductNumber] varchar(50)  NOT NULL,
    [ProductId] varchar(25)  NULL,
    [TypeDebitOrCredit] varchar(50)  NULL,
    [Jenis] varchar(50)  NULL,
    [Ammount] float  NULL,
    [RitaseId] int  NULL,
    [AdjustmentId] bigint  NULL,
    [ConvertionId] bigint  NULL,
    [MixingId] bigint  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- Creating table 'UserClientMapping'
CREATE TABLE [dbo].[UserClientMapping] (
    [UserName] varchar(50)  NOT NULL,
    [ClientId] int  NOT NULL,
    [ChangedPasswordCounter] int  NULL,
    [Remark] varchar(512)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [UpdatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [UpdatedDate] datetime  NULL,
    [IsActive] smallint  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ARMADAID] in table 'ARMADA'
ALTER TABLE [dbo].[ARMADA]
ADD CONSTRAINT [PK_ARMADA]
    PRIMARY KEY CLUSTERED ([ARMADAID] ASC);
GO

-- Creating primary key on [GroupName] in table 'AspNetGroups'
ALTER TABLE [dbo].[AspNetGroups]
ADD CONSTRAINT [PK_AspNetGroups]
    PRIMARY KEY CLUSTERED ([GroupName] ASC);
GO

-- Creating primary key on [Username], [GroupName] in table 'AspNetGroupUser'
ALTER TABLE [dbo].[AspNetGroupUser]
ADD CONSTRAINT [PK_AspNetGroupUser]
    PRIMARY KEY CLUSTERED ([Username], [GroupName] ASC);
GO

-- Creating primary key on [RolesId], [GroupName] in table 'AspNetRoleGroup'
ALTER TABLE [dbo].[AspNetRoleGroup]
ADD CONSTRAINT [PK_AspNetRoleGroup]
    PRIMARY KEY CLUSTERED ([RolesId], [GroupName] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Username] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [BatchCode] in table 'BATCH'
ALTER TABLE [dbo].[BATCH]
ADD CONSTRAINT [PK_BATCH]
    PRIMARY KEY CLUSTERED ([BatchCode] ASC);
GO

-- Creating primary key on [BatchMixCode] in table 'BatchMix'
ALTER TABLE [dbo].[BatchMix]
ADD CONSTRAINT [PK_BatchMix]
    PRIMARY KEY CLUSTERED ([BatchMixCode] ASC);
GO

-- Creating primary key on [BatchMixCode], [ProductSourceId] in table 'BatchMixProduct'
ALTER TABLE [dbo].[BatchMixProduct]
ADD CONSTRAINT [PK_BatchMixProduct]
    PRIMARY KEY CLUSTERED ([BatchMixCode], [ProductSourceId] ASC);
GO

-- Creating primary key on [BatchCode], [ProductId] in table 'BatchProduct'
ALTER TABLE [dbo].[BatchProduct]
ADD CONSTRAINT [PK_BatchProduct]
    PRIMARY KEY CLUSTERED ([BatchCode], [ProductId] ASC);
GO

-- Creating primary key on [Id] in table 'CLIENT'
ALTER TABLE [dbo].[CLIENT]
ADD CONSTRAINT [PK_CLIENT]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ContractId] in table 'CONTRACT'
ALTER TABLE [dbo].[CONTRACT]
ADD CONSTRAINT [PK_CONTRACT]
    PRIMARY KEY CLUSTERED ([ContractId] ASC);
GO

-- Creating primary key on [ContractId], [ProductId] in table 'ContractProduct'
ALTER TABLE [dbo].[ContractProduct]
ADD CONSTRAINT [PK_ContractProduct]
    PRIMARY KEY CLUSTERED ([ContractId], [ProductId] ASC);
GO

-- Creating primary key on [DeliveryOrderNumber], [DeliveryRequestId] in table 'DeliveryOrder'
ALTER TABLE [dbo].[DeliveryOrder]
ADD CONSTRAINT [PK_DeliveryOrder]
    PRIMARY KEY CLUSTERED ([DeliveryOrderNumber], [DeliveryRequestId] ASC);
GO

-- Creating primary key on [DeliveryOrderInvoiceNumber], [DeliveryOrderNumber] in table 'DeliveryOrderInvoice'
ALTER TABLE [dbo].[DeliveryOrderInvoice]
ADD CONSTRAINT [PK_DeliveryOrderInvoice]
    PRIMARY KEY CLUSTERED ([DeliveryOrderInvoiceNumber], [DeliveryOrderNumber] ASC);
GO

-- Creating primary key on [DeliveryRequestId] in table 'DeliveryRequest'
ALTER TABLE [dbo].[DeliveryRequest]
ADD CONSTRAINT [PK_DeliveryRequest]
    PRIMARY KEY CLUSTERED ([DeliveryRequestId] ASC);
GO

-- Creating primary key on [Id] in table 'DeliveryRequestProductDetailTransaction'
ALTER TABLE [dbo].[DeliveryRequestProductDetailTransaction]
ADD CONSTRAINT [PK_DeliveryRequestProductDetailTransaction]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [EMPLOYEEID] in table 'EMPLOYEE'
ALTER TABLE [dbo].[EMPLOYEE]
ADD CONSTRAINT [PK_EMPLOYEE]
    PRIMARY KEY CLUSTERED ([EMPLOYEEID] ASC);
GO

-- Creating primary key on [ContractId] in table 'FinanceBalance'
ALTER TABLE [dbo].[FinanceBalance]
ADD CONSTRAINT [PK_FinanceBalance]
    PRIMARY KEY CLUSTERED ([ContractId] ASC);
GO

-- Creating primary key on [Id] in table 'FinanceTransaction'
ALTER TABLE [dbo].[FinanceTransaction]
ADD CONSTRAINT [PK_FinanceTransaction]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [CodeNostroBank], [FinanceTransactionId] in table 'FinanceTransactionNostro'
ALTER TABLE [dbo].[FinanceTransactionNostro]
ADD CONSTRAINT [PK_FinanceTransactionNostro]
    PRIMARY KEY CLUSTERED ([CodeNostroBank], [FinanceTransactionId] ASC);
GO

-- Creating primary key on [MenuId] in table 'Menu'
ALTER TABLE [dbo].[Menu]
ADD CONSTRAINT [PK_Menu]
    PRIMARY KEY CLUSTERED ([MenuId] ASC);
GO

-- Creating primary key on [CodeNostroBank] in table 'NostroBank'
ALTER TABLE [dbo].[NostroBank]
ADD CONSTRAINT [PK_NostroBank]
    PRIMARY KEY CLUSTERED ([CodeNostroBank] ASC);
GO

-- Creating primary key on [Name] in table 'ParameterSetup'
ALTER TABLE [dbo].[ParameterSetup]
ADD CONSTRAINT [PK_ParameterSetup]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- Creating primary key on [PRODUCTID] in table 'PRODUCT'
ALTER TABLE [dbo].[PRODUCT]
ADD CONSTRAINT [PK_PRODUCT]
    PRIMARY KEY CLUSTERED ([PRODUCTID] ASC);
GO

-- Creating primary key on [AdjustmentId] in table 'ProductAdjustment'
ALTER TABLE [dbo].[ProductAdjustment]
ADD CONSTRAINT [PK_ProductAdjustment]
    PRIMARY KEY CLUSTERED ([AdjustmentId] ASC);
GO

-- Creating primary key on [ConvertionId] in table 'ProductConvertion'
ALTER TABLE [dbo].[ProductConvertion]
ADD CONSTRAINT [PK_ProductConvertion]
    PRIMARY KEY CLUSTERED ([ConvertionId] ASC);
GO

-- Creating primary key on [ProductMixingId] in table 'ProductMixing'
ALTER TABLE [dbo].[ProductMixing]
ADD CONSTRAINT [PK_ProductMixing]
    PRIMARY KEY CLUSTERED ([ProductMixingId] ASC);
GO

-- Creating primary key on [ProductId], [SiteName] in table 'ProductSite'
ALTER TABLE [dbo].[ProductSite]
ADD CONSTRAINT [PK_ProductSite]
    PRIMARY KEY CLUSTERED ([ProductId], [SiteName] ASC);
GO

-- Creating primary key on [RITASEID] in table 'RITASE'
ALTER TABLE [dbo].[RITASE]
ADD CONSTRAINT [PK_RITASE]
    PRIMARY KEY CLUSTERED ([RITASEID] ASC);
GO

-- Creating primary key on [SITENAME] in table 'SITE'
ALTER TABLE [dbo].[SITE]
ADD CONSTRAINT [PK_SITE]
    PRIMARY KEY CLUSTERED ([SITENAME] ASC);
GO

-- Creating primary key on [Id] in table 'SOURCE'
ALTER TABLE [dbo].[SOURCE]
ADD CONSTRAINT [PK_SOURCE]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ProductId] in table 'StockProduct'
ALTER TABLE [dbo].[StockProduct]
ADD CONSTRAINT [PK_StockProduct]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [Id], [Year] in table 'TableSequence'
ALTER TABLE [dbo].[TableSequence]
ADD CONSTRAINT [PK_TableSequence]
    PRIMARY KEY CLUSTERED ([Id], [Year] ASC);
GO

-- Creating primary key on [TransactionCode1] in table 'TransactionCode'
ALTER TABLE [dbo].[TransactionCode]
ADD CONSTRAINT [PK_TransactionCode]
    PRIMARY KEY CLUSTERED ([TransactionCode1] ASC);
GO

-- Creating primary key on [Id] in table 'TransactionProduct'
ALTER TABLE [dbo].[TransactionProduct]
ADD CONSTRAINT [PK_TransactionProduct]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserName], [ClientId] in table 'UserClientMapping'
ALTER TABLE [dbo].[UserClientMapping]
ADD CONSTRAINT [PK_UserClientMapping]
    PRIMARY KEY CLUSTERED ([UserName], [ClientId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SITENAME] in table 'ARMADA'
ALTER TABLE [dbo].[ARMADA]
ADD CONSTRAINT [FK_ARMADA_ToSite]
    FOREIGN KEY ([SITENAME])
    REFERENCES [dbo].[SITE]
        ([SITENAME])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ARMADA_ToSite'
CREATE INDEX [IX_FK_ARMADA_ToSite]
ON [dbo].[ARMADA]
    ([SITENAME]);
GO

-- Creating foreign key on [ARMADA] in table 'RITASE'
ALTER TABLE [dbo].[RITASE]
ADD CONSTRAINT [FK_RITASE_RELATIONS_ARMADA]
    FOREIGN KEY ([ARMADA])
    REFERENCES [dbo].[ARMADA]
        ([ARMADAID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RITASE_RELATIONS_ARMADA'
CREATE INDEX [IX_FK_RITASE_RELATIONS_ARMADA]
ON [dbo].[RITASE]
    ([ARMADA]);
GO

-- Creating foreign key on [GroupName] in table 'AspNetGroupUser'
ALTER TABLE [dbo].[AspNetGroupUser]
ADD CONSTRAINT [FK_AspNetGroupUser_ToGroup]
    FOREIGN KEY ([GroupName])
    REFERENCES [dbo].[AspNetGroups]
        ([GroupName])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetGroupUser_ToGroup'
CREATE INDEX [IX_FK_AspNetGroupUser_ToGroup]
ON [dbo].[AspNetGroupUser]
    ([GroupName]);
GO

-- Creating foreign key on [GroupName] in table 'AspNetRoleGroup'
ALTER TABLE [dbo].[AspNetRoleGroup]
ADD CONSTRAINT [FK_AspNetRoleGroup_ToGroup]
    FOREIGN KEY ([GroupName])
    REFERENCES [dbo].[AspNetGroups]
        ([GroupName])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetRoleGroup_ToGroup'
CREATE INDEX [IX_FK_AspNetRoleGroup_ToGroup]
ON [dbo].[AspNetRoleGroup]
    ([GroupName]);
GO

-- Creating foreign key on [Username] in table 'AspNetGroupUser'
ALTER TABLE [dbo].[AspNetGroupUser]
ADD CONSTRAINT [FK_AspNetGroupUser_ToUser]
    FOREIGN KEY ([Username])
    REFERENCES [dbo].[AspNetUsers]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RolesId] in table 'AspNetRoleGroup'
ALTER TABLE [dbo].[AspNetRoleGroup]
ADD CONSTRAINT [FK_AspNetRoleGroup_ToRoles]
    FOREIGN KEY ([RolesId])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ParentId] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [FK_AspNetRoles_ToAspNetRoles]
    FOREIGN KEY ([ParentId])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetRoles_ToAspNetRoles'
CREATE INDEX [IX_FK_AspNetRoles_ToAspNetRoles]
ON [dbo].[AspNetRoles]
    ([ParentId]);
GO

-- Creating foreign key on [AspNetRoles] in table 'Menu'
ALTER TABLE [dbo].[Menu]
ADD CONSTRAINT [FK_Menu_ToMenuAspNetRoles]
    FOREIGN KEY ([AspNetRoles])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Menu_ToMenuAspNetRoles'
CREATE INDEX [IX_FK_Menu_ToMenuAspNetRoles]
ON [dbo].[Menu]
    ([AspNetRoles]);
GO

-- Creating foreign key on [UserName] in table 'UserClientMapping'
ALTER TABLE [dbo].[UserClientMapping]
ADD CONSTRAINT [FK_UserClientMapping_ToAspNetUsers]
    FOREIGN KEY ([UserName])
    REFERENCES [dbo].[AspNetUsers]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RawProduct] in table 'BATCH'
ALTER TABLE [dbo].[BATCH]
ADD CONSTRAINT [FK_Batch_ToProduct]
    FOREIGN KEY ([RawProduct])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Batch_ToProduct'
CREATE INDEX [IX_FK_Batch_ToProduct]
ON [dbo].[BATCH]
    ([RawProduct]);
GO

-- Creating foreign key on [BatchCode] in table 'BatchProduct'
ALTER TABLE [dbo].[BatchProduct]
ADD CONSTRAINT [FK_BatchProduct_ToBatch]
    FOREIGN KEY ([BatchCode])
    REFERENCES [dbo].[BATCH]
        ([BatchCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BatchCode] in table 'ProductConvertion'
ALTER TABLE [dbo].[ProductConvertion]
ADD CONSTRAINT [FK_ProductConvertion_BATCH]
    FOREIGN KEY ([BatchCode])
    REFERENCES [dbo].[BATCH]
        ([BatchCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductConvertion_BATCH'
CREATE INDEX [IX_FK_ProductConvertion_BATCH]
ON [dbo].[ProductConvertion]
    ([BatchCode]);
GO

-- Creating foreign key on [BatchCode] in table 'RITASE'
ALTER TABLE [dbo].[RITASE]
ADD CONSTRAINT [FK_RITASE_RELATIONS_BATCH]
    FOREIGN KEY ([BatchCode])
    REFERENCES [dbo].[BATCH]
        ([BatchCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RITASE_RELATIONS_BATCH'
CREATE INDEX [IX_FK_RITASE_RELATIONS_BATCH]
ON [dbo].[RITASE]
    ([BatchCode]);
GO

-- Creating foreign key on [OutputProduct] in table 'BatchMix'
ALTER TABLE [dbo].[BatchMix]
ADD CONSTRAINT [FK_BatchMix_ToProduct]
    FOREIGN KEY ([OutputProduct])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BatchMix_ToProduct'
CREATE INDEX [IX_FK_BatchMix_ToProduct]
ON [dbo].[BatchMix]
    ([OutputProduct]);
GO

-- Creating foreign key on [BatchMixCode] in table 'BatchMixProduct'
ALTER TABLE [dbo].[BatchMixProduct]
ADD CONSTRAINT [FK_BatchMixProduct_ToBatchMix]
    FOREIGN KEY ([BatchMixCode])
    REFERENCES [dbo].[BatchMix]
        ([BatchMixCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BatchMixCode] in table 'ProductMixing'
ALTER TABLE [dbo].[ProductMixing]
ADD CONSTRAINT [FK_ProductMixing_BatchMix]
    FOREIGN KEY ([BatchMixCode])
    REFERENCES [dbo].[BatchMix]
        ([BatchMixCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductMixing_BatchMix'
CREATE INDEX [IX_FK_ProductMixing_BatchMix]
ON [dbo].[ProductMixing]
    ([BatchMixCode]);
GO

-- Creating foreign key on [ProductSourceId] in table 'BatchMixProduct'
ALTER TABLE [dbo].[BatchMixProduct]
ADD CONSTRAINT [FK_BatchMixProduct_ToProduct]
    FOREIGN KEY ([ProductSourceId])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BatchMixProduct_ToProduct'
CREATE INDEX [IX_FK_BatchMixProduct_ToProduct]
ON [dbo].[BatchMixProduct]
    ([ProductSourceId]);
GO

-- Creating foreign key on [ProductId] in table 'BatchProduct'
ALTER TABLE [dbo].[BatchProduct]
ADD CONSTRAINT [FK_BatchProduct_ToProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BatchProduct_ToProduct'
CREATE INDEX [IX_FK_BatchProduct_ToProduct]
ON [dbo].[BatchProduct]
    ([ProductId]);
GO

-- Creating foreign key on [ClientIdPK] in table 'CONTRACT'
ALTER TABLE [dbo].[CONTRACT]
ADD CONSTRAINT [FK_CONTRACT_ToClient]
    FOREIGN KEY ([ClientIdPK])
    REFERENCES [dbo].[CLIENT]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CONTRACT_ToClient'
CREATE INDEX [IX_FK_CONTRACT_ToClient]
ON [dbo].[CONTRACT]
    ([ClientIdPK]);
GO

-- Creating foreign key on [ClientId] in table 'UserClientMapping'
ALTER TABLE [dbo].[UserClientMapping]
ADD CONSTRAINT [FK_UserClientMapping_ToClient]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[CLIENT]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserClientMapping_ToClient'
CREATE INDEX [IX_FK_UserClientMapping_ToClient]
ON [dbo].[UserClientMapping]
    ([ClientId]);
GO

-- Creating foreign key on [SiteName] in table 'CONTRACT'
ALTER TABLE [dbo].[CONTRACT]
ADD CONSTRAINT [FK_CONTRACT_ToSite]
    FOREIGN KEY ([SiteName])
    REFERENCES [dbo].[SITE]
        ([SITENAME])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CONTRACT_ToSite'
CREATE INDEX [IX_FK_CONTRACT_ToSite]
ON [dbo].[CONTRACT]
    ([SiteName]);
GO

-- Creating foreign key on [ContractId] in table 'ContractProduct'
ALTER TABLE [dbo].[ContractProduct]
ADD CONSTRAINT [FK_ContractProduct_ToContract]
    FOREIGN KEY ([ContractId])
    REFERENCES [dbo].[CONTRACT]
        ([ContractId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ContractId] in table 'DeliveryRequest'
ALTER TABLE [dbo].[DeliveryRequest]
ADD CONSTRAINT [FK_DeliveryRequest_ToContract]
    FOREIGN KEY ([ContractId])
    REFERENCES [dbo].[CONTRACT]
        ([ContractId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryRequest_ToContract'
CREATE INDEX [IX_FK_DeliveryRequest_ToContract]
ON [dbo].[DeliveryRequest]
    ([ContractId]);
GO

-- Creating foreign key on [ContractId] in table 'FinanceBalance'
ALTER TABLE [dbo].[FinanceBalance]
ADD CONSTRAINT [FK_FinanceBalance_ToContract]
    FOREIGN KEY ([ContractId])
    REFERENCES [dbo].[CONTRACT]
        ([ContractId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ContractId] in table 'FinanceTransaction'
ALTER TABLE [dbo].[FinanceTransaction]
ADD CONSTRAINT [FK_FinanceTransaction_ToContract]
    FOREIGN KEY ([ContractId])
    REFERENCES [dbo].[CONTRACT]
        ([ContractId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FinanceTransaction_ToContract'
CREATE INDEX [IX_FK_FinanceTransaction_ToContract]
ON [dbo].[FinanceTransaction]
    ([ContractId]);
GO

-- Creating foreign key on [ProductId] in table 'ContractProduct'
ALTER TABLE [dbo].[ContractProduct]
ADD CONSTRAINT [FK_ContractProduct_ToProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractProduct_ToProduct'
CREATE INDEX [IX_FK_ContractProduct_ToProduct]
ON [dbo].[ContractProduct]
    ([ProductId]);
GO

-- Creating foreign key on [DeliveryRequestId] in table 'DeliveryOrder'
ALTER TABLE [dbo].[DeliveryOrder]
ADD CONSTRAINT [FK_DeliveryOrder_ToDeliveryRequest]
    FOREIGN KEY ([DeliveryRequestId])
    REFERENCES [dbo].[DeliveryRequest]
        ([DeliveryRequestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryOrder_ToDeliveryRequest'
CREATE INDEX [IX_FK_DeliveryOrder_ToDeliveryRequest]
ON [dbo].[DeliveryOrder]
    ([DeliveryRequestId]);
GO

-- Creating foreign key on [DeliveryRequestId] in table 'DeliveryRequestProductDetailTransaction'
ALTER TABLE [dbo].[DeliveryRequestProductDetailTransaction]
ADD CONSTRAINT [FK_DeliveryRequestProductDetailTransaction_ToDeliveryRequest]
    FOREIGN KEY ([DeliveryRequestId])
    REFERENCES [dbo].[DeliveryRequest]
        ([DeliveryRequestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryRequestProductDetailTransaction_ToDeliveryRequest'
CREATE INDEX [IX_FK_DeliveryRequestProductDetailTransaction_ToDeliveryRequest]
ON [dbo].[DeliveryRequestProductDetailTransaction]
    ([DeliveryRequestId]);
GO

-- Creating foreign key on [ProductId] in table 'DeliveryRequestProductDetailTransaction'
ALTER TABLE [dbo].[DeliveryRequestProductDetailTransaction]
ADD CONSTRAINT [FK_DeliveryRequestProductDetailTransaction_ToProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryRequestProductDetailTransaction_ToProduct'
CREATE INDEX [IX_FK_DeliveryRequestProductDetailTransaction_ToProduct]
ON [dbo].[DeliveryRequestProductDetailTransaction]
    ([ProductId]);
GO

-- Creating foreign key on [SiteName] in table 'EMPLOYEE'
ALTER TABLE [dbo].[EMPLOYEE]
ADD CONSTRAINT [FK_EMPLOYEE_ToSite]
    FOREIGN KEY ([SiteName])
    REFERENCES [dbo].[SITE]
        ([SITENAME])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EMPLOYEE_ToSite'
CREATE INDEX [IX_FK_EMPLOYEE_ToSite]
ON [dbo].[EMPLOYEE]
    ([SiteName]);
GO

-- Creating foreign key on [DRIVERARMADA] in table 'RITASE'
ALTER TABLE [dbo].[RITASE]
ADD CONSTRAINT [FK_RITASE_RELATIONS_EMPLOYEE]
    FOREIGN KEY ([DRIVERARMADA])
    REFERENCES [dbo].[EMPLOYEE]
        ([EMPLOYEEID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RITASE_RELATIONS_EMPLOYEE'
CREATE INDEX [IX_FK_RITASE_RELATIONS_EMPLOYEE]
ON [dbo].[RITASE]
    ([DRIVERARMADA]);
GO

-- Creating foreign key on [TransactionCode] in table 'FinanceTransaction'
ALTER TABLE [dbo].[FinanceTransaction]
ADD CONSTRAINT [FK_FinanceTransaction_ToTransactionCode]
    FOREIGN KEY ([TransactionCode])
    REFERENCES [dbo].[TransactionCode]
        ([TransactionCode1])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FinanceTransaction_ToTransactionCode'
CREATE INDEX [IX_FK_FinanceTransaction_ToTransactionCode]
ON [dbo].[FinanceTransaction]
    ([TransactionCode]);
GO

-- Creating foreign key on [FinanceTransactionId] in table 'FinanceTransactionNostro'
ALTER TABLE [dbo].[FinanceTransactionNostro]
ADD CONSTRAINT [FK_FinanceTransactionNostro_ToFinanceTransaction]
    FOREIGN KEY ([FinanceTransactionId])
    REFERENCES [dbo].[FinanceTransaction]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FinanceTransactionNostro_ToFinanceTransaction'
CREATE INDEX [IX_FK_FinanceTransactionNostro_ToFinanceTransaction]
ON [dbo].[FinanceTransactionNostro]
    ([FinanceTransactionId]);
GO

-- Creating foreign key on [CodeNostroBank] in table 'FinanceTransactionNostro'
ALTER TABLE [dbo].[FinanceTransactionNostro]
ADD CONSTRAINT [FK_FinanceTransactionNostro_ToNostro]
    FOREIGN KEY ([CodeNostroBank])
    REFERENCES [dbo].[NostroBank]
        ([CodeNostroBank])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MenuParentId] in table 'Menu'
ALTER TABLE [dbo].[Menu]
ADD CONSTRAINT [FK_Menu_ToMenuParent]
    FOREIGN KEY ([MenuParentId])
    REFERENCES [dbo].[Menu]
        ([MenuId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Menu_ToMenuParent'
CREATE INDEX [IX_FK_Menu_ToMenuParent]
ON [dbo].[Menu]
    ([MenuParentId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductAdjustment'
ALTER TABLE [dbo].[ProductAdjustment]
ADD CONSTRAINT [FK_ProductAdjustment_ToProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAdjustment_ToProduct'
CREATE INDEX [IX_FK_ProductAdjustment_ToProduct]
ON [dbo].[ProductAdjustment]
    ([ProductId]);
GO

-- Creating foreign key on [SourceProudct] in table 'ProductConvertion'
ALTER TABLE [dbo].[ProductConvertion]
ADD CONSTRAINT [FK_ProductConvertion_PRODUCT]
    FOREIGN KEY ([SourceProudct])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductConvertion_PRODUCT'
CREATE INDEX [IX_FK_ProductConvertion_PRODUCT]
ON [dbo].[ProductConvertion]
    ([SourceProudct]);
GO

-- Creating foreign key on [OutcomeProudct] in table 'ProductMixing'
ALTER TABLE [dbo].[ProductMixing]
ADD CONSTRAINT [FK_ProductMixing_PRODUCT]
    FOREIGN KEY ([OutcomeProudct])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductMixing_PRODUCT'
CREATE INDEX [IX_FK_ProductMixing_PRODUCT]
ON [dbo].[ProductMixing]
    ([OutcomeProudct]);
GO

-- Creating foreign key on [ProductId] in table 'ProductSite'
ALTER TABLE [dbo].[ProductSite]
ADD CONSTRAINT [FK_ProductSite_ToProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PRODUCT] in table 'RITASE'
ALTER TABLE [dbo].[RITASE]
ADD CONSTRAINT [FK_RITASE_RELATIONS_PRODUCT]
    FOREIGN KEY ([PRODUCT])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RITASE_RELATIONS_PRODUCT'
CREATE INDEX [IX_FK_RITASE_RELATIONS_PRODUCT]
ON [dbo].[RITASE]
    ([PRODUCT]);
GO

-- Creating foreign key on [ProductId] in table 'StockProduct'
ALTER TABLE [dbo].[StockProduct]
ADD CONSTRAINT [FK_StockProduct_ToProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'TransactionProduct'
ALTER TABLE [dbo].[TransactionProduct]
ADD CONSTRAINT [FK_TransactionProduct_ToProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[PRODUCT]
        ([PRODUCTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionProduct_ToProduct'
CREATE INDEX [IX_FK_TransactionProduct_ToProduct]
ON [dbo].[TransactionProduct]
    ([ProductId]);
GO

-- Creating foreign key on [SiteName] in table 'ProductAdjustment'
ALTER TABLE [dbo].[ProductAdjustment]
ADD CONSTRAINT [FK_ProductAdjustment_ToSite]
    FOREIGN KEY ([SiteName])
    REFERENCES [dbo].[SITE]
        ([SITENAME])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAdjustment_ToSite'
CREATE INDEX [IX_FK_ProductAdjustment_ToSite]
ON [dbo].[ProductAdjustment]
    ([SiteName]);
GO

-- Creating foreign key on [AdjustmentId] in table 'TransactionProduct'
ALTER TABLE [dbo].[TransactionProduct]
ADD CONSTRAINT [FK_TransactionProduct_ToProductAdjustment]
    FOREIGN KEY ([AdjustmentId])
    REFERENCES [dbo].[ProductAdjustment]
        ([AdjustmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionProduct_ToProductAdjustment'
CREATE INDEX [IX_FK_TransactionProduct_ToProductAdjustment]
ON [dbo].[TransactionProduct]
    ([AdjustmentId]);
GO

-- Creating foreign key on [Site] in table 'ProductConvertion'
ALTER TABLE [dbo].[ProductConvertion]
ADD CONSTRAINT [FK_ProductConvertion_SITE]
    FOREIGN KEY ([Site])
    REFERENCES [dbo].[SITE]
        ([SITENAME])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductConvertion_SITE'
CREATE INDEX [IX_FK_ProductConvertion_SITE]
ON [dbo].[ProductConvertion]
    ([Site]);
GO

-- Creating foreign key on [ConvertionId] in table 'TransactionProduct'
ALTER TABLE [dbo].[TransactionProduct]
ADD CONSTRAINT [FK_TransactionProduct_ToProductProductConvertion]
    FOREIGN KEY ([ConvertionId])
    REFERENCES [dbo].[ProductConvertion]
        ([ConvertionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionProduct_ToProductProductConvertion'
CREATE INDEX [IX_FK_TransactionProduct_ToProductProductConvertion]
ON [dbo].[TransactionProduct]
    ([ConvertionId]);
GO

-- Creating foreign key on [Site] in table 'ProductMixing'
ALTER TABLE [dbo].[ProductMixing]
ADD CONSTRAINT [FK_ProductMixing_SITE]
    FOREIGN KEY ([Site])
    REFERENCES [dbo].[SITE]
        ([SITENAME])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductMixing_SITE'
CREATE INDEX [IX_FK_ProductMixing_SITE]
ON [dbo].[ProductMixing]
    ([Site]);
GO

-- Creating foreign key on [MixingId] in table 'TransactionProduct'
ALTER TABLE [dbo].[TransactionProduct]
ADD CONSTRAINT [FK_TransactionProduct_ToProductMixing]
    FOREIGN KEY ([MixingId])
    REFERENCES [dbo].[ProductMixing]
        ([ProductMixingId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionProduct_ToProductMixing'
CREATE INDEX [IX_FK_TransactionProduct_ToProductMixing]
ON [dbo].[TransactionProduct]
    ([MixingId]);
GO

-- Creating foreign key on [SiteName] in table 'ProductSite'
ALTER TABLE [dbo].[ProductSite]
ADD CONSTRAINT [FK_ProductSite_ToSite]
    FOREIGN KEY ([SiteName])
    REFERENCES [dbo].[SITE]
        ([SITENAME])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductSite_ToSite'
CREATE INDEX [IX_FK_ProductSite_ToSite]
ON [dbo].[ProductSite]
    ([SiteName]);
GO

-- Creating foreign key on [SITE] in table 'RITASE'
ALTER TABLE [dbo].[RITASE]
ADD CONSTRAINT [FK_RITASE_RELATIONS_SITE]
    FOREIGN KEY ([SITE])
    REFERENCES [dbo].[SITE]
        ([SITENAME])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RITASE_RELATIONS_SITE'
CREATE INDEX [IX_FK_RITASE_RELATIONS_SITE]
ON [dbo].[RITASE]
    ([SITE]);
GO

-- Creating foreign key on [SOURCEID] in table 'RITASE'
ALTER TABLE [dbo].[RITASE]
ADD CONSTRAINT [FK_RITASE_RELATIONS_SOURCE]
    FOREIGN KEY ([SOURCEID])
    REFERENCES [dbo].[SOURCE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RITASE_RELATIONS_SOURCE'
CREATE INDEX [IX_FK_RITASE_RELATIONS_SOURCE]
ON [dbo].[RITASE]
    ([SOURCEID]);
GO

-- Creating foreign key on [RitaseId] in table 'TransactionProduct'
ALTER TABLE [dbo].[TransactionProduct]
ADD CONSTRAINT [FK_TransactionProduct_ToRitase]
    FOREIGN KEY ([RitaseId])
    REFERENCES [dbo].[RITASE]
        ([RITASEID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionProduct_ToRitase'
CREATE INDEX [IX_FK_TransactionProduct_ToRitase]
ON [dbo].[TransactionProduct]
    ([RitaseId]);
GO

-- Creating foreign key on [SourceId] in table 'SITE'
ALTER TABLE [dbo].[SITE]
ADD CONSTRAINT [FK_SITE_ToSource]
    FOREIGN KEY ([SourceId])
    REFERENCES [dbo].[SOURCE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SITE_ToSource'
CREATE INDEX [IX_FK_SITE_ToSource]
ON [dbo].[SITE]
    ([SourceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------