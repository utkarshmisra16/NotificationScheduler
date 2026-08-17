CREATE TABLE [dbo].[Recipients] (
    [RecipientId] int IDENTITY(1,1) NOT NULL,
    [ScheduleId] int NOT NULL,
    [Name] nvarchar(200) NULL,
    [Email] nvarchar(255) NOT NULL,
    [Source] nvarchar(50) NULL,
    [AddedAt] datetime2(7) NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_Recipients] PRIMARY KEY ([RecipientId])
);