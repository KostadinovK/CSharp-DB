using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SoftJail.Data.Models;
using SoftJail.Data.Models.Enums;
using SoftJail.DataProcessor.ImportDto;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Deserializer
    {
        private static string ErrorMessage = "Invalid Data";

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentDtos = JsonConvert.DeserializeObject<IEnumerable<ImportDepartmentDto>>(jsonString);

            var sb = new StringBuilder();

            foreach (var departmentDto in departmentDtos)
            {
                if (departmentDto.Cells.Any(c => !IsValid(c)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!IsValid(departmentDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var department = new Department
                {
                    Name = departmentDto.Name
                };

                context.Departments.Add(department);
                sb.AppendLine($"Imported {department.Name} with {departmentDto.Cells.Count} cells");

                foreach (var cellDto in departmentDto.Cells)
                {
                    var cell = new Cell
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow,
                        DepartmentId = department.Id,
                        Department = department
                    };

                    context.Cells.Add(cell);
                }
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        
        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonerDtos = JsonConvert.DeserializeObject<IEnumerable<ImportPrisonerDto>>(jsonString);

            var sb = new StringBuilder();

            foreach (var prisonerDto in prisonerDtos)
            {
                if (prisonerDto.Mails.Any(m => !IsValid(m)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!IsValid(prisonerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime? releaseDate = null;
                if (prisonerDto.ReleaseDate != null)
                {
                    releaseDate = DateTime.ParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);
                }

                var prisoner = new Prisoner
                {
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    Age = prisonerDto.Age,
                    IncarcerationDate = DateTime.ParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    ReleaseDate = releaseDate,
                    Bail = prisonerDto.Bail,
                    CellId = prisonerDto.CellId
                };

                context.Prisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");

                foreach (var mailDto in prisonerDto.Mails)
                {
                    var mail = new Mail
                    {
                        Description = mailDto.Description,
                        Sender = mailDto.Sender,
                        Address = mailDto.Address,
                        Prisoner = prisoner,
                        PrisonerId = prisoner.Id
                    };

                    context.Mails.Add(mail);
                }
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            
            var officerDtos = new List<ImportOfficerDto>();

            var serializer = new XmlSerializer(officerDtos.GetType(), new XmlRootAttribute("Officers"));
            officerDtos = (List<ImportOfficerDto>)serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            foreach (var officerDto in officerDtos)
            {
                if(!Enum.IsDefined(typeof(Position), officerDto.Position) || !Enum.IsDefined(typeof(Weapon), officerDto.Weapon))
                {
                    continue;
                    sb.AppendLine(ErrorMessage);
                }

                var officer = new Officer
                {
                    FullName = officerDto.Name,
                    Salary = officerDto.Money,
                    Position = Enum.Parse<Position>(officerDto.Position),
                    Weapon = Enum.Parse<Weapon>(officerDto.Weapon),
                    DepartmentId = officerDto.DepartmentId
                };

                officer.OfficerPrisoners = officerDto.Prisoners
                    .Select(p => new OfficerPrisoner
                    {
                        PrisonerId = p.Id
                    })
                    .ToList();

                context.Officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
    }
}