namespace P01_StudentSystem
{
    using Microsoft.EntityFrameworkCore;
    using Data;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new StudentSystemContext();

            db.Database.Migrate();

            Seeder.AddStudents(db, 3);
            Seeder.AddCourses(db, 3);
            Seeder.AddResources(db,3);
            Seeder.AddHomework(db, 3);
            Seeder.AddStudentCourse(db, 2);

            db.SaveChanges();
        }
    }
}
