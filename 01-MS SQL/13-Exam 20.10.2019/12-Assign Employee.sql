CREATE PROC usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT) AS
BEGIN

	DECLARE @EmployeeDepartmentId INT = ( SELECT DepartmentId FROM Employees WHERE Id = @EmployeeId)

	IF(
		EXISTS(
			SELECT * FROM Reports r
			JOIN Categories c ON c.Id = r.CategoryId
			WHERE r.Id = @ReportId AND c.DepartmentId = @EmployeeDepartmentId
		)
	)
	BEGIN
		 UPDATE Reports
			SET EmployeeId = @EmployeeId
		 WHERE Id = @ReportId
	END
	ELSE
	BEGIN
		;THROW 50001, 'Employee doesn''t belong to the appropriate department!', 1
	END
END;

EXEC usp_AssignEmployeeToReport 30, 1