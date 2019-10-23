public static class QueryHolder
{
    public const string GetVillainName =
        @"SELECT Name FROM Villains WHERE Id = @Id";

    public const string GetMinionsByVillain =
        @"SELECT 
            ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
            m.Name, 
            m.Age
        FROM MinionsVillains AS mv
        JOIN Minions As m ON mv.MinionId = m.Id
        WHERE mv.VillainId = @Id
        ORDER BY m.Name";
}

