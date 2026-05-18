// 1. Прим — минимальное остовное дерево (матрица смежности: 0 — нет ребра, иначе вес)
using System;
using System.Linq;

class Prima
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

    static int Prim(int[,] w, int n)
    {
        var inTree = new bool[n];
        var minE = Enumerable.Repeat(int.MaxValue, n).ToArray();
        var parent = Enumerable.Repeat(-1, n).ToArray();
        minE[0] = 0;
        int sum = 0;
        for (int k = 0; k < n; k++)
        {
            int v = -1;
            for (int i = 0; i < n; i++)
                if (!inTree[i] && (v < 0 || minE[i] < minE[v])) v = i;
            if (minE[v] == int.MaxValue) return -1;
            inTree[v] = true;
            if (parent[v] >= 0) { sum += minE[v]; Console.WriteLine($"{parent[v]+1} — {v+1}, вес {minE[v]}"); }
            for (int u = 0; u < n; u++)
            {
                int c = w[v, u] != 0 ? w[v, u] : w[u, v];
                if (!inTree[u] && c > 0 && c < minE[u]) { minE[u] = c; parent[u] = v; }
            }
        }
        return sum;
    }

    static void Main()
    {
        var w = ReadMatrix(out int n);
        int s = Prim(w, n);
        Console.WriteLine(s < 0 ? "Граф несвязный" : $"Суммарный вес: {s}");
    }
}
