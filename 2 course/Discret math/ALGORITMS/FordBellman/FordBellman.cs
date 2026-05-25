// 4. Форд — Беллман
using System;
using System;

class FordBellman
{
    const long Inf = 1_000_000_000;

    static int[,] ReadMatrix(out int n, out int s)
    {
        Console.Write("n = ");
        n = int.Parse(Console.ReadLine()!);

        var m = new int[n, n];

        Console.WriteLine($"Матрица {n}x{n} (0 — нет ребра):");

        for (int i = 0; i < n; i++)
        {
            var a = Console.ReadLine()!
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < n; j++)
                m[i, j] = int.Parse(a[j]);
        }

        Console.Write($"Начальная вершина (1..{n}): ");
        s = int.Parse(Console.ReadLine()!) - 1;

        return m;
    }

    static void Run(int[,] w, int n, int s)
    {
        var d = new long[n];

        for (int i = 0; i < n; i++)
            d[i] = Inf;

        d[s] = 0;

        for (int k = 0; k < n - 1; k++)
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (w[i, j] != 0 &&
                        d[i] < Inf &&
                        d[i] + w[i, j] < d[j])
                    {
                        d[j] = d[i] + w[i, j];
                    }

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (w[i, j] != 0 &&
                    d[i] < Inf &&
                    d[i] + w[i, j] < d[j])
                {
                    Console.WriteLine("Цикл отрицательного веса");
                    return;
                }

        for (int i = 0; i < n; i++)
            Console.WriteLine(
                $"до {i + 1}: {(d[i] >= Inf ? "нет" : d[i])}"
            );
    }

    static void Main()
    {
        Run(ReadMatrix(out int n, out int s), n, s);
    }
}
