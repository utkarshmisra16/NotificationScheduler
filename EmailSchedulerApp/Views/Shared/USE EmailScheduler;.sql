USE EmailScheduler;
GO

-- CREATE TABLE Users (
--     UserId INT IDENTITY(1,1) PRIMARY KEY,
--     Username NVARCHAR(100) NOT NULL,
--     PasswordHash NVARCHAR(200) NOT NULL,
--     FullName NVARCHAR(200),
--     RoleName NVARCHAR(50),
--     IsActive BIT DEFAULT 1,
--     CreatedOn DATETIME DEFAULT GETDATE()
-- );

-- CREATE TABLE EmailSchedule (
--     ScheduleId INT IDENTITY(1,1) PRIMARY KEY,
--     Subject NVARCHAR(500),
--     Body NVARCHAR(MAX),
--     ToEmails NVARCHAR(MAX),
--     CCEmails NVARCHAR(MAX),
--     BCCEmails NVARCHAR(MAX),
--     ScheduledTime DATETIME,
--     IsSent BIT DEFAULT 0,
--     CreatedOn DATETIME DEFAULT GETDATE()
-- );

-- CREATE TABLE EmailLog (
--     LogId INT IDENTITY(1,1) PRIMARY KEY,
--     ScheduleId INT,
--     Status NVARCHAR(50),
--     ErrorMessage NVARCHAR(MAX),
--     SentOn DATETIME DEFAULT GETDATE()
-- );

INSERT INTO Users (Username, PasswordHash, FullName, RoleName, IsActive)
VALUES ('admin', '1234', 'Admin User', 'Admin', 1);

DROP Table EmailSchedule;

CREATE TABLE EmailSchedule
(
    ScheduleId INT IDENTITY(1,1) PRIMARY KEY,
    Subject NVARCHAR(500) NOT NULL,
    Body NVARCHAR(MAX) NOT NULL,
    ScheduledTime DATETIME NOT NULL,
    Status INT DEFAULT 0,
    RetryCount INT DEFAULT 0,
    CreatedBy INT NULL,
    CreatedOn DATETIME DEFAULT GETDATE(),
    UpdatedOn DATETIME NULL
);

-- CREATE TABLE EmailRecipient
-- (
--     RecipientId INT IDENTITY(1,1) PRIMARY KEY,
--     ScheduleId INT NOT NULL,
--     EmailAddress NVARCHAR(255) NOT NULL,
--     RecipientType INT NOT NULL,
--     CONSTRAINT FK_EmailRecipient_EmailSchedule
--     FOREIGN KEY (ScheduleId)
--     REFERENCES EmailSchedule(ScheduleId)
--     ON DELETE CASCADE
-- );

-- CREATE TABLE EmailLog
-- (
--     LogId INT IDENTITY(1,1) PRIMARY KEY,
--     ScheduleId INT NOT NULL,
--     Status INT NOT NULL,
--     ErrorMessage NVARCHAR(MAX),
--     AttemptCount INT DEFAULT 0,
--     SentOn DATETIME NULL,
--     CreatedOn DATETIME DEFAULT GETDATE(),
--     CONSTRAINT FK_EmailLog_EmailSchedule
--     FOREIGN KEY (ScheduleId)
--     REFERENCES EmailSchedule(ScheduleId)
-- );

SELECT * FROM EmailSchedule;
SELECT name FROM sys.databases;
GO

ALTER TABLE EmailSchedule
DROP COLUMN Recipients;

DROP Table EmailLog;

CREATE TABLE EmailRecipients
(
    RecipientId INT IDENTITY(1,1) PRIMARY KEY,

    ScheduleId INT NOT NULL,

    RecipientName NVARCHAR(200) NULL,

    RecipientEmail NVARCHAR(300) NOT NULL,

    Status INT DEFAULT 0,

    RetryCount INT DEFAULT 0,

    SentOn DATETIME NULL,

    ErrorMessage NVARCHAR(MAX) NULL,

    FOREIGN KEY (ScheduleId)
    REFERENCES EmailSchedule(ScheduleId)
);

CREATE TABLE EmailLogs
(
    LogId INT IDENTITY(1,1) PRIMARY KEY,

    RecipientId INT NOT NULL,

    Status INT NOT NULL,

    LogMessage NVARCHAR(MAX) NULL,

    CreatedOn DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (RecipientId)
    REFERENCES EmailRecipients(RecipientId)
);