using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Cinema.Data.Models;
using Cinema.Data.Models.Enums;
using Cinema.DataProcessor.ImportDto;

namespace Cinema
{
    public class CinemaProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public CinemaProfile()
        {
            this.CreateMap<ImportMovieDto, Movie>()
                .ForMember(m => m.Genre, x => x.MapFrom(y => Enum.Parse<Genre>(y.Genre)))
                .ForMember(m => m.Duration, x => x.MapFrom(y => TimeSpan.ParseExact(y.Duration, "c", CultureInfo.InvariantCulture)));

            this.CreateMap<ImportHallDto, Hall>()
                .ForMember(x => x.Seats, y => y.MapFrom(s => new List<Seat>()));

            this.CreateMap<ImportCustomerDto, Customer>()
                .ForMember(x => x.Tickets, y => y.MapFrom(s => new List<Ticket>()));
        }
    }
}
