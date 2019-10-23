using System;
using System.Collections.Generic;
using System.Text;

public static class QueryHolder
{
    public const string ExecProc = @"EXEC usp_GetOlder @Id";

    public const string GetMinion = @"SELECT Name, Age FROM Minions WHERE Id = @Id";
}

