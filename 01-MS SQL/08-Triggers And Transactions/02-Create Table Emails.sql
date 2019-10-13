CREATE TABLE NotificationEmails
(
	Id INT IDENTITY PRIMARY KEY,
	Recipent INT FOREIGN KEY REFERENCES Accounts(Id) NOT NULL,
	[Subject] VARCHAR(30) NOT NULL,
	Body VARCHAR(50) NOT NULL
);

CREATE TRIGGER tr_SendNotification ON Logs AFTER INSERT
AS
BEGIN
	DECLARE @accountID INT = (SELECT AccountId FROM inserted);
	DECLARE @oldSum DECIMAL(18, 2) = (SELECT OldSum FROM inserted);
	DECLARE @newSum DECIMAL(18, 2) = (SELECT NewSum FROM inserted);
	DECLARE @subject NVARCHAR(30) = 'Balance change for account: ' + CAST(@accountId AS VARCHAR(10));
	DECLARE @body NVARCHAR(50) = 'On ' + CAST(GETDATE() AS VARCHAR(20)) + ' your balance was changed from ' + CAST(@oldSum AS VARCHAR(20)) + ' to ' + CAST(@newSum AS VARCHAR(20)) + '.';

	INSERT INTO NotificationEmails VALUES
	(@accountID, @subject, @body);
END