using System;
using System.Collections.Generic;
using System.Data.SqlClient;
class Program
{
    public const string ConnectionString = @"Server=.\SQLEXPRESS;Database=MinionsDb;Integrated Security=True;";
    static void Main()
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        var commands = new List<SqlCommand>();

        commands.Add(new SqlCommand(QueryHolder.CreateTables, connection));
        commands.Add(new SqlCommand(QueryHolder.InsertData, connection));

        foreach (var command in commands)
        {
            command.ExecuteNonQuery();
        }
    }
}
