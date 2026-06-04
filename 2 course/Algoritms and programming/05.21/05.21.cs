class Menu
{
    static void Main()
    {
        Run();
    }
    public static void Run()
    {
        Console.Write("Строки: ");
        int r = int.Parse(Console.ReadLine());
        Console.Write("Cтолбцы: ");
        int c = int.Parse(Console.ReadLine());

        int[,] m = new int[r, c];
        for (int i = 0; i < r; i++)
        {
            Console.Write($"{i+1}-я строка\n");
            for (int j = 0; j < c; j++)
            {
                m[i, j] = int.Parse(Console.ReadLine());
            }
        }
        var p = new Matrix(m, r, c);

        while (true)
        {
            Console.Write("1.Максимальное 2:Сумма больше произведения 3.Сортировка 4.Отрицательные в столбцах 5.0 в 1 0.Выход\n");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine(string.Join(" ", p.GetMax()));
                    break;
                case "2":
                    Console.WriteLine(p.Count());
                    break;
                case "3":
                    p.Sort();
                    p.Print();
                    break;
                case "4":
                    Console.WriteLine(p.CheckNeg());
                    break;
                case "5":
                    p.Replace();
                    p.Print(); break;
                case "0":
                    return;
            }
        }
    }
}

unsafe class Matrix
{
    private int[,] _m;
    private int _r, _c;

    public Matrix(int[,] matrix, int rows, int cols)
    {
        _m = matrix; _r = rows; _c = cols;
    }

    public void Print()
    {
        for (int i = 0; i < _r; i++)
        {
            for (int j = 0; j < _c; j++)
            {
                Console.Write(_m[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    // 1.Максимальные элементы строк
    public int[] GetMax()
    {
        int[] res = new int[_r];
        fixed (int* p = _m)
        {
            for (int i = 0; i < _r; i++)
            {
                int max = *(p + i * _c);
                for (int j = 1; j < _c; j++)
                    if (*(p + i * _c + j) > max)
                    {
                        max = *(p + i * _c + j);
                    }
                res[i] = max;
            }
        }
        return res;
    }

    // 2.Столбцы, где сумма > произведения
    public int Count()
    {
        int k = 0;
        fixed (int* p = _m)
        {
            for (int j = 0; j < _c; j++)
            {
                int s = 0, pr = 1;
                for (int i = 0; i < _r; i++)
                {
                    int v = *(p + i * _c + j);
                    s += v; pr *= v;
                }
                if (s > pr)
                {
                    k++;
                }
            }
        }
        return k;
    }

    // 3.Сортировка строк по убыванию минимальных элементов
    public void Sort()
    {
        fixed (int* p = _m)
        {
            int[] mins = new int[_r];
            for (int i = 0; i < _r; i++)
            {
                int min = *(p + i * _c);
                for (int j = 1; j < _c; j++)
                    if (*(p + i * _c + j) < min)
                    {
                        min = *(p + i * _c + j);
                    }
                mins[i] = min;
            }

            for (int i = 0; i < _r - 1; i++)
                for (int k = 0; k < _r - i - 1; k++)
                    if (mins[k] < mins[k + 1])
                    {
                        int tm = mins[k]; mins[k] = mins[k + 1]; mins[k + 1] = tm;
                        for (int j = 0; j < _c; j++)
                        {
                            int t = *(p + k * _c + j);
                            *(p + k * _c + j) = *(p + (k + 1) * _c + j);
                            *(p + (k + 1) * _c + j) = t;
                        }
                    }
        }
    }

    // 4.В каждом ли столбце есть отрицательные
    public bool CheckNeg()
    {
        fixed (int* p = _m)
        {
            for (int j = 0; j < _c; j++)
            {
                bool has = false;
                for (int i = 0; i < _r; i++)
                    if (*(p + i * _c + j) < 0)
                    {
                        has = true;
                        break;
                    }
                if (!has)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // 5.Замена на 1 в столбцах с нулевым произведением
    public void Replace()
    {
        fixed (int* p = _m)
        {
            for (int j = 0; j < _c; j++)
            {
                bool zero = false;
                for (int i = 0; i < _r; i++)
                    if (*(p + i * _c + j) == 0)
                    {
                        zero = true;
                        break;
                    }
                if (zero)
                    for (int i = 0; i < _r; i++){
                        *(p + i * _c + j) = 1;
                    }
            }
        }
    }
}