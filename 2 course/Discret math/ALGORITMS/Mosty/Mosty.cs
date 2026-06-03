// Поиск мостов (алгоритм Тарьяна)
using System;
using System.Collections.Generic;

class Mosty
{
    static void Dfs(int v, int[,] g, int n, int[] tin, int[] low, bool[] used, ref int timer, List<(int, int)> bridges)
    {
        used[v] = true;
        tin[v] = low[v] = ++timer;
        for (int u = 0; u < n; u++)
        {
            if (g[v, u] == 0 && g[u, v] == 0) continue;
            if (!used[u])
            {
                Dfs(u, g, n, tin, low, used, ref timer, bridges);
                low[v] = Math.Min(low[v], low[u]);
                if (low[u] > tin[v])
                    bridges.Add((Math.Min(v, u), Math.Max(v, u)));
            }
            else if (u != v)
                low[v] = Math.Min(low[v], tin[u]);
        }
    }

    static void Main()
    {
        Console.Write("n = ");
        int n = int.Parse(Console.ReadLine()!);
        var g = new int[n, n];
        Console.WriteLine($"Матрица {n}x{n}:");
        for (int i = 0; i < n; i++)
        {
            var a = Console.ReadLine()!.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < n; j++) g[i, j] = int.Parse(a[j]);
        }
        var tin = new int[n];
        var low = new int[n];
        var used = new bool[n];
        int timer = 0;
        var bridges = new List<(int, int)>();

        for (int i = 0; i < n; i++)
            if (!used[i])
                Dfs(i, g, n, tin, low, used, ref timer, bridges);

        if (bridges.Count == 0)
            Console.WriteLine("Мостов нет");
        else
        {
            Console.WriteLine("Мосты:");
            foreach (var (a, b) in bridges)
                Console.WriteLine($"  {a + 1} — {b + 1}");
        }
    }
}
