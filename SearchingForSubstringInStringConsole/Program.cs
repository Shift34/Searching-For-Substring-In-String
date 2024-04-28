using Searching_For_Substring_In_String;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace SearchingForSubstringInStringConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var algms = new List<ISubstringSearch>()
            {
                new BoyerMoore(),
                new BruteForce(),
                new RabinKarp(),
                new KnuthMorrisPratt()
            };
            string text;
            using (var sr = new StreamReader("anna.txt", Encoding.UTF8))
            {
                text = sr.ReadToEnd().ToLower();
            }
            string pattern = "спросил";
            foreach (var algm in algms)
            {
                var actual = algm.IndexesOf(pattern, text);
            }
            foreach (var algm in algms)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var actual = algm.IndexesOf(pattern, text);
                stopwatch.Stop();
                Console.WriteLine(algm.Name + ": " + stopwatch.ElapsedMilliseconds);
            }
            Console.ReadKey();
        }
    }
}
