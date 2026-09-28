CREATE TABLE [dbo].[EmailExecutionLogs]
(
    [EmailExecutionId] BIGINT IDENTITY(1,1) NOT NULL,
    [ExecutionId] BIGINT NOT NULL,
    [RecipientId] INT NOT NULL,
    [Status] NVARCHAR(20) NOT NULL,
    [SentAt] DATETIME2(7) NULL,
    [ErrorMessage] NVARCHAR(MAX) NULL,
    [CreatedOn] DATETIME2(7) NOT NULL DEFAULT (GETDATE()),

    CONSTRAINT [PK_EmailExecutionLogs] PRIMARY KEY ([EmailExecutionId]),
    CONSTRAINT [FK_EmailExecutionLogs_ScheduleExecutionLogs] FOREIGN KEY ([ExecutionId]) REFERENCES [dbo].[ScheduleExecutionLogs] ([ExecutionId]),
    CONSTRAINT [FK_EmailExecutionLogs_Recipients] FOREIGN KEY ([RecipientId]) REFERENCES [dbo].[Recipients] ([RecipientId])
);