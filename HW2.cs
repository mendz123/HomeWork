// A utility to analyze text files and provide statistics
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FileAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                return;
            }

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }

            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");

                // Read the file content
                string content = File.ReadAllText(filePath);

                // TODO: Implement analysis functionality
                // 1. Count words
                char[] delimiters = { ' ', '\r', '\n', '\t' };
                string[] words = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                int wordCount = words.Length;
                Console.WriteLine($"Number of words: {wordCount}");

                // 2. Count characters (with and without whitespace)
                int charCountWithWhitespace = content.Length;
                int charCountWithoutWhitespace = content.Count(c => !char.IsWhiteSpace(c));
                Console.WriteLine($"Number of characters (with whitespace): {charCountWithWhitespace}");
                Console.WriteLine($"Number of characters (without whitespace): {charCountWithoutWhitespace}");

                // 3. Count sentences
                // A simple approach: count '.', '!', and '?' as sentence terminators
                int sentenceCount = content.Count(c => c == '.' || c == '!' || c == '?');

                // 4. Identify most common words
                var wordFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (var word in words)
                {
                    var cleanWord = new string(word.Where(char.IsLetterOrDigit).ToArray());
                    if (string.IsNullOrEmpty(cleanWord)) continue;
                    if (wordFrequency.ContainsKey(cleanWord))
                        wordFrequency[cleanWord]++;
                    else
                        wordFrequency[cleanWord] = 1;
                }
                var mostCommonWords = wordFrequency.OrderByDescending(kv => kv.Value)
                                                   .ThenBy(kv => kv.Key)
                                                   .Take(5)
                                                   .ToList();
                Console.WriteLine("Most common words:");
                foreach (var kv in mostCommonWords)
                {
                    Console.WriteLine($"  {kv.Key}: {kv.Value}");
                }

                // 5. Average word length
                double averageWordLength = wordCount > 0 ? words.Average(w => w.Length) : 0;
                Console.WriteLine($"Average word length: {averageWordLength:F2}");
                Console.WriteLine($"Number of sentences: {sentenceCount}");
                // 4. Identify most common words
                // 5. Average word length

                // Example implementation for counting lines:
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"Number of lines: {lineCount}");

                // TODO: Additional analysis to be implemented
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}