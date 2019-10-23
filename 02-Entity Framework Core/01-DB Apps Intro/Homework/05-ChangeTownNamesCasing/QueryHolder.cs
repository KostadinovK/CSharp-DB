using System;
using System.Collections.Generic;
using System.Text;

public static class QueryHolder
{
    public const string UpdateTowns =
        @"UPDATE Towns
            SET Name = UPPER(Name)
        WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

    public const string GetTownNames =
        @"SELECT t.Name 
        FROM Towns as t
        JOIN Countries AS c ON c.Id = t.CountryCode
        WHERE c.Name = @countryName";
}

