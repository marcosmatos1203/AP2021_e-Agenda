CREATE TABLE [dbo].[TBTAREFA] (
    [Id]                  INT      IDENTITY (1, 1) NOT NULL,
    [Prioridade]          INT      NULL,
    [PercentualConcluido] INT      NULL,
    [DataCriacao]         DATETIME NULL,
    [DataConclusao]       DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

