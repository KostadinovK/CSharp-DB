﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_StudentSystem.Data.Models
{

    using static DataValidation.Resource;
    using Enums;

    public class Resource
    {
        public int ResourceId { get; set; }

        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
