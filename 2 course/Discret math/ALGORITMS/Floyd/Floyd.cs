// 5. Флойд 
using System;

class Floyd
{
    const long Inf = 1_000_000_000;

    static int[,] ReadMatrix(out int n)
    {
        Console.Write("Количество вершин n = ");
        n = int.Parse(Console.ReadLine()!);

        var m = new int[n, n];

        Console.WriteLine($"Введите матрицу смежности {n}x{n}:");
        Console.WriteLine("0 означает отсутствие ребра");

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Строка для вершины {i + 1}: ");

            var a = Console.ReadLine()!
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < n; j++)
                m[i, j] = int.Parse(a[j]);
        }

        return m;
    }

    static long[,] FloydWarshall(int[,] w, int n)
    {
        var d = new long[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                    d[i, j] = 0;
                else if (w[i, j] > 0)
                    d[i, j] = w[i, j];
                else
                    d[i, j] = Inf;
            }
        }

        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (d[i, k] + d[k, j] < d[i, j])
                        d[i, j] = d[i, k] + d[k, j];
                }
            }
        }

        return d;
    }

    static void PrintResult(long[,] d, int n)
    {
        Console.WriteLine("\nМатрица кратчайших расстояний:");

        Console.Write("     ");
        for (int j = 0; j < n; j++)
            Console.Write($"{j + 1,5}");

        Console.WriteLine();

        for (int i = 0; i < n; i++)
        {
            Console.Write($"{i + 1,5}");

            for (int j = 0; j < n; j++)
            {
                if (d[i, j] >= Inf)
                    Console.Write($"{"inf",5}");
                else
                    Console.Write($"{d[i, j],5}");
            }

            Console.WriteLine();
        }
    }

    static void Main()
    {
        var w = ReadMatrix(out int n);

        var d = FloydWarshall(w, n);

        PrintResult(d, n);
    }
}