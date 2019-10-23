public static class QueryHolder
{
    public const string GetVillainId = @"SELECT Id FROM Villains WHERE Name = @Name";

    public const string GetMinionId = @"SELECT Id FROM Minions WHERE Name = @Name";

    public const string GetTownId = @"SELECT Id FROM Towns WHERE Name = @townName";

    public const string InsertMinionsVillains =
        @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";

    public const string InsertVillains = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";

    public const string InsertMinions = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@nam, @age, @townId)";

    public const string InsertTowns = @"INSERT INTO Towns (Name) VALUES (@townName)";

}

