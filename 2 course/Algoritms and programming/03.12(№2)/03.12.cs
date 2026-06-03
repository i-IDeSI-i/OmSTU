using System;

namespace PhoneBookApp
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString() => $"Имя: {Name}, Адрес: {Address}";
    }

    class Program
    {
        // Ключ - телефон (string), Значение - объект UserInfo
        static Dictionary<string, UserInfo> phoneBook = new Dictionary<string, UserInfo>();

        static void Main()
        {
            AddEntry("89001112233", "Матвей", "Москва, ул. Пушкина");
            AddEntry("89995554422", "Егор", "Омск, ул. 20 лет РККА");
            AddEntry("88005553535", "Анна", "Екатеринбург, ул. Мира");

            ShowAllEntries();

            SearchPhone("89001112233");
            SearchPhone("80000000000"); // Несуществующий

            DeleteEntry("89995554422");
            DeleteEntry("80000000000"); // Попытка удалить то, чего нет

            ShowAllEntries();
        }

        static void AddEntry(string phone, string name, string address)
        {
            if (!phoneBook.ContainsKey(phone))
            {
                phoneBook.Add(phone, new UserInfo { Name = name, Address = address });
                Console.WriteLine($"Добавлено: {phone}");
            }
            else
            {
                Console.WriteLine($"Ошибка: Номер {phone} уже существует.");
            }
        }

        static void DeleteEntry(string phone)
        {
            if (phoneBook.ContainsKey(phone))
            {
                phoneBook.Remove(phone);
                Console.WriteLine($"Номер {phone} удален.");
            }
            else
            {
                Console.WriteLine($"Удаление невозможно: Номер {phone} не найден.");
            }
        }

        static void SearchPhone(string phone)
        {
            if (phoneBook.TryGetValue(phone, out UserInfo info))
            {
                Console.WriteLine($"Найдено: {info}");
            }
            else
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }
    }
}