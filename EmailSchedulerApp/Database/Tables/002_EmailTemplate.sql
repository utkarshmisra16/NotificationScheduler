CREATE TABLE [dbo].[EmailTemplate] (
    [TemplateId] int IDENTITY(1,1) NOT NULL,
    [TemplateName] nvarchar(100) NOT NULL,
    [Subject] nvarchar(500) NOT NULL,
    [Body] nvarchar(MAX) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT ((1)),
    [CreatedOn] datetime2(7) NOT NULL DEFAULT (getdate()),
    [UpdatedOn] datetime2(7) NULL,
    [CreatedBy] int NULL,
    CONSTRAINT [PK_EmailTemplate] PRIMARY KEY ([TemplateId])
);