using System;
using System.Data.SqlClient;

public class Program
{
    private const string ConnectionStr = @"Server=.\SQLEXPRESS;Database=MinionsDb;Integrated Security=True";
    public static void Main()
    {
        int id = int.Parse(Console.ReadLine());

        using var connection = new SqlConnection(ConnectionStr);
        connection.Open();

        var transaction = connection.BeginTransaction();

        var command = connection.CreateCommand();
        command.Transaction = transaction;

        try
        {
            command.CommandText = QueryHolder.GetVillainName;
            command.Parameters.AddWithValue("@villainId", id);

            var value = command.ExecuteScalar();
            if (value == null)
            {
                throw new ArgumentException("No such villain was found.");
            }

            var villainName = (string) value;

            command.CommandText = QueryHolder.DeleteMinionsVillains;
            var minionsCount = command.ExecuteNonQuery();

            command.CommandText = QueryHolder.DeleteVillain;
            command.ExecuteNonQuery();

            Console.WriteLine($"{villainName} was deleted.");
            Console.WriteLine($"{minionsCount} minions were released.");

            transaction.Commit();
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);

            try
            {
                transaction.Rollback();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

    }
}

