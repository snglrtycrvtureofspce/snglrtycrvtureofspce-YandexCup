using System;
using System.IO;

namespace snglrtycrvtureofspce_YandexCup_FamiliarizationContest.Classes;

internal static class Ab2
{
    public static void Ab2Main()
    {
        var lines = File.ReadAllLines("input.txt");
        
        var parts = lines[0].Split(' ');
        var a = long.Parse(parts[0]);
        var b = long.Parse(parts[1]);
        
        var sum = a + b;
        
        File.WriteAllText("output.txt", sum.ToString());
    }
}