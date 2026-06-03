using System;
using System.Collections.Generic;

class RobertFloris
{
    static readonly List<List<int>> paths = new();

    static void Search(int[,] g, int n, int v, List<int> path, bool[] used)
    {
        if (path.Count == n)
        {
            paths.Add(new List<int>(path));
            return;
        }
        for (int u = 0; u < n; u++)
            if (g[v, u] == 1 && !used[u])
            {
                used[u] = true;
                path.Add(u);
                Search(g, n, u, path, used);
                path.RemoveAt(path.Count - 1);
                used[u] = false;
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
        Console.Write($"Начальная вершина пути (1..{n}): ");
        int start = int.Parse(Console.ReadLine()!) - 1;

        paths.Clear();
        var used = new bool[n];
        used[start] = true;
        Search(g, n, start, new List<int> { start }, used);

        if (paths.Count == 0)
            Console.WriteLine("Гамильтоновы пути не найдены");
        else
            foreach (var p in paths)
                Console.WriteLine(string.Join(" -> ", p.ConvertAll(x => x + 1)));
    }
}
