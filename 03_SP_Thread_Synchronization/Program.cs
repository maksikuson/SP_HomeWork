using System;
using System.IO;
using System.Linq;
using System.Threading;


namespace _03_SP_Thread_Synchronization
{
    internal class Program
    {
        public class AnalysisResult
        {
            public int Words { get; set; }
            public int Lines { get; set; }
            public int Punctuation { get; set; }
        }

        private static AnalysisResult _globalResult = new AnalysisResult();
        private static object _lockObject = new object();

        static void Main(string[] args)
        {
            string directoryPath = @"C:\Users\maksi\OneDrive";

            string[] files = Directory.GetFiles(directoryPath, "*.txt");

            Thread[] threads = new Thread[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                string filePath = files[i];
                threads[i] = new Thread(() => AnalyzeFile(filePath));
                threads[i].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"Total number of words : {_globalResult.Words}");
            Console.WriteLine($"Total number of lines : {_globalResult.Lines}");
            Console.WriteLine($"Total number of punctuation marks : {_globalResult.Punctuation}");
        }

        static void AnalyzeFile(string filePath)
        {
            var localResult = new AnalysisResult();

            string[] lines = File.ReadAllLines(filePath);
            localResult.Lines = lines.Length;

            foreach (string line in lines)
            {
                localResult.Words += line.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
                localResult.Punctuation += line.Count(c => IsPunctuation(c));
            }

            lock (_lockObject)
            {
                _globalResult.Words += localResult.Words;
                _globalResult.Lines += localResult.Lines;
                _globalResult.Punctuation += localResult.Punctuation;
            }
        }

        static bool IsPunctuation(char c)
        {
            char[] punctuationMarks = { '.', ',', ';', ':', '-', '—', '‒', '…', '!', '?', '"', '\'', '«', '»', '(', ')', '{', '}', '[', ']', '<', '>', '/' };
            return punctuationMarks.Contains(c);
        }
    }
}
