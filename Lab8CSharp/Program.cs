using System;
using System.IO;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;


namespace Lab8CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab#8 ");
            Console.WriteLine("What task do you want?");
            Console.WriteLine("1. Task 1");
            Console.WriteLine("2. Task 2");
            Console.WriteLine("3. Task 3");
            Console.WriteLine("4. Task 4");
            Console.WriteLine("5. Task 5");
            Console.WriteLine("6. Exit");

            int choice;
            bool isValidChoice = false;

            do
            {
                Console.Write("Enter number of task ");
                isValidChoice = int.TryParse(Console.ReadLine(), out choice);

                if (!isValidChoice || choice < 1 || choice > 7)
                {
                    Console.WriteLine("This task not exist");
                    isValidChoice = false;
                }
            } while (!isValidChoice);
            switch (choice)
            {
                case 1:
                    task1();
                    break;
                case 2:
                    task2();
                    break;
                case 3:
                    task3();
                    break;
                case 4:
                    task4();
                    break;
                case 5:
                    task5();
                    break;
                case 6:
                    break;
            }
        }

        static void task1()
        {
            Console.Write("Task 1\n");
            string inputFilePath = "input.txt";
            string text = File.ReadAllText(inputFilePath);

            Regex regex = new Regex(@"\b[\w.%+-]+@gmail.com");

            MatchCollection matches = regex.Matches(text);

            string outputFilePath = "output_task1.txt";
            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                writer.WriteLine("Found email addresses:");

                foreach (Match match in matches)
                {
                    writer.WriteLine(match.Value);
                }

                writer.WriteLine($"Total count: {matches.Count}");
            }

            Console.WriteLine("Operation completed. Results written to output.txt");
        }


        static void task2()
        {
            Console.Write("Task 2\n");
            string inputFilePath = "input.txt";
            string text = File.ReadAllText(inputFilePath);

            text = Regex.Replace(text, @"\b[0-9a-fA-F]+\b", "+");

            string outputFilePath = "output_task2.txt";
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            File.WriteAllText(outputFilePath, text);

            Console.WriteLine("Operation completed. Results written to output_task2.txt");
        }

        static string RemoveMiddleChar(string word)
        {
            if (word.Length % 2 != 0)
            {
                int middleIndex = word.Length / 2;
                return word.Remove(middleIndex, 1);
            }
            return word;
        }

        static string RepChar(string[] words)
        {
            string result = "";

            foreach (string word in words)
            {
                result += RemoveMiddleChar(word) + " ";
            }

            return result.Trim();
        }

        static void task3()
        {
            Console.Write("Task 3\n");
            string inputFilePath = "input.txt";
            string[] words = File.ReadAllText(inputFilePath).Split(new[] { ' ', ',', '.', ';', ':', '-', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            string modifiedText = RepChar(words);

            string outputFilePath = "output_task3.txt";
            File.WriteAllText(outputFilePath, modifiedText);

            Console.WriteLine($"Result has been written to {outputFilePath}");
        }
        static void task4()
        {
            Console.WriteLine("Task 4\n");
            string inputFilePath = "input.txt";
            string text = File.ReadAllText(inputFilePath);

            // Розділити текст на слова
            string[] separators = { " ", ",", ".", ";", ":", "-", "\n", "\r", "\t" };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            List<string> matchingWords = new List<string>();

            foreach (string word in words)
            {
                if (word.Length > 1 && word[0] == word[word.Length - 1])
                {
                    matchingWords.Add(word);
                }
            }

            string outputFilePath = "output_task4.txt";
            File.WriteAllLines(outputFilePath, matchingWords);

            Console.WriteLine($"Operation completed. Results written to {outputFilePath}");
        }

        static void PrintFileInfo(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine($"File Name: {fileInfo.Name}");
            Console.WriteLine($"Directory: {fileInfo.DirectoryName}");
            Console.WriteLine($"Size (bytes): {fileInfo.Length}");
            Console.WriteLine($"Creation Time: {fileInfo.CreationTime}");
            Console.WriteLine($"Last Access Time: {fileInfo.LastAccessTime}");
            Console.WriteLine($"Last Write Time: {fileInfo.LastWriteTime}");
            Console.WriteLine();
        }

        static void task5()
        {
            Console.WriteLine("Task 5\n");
            string studentName = "Pashniak";
            string folder1Path = $"C:\\TEMP\\{studentName}1";
            string folder2Path = $"C:\\TEMP\\{studentName}2";
            string allFolderPath = $"C:\\TEMP\\ALL";

            // Task1
            Directory.CreateDirectory(folder1Path);
            Directory.CreateDirectory(folder2Path);

            // Task2
            string t1FilePath = Path.Combine(folder1Path, "t1.txt");
            string t2FilePath = Path.Combine(folder1Path, "t2.txt");

            string t1Text = "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми";
            string t2Text = "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ";

            File.WriteAllText(t1FilePath, t1Text);
            File.WriteAllText(t2FilePath, t2Text);

            // Task3
            string t3FilePath = Path.Combine(folder2Path, "t3.txt");
            File.WriteAllText(t3FilePath, File.ReadAllText(t1FilePath) + "\n" + File.ReadAllText(t2FilePath));

            // Task4
            PrintFileInfo(t1FilePath);
            PrintFileInfo(t2FilePath);
            PrintFileInfo(t3FilePath);

            // Task5
            string moveT2FilePath = Path.Combine(folder2Path, "t2.txt");
            if (File.Exists(moveT2FilePath))
            {
                File.Delete(moveT2FilePath);
            }
            File.Move(t2FilePath, moveT2FilePath);

            // Task6
            string copyT1FilePath = Path.Combine(folder2Path, "t1.txt");
            File.Copy(t1FilePath, copyT1FilePath);

            // Task7
            if (Directory.Exists(allFolderPath))
            {
                Directory.Delete(allFolderPath, true);
            }
            Directory.Move(folder1Path, allFolderPath);

            // Task8
            Console.WriteLine("\nFiles in All directory:");
            string[] filesInAll = Directory.GetFiles(allFolderPath);
            foreach (string file in filesInAll)
            {
                PrintFileInfo(file);
            }
        }

    }
}

