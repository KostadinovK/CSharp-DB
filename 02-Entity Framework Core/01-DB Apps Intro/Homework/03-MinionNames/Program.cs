using System;
using System.Collections.Generic;
using System.Data.SqlClient;

class Program
{
    public const string ConnectionString = @"Server=.\SQLEXPRESS;Database=MinionsDb;Integrated Security=True;";
    static void Main()
    {
        int id = int.Parse(Console.ReadLine());
        string villainName = "";
        var minions = new List<string>();

        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        var getVillainName = new SqlCommand(QueryHolder.GetVillainName, connection);
        getVillainName.Parameters.AddWithValue("@Id", id);
        var getMinions = new SqlCommand(QueryHolder.GetMinionsByVillain, connection);
        getMinions.Parameters.AddWithValue("@Id", id);

        using (var villainReader = getVillainName.ExecuteReader())
        {
            while (villainReader.Read())
            {
                villainName = (string)villainReader[0];
            }

            if (villainName == "")
            {
                Console.WriteLine($"No villain with ID {id} exists in the database.");
                return;
            }

            Console.WriteLine("Villain: " + villainName);
        }

        
        using var minionsReader = getMinions.ExecuteReader();

        while (minionsReader.Read())
        {
            minions.Add($"{minionsReader[0]}. {minionsReader[1]} {minionsReader[2]}");
        }

        Console.WriteLine(minions.Count == 0 ? "(no minions)" : String.Join(Environment.NewLine, minions));
    }
}
