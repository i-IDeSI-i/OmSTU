using System;

public class PolskaCalculator
{
    // ключ - строка-оператор, значение - функция, принимающая два числа
    private Dictionary<string, Func<int, int, int>> _operators =
        new Dictionary<string, Func<int, int, int>> {
            { "+", (a, b) => a + b },
            { "-", (a, b) => a - b },
            { "*", (a, b) => a * b },
            { "/", (a, b) => {
                if (b == 0) throw new DivideByZeroException("Деление на ноль невозможно.");
                return a / b;
            }}
        };

    public int PolskaCalculate(string[] symbols)
    {
        Stack<int> stack = new Stack<int>();
        foreach (string sym in symbols)
        {
            if (_operators.ContainsKey(sym))
            {
                // ПРОВЕРКА: Хватает ли операндов в стеке?
                if (stack.Count < 2)
                {
                    throw new InvalidOperationException($"Недостаточно чисел в стеке для операции '{sym}'.");
                }

                int b = stack.Pop();
                int a = stack.Pop();

                stack.Push(_operators[sym](a, b));
            }
            else
            {
                // ПРОВЕРКА: Действительно ли это число?
                if (int.TryParse(sym, out int number))
                {
                    stack.Push(number);
                }
                else
                {
                    throw new ArgumentException($"Некорректный символ: {sym}");
                }
            }
        }

        // ПРОВЕРКА: Не осталось ли лишних чисел?
        if (stack.Count != 1)
        {
            throw new InvalidOperationException("Выражение некорректно: в стеке осталось лишнее количество чисел.");
        }

        return stack.Pop();
    }
}
class Program
{
    static void Main()
    {
        var calc = new PolskaCalculator();

        string[] exp1 = { "2", "3", "4", "*", "+" };
        Console.WriteLine($"Результат 1: {calc.PolskaCalculate(exp1)}");

        string[] exp2 = { "4", "5", "*", "6", "+", "7", "9", "*", "+" };
        Console.WriteLine($"Результат 2: {calc.PolskaCalculate(exp2)}");

        string[] exp3 = { "5" ,"0" , "/" };
        calc.PolskaCalculate(exp3);
    }
}