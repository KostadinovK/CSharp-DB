using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configurations
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> studentCourse)
        {
            studentCourse
                .HasKey(sc => new { sc.CourseId, sc.StudentId });

            studentCourse
                .HasOne(sc => sc.Student)
                .WithMany(sc => sc.CourseEnrollments)
                .HasForeignKey(sc => sc.StudentId);

            studentCourse
                .HasOne(sc => sc.Course)
                .WithMany(sc => sc.StudentsEnrolled)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
