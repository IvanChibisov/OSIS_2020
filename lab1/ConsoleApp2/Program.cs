using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static string[] SearchDirectory(string path) 
        {
            string[] searchResult = Directory.GetDirectories(path);
            return searchResult;
        }

        static string[] SearchFiles(string path, string pattern) 
        {
            string[] searchResult = Directory.GetFiles(path, pattern, SearchOption.AllDirectories);
            return searchResult;
        }

        static List<string> FoundFiles(string[] result, string pathForSearch)
        {
            string printRes = "\nFound files on " + pathForSearch + " :\n";
            List<string> allFiles = new List<string>();
            foreach (string folder in result)
            {
                try
                {
                    string[] files = SearchFiles(folder, "*text*" + "*.txt"); 
                    foreach (string file in files)
                    {
                        printRes += file + "\n";
                        allFiles.Add(file);
                    }
                }
                catch
                {
                    Console.WriteLine("Access denied: " + folder);
                }
            }
            if (allFiles.Count != 0)
                Console.WriteLine(printRes);
            else Console.WriteLine("\nNothing found");
            Console.WriteLine("- - - - - - - - - - - -");
            return allFiles;
        }

        static void WriteResult(string resultFile, List<string> allFiles)
        {
            List<string> resultText = new List<string>();
            foreach (string file in allFiles)
            {
                resultText.AddRange(File.ReadAllLines(file));
            }
            File.AppendAllLines(resultFile, resultText);
        }

        static void DriveSearch(string pathForSearch, string resultFile)
        {
            Console.WriteLine($"Searching files on {pathForSearch} drive...");
            string[] result = SearchDirectory(pathForSearch);
            List<string> allFiles = FoundFiles(result, pathForSearch);
            WriteResult(resultFile, allFiles);
        }

        static void Main(string[] args)
        {
            DriveSearch("E:\\", "E:\\OSIS_2020\\lab1\\result.txt");
            DriveSearch("C:\\", "E:\\OSIS_2020\\lab1\\result.txt");
            Console.ReadKey();
        }
    }
}
