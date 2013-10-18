CREATE TABLE [dbo].[Category] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Title]            NVARCHAR (MAX) NOT NULL,
    [Description]      NVARCHAR (MAX) NULL,
    [CreationDate]     DATETIME       NOT NULL,
    [ModificationDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC)
);

