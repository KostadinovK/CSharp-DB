CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(7)
AS
BEGIN
	IF(@salary < 30000)
	BEGIN
		RETURN 'Low';
	END
	
	IF(@salary < 50000)
	BEGIN
		RETURN 'Average';
	END

	RETURN 'High';
END;