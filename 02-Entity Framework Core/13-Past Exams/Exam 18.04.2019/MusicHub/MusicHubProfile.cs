using System;
using System.Collections.Generic;
using System.Globalization;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.DataProcessor.ImportDtos;


namespace MusicHub
{
    using AutoMapper;

    public class MusicHubProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public MusicHubProfile()
        {
            this.CreateMap<ImportProducerDto, Producer>()
                .ForMember(x => x.Albums, y => y.MapFrom(s => new HashSet<Album>()));

            this.CreateMap<ImportAlbumDto, Album>()
                .ForMember(x => x.ReleaseDate,
                    y => y.MapFrom(s =>
                        DateTime.ParseExact(s.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            this.CreateMap<ImportSongDto, Song>()
                .ForMember(x => x.CreatedOn,
                    y => y.MapFrom(s => DateTime.ParseExact(s.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(x => x.Genre, y => y.MapFrom(s => Enum.Parse<Genre>(s.Genre)))
                .ForMember(x => x.Duration,
                    y => y.MapFrom(s => TimeSpan.ParseExact(s.Duration, "c", CultureInfo.InvariantCulture)));

            this.CreateMap<ImportPerformerDto, Performer>();
        }
    }
}
