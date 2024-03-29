﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Song")]
    public class ImportSongDto
    {
        [Required]
        [MaxLength(20), MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        public string Genre { get; set; }

        public int? AlbumId { get; set; }

        [Required]
        public int WriterId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
