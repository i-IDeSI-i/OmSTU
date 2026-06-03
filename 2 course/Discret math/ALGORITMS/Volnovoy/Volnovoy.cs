using System;
using System.Collections.Generic;

class Volnovoy
{
    static void Run(int[,] g, int n, int s)
    {
        var dist = new int[n];
        Array.Fill(dist, -1);
        var q = new Queue<int>();
        dist[s] = 0;
        q.Enqueue(s);

        while (q.Count > 0)
        {
            int v = q.Dequeue();
            for (int u = 0; u < n; u++)
            {
                if (g[v, u] == 0 && g[u, v] == 0) continue;
                if (dist[u] >= 0) continue;
                dist[u] = dist[v] + 1;
                q.Enqueue(u);
            }
        }

        Console.WriteLine($"\nРасстояния от вершины {s + 1} (номер волны):");
        for (int i = 0; i < n; i++)
            Console.WriteLine($"  до {i + 1}: {(dist[i] < 0 ? "недостижима" : dist[i].ToString())}");
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
        Console.Write($"Начальная вершина (1..{n}): ");
        int s = int.Parse(Console.ReadLine()!) - 1;
        Run(g, n, s);
    }
}
