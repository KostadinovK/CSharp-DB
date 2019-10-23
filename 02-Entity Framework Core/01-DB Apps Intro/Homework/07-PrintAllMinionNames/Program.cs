using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Program
{
    private const string ConnectionStr = @"Server=.\SQLEXPRESS;Database=MinionsDb;Integrated Security=True";
    public static void Main()
    {
        var minions = new List<string>();

        using var connection = new SqlConnection(ConnectionStr);
        connection.Open();

        var command = new SqlCommand(QueryHolder.GetMinionsName, connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            minions.Add((string)reader[0]);
        }
        
        Console.WriteLine("Original Order: ");
        Console.WriteLine(String.Join(Environment.NewLine, minions));
        Console.WriteLine();
        Console.WriteLine("New Order: ");

        if (minions.Count % 2 == 0)
        {
            for (int i = 0; i < minions.Count / 2; i++)
            {
                Console.WriteLine(minions[i]);
                Console.WriteLine(minions[^(i+1)]);
            }
        }
        else
        {
            for (int i = 0; i <= minions.Count / 2; i++)
            {
                Console.WriteLine(minions[i]);
                if (i == minions.Count / 2)
                {
                    return;
                }
                Console.WriteLine(minions[minions.Count - i - 1]);
            }
        }
        
    }
}

