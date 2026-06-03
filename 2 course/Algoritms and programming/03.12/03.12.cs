using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "input.txt";


        string text = File.ReadAllText(filePath).ToLower();
        string[] words = text.Split();

        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

        foreach (string word in words)
        {
            if (wordCounts.ContainsKey(word))
            {
                wordCounts[word]++;
            }
            else
            {
                wordCounts[word] = 1;
            }
        }

        foreach (var pair in wordCounts)
        {
            Console.WriteLine($"{pair.Key} | {pair.Value}");
        }
   
    }
}