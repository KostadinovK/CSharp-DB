using System;
using System.Collections.Generic;
using System.Text;
using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Enums;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem
{
    public static class Seeder
    {
        public static void AddStudents(StudentSystemContext db, int studentsCount)
        {
            for (int i = 0; i < studentsCount; i++)
            {
                var st = new Student()
                {
                    Name = $"Test Testov{i}",
                    RegisteredOn = DateTime.Now.AddDays(-100),
                    Birthday = DateTime.Now.AddDays(-20)
                };

                db.Students.Add(st);
            }
        }

        public static void AddCourses(StudentSystemContext db, int coursesCount)
        {
            for (int i = 0; i < coursesCount; i++)
            {
                var c = new Course()
                {
                    Name = $"Course N{i}",
                    StartDate = DateTime.Now.AddDays(-60),
                    EndDate = DateTime.Now,
                    Price = 200
                };

                db.Courses.Add(c);
            }
        }

        public static void AddResources(StudentSystemContext db, int resourcesCount)
        {

            for (int i = 0; i < resourcesCount; i++)
            {
                var r = new Resource()
                {
                    Name = $"Resource N{i}",
                    ResourceType = ResourceType.Video,
                    CourseId = 1
                };

                db.Resources.Add(r);
            }
        }

        public static void AddHomework(StudentSystemContext db, int homeworkCount)
        {

            for (int i = 1; i <= homeworkCount; i++)
            {
                var h = new Homework()
                {
                    Content = $"Homework N{i}",
                    ContentType = ContentType.Zip,
                    SubmissionTime = DateTime.Now,
                    CourseId = 1,
                    StudentId = 1
                };

                db.HomeworkSubmissions.Add(h);
            }
        }

        public static void AddStudentCourse(StudentSystemContext db, int count)
        {

            for (int i = 1; i <= count; i++)
            {
                var sc = new StudentCourse()
                {
                    CourseId = i,
                    StudentId = i
                };

                db.StudentCourses.Add(sc);
            }
        }
    }
}
