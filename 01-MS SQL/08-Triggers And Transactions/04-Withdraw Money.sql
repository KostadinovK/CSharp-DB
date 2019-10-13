CREATE PROC usp_WithdrawMoney(@accountId INT, @moneyAmount DECIMAL(18, 4)) AS
BEGIN
	BEGIN TRANSACTION
		UPDATE Accounts
			SET Balance -= @moneyAmount
		WHERE Id = @accountId

		IF(@moneyAmount < 0)
		BEGIN
			ROLLBACK
		END
	COMMIT
END