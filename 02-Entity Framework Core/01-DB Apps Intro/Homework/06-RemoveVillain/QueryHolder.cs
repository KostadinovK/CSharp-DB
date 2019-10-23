using System;
using System.Collections.Generic;
using System.Text;

public static class QueryHolder
{
    public const string GetVillainName = @"SELECT Name FROM Villains WHERE Id = @villainId";

    public const string DeleteMinionsVillains =
        @"DELETE FROM MinionsVillains 
        WHERE VillainId = @villainId";

    public const string DeleteVillain =
        @"DELETE FROM Villains
        WHERE Id = @villainId";
   
}

