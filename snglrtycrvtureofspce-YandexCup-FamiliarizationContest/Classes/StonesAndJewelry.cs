using System;
using System.Collections.Generic;
using System.Linq;

namespace snglrtycrvtureofspce_YandexCup_FamiliarizationContest.Classes;

internal static class StonesAndJewelry
{
    public static void StonesAndJewelryMain()
    {
        var j = Console.ReadLine();
        var s = Console.ReadLine();

        if (j != null)
        {
            var jewels = new HashSet<char>(j);
            if (s != null)
            {
                var result = s.Count(ch => jewels.Contains(ch));
            
                Console.WriteLine(result);
            }
        }
    }
}