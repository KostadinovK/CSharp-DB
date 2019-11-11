using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Models.Enums;
using Remotion.Linq.Clauses;

namespace BookShop
{
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                Console.WriteLine(RemoveBooks(db));
            }
        }

        public static int RemoveBooks(BookShopContext db)
        {
            var booksToRemove = db.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            var count = booksToRemove.Count;

            db.Books.RemoveRange(booksToRemove);
            db.SaveChanges();

            return count;
        }

        public static void IncreasePrices(BookShopContext db)
        {
            var books = db.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            db.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext db)
        {
            var sb = new StringBuilder();

            var categories = db.Categories
                .Select(c => new
                {
                    c.Name,
                    Books = c.CategoryBooks
                        .OrderByDescending(b => b.Book.ReleaseDate.Value)
                        .Take(3)
                        .Select(b => new
                        {
                            b.Book.Title,
                            b.Book.ReleaseDate.Value.Year
                        })
                        .ToList()
                })
                .OrderBy(c => c.Name)
                .ToList();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");
                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext db)
        {
            var sb = new StringBuilder();

            var categories = db.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var category in categories)
            {
                sb.AppendLine($"{category.Name} ${category.Profit:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string CountCopiesByAuthor(BookShopContext db)
        {
            var sb = new StringBuilder();

            var authors = db.Authors
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,
                    Copies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.Copies)
                .ToList();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FullName} - {author.Copies}");
            }

            return sb.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext db, int lengthCheck)
        {
            return db.Books
                .Count(b => b.Title.Length > lengthCheck);
        } 

        public static string GetBooksByAuthor(BookShopContext db, string input)
        {
            var sb = new StringBuilder();

            var books = db.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    AuthorFullName = b.Author.FirstName + " " + b.Author.LastName
                })
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorFullName})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext db, string input)
        {
            var sb = new StringBuilder();

            var books = db.Books
                .Select(b => b.Title)
                .Where(b => b.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b)
                .ToList();

            foreach (var title in books)
            {
                sb.AppendLine($"{title}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext db, string nameEnding)
        {
            var sb = new StringBuilder();

            var authors = db.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName
                })
                .Where(a => a.FirstName.EndsWith(nameEnding))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();

            foreach (var a in authors)
            {
                sb.AppendLine($"{a.FirstName} {a.LastName}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksReleasedBefore(BookShopContext db, string date)
        {
            var sb = new StringBuilder();

            var dateParams = date.Split("-").Select(int.Parse).ToList();
            var day = dateParams[0];
            var month = dateParams[1];
            var year = dateParams[2];

            var books = db.Books
                .Where(b => b.ReleaseDate.Value < new DateTime(year, month, day))
                .OrderByDescending(b => b.ReleaseDate.Value)
                .Select(b => new {
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByCategory(BookShopContext db, string input)
        {
            var sb = new StringBuilder();

            var categories = input.Split(" ");
            var totalBooks = new List<string>();

            foreach (var category in categories)
            {
                var books = db.Books
                    .Where(b => b.BookCategories.Any(c => c.Category.Name.ToLower() == category.ToLower()))
                    .Select(b => b.Title)
                    .ToList();

                totalBooks.AddRange(books);
            }

            foreach (var book in totalBooks.OrderBy(b => b))
            {
                sb.AppendLine($"{book}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext db, int year)
        {
            var sb = new StringBuilder();

            var books = db.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByPrice(BookShopContext db)
        {
            var sb = new StringBuilder();

            var books = db.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetGoldenBooks(BookShopContext db)
        {
            var sb = new StringBuilder();

            var goldenBooks = db.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            foreach (var book in goldenBooks)
            {
                sb.AppendLine($"{book}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByAgeRestriction(BookShopContext db, string command)
        {
            var sb = new StringBuilder();
            var books = new List<string>();

            if (command.ToLower() == "minor")
            {
                books = db.Books
                    .Where(b => b.AgeRestriction == AgeRestriction.Minor)
                    .Select(b => b.Title)
                    .OrderBy(t => t)
                    .ToList();
            }else if (command.ToLower() == "teen")
            {
                books = db.Books
                    .Where(b => b.AgeRestriction == AgeRestriction.Teen)
                    .Select(b => b.Title)
                    .OrderBy(t => t)
                    .ToList();
            }else if (command.ToLower() == "adult")
            {
                books = db.Books
                    .Where(b => b.AgeRestriction == AgeRestriction.Adult)
                    .Select(b => b.Title)
                    .OrderBy(t => t)
                    .ToList();
            }

            foreach (var title in books)
            {
                sb.AppendLine($"{title}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
