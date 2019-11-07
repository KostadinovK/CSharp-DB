namespace P01_StudentSystem.Data.Models
{
    using static DataValidation.Course;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();

        public ICollection<Homework> HomeworkSubmissions { get; set; } = new HashSet<Homework>();

        public ICollection<StudentCourse> StudentsEnrolled { get; set; } = new HashSet<StudentCourse>();
    }
}
