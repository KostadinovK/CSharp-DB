using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enum;
using VaporStore.DataProcessor.ImportDtos;

namespace VaporStore.DataProcessor
{
	using System;
	using Data;

	public static class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }

		public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var gameDtos = JsonConvert.DeserializeObject<IEnumerable<ImportGameDto>>(jsonString);

            var sb = new StringBuilder();

            foreach (var gameDto in gameDtos)
            {
                if (!IsValid(gameDto) || gameDto.Tags.Count < 1)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var developer = context.Developers.FirstOrDefault(d => d.Name == gameDto.Developer);

                if (developer == null)
                {
                    developer = new Developer()
                    {
                        Name = gameDto.Developer
                    };

                    context.Developers.Add(developer);
                    context.SaveChanges();
                }

                var genre = context.Genres.FirstOrDefault(g => g.Name == gameDto.Genre);

                if (genre == null)
                {
                    genre = new Genre()
                    {
                        Name = gameDto.Genre
                    };

                    context.Genres.Add(genre);
                    context.SaveChanges();
                }

                var game = new Game
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = DateTime.ParseExact(gameDto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DeveloperId = developer.Id,
                    GenreId = genre.Id
                };

                context.Games.Add(game);
                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {gameDto.Tags.Count} tags");

                foreach (var tagName in gameDto.Tags)
                {
                    var tag = context.Tags.FirstOrDefault(t => t.Name == tagName);

                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagName
                        };

                        context.Tags.Add(tag);
                        context.SaveChanges();
                    }

                    var gameTag = new GameTag
                    {
                        GameId = game.Id,
                        Game = game,
                        Tag = tag,
                        TagId = tag.Id
                    };

                    context.GameTags.Add(gameTag);
                }
            }

            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var userDtos = JsonConvert.DeserializeObject<IEnumerable<ImportUserDto>>(jsonString);

            var sb = new StringBuilder();

            foreach (var userDto in userDtos)
            {
                if (userDto.Cards.Count < 1 || userDto.Cards.Any(c => !IsValid(c) || (c.Type != "Debit" && c.Type != "Credit")))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!IsValid(userDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = new User
                {
                    FullName = userDto.FullName,
                    Email = userDto.Email,
                    Username = userDto.Username,
                    Age = userDto.Age
                };

                context.Users.Add(user);
                sb.AppendLine($"Imported {user.Username} with {userDto.Cards.Count} cards");

                foreach (var cardDto in userDto.Cards)
                {
                    var card = new Card
                    {
                        Number = cardDto.Number,
                        Cvc = cardDto.Cvc,
                        Type = Enum.Parse<CardType>(cardDto.Type),
                        UserId = user.Id,
                        User = user
                    };

                    context.Cards.Add(card);
                }
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var purchaseDtos = new List<ImportPurchaseDto>();

            var serializer = new XmlSerializer(purchaseDtos.GetType(), new XmlRootAttribute("Purchases"));
            purchaseDtos = (List<ImportPurchaseDto>)serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            foreach (var purchaseDto in purchaseDtos)
            {
                if (!IsValid(purchaseDto) || (purchaseDto.Type != "Retail" && purchaseDto.Type != "Digital"))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.Card);

                if (card == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var game = context.Games.FirstOrDefault(g => g.Name == purchaseDto.Game);

                if (game == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var purchase = new Purchase
                {
                    Type = Enum.Parse<PurchaseType>(purchaseDto.Type),
                    ProductKey = purchaseDto.Key,
                    CardId = card.Id,
                    Card = card,
                    Date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    Game = game,
                    GameId = game.Id
                };

                context.Purchases.Add(purchase);
                sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
	}
}