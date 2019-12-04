using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using VaporStore.Data.Models.Enum;
using VaporStore.DataProcessor.ExportDtos;
using Formatting = Newtonsoft.Json.Formatting;

namespace VaporStore.DataProcessor
{
	using System;
	using Data;

	public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genres = context.Genres
                .Where(g => genreNames.Any(x => x == g.Name))
                .Select(g => new
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games
                        .Where(x => x.Purchases.Count != 0)
                        .OrderByDescending(x => x.Purchases.Count)
                        .ThenBy(x => x.Id)
                        .Select(x => new 
                        {
                            Id = x.Id,
                            Title = x.Name,
                            Developer = x.Developer.Name,
                            Tags = String.Join(", ", x.GameTags.Select(gt => gt.Tag.Name)),
                            Players = x.Purchases.Count
                        }).ToList(),
                    TotalPlayers = g.Games.Sum(x => x.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToList();

            var json = JsonConvert.SerializeObject(genres, Formatting.Indented);

            return json;
        }

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var users = context.Users
                .Select(u => new ExportUserDto
                {
                    Username = u.Username,
                    Purchases = u.Cards
                        .SelectMany(c => c.Purchases)
                        .Where(p => p.Type == Enum.Parse<PurchaseType>(storeType))
                        .OrderBy(p => p.Date)
                        .Select(p => new ExportPurchaseDto
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new ExportGameDto
                            {
                                Title = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price
                            },
                            
                        }).ToList(),

                    TotalSpent = u.Cards.SelectMany(c => c.Purchases)
                        .Where(p => p.Type.ToString() == storeType)
                        .Sum(p => p.Game.Price)
                })
                .Where(u => u.Purchases.Any())
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToList();

            var serializer = new XmlSerializer(users.GetType(), new XmlRootAttribute("Users"));
            var namespaces = new XmlSerializerNamespaces(new []{new XmlQualifiedName() });

            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString().TrimEnd();
        }
	}
}