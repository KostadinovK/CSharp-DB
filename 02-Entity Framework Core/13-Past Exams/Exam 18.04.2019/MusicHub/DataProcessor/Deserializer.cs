using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.DataProcessor.ImportDtos;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;


namespace MusicHub.DataProcessor
{
    using System;

    using Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter 
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone 
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong 
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        private static bool IsValid(object obj)
        {
            var valContext = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, valContext, results, true);
        }

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writers = JsonConvert.DeserializeObject<IEnumerable<Writer>>(jsonString);

            var sb = new StringBuilder();

            foreach (var writer in writers)
            {
                if (writer.Name == null || !IsValid(writer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                context.Writers.Add(writer);
                sb.AppendLine(String.Format(SuccessfullyImportedWriter, writer.Name));
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

       

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producerDtos = JsonConvert.DeserializeObject<IEnumerable<ImportProducerDto>>(jsonString);

            var sb = new StringBuilder();

            foreach (var producerDto in producerDtos)
            {
                if (producerDto.Name == null || producerDto.Albums.Any(a => !IsValid(a)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var producer = Mapper.Map<Producer>(producerDto);

                if (!IsValid(producer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (var albumDto in producerDto.Albums)
                {
                    var album = Mapper.Map<Album>(albumDto);

                    producer.Albums.Add(album);
                }

                context.Producers.Add(producer);

                if (producer.PhoneNumber == null)
                {
                    sb.AppendLine(String.Format(SuccessfullyImportedProducerWithNoPhone, producer.Name,
                        producer.Albums.Count));
                }
                else
                {
                    sb.AppendLine(String.Format(SuccessfullyImportedProducerWithPhone, producer.Name,
                        producer.PhoneNumber, producer.Albums.Count));
                }
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var songDtos = new List<ImportSongDto>();
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(songDtos.GetType(), new XmlRootAttribute("Songs"));

            songDtos = (List<ImportSongDto>)serializer.Deserialize(new StringReader(xmlString));

            foreach (var songDto in songDtos)
            {
                if (!IsValid(songDto) 
                    || !Enum.GetNames(typeof(Genre)).Contains(songDto.Genre) 
                    || context.Albums.All(a => a.Id != songDto.AlbumId) 
                    || context.Writers.All(w => w.Id != songDto.WriterId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var song = Mapper.Map<Song>(songDto);

                context.Songs.Add(song);
                sb.AppendLine(String.Format(SuccessfullyImportedSong, song.Name, song.Genre.ToString(), song.Duration));
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var performerDtos = new List<ImportPerformerDto>();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(performerDtos.GetType(), new XmlRootAttribute("Performers"));

            performerDtos = (List<ImportPerformerDto>)serializer.Deserialize(new StringReader(xmlString));

            foreach (var performerDto in performerDtos)
            {
                var isValidPerformerDto = true;

                foreach (var songDto in performerDto.PerformersSongs)
                {
                    if (context.Songs.All(s => s.Id != songDto.Id))
                    {
                        isValidPerformerDto = false;
                        break;
                    }
                }

                if (!isValidPerformerDto)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var performer = Mapper.Map<Performer>(performerDto);

                if (!IsValid(performer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                context.Performers.Add(performer);
                sb.AppendLine(String.Format(SuccessfullyImportedPerformer, performer.FirstName,
                    performerDto.PerformersSongs.Count));

                foreach (var song in performerDto.PerformersSongs)
                {
                    var sp = new SongPerformer()
                    {
                        SongId = song.Id,
                        Song = context.Songs.Find(song.Id),
                        Performer = performer,
                        PerformerId = performer.Id
                    };

                    context.SongsPerformers.Add(sp);
                }
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
    }
}