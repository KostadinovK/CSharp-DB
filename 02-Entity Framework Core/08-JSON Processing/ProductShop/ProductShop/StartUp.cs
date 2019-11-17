using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            using (var context = new ProductShopContext())
            {
                Console.WriteLine(GetUsersWithProducts(context));
            }
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersObj = new
            {
                usersCount = context.Users.Count(u => u.ProductsSold.Any(p => p.BuyerId != null)),
                users = context.Users
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                    .OrderByDescending(u => u.ProductsSold.Count(p => p.BuyerId != null))
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new
                        {
                            count = u.ProductsSold.Count(x => x.BuyerId != null),
                            products = u.ProductsSold
                                .Where(ps => ps.BuyerId != null)
                                .Select(ps => new
                                {
                                    name = ps.Name,
                                    price = ps.Price
                                })
                                .ToList()
                        }
                    })
                    .ToList()
            };

            var jsonStr = JsonConvert.SerializeObject(usersObj,
                new JsonSerializerSettings()
                    { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

            return jsonStr;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = $"{c.CategoryProducts.Average(p => p.Product.Price):f2}",
                    totalRevenue = $"{c.CategoryProducts.Sum(p => p.Product.Price):f2}"
                })
                .ToList();

            var jsonStr = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return jsonStr;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var sellers = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Where(sp => sp.BuyerId != null)
                        .Select(sp => new {
                            name = sp.Name,
                            price = sp.Price,
                            buyerFirstName = sp.Buyer.FirstName,
                            buyerLastName = sp.Buyer.LastName
                        })
                        .ToList()
                })
                .OrderBy(s => s.lastName)
                .ThenBy(s => s.firstName)
                .ToList();

            var jsonStr = JsonConvert.SerializeObject(sellers, Formatting.Indented);

            return jsonStr;
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName
                })
                .ToList();

            var jsonStr = JsonConvert.SerializeObject(products, Formatting.Indented);

            return jsonStr;
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoriesProducts = JsonConvert
                .DeserializeObject<IEnumerable<CategoryProduct>>(inputJson);

            context.CategoryProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Count()}";
        }

        public static string ImportCategories(ProductShopContext context, string jsonStr)
        {
            var categories = JsonConvert
                .DeserializeObject<IEnumerable<Category>>(jsonStr)
                .Where(c => c.Name?.Length >= 3 && c.Name?.Length <= 15);

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count()}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert
                .DeserializeObject<IEnumerable<Product>>(inputJson)
                .Where(p => p.Name.Length >= 3);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        } 

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert
                .DeserializeObject<IEnumerable<User>>(inputJson)
                .Where(u => u.LastName?.Length >= 3);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }
    }
}