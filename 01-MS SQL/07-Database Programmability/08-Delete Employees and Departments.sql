CREATE PROC usp_DeleteEmployeesFromDepartment(@departmentId INT) AS
BEGIN
	DELETE FROM EmployeesProjects
	WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

	ALTER TABLE Employees
	ALTER COLUMN ManagerID INT NULL

	ALTER TABLE Departments
	ALTER COLUMN ManagerID INT NULL

	UPDATE Employees
	SET ManagerID = NULL
	WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

	UPDATE Departments
	SET ManagerID = NULL
	WHERE DepartmentID = @departmentId

	DELETE FROM Employees
	WHERE DepartmentID = @departmentId

	DELETE FROM Departments
	WHERE DepartmentID = @departmentId

	SELECT COUNT(*) FROM Employees WHERE DepartmentID = @departmentId
END;