public class Menu
{
string[] ReadLinesFromUser()
{
Console.Write("Введите имя файла: ");
string fileName = Console.ReadLine();

if (!File.Exists(fileName))
{
Console.WriteLine($"Ошибка: файл '{fileName}' не найден в текущей директории.");
return null;
}
return File.ReadAllLines(fileName);
}

public void DoTask1()
{
string[] lines = ReadLinesFromUser();
if (lines == null || lines.Length == 0) return;

int maxLen = 0;
var bestLines = new List<string>();

foreach (string line in lines)
{
int currentMax = 1;
int currentStreak = 1;

for (int i = 1; i < line.Length; i++)
{
if (line[i] == line[i - 1])
{
currentStreak++;
}
else
{
if (currentStreak > currentMax) currentMax = currentStreak;
currentStreak = 1;
}
}
if (currentStreak > currentMax) currentMax = currentStreak;

if (currentMax > maxLen)
{
maxLen = currentMax;
bestLines.Clear();
bestLines.Add(line);
}
else if (currentMax == maxLen)
{
bestLines.Add(line);
}
}

Console.WriteLine($"Наибольшая длина подпоследовательности: {maxLen}");
Console.WriteLine("Строки:");
foreach (string line in bestLines)
{
Console.WriteLine(line);
}
}

public void DoTask2()
{
string[] lines = ReadLinesFromUser();
if (lines == null || lines.Length == 0) return;

foreach (string line in lines)
{
int[] row = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
.Select(int.Parse)
.ToArray();

bool isAscending = true;

for (int i = 0; i < row.Length - 1; i++)
{
if (row[i] > row[i + 1])
{
isAscending = false;
break;
}
}

if (!isAscending)
{
Console.WriteLine(line);
}
}
}

}

class Program
{
static void Main()
{
Menu menu = new Menu();
Console.WriteLine("Задача 1");
menu.DoTask1();
Console.WriteLine("Задача 2");
menu.DoTask2();
}
}