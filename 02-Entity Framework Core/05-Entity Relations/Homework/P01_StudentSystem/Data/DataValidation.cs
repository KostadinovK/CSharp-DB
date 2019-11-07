using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    public static class DataValidation
    {
        public static class Student
        {
            public const int MaxNameLength = 100;
            public const int PhoneNumberLength = 10;
        }

        public static class Course
        {
            public const int MaxNameLength = 80;
        }

        public static class Resource
        {
            public const int MaxNameLength = 50;
        }
    }
}
