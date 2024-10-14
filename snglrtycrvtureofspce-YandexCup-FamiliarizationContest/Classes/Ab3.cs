using System;

namespace snglrtycrvtureofspce_YandexCup_FamiliarizationContest.Classes;

internal static class Ab3
{
    public static void Ab3Main()
    {
        var input = Console.ReadLine();

        if (input != null)
        {
            var parts = input.Split(' ');
            var a = long.Parse(parts[0]);
            var b = long.Parse(parts[1]);
        
            var sum = a + b;
        
            Console.WriteLine(sum);
        }
    }
}