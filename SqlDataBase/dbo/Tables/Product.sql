CREATE TABLE [dbo].[Product] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Title]            NVARCHAR (MAX) NOT NULL,
    [Description]      NVARCHAR (MAX) NULL,
    [Price]            FLOAT (53)     NOT NULL,
    [Count]            INT            NOT NULL,
    [CreationDate]     DATETIME       NOT NULL,
    [ModificationDate] DATETIME       NOT NULL,
    [CategoryId]       INT            NOT NULL,
    [ImageId]          INT            NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Product_Product] FOREIGN KEY ([ImageId]) REFERENCES [dbo].[Image] ([Id]) ON DELETE CASCADE
);

