CREATE TABLE [dbo].[DevJoke] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Author]   NVARCHAR (MAX) NULL,
    [Text]     NVARCHAR (MAX) NULL,
    [ImageUrl] NVARCHAR (MAX) NULL,
    [Tags]      NVARCHAR (MAX) NULL,
    [LikeCount] INT CONSTRAINT [DF_DevJoke_LikeCount] DEFAULT ((0)) NOT NULL,
    [CategoryId] INT NOT NULL,
	CONSTRAINT [fk_category] foreign key ([CategoryId]) references Category(Id),
	CONSTRAINT [PK_DevJoke2] PRIMARY KEY CLUSTERED ([Id] ASC)
);



