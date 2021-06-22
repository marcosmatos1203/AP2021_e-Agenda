CREATE TABLE [dbo].[TBCONTATO] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Nome]     VARCHAR (100) NULL,
    [Email]    VARCHAR (100) NULL,
    [Telefone] VARCHAR (20)  NULL,
    [Cargo]    VARCHAR (100) NULL,
    [Empresa]  VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

