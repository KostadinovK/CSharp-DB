CREATE TABLE Logs
(
	LogId INT IDENTITY PRIMARY KEY,
	AccountId INT NOT NULL,
	OldSum DECIMAL(18, 2) NOT NULL,
	NewSum DECIMAL(18, 2) NOT NULL	
);

ALTER TABLE Logs
ADD CONSTRAINT FK_Logs_Account FOREIGN KEY(AccountId) REFERENCES Accounts(Id);

CREATE TRIGGER tr_AccountUpdate ON Accounts AFTER UPDATE
AS
BEGIN
	INSERT INTO Logs (AccountId, OldSum, NewSum)
	SELECT 
		i.AccountHolderId,
		d.Balance,
        i.Balance
	FROM inserted i
	INNER JOIN deleted d ON d.AccountHolderId = i.AccountHolderId
END;
