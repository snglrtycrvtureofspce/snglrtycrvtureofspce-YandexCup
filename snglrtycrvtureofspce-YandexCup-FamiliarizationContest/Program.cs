using snglrtycrvtureofspce_YandexCup_FamiliarizationContest.Classes;

namespace snglrtycrvtureofspce_YandexCup_FamiliarizationContest;

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
                Ab1.Ab1Main();
                break;
            }
            case "2":
            {
                Ab2.Ab2Main();
                break;
            }
            case "3":
            {
                Ab3.Ab3Main();
                break;
            }
            case "4":
            {
                StonesAndJewelry.StonesAndJewelryMain();
                break;
            }
        }
    }
}