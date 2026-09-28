CREATE TABLE [dbo].[ScheduleExecutionLogs]
(
    [ExecutionId] BIGINT IDENTITY(1,1) NOT NULL,
    [ScheduleId] INT NOT NULL,
    [ScheduledAt] DATETIME2(7) NOT NULL,
    [StartedAt] DATETIME2(7) NULL,
    [CompletedAt] DATETIME2(7) NULL,
    [Status] NVARCHAR(20) NOT NULL,
    [CreatedOn] DATETIME2(7) NOT NULL DEFAULT (GETDATE()),

    CONSTRAINT [PK_ScheduleExecutionLogs] PRIMARY KEY ([ExecutionId]),
    CONSTRAINT [FK_ScheduleExecutionLogs_Schedules] FOREIGN KEY ([ScheduleId]) REFERENCES [dbo].[Schedules] ([ScheduleId])
);