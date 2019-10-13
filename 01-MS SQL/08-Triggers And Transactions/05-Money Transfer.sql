CREATE PROC usp_TransferMoney(@senderId INT, @receiverId INT, @amount DECIMAL(18, 4)) AS
BEGIN
	BEGIN TRANSACTION
		EXEC usp_WithdrawMoney @senderId, @amount
		EXEC usp_DepositMoney @receiverId, @amount
	COMMIT
END