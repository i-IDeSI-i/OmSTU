// 2. Краскал — минимальное остовное дерево
using System;
using System.Collections.Generic;
using System.Linq;

class Kraskal
{
    static int[,] ReadMatrix(out int n)
    {
        Console.Write("n = ");
        n = int.Parse(Console.ReadLine()!);
        var m = new int[n, n];
        Console.WriteLine($"Матрица {n}x{n}:");
        for (int i = 0; i < n; i++)
        {
            var a = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < n; j++) m[i, j] = int.Parse(a[j]);
        }
        return m;
    }

    static int Find(int[] p, int x) => p[x] == x ? x : p[x] = Find(p, p[x]);

    static int Kruskal(int[,] w, int n)
    {
        var edges = new List<(int u, int v, int c)>();
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                int c = w[i, j] != 0 ? w[i, j] : w[j, i];
                if (c > 0) edges.Add((i, j, c));
            }
        edges.Sort((a, b) => a.c.CompareTo(b.c));
        var p = Enumerable.Range(0, n).ToArray();
        int sum = 0, cnt = 0;
        foreach (var (u, v, c) in edges)
        {
            int ru = Find(p, u), rv = Find(p, v);
            if (ru == rv) continue;
            p[ru] = rv;
            Console.WriteLine($"{u+1} — {v+1}, вес {c}");
            sum += c;
            if (++cnt == n - 1) break;
        }
        return cnt == n - 1 ? sum : -1;
    }

    static void Main()
    {
        var w = ReadMatrix(out int n);
        int s = Kruskal(w, n);
        Console.WriteLine(s < 0 ? "Граф несвязный" : $"Суммарный вес: {s}");
    }
}
