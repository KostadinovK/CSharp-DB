using System;
using System.Data.SqlClient;
using System.Linq;

public class Program
{
    private const string ConnectionStr = @"Server=.\SQLEXPRESS;Database=MinionsDb;Integrated Security=True";
    static void Main()
    {
        var ids = Console.ReadLine().Split().Select(int.Parse).ToList();

        using var connection = new SqlConnection(ConnectionStr);
        connection.Open();

        var updateMinionCommand = new SqlCommand(QueryHolder.UpdateMinion, connection);

        foreach (var id in ids)
        {
            if (updateMinionCommand.Parameters.Contains("@Id"))
            {
                updateMinionCommand.Parameters.RemoveAt("@Id");
            }

            updateMinionCommand.Parameters.AddWithValue("@Id", id);

            updateMinionCommand.ExecuteNonQuery();
        }

        var getMinionsCommand = new SqlCommand(QueryHolder.GetMinions, connection);

        using var reader = getMinionsCommand.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader[0]} {reader[1]}");
        }
    }
}
