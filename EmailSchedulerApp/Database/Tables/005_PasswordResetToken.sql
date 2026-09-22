CREATE TABLE [dbo].[PasswordResetToken]
(
    [Id] BIGINT IDENTITY(1,1) NOT NULL,
    [UserId] INT NOT NULL,
    [TokenHash] NVARCHAR(256) NOT NULL,
    [ExpiresAt] DATETIME2(7) NOT NULL,
    [IsUsed] BIT NOT NULL DEFAULT 0,
    [CreatedAt] DATETIME2(7) NOT NULL DEFAULT (GETDATE()),

    CONSTRAINT [PK_PasswordResetToken]
        PRIMARY KEY ([Id]),

    CONSTRAINT [FK_PasswordResetToken_Users]
        FOREIGN KEY ([UserId])
        REFERENCES [dbo].[Users]([UserId])
);

CREATE INDEX [IX_PasswordResetToken_TokenHash]
ON [dbo].[PasswordResetToken] ([TokenHash]);