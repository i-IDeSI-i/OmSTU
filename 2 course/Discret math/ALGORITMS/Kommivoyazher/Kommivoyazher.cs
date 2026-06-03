using System;
class Kommivoyazher
{
    const int Inf = 1_000_000_000;

    static int n;
    static int[,] w = null!;
    static int best = Inf;
    static int[]? bestRoute;
    static bool[] visited = null!;
    static int LowerBound(int cost)
    {
        if (cost >= best) return Inf;
        int lb = cost;

        for (int i = 0; i < n; i++)
        {
            if (visited[i]) continue;
            int minOut = Inf;
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                if (visited[j] && j != 0) continue;
                if (w[i, j] > 0)
                    minOut = Math.Min(minOut, w[i, j]);
            }
            if (minOut == Inf) return Inf;
            lb += minOut;
        }

        return lb;
    }

    static void Branch(int depth, int cost, int last, int[] path)
    {
        if (cost >= best) return;

        if (depth == n)
        {
            if (w[last, 0] > 0 && cost + w[last, 0] < best)
            {
                best = cost + w[last, 0];
                bestRoute = (int[])path.Clone();
            }
            return;
        }

        if (LowerBound(cost) >= best) return;

        for (int next = 0; next < n; next++)
        {
            if (visited[next]) continue;
            if (w[last, next] <= 0) continue;

            visited[next] = true;
            path[depth] = next;
            Branch(depth + 1, cost + w[last, next], next, path);
            visited[next] = false;
        }
    }

    static void Main()
    {
        Console.Write("n = ");
        n = int.Parse(Console.ReadLine()!);
        w = new int[n, n];
        Console.WriteLine($"Матрица {n}x{n}:");
        for (int i = 0; i < n; i++)
        {
            var a = Console.ReadLine()!.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < n; j++) w[i, j] = int.Parse(a[j]);
        }

        if (n == 1)
        {
            Console.WriteLine("Длина: 0\nМаршрут: 1 -> 1");
            return;
        }

        visited = new bool[n];
        visited[0] = true;
        var path = new int[n];
        path[0] = 0;
        best = Inf;
        bestRoute = null;

        Branch(1, 0, 0, path);

        if (bestRoute == null)
            Console.WriteLine("Тур не найден");
        else
        {
            Console.WriteLine($"Длина: {best}");
            Console.Write("Маршрут:");
            for (int i = 0; i < n; i++)
                Console.Write($" {bestRoute[i] + 1} ->");
            Console.WriteLine($" {bestRoute[0] + 1}");
        }
    }
}
