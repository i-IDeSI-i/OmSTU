using System;
using System.Collections.Generic;

class FordFulkersonBezRaspredeleniya
{
    static int[,] ReadCapacity(out int n, out int s, out int t)
    {
        Console.Write("n = ");
        n = int.Parse(Console.ReadLine()!);
        var cap = new int[n, n];
        Console.WriteLine($"Матрица {n}x{n}:");
        for (int i = 0; i < n; i++)
        {
            var a = Console.ReadLine()!.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < n; j++) cap[i, j] = int.Parse(a[j]);
        }
        Console.Write($"Исток (1..{n}): ");
        s = int.Parse(Console.ReadLine()!) - 1;
        Console.Write($"Сток (1..{n}): ");
        t = int.Parse(Console.ReadLine()!) - 1;
        if (s == t)
        {
            Console.WriteLine("Исток и сток должны различаться");
            Environment.Exit(1);
        }
        return cap;
    }
    static bool DfsPath(int[,] r, int n, int v, int t, int[] parent, bool[] vis)
    {
        if (v == t) return true;
        vis[v] = true;
        for (int u = 0; u < n; u++)
        {
            if (!vis[u] && r[v, u] > 0 && DfsPath(r, n, u, t, parent, vis))
            {
                parent[u] = v;
                return true;
            }
        }
        return false;
    }

    static int MaxFlow(int[,] cap, int n, int s, int t)
    {
        var r = (int[,])cap.Clone();
        int flow = 0;
        var parent = new int[n];
        while (true)
        {
            Array.Fill(parent, -1);
            if (!DfsPath(r, n, s, t, parent, new bool[n])) break;
            int f = int.MaxValue;
            for (int v = t; v != s; v = parent[v])
                f = Math.Min(f, r[parent[v], v]);
            for (int v = t; v != s; v = parent[v])
            {
                int u = parent[v];
                r[u, v] -= f;
                r[v, u] += f;
            }
            flow += f;
        }
        return flow;
    }

    static void Main()
    {
        var cap = ReadCapacity(out int n, out int s, out int t);
        Console.WriteLine($"Максимальный поток: {MaxFlow(cap, n, s, t)}");
    }
}
