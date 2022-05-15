CREATE TABLE [dbo].[Utilizatori] (
    [Id_Utilizator] INT           NOT NULL,
    [Username]      VARCHAR (50)  NOT NULL,
    [Parola]        NVARCHAR (50) NOT NULL,
    [Tip de cont]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Utilizator] ASC)
);

