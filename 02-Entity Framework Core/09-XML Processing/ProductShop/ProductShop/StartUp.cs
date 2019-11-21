using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(x => x.AddProfile<ProductShopProfile>());

            using (var context = new ProductShopContext())
            {
                Console.WriteLine(GetUsersWithProducts(context));
            }
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = new ExportUsersAndProductsDTO(){
                Count = context.Users.Count(u => u.ProductsSold.Any(p => p.BuyerId != null)),
                Users = context.Users
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                    .OrderByDescending(u => u.ProductsSold.Count(p => p.BuyerId != null))
                    .Select(u => new ExportUserDTO(){
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new ExportSoldProductsDTO()
                        {
                            Count = u.ProductsSold.Count(p => p.BuyerId != null),
                            SoldProducts = u.ProductsSold
                                .Where(p => p.BuyerId != null)
                                .OrderByDescending(p => p.Price)
                                .Select(p => new ExportSoldProductDTO(){
                                    Name = p.Name,
                                    Price = p.Price
                                })
                                .ToList()
                        }
                    })
                    .Take(10)
                    .ToList()
            };
           
            var serializer = new XmlSerializer(users.GetType());
            var namespaces = new XmlSerializerNamespaces(new []{ new XmlQualifiedName() });

            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new ExportCategoryDTO()
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToList();

            var serializer = new XmlSerializer(categories.GetType(), new XmlRootAttribute("Categories"));
            var namespaces = new XmlSerializerNamespaces(new []{ new XmlQualifiedName() });

            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), categories, namespaces);
            return sb.ToString().TrimEnd();
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new ExportUserSoldProductsDTO()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Where(p => p.BuyerId != null)
                        .Select(p => new ExportSoldProductDTO(){ 
                            Name = p.Name,
                            Price = p.Price
                        }).ToList()
                })
                .Take(5)
                .ToList();

            var serializer = new XmlSerializer(users.GetType(), new XmlRootAttribute("Users"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new [] {
                new XmlQualifiedName(), 
            });

            serializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .ProjectTo<ExportProductInRangeDTO>()
                .Take(10)
                .ToList();

            var serializer = new XmlSerializer(products.GetType(), new XmlRootAttribute("Products"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new []{
                new XmlQualifiedName(), 
            });

            serializer.Serialize(new StringWriter(sb), products, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var categoriesProducts = new List<ImportCategoryProductDTO>();

            var serializer = new XmlSerializer(categoriesProducts.GetType(), new XmlRootAttribute("CategoryProducts"));
            using (var stream = CreateStreamFromString(inputXml))
            {
                categoriesProducts = (List<ImportCategoryProductDTO>)serializer.Deserialize(stream);
            }

            foreach (var cpDTO in categoriesProducts)
            {
                var cp = Mapper.Map<CategoryProduct>(cpDTO);
                context.CategoryProducts.Add(cp);
            }
            
            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var categoriesDto = new List<ImportCategoryDTO>();
            var serializer = new XmlSerializer(categoriesDto.GetType(), new XmlRootAttribute("Categories"));

            using (var stream = CreateStreamFromString(inputXml))
            {
                categoriesDto = (List<ImportCategoryDTO>)serializer.Deserialize(stream);
            }

            foreach (var categoryDto in categoriesDto.Where(c => c.Name != null))
            {
                var category = Mapper.Map<Category>(categoryDto);
                context.Categories.Add(category);
            }

            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var productsDto = new List<ImportProductDTO>();
            var serializer = new XmlSerializer(productsDto.GetType(), new XmlRootAttribute("Products"));

            using (var stream = CreateStreamFromString(inputXml))
            {
                productsDto = (List<ImportProductDTO>)serializer.Deserialize(stream);
            }

            foreach (var productDto in productsDto.Where(p => p.Name != null))
            {
                var product = Mapper.Map<Product>(productDto);
                context.Add(product);
            }

            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var usersDto = new List<ImportUserDTO>();

            var serializer = new XmlSerializer(usersDto.GetType(), new XmlRootAttribute("Users"));
            
            using (var stream = CreateStreamFromString(inputXml))
            {
                usersDto = (List<ImportUserDTO>)serializer.Deserialize(stream);
            }

            foreach (var userDto in usersDto.Where(u => u.LastName != null))
            {
                var user = Mapper.Map<User>(userDto);
                context.Add(user);
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