using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(x => x.AddProfile<CarDealerProfile>());

            using (var context = new CarDealerContext())
            {
                Console.WriteLine(GetSalesWithAppliedDiscount(context));
            }
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new ExportSaleDto()
                {
                    Car = new ExportCarInSaleDto()
                    { 
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(pc => pc.Part.Price) - (s.Discount * s.Car.PartCars.Sum(pc => pc.Part.Price) / 100)
                })
                .ToList();

            var serializer = new XmlSerializer(sales.GetType(), new XmlRootAttribute("sales"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName() });

            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), sales, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Any(s => s.Car != null))
                .Select(c => new ExportCustomerDto()
                {
                    Name = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
                })
                .OrderByDescending(c => c.SpentMoney)
                .ToList();

            var serializer = new XmlSerializer(customers.GetType(), new XmlRootAttribute("customers"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName() });

            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), customers, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new ExportCarWithPartsDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars
                        .Select(p => new ExportPartDto()
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToList()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToList();

            var serializer = new XmlSerializer(cars.GetType(), new XmlRootAttribute("cars"));
            var namespaces = new XmlSerializerNamespaces(new []{ new XmlQualifiedName() });

            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), cars, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new ExportLocalSupplierDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            var serializer = new XmlSerializer(suppliers.GetType(), new XmlRootAttribute("suppliers"));
            var namespaces = new XmlSerializerNamespaces(new []{ new XmlQualifiedName() });

            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), suppliers, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "BMW")
                .Select(c => new ExportBmwCarDto()
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            var serializer = new XmlSerializer(cars.GetType(), new XmlRootAttribute("cars"));
            var namespaces = new XmlSerializerNamespaces(new []{ new XmlQualifiedName() });

            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), cars, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Select(c => new ExportCarWithDistanceDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .Take(10)
                .ToList();

            var serializer = new XmlSerializer(cars.GetType(), new XmlRootAttribute("cars"));
            var namespaces = new XmlSerializerNamespaces(new []{ new XmlQualifiedName() });

            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), cars, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var sales = new List<ImportSaleDto>();
            var serializer = new XmlSerializer(sales.GetType(), new XmlRootAttribute("Sales"));

            using (var stream = CreateStreamFromString(inputXml))
            {
                sales = (List<ImportSaleDto>)serializer.Deserialize(stream);
            }

            foreach (var saleDto in sales.Where(s => s.CarId <= 36))
            {
                var sale = Mapper.Map<Sale>(saleDto);
                context.Sales.Add(sale);
            }

            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var customers = new List<ImportCustomerDto>();
            var serializer = new XmlSerializer(customers.GetType(), new XmlRootAttribute("Customers"));

            using (var stream = CreateStreamFromString(inputXml))
            {
                customers = (List<ImportCustomerDto>)serializer.Deserialize(stream);
            }

            foreach (var customerDto in customers)
            {
                var customer = Mapper.Map<Customer>(customerDto);
                context.Customers.Add(customer);
            }

            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsDto = new List<ImportCarDto>();
            var serializer = new XmlSerializer(carsDto.GetType(), new XmlRootAttribute("Cars"));

            using (var stream = CreateStreamFromString(inputXml))
            {
                carsDto = (List<ImportCarDto>)serializer.Deserialize(stream);
            }

            var cars = new List<Car>();

            foreach (var carDto in carsDto)
            {
                var car = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };

                cars.Add(car);
                context.Cars.Add(car);

                foreach (var partId in carDto.Parts)
                {
                    var carPart = new PartCar()
                    {
                        PartId = partId.Id,
                        Car = car
                    };

                    if (car.PartCars.All(pc => pc.PartId != carPart.PartId))
                    {
                        context.PartCars.Add(carPart);
                    }
                }
            }

            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var parts = new List<ImportPartDto>();
            var serializer = new XmlSerializer(parts.GetType(), new XmlRootAttribute("Parts"));

            using (var stream = CreateStreamFromString(inputXml))
            {
                parts = (List<ImportPartDto>)serializer.Deserialize(stream);
            }

            foreach (var partDto in parts.Where(p => p.SupplierId <= 31))
            {
                var part = Mapper.Map<Part>(partDto);
                context.Parts.Add(part);
            }

            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var suppliers = new List<ImportSupplierDto>();
            var serializer = new XmlSerializer(suppliers.GetType(), new XmlRootAttribute("Suppliers"));

            using (var stream = CreateStreamFromString(inputXml))
            {
                suppliers = (List<ImportSupplierDto>)serializer.Deserialize(stream);
            }
            
            foreach (var sDto in suppliers)
            {
                var supplier = Mapper.Map<Supplier>(sDto);
                context.Suppliers.Add(supplier);
            }

            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static Stream CreateStreamFromString(string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(str);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}