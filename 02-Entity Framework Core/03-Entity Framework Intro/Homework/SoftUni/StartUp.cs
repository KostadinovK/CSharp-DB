using System;
using System.Globalization;
using System.Linq;
using System.Text;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main()
        {
            using (var db = new SoftUniContext())
            {
                Console.WriteLine(RemoveTown(db));
            }
        }

        public static string RemoveTown(SoftUniContext db)
        {
            var townToRemove = db.Towns.FirstOrDefault(t => t.Name == "Seattle");

            var addresses = db.Addresses.Where(a => a.Town == townToRemove).ToList();
            var addressesCount = addresses.Count;

            foreach (var address in addresses)
            {
                var employees = db.Employees.Where(e => e.Address == address).ToList();

                foreach (var e in employees)
                {
                    e.AddressId = null;
                }

                db.Addresses.Remove(address);
            }

            db.Towns.Remove(townToRemove);

            return $"{addressesCount} addresses in {townToRemove.Name} were deleted";
        }

        public static string DeleteProjectById(SoftUniContext db)
        {
            var project = db.Projects.Find(2);

            var employeesProjectsToRemove = db.EmployeesProjects.Where(e => e.Project == project).ToList();

            db.EmployeesProjects.RemoveRange(employeesProjectsToRemove);

            db.Projects.Remove(project);

            db.SaveChanges();

            var projects = db.Projects
                .Select(p => p.Name)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var p in projects)
            {
                sb.AppendLine($"{p}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext db)
        {
            var employees = db.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary,
                    e.JobTitle
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext db)
        {
            var employees = db.Employees
                .Where(e => e.Department.Name == "Engineering" ||
                            e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" ||
                            e.Department.Name == "Information Services")
                .ToList();

            foreach (var employee in employees)
            {
                employee.Salary += employee.Salary * (decimal)0.12;
            }

            db.SaveChanges();

            var employeesWithIncreasedSalaries = db.Employees
                .Where(e => e.Department.Name == "Engineering" ||
                            e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" ||
                            e.Department.Name == "Information Services")
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => new 
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                })
                .ToList();
            
            var sb = new StringBuilder();

            foreach (var emp in employeesWithIncreasedSalaries)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext db)
        {
            var projects = db.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .ToList();

            var sb = new StringBuilder();
            var dateFormat = @"M/d/yyyy h:mm:ss tt";

            foreach (var project in projects)
            {
                sb.AppendLine($"{project.Name}");
                sb.AppendLine($"{project.Description}");
                sb.AppendLine($"{project.StartDate.ToString(dateFormat, CultureInfo.InvariantCulture)}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext db)
        {
            var departments = db.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    d.Name,
                    ManagerName = d.Manager.FirstName + " " + d.Manager.LastName,
                    Employees = d.Employees
                        .Select(e => new 
                        {
                            e.FirstName,
                            e.LastName,
                            e.JobTitle
                        })
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .ToList()
                })
                .ToList();

            var sb = new StringBuilder();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.Name} - {department.ManagerName}");

                foreach (var emp in department.Employees)
                {
                    sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext db)
        {
            var employee = db.Employees.Where(e => e.EmployeeId == 147).Select(e => new
            {
                EmployeeFullName = e.FirstName + " " + e.LastName,
                e.JobTitle,
                Projects = e.EmployeesProjects.Select(p => new
                {
                    p.Project.Name
                }).OrderBy(p => p.Name).ToList()
            }).FirstOrDefault();

            var sb = new StringBuilder();

            sb.AppendLine($"{employee.EmployeeFullName} - {employee.JobTitle}");

            foreach (var project in employee.Projects)
            {
                sb.AppendLine($"{project.Name}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext db)
        {
            var addresses = db.Addresses.Select(a => new
            {
                a.AddressText,
                TownName = a.Town.Name,
                EmployeesCount = a.Employees.Count
            })
                .OrderByDescending(a => a.EmployeesCount)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeesCount} employees");
            }


            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext db)
        {
            var employees = db.Employees
                .Where(e => e.EmployeesProjects.Any(p =>
                    p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    EmployeeFullName = e.FirstName + " " + e.LastName,
                    ManagerFullName = e.Manager.FirstName + " " + e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(p => new
                    {
                        p.Project.Name,
                        p.Project.StartDate,
                        p.Project.EndDate
                    }).ToList()
                })
                .Take(10)
                .ToList();

            var dateFormat = @"M/d/yyyy h:mm:ss tt";

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.EmployeeFullName} - Manager: {employee.ManagerFullName}");

                foreach (var project in employee.Projects)
                {
                    var startDate = project.StartDate.ToString(dateFormat);

                    var endDate = project.EndDate.HasValue
                        ? project.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture)
                        : "not finished";

                    sb.AppendLine($"--{project.Name} - {startDate} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext db)
        {
            var newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            db.Addresses.Add(newAddress);

            var employee = db.Employees.FirstOrDefault(e => e.LastName == "Nakov");

            if(employee == null)
            {
                throw new InvalidOperationException("Database has no employee with last name Nakov!");
            }

            employee.Address = newAddress;
            db.SaveChanges();

            var addresses = db.Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => e.Address.AddressText)
                .Take(10)
                .ToList();

            var res = new StringBuilder();

            foreach (var address in addresses)
            {
                res.AppendLine(address);
            }

            return res.ToString();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext db)
        {
            var employees = db.Employees.Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.Salary,
                e.Department
            })
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            var res = new StringBuilder();

            foreach (var employee in employees)
            {
                res.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.Department.Name} - ${employee.Salary:f2}");    
            }

            return res.ToString();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext db)
        {
            var employees = db.Employees.Select(e => new 
                    {
                        e.FirstName,
                        e.Salary
                    })
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName);

            var res = new StringBuilder();

            foreach (var employee in employees)
            {
                res.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }
                
            return res.ToString();
        }

        public static string GetEmployeesFullInformation(SoftUniContext db)
        {
            var employees = db.Employees.Select(e => new
            {
                e.FirstName,
                e.MiddleName,
                e.LastName,
                e.Salary,
                e.JobTitle
            }).ToList();

            var res = new StringBuilder();

            foreach (var employee in employees)
            {
                res.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return res.ToString();
        }
    }
}

