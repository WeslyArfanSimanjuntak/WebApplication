CREATE TABLE [dbo].[ProductMixing] (
    [ProductMixingId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [ProductMixingNumber] VARCHAR (50)  NOT NULL,
    [Site]             VARCHAR (50)  NULL,
    [BatchMixCode]        VARCHAR (50)  NULL,
    [OutcomeProudct]    VARCHAR (25)  NULL,
    [Ammount]          FLOAT (53)    NULL,
    [MixingTime]   DATETIME2 (7) NULL,
    [MixedBy]        VARCHAR (50)  NULL,
    [Remark]           VARCHAR (512) NULL,
    [CreatedBy]        VARCHAR (50)  NULL,
    [UpdatedBy]        VARCHAR (50)  NULL,
    [CreatedDate]      DATETIME      NULL,
    [UpdatedDate]      DATETIME      NULL,
    [IsActive]         SMALLINT      NULL,
    PRIMARY KEY CLUSTERED ([ProductMixingId] ASC),
    UNIQUE NONCLUSTERED ([ProductMixingNumber] ASC),
    CONSTRAINT [FK_ProductMixing_SITE] FOREIGN KEY ([Site]) REFERENCES [dbo].[SITE] ([SITENAME]),
    CONSTRAINT [FK_ProductMixing_PRODUCT] FOREIGN KEY ([OutcomeProudct]) REFERENCES [dbo].[PRODUCT] ([PRODUCTID]),
    CONSTRAINT [FK_ProductMixing_BatchMix] FOREIGN KEY ([BatchMixCode]) REFERENCES [dbo].[BatchMix] ([BatchMixCode])
);

