CREATE PROC usp_DepositMoney(@accountId INT, @moneyAmount DECIMAL(18, 2)) AS
BEGIN
	BEGIN TRANSACTION
		UPDATE Accounts
			SET Balance += @moneyAmount
		WHERE Id = @accountId

		IF(@moneyAmount < 0)
		BEGIN
			ROLLBACK
		END
	COMMIT
END;