CREATE FUNCTION ufn_CalculateFutureValue(@sum DECIMAL(18, 4), @yearlyInterestRate FLOAT, @years INT)
RETURNS DECIMAL(18, 4) AS
BEGIN
	DECLARE @futureValue DECIMAL(18, 4) = 0;
	SET @futureValue = @sum * POWER(1 + @yearlyInterestRate, @years) 

	RETURN @futureValue;
END;