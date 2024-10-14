using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace snglrtycrvtureofspce_YandexCup.Classes;

internal static class ClayStatistics
{
    public static void ClayStatisticsMain()
    {
        var patients = new Dictionary<int, decimal>();
        decimal totalTemperature = 0;
        var patientCount = 0;

        while (true)
        {
            var input = Console.ReadLine();
            
            if (input == "!")
            {
                break;
            }

            if (input != null)
            {
                var parts = input.Split();

                switch (parts[0])
                {
                    case "+":
                    {
                        var id = int.Parse(parts[1]);
                        var temperature = decimal.Parse(parts[2], CultureInfo.InvariantCulture);
                        if (!patients.ContainsKey(id))
                        {
                            patients[id] = temperature;
                            totalTemperature += temperature;
                            patientCount++;
                        }
                        break;
                    }
                    case "~":
                    {
                        var id = int.Parse(parts[1]);
                        var newTemperature = decimal.Parse(parts[2], CultureInfo.InvariantCulture);
                        if (patients.TryGetValue(id, out var oldTemperature))
                        {
                            totalTemperature = totalTemperature - oldTemperature + newTemperature;
                            patients[id] = newTemperature;
                        }
                        break;
                    }
                    case "-":
                    {
                        var id = int.Parse(parts[1]);
                        if (patients.TryGetValue(id, out var oldTemperature))
                        {
                            totalTemperature -= oldTemperature;
                            patientCount--;
                            patients.Remove(id);
                        }
                        break;
                    }
                    case "?":
                    {
                        if (patientCount > 0)
                        {
                            var averageTemperature = totalTemperature / patientCount;
                            Console.WriteLine(averageTemperature.ToString("F9", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            Console.WriteLine("0.000000000");
                        }
                        break;
                    }
                }
            }
        }
    }
}