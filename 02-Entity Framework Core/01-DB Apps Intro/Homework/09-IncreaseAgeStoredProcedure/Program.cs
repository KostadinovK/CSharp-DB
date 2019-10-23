using System;
using System.Data;
using System.Data.SqlClient;

public class Program
{
    private const string ConnectedStr = @"Server=.\SQLEXPRESS;Database=MinionsDb;Integrated Security=True;";
    public static void Main()
    {
        int id = int.Parse(Console.ReadLine());

        using var connection = new SqlConnection(ConnectedStr);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = QueryHolder.ExecProc;
        command.Parameters.AddWithValue("@Id", id);

        var hasMinionWithId = command.ExecuteNonQuery() > 0;

        if (!hasMinionWithId)
        {
            Console.WriteLine("There is no minion with Id " + id);
            return;
        }

        command.CommandText = QueryHolder.GetMinion;
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader[0]} - {reader[1]} years old");
        }
    }
}

