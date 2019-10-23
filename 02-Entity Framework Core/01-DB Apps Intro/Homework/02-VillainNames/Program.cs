using System;
using System.Data.SqlClient;

class Program
{
    public const string ConnectionString = @"Server=.\SQLEXPRESS;Database=MinionsDb;Integrated Security=True;";
    static void Main()
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        var getVillainNames = new SqlCommand(QueryHolder.GetVillainsNamesQuery, connection);
        using var reader = getVillainNames.ExecuteReader();

        while (reader.Read())
        { 
            Console.WriteLine($"{reader[0]} - {reader[1]}");
        }
    }
}

