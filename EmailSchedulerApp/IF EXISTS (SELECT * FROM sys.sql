IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Schedules]') AND type in (N'U'))
DROP TABLE [dbo].[Schedules]
GO

ALTER TABLE Schedules
ADD
    Name NVARCHAR(100) NOT NULL,
    Channel NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500) NULL,
    StartTime TIME NOT NULL,
    Timezone NVARCHAR(50) NOT NULL,
    Frequency INT NOT NULL,
    WeekDays NVARCHAR(100) NULL,
    CronExpression NVARCHAR(200) NULL,
    Tags NVARCHAR(500) NULL,
    Priority INT NOT NULL,
    UpdatedOn DATETIME2 NULL;

    ALTER TABLE Schedules Drop COLUMN UserId;
    ALTER TABLE Schedules Drop COLUMN Title;
    ALTER TABLE Schedules Drop COLUMN RecurrenceType;
    ALTER TABLE Schedules Drop COLUMN DaysOfWeek;
    ALTER TABLE Schedules Drop COLUMN TimeOfDay;
    ALTER TABLE Schedules Drop COLUMN DaysOfWeek;
    ALTER TABLE Schedules Drop COLUMN NextRunAtUtc;
    ALTER TABLE Schedules Drop COLUMN LastRunAtUtc;
    ALTER TABLE Schedules ADD CreatedBy NVARCHAR(100) NOT NULL;

INSERT INTO [dbo].[Templates] (UserId, TemplateName, Subject, Body, IsActive, CreatedOn)
VALUES 
(NULL, 'Welcome Email', 'Welcome to MyGSTcafe!', '<h1>Welcome!</h1><p>Thank you for joining us.</p>', 1, GETDATE()),
(NULL, 'Reminder Email', 'Reminder: Action Required', '<h1>Reminder</h1><p>Please complete your pending action.</p>', 1, GETDATE()),
(NULL, 'Newsletter', 'Monthly Newsletter - June 2026', '<h1>Newsletter</h1><p>Here are this month updates.</p>', 1, GETDATE());

-- Verify karo
SELECT * FROM [dbo].[Templates];