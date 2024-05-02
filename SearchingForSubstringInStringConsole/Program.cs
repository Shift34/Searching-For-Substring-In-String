using Searching_For_Substring_In_String;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SearchingForSubstringInStringConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var algms = new List<ISubstringSearch>()
            {
                new BruteForce(),
                new BoyerMoore(),
                new RabinKarp(),
                new KnuthMorrisPratt()
            };
            string text;
            using (var sr = new StreamReader("anna.txt", Encoding.UTF8))
            {
                text = sr.ReadToEnd().ToLower();
            }
            string pattern = "предпринять";
            foreach (var algm in algms)
            {
                var actual = algm.IndexesOf(pattern, text);
            }
            foreach (var algm in algms)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                List<int> find = algm.IndexesOf(pattern, text);
                stopwatch.Stop();   
                Console.WriteLine(algm.Name + ": " + stopwatch.ElapsedMilliseconds);
            }
            Console.ReadKey();
        }
    }
}
