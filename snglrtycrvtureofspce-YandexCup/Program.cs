using snglrtycrvtureofspce_YandexCup.Classes;

namespace snglrtycrvtureofspce_YandexCup;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("Enter task:");
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
            {
                ClayStatistics.ClayStatisticsMain();
                break;
            }
            case "2":
            {
                TombEntrance.TombEntranceMain();
                break;
            }
            case "3":
            {
                UnloadingOfShips.UnloadingOfShipsMain();
                break;
            }
            case "4":
            {
                ElamArchives.ElamArchivesMain(new string[] { }).GetAwaiter().GetResult();
                break;
            }
        }
    }
}