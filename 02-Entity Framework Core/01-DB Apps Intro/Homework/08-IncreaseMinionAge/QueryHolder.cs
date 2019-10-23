using System;
using System.Collections.Generic;
using System.Text;

public static class QueryHolder
{
    public const string UpdateMinion =
        @"UPDATE Minions
            SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
        WHERE Id = @Id";

    public const string GetMinions = @"SELECT Name, Age FROM Minions";
}

