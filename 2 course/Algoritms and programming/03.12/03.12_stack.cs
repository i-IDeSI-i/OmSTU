using System;
using System.Collections.Generic;

namespace Stack
{
    // Обобщенный класс стека
    public class Stack<T>
    {
        private List<T> _items = new List<T>();

        public bool IsEmpty => _items.Count == 0;

        public void Push(T item)
        {
            _items.Add(item);
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Стек пуст.");
            T lastItem = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);
            return lastItem;
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Стек пуст.");

            return _items[_items.Count - 1];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Stack<int> intStack1 = new Stack<int>();
            Stack<int> intStack2 = new Stack<int>();

            Stack<string> stringStack1 = new Stack<string>();
            Stack<string> stringStack2 = new Stack<string>();

            intStack1.Push(10);
            intStack1.Push(20);
            Console.WriteLine($"Верхний элемент: {intStack1.Peek()}");
            Console.WriteLine($"Извлечено: {intStack1.Pop()}");
            Console.WriteLine($"Пуст? {intStack1.IsEmpty}");

            stringStack1.Push("Привет");
            stringStack1.Push("Мир");
            Console.WriteLine($"Извлечено: {stringStack1.Pop()}");
            Console.WriteLine($"Текущий верхний: {stringStack1.Peek()}");

            intStack2.Push(100);
            stringStack2.Push("Второй строковый стек");

            Console.WriteLine($"В intStack2: {intStack2.Peek()}");
            Console.WriteLine($"В stringStack2: {stringStack2.Peek()}");
        }
    }
}