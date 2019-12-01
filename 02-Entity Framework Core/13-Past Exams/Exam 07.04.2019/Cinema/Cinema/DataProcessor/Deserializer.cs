using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Cinema.Data.Models;
using Cinema.DataProcessor.ImportDto;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Cinema.DataProcessor
{
    using System;

    using Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat 
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var moviesDto = JsonConvert.DeserializeObject<IEnumerable<ImportMovieDto>>(jsonString);

            var sb = new StringBuilder();

            foreach (var movieDto in moviesDto)
            {
                if (context.Movies.Any(m => m.Title == movieDto.Title))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = Mapper.Map<Movie>(movieDto);

                if (!IsValid(movie))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                context.Movies.Add(movie);
                sb.AppendLine(String.Format(SuccessfulImportMovie, movie.Title, movie.Genre.ToString(), $"{movie.Rating:f2}"));
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var hallDtos = JsonConvert.DeserializeObject<IEnumerable<ImportHallDto>>(jsonString);

            var sb = new StringBuilder();

            foreach (var hallDto in hallDtos)
            {
                if (hallDto.Seats <= 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = Mapper.Map<Hall>(hallDto);

                if (!IsValid(hall))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                context.Halls.Add(hall);

                var projectionType = "";
                if (hall.Is3D && hall.Is4Dx)
                {
                    projectionType = "4Dx/3D";
                }else if (hall.Is4Dx)
                {
                    projectionType = "4Dx";
                }else if (hall.Is3D)
                {
                    projectionType = "3D";
                }
                else
                {
                    projectionType = "Normal";
                }

                sb.AppendLine(String.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hallDto.Seats));

                for (int i = 0; i < hallDto.Seats; i++)
                {
                    var seat = new Seat()
                    {
                        HallId = hall.Id,
                        Hall = hall
                    };

                    hall.Seats.Add(seat);
                    context.Seats.Add(seat);
                }
               
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            
            var projectionDtos = new List<ImportProjectionDto>();

            var serializer = new XmlSerializer(projectionDtos.GetType(), new XmlRootAttribute("Projections"));

            projectionDtos = (List<ImportProjectionDto>)serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            foreach (var projectionDto in projectionDtos)
            {
                if (projectionDto.HallId <= 0 || projectionDto.HallId > context.Halls.Count())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (projectionDto.MovieId <= 0 || projectionDto.MovieId > context.Movies.Count())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var dateTime = DateTime.ParseExact(projectionDto.DateTime, "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");


                var projection = new Projection()
                {
                    MovieId = projectionDto.MovieId,
                    Movie = context.Movies.Find(projectionDto.MovieId),
                    HallId = projectionDto.HallId,
                    Hall = context.Halls.Find(projectionDto.HallId),
                    DateTime = DateTime.ParseExact(dateTime, "MM/dd/yyyy", CultureInfo.InvariantCulture)
                };

                context.Projections.Add(projection);

                sb.AppendLine(String.Format(SuccessfulImportProjection, projection.Movie.Title,
                    projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var customerDtos = new List<ImportCustomerDto>();

            var serializer = new XmlSerializer(customerDtos.GetType(), new XmlRootAttribute("Customers"));

            customerDtos = (List<ImportCustomerDto>)serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            foreach (var customerDto in customerDtos)
            {
                if (customerDto.Tickets.Any(t => t.ProjectionId <= 0 || t.ProjectionId > context.Projections.Count()))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (customerDto.Tickets.Any(t => t.Price < 0.01m))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = Mapper.Map<Customer>(customerDto);

                if (!IsValid(customer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                context.Customers.Add(customer);
                sb.AppendLine(String.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName,
                    customerDto.Tickets.Count()));

                foreach (var ticketDto in customerDto.Tickets)
                {
                    var ticket = new Ticket()
                    {
                        ProjectionId = ticketDto.ProjectionId,
                        Projection = context.Projections.Find(ticketDto.ProjectionId),
                        Price = ticketDto.Price,
                        CustomerId = customer.Id,
                        Customer = customer
                    };

                    context.Tickets.Add(ticket);
                    customer.Tickets.Add(ticket);
                }
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
    }
}