using System;
using System.Data.SqlClient;
using System.Linq;

class Program
{
    public const string ConnectionStr = @"Server=.\SQLEXPRESS;Database=MinionsDb;Integrated Security=True";

    static void Main()
    {
        var minionInfo = Console.ReadLine().Split()[1..];
        var villainName = Console.ReadLine().Split()[1];

        var minionName = minionInfo[0];
        var minionAge = int.Parse(minionInfo[1]);
        var minionTown = minionInfo[2];

        using var connection = new SqlConnection(ConnectionStr);
        connection.Open();

        var getMinionIdCommand = new SqlCommand(QueryHolder.GetMinionId, connection);
        getMinionIdCommand.Parameters.AddWithValue("@Name", minionName);

        var getTownIdCommand = new SqlCommand(QueryHolder.GetTownId, connection);
        getTownIdCommand.Parameters.AddWithValue("@townName", minionTown);

        var getVillainIdCommand = new SqlCommand(QueryHolder.GetVillainId, connection);
        getVillainIdCommand.Parameters.AddWithValue("@Name", villainName);

        var townId = GetId(getTownIdCommand);

        if (townId == -1)
        {
            var insertTownCommand = new SqlCommand(QueryHolder.InsertTowns, connection);
            insertTownCommand.Parameters.AddWithValue("@townName", minionTown);

            insertTownCommand.ExecuteNonQuery();

            Console.WriteLine($"Town {minionTown} was added to the database.");

            townId = GetId(getTownIdCommand);
        }

        var villainId = GetId(getVillainIdCommand);

        if (villainId == -1)
        {
            var insertVillainCommand = new SqlCommand(QueryHolder.InsertVillains, connection);
            insertVillainCommand.Parameters.AddWithValue("@villainName", villainName);

            insertVillainCommand.ExecuteNonQuery();

            Console.WriteLine($"Villain {villainName} was added to the database.");

            villainId = GetId(getVillainIdCommand);
        }

        var insertMinionCommand = new SqlCommand(QueryHolder.InsertMinions, connection);
        insertMinionCommand.Parameters.AddWithValue("@nam", minionName);
        insertMinionCommand.Parameters.AddWithValue("@age", minionAge);
        insertMinionCommand.Parameters.AddWithValue("@townId", townId);

        insertMinionCommand.ExecuteNonQuery();

        var minionId = GetId(getMinionIdCommand);

        var insertMinionVillainCommand = new SqlCommand(QueryHolder.InsertMinionsVillains, connection);
        insertMinionVillainCommand.Parameters.AddWithValue("@villainId", villainId);
        insertMinionVillainCommand.Parameters.AddWithValue("@minionId", minionId);

        insertMinionVillainCommand.ExecuteNonQuery();

        Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
    }

    private static int GetId(SqlCommand getCommand)
    {
        if (getCommand.ExecuteScalar() != null)
        {
            return (int) getCommand.ExecuteScalar();
        }

        return -1;
    }
}
