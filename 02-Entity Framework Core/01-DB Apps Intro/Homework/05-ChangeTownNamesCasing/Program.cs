using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Program
{
    private const string ConnectionStr = @"Server=.\SQLEXPRESS;Database=MinionsDb;Integrated Security=True";

    public static void Main()
    {
        string country = Console.ReadLine();

        using var connection = new SqlConnection(ConnectionStr);
        connection.Open();

        var updateTownsCommand = new SqlCommand(QueryHolder.UpdateTowns, connection);
        updateTownsCommand.Parameters.AddWithValue("@countryName", country);

        var townsCount = updateTownsCommand.ExecuteNonQuery();

        if (townsCount == 0)
        {
            Console.WriteLine("No town names were affected.");
            return;
        }

        Console.WriteLine($"{townsCount} town names were affected.");

        var getTownsCommand = new SqlCommand(QueryHolder.GetTownNames, connection);
        getTownsCommand.Parameters.AddWithValue("@countryName", country);

        var towns = new List<string>();

        using var reader = getTownsCommand.ExecuteReader();

        while (reader.Read())
        {
            towns.Add((string)reader[0]);
        }

        Console.WriteLine($"[{String.Join(", ", towns)}]");
    }
}

