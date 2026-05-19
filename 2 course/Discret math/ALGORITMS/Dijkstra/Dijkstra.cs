// 3. Дейкстра — кратчайшие пути от одной вершины
using System;
using System.Collections.Generic;

class Dijkstra
{
    const int Inf = int.MaxValue / 4;

    static int[,] ReadMatrix(out int n, out int s)
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
        Console.Write("Начальная вершина: ");
        s = int.Parse(Console.ReadLine()!) - 1;

        if (s < 0 || s >= n)
        {
            Console.WriteLine("Неверный номер вершины");
            Environment.Exit(0);
        }
        return m;
    }

    static void Run(int[,] w, int n, int s)
    {
        var d = new int[n];
        var used = new bool[n];
        var par = new int[n];
        for (int i = 0; i < n; i++) { d[i] = Inf; par[i] = -1; }
        d[s] = 0;
        for (int k = 0; k < n; k++)
        {
            int v = -1;
            for (int i = 0; i < n; i++)
                if (!used[i] && (v < 0 || d[i] < d[v])) v = i;
            if (v < 0 || d[v] >= Inf) break;
            used[v] = true;
            for (int u = 0; u < n; u++)
                if (w[v, u] > 0 && d[v] + w[v, u] < d[u])
                { d[u] = d[v] + w[v, u]; par[u] = v; }
        }
        for (int i = 0; i < n; i++)
        {
            Console.Write($"до {i + 1}: ");
            if (d[i] >= Inf) { Console.WriteLine("нет"); continue; }
            Console.WriteLine(d[i]);
            if (i == s) continue;
            var path = new List<int>();
            for (int x = i; x != -1; x = par[x]) path.Add(x);
            path.Reverse();
            Console.WriteLine("  " + string.Join(" -> ", path.ConvertAll(x => x + 1)));
        }
    }

    static void Main()
    {
        Run(ReadMatrix(out int n, out int s), n, s);
    }
}
