using System;
using System.Linq;

namespace snglrtycrvtureofspce_YandexCup.Classes;

internal static class TombEntrance
{
    private const int Mod = 23;

    public static void TombEntranceMain()
    {
        var n = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        var m = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

        var x = Console.ReadLine()?.Split().Select(int.Parse).ToArray();
        var b = Console.ReadLine()?.Split().Select(int.Parse).ToArray();

        if (x == null || b == null || x.Length != m || b.Length != m)
        {
            throw new InvalidOperationException("Invalid input data");
        }

        var matrix = new int[m, m + 1];
        
        for (var j = 0; j < m; j++)
        {
            for (var i = 0; i < m; i++)
            {
                matrix[j, i] = Power(x[j], i) % Mod;
            }

            matrix[j, m] = b[j];
        }
        
        var password = GaussMod(matrix, m, Mod);
        
        password = password.Concat(Enumerable.Repeat(0, n - m)).ToArray();
        
        var result = new string(password.Select(x =>
        {
            if (x < 0 || x >= Mod)
            {
                Console.Error.WriteLine(
                    $"Ошибка: значение {x} выходит за пределы допустимого диапазона [0, {Mod - 1}]");
            }

            return (char)('a' + x);
        }).ToArray());

        Console.WriteLine(result);
    }
    
    private static int Power(int baseVal, int exp)
    {
        var result = 1;
        for (var i = 0; i < exp; i++)
        {
            result = (result * baseVal) % Mod;
        }

        return result;
    }
    
    private static int[] GaussMod(int[,] matrix, int m, int mod)
    {
        var result = new int[m];

        for (var i = 0; i < m; i++)
        {
            if (matrix[i, i] == 0)
            {
                for (var j = i + 1; j < m; j++)
                {
                    if (matrix[j, i] != 0)
                    {
                        SwapRows(matrix, i, j, m + 1);
                        break;
                    }
                }
            }
            
            var inv = ModInverse(matrix[i, i], mod);
            for (var k = 0; k <= m; k++)
            {
                matrix[i, k] = (matrix[i, k] * inv) % mod;
            }
            
            for (var j = 0; j < m; j++)
            {
                if (i == j) continue;
                var factor = matrix[j, i];
                for (var k = 0; k <= m; k++)
                {
                    matrix[j, k] = (matrix[j, k] - factor * matrix[i, k] % mod + mod) % mod;
                    if (matrix[j, k] < 0) matrix[j, k] += mod;
                }
            }
        }
        
        for (var i = 0; i < m; i++)
        {
            result[i] = matrix[i, m];
        }
        
        return result.Select(x => (x + mod) % mod).ToArray();
    }
    
    private static int ModInverse(int a, int mod)
    {
        int m0 = mod, t, q;
        int x0 = 0, x1 = 1;

        if (mod == 1) return 0;

        while (a > 1)
        {
            q = a / mod;
            t = mod;
            
            mod = a % mod;
            a = t;
            t = x0;
            
            x0 = x1 - q * x0;
            x1 = t;
        }
        
        if (x1 < 0) x1 += m0;

        return x1;
    }
    
    private static void SwapRows(int[,] matrix, int row1, int row2, int cols)
    {
        for (var i = 0; i < cols; i++)
        {
            var temp = matrix[row1, i];
            matrix[row1, i] = matrix[row2, i];
            matrix[row2, i] = temp;
        }
    }
}