using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data
{
    public static class DataValidation
    {
        public static class Team
        {
            public const int MaxNameLength = 40;
            public const int MaxInitialsLength = 3;
        }

        public static class Color
        {
            public const int MaxNameLength = 30;
        }

        public static class Town
        {
            public const int MaxNameLength = 30;
        }

        public static class Country
        {
            public const int MaxNameLength = 30;
        }

        public static class Player
        {
            public const int MaxNameLength = 40;
        }

        public static class Position
        {
            public const int MaxNameLength = 30;
        }

        public static class User
        {
            public const int MaxNameLength = 30;
            public const int MaxUsernameLength = 20;
            public const int MaxPasswordLength = 20;
            public const int MaxEmailLength = 20;
        }
    }
}
