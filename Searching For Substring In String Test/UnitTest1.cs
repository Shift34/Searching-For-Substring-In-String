using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Searching_For_Substring_In_String;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;

namespace Searching_For_Substring_In_String_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SearchTest()
        {
            var algms = new List<ISubstringSearch>()
            {
                new BoyerMoore(),
                new BruteForce(),
                new RabinKarp(),
                new KnuthMorrisPratt()
            };
            string text = "aaaaaaaaaa"; //10
            string pattern = "aa";
            var expected = Enumerable.Range(0, 9).ToList();
            foreach (var algm in algms)
            {
                var actual = algm.IndexesOf(pattern, text);
                CollectionAssert.AreEqual(expected, actual);
            }
        }
        [TestMethod]
        public void SearchBagOfWordsOnAnnaTxt()
        {
            var algms = new List<ISubstringSearch>()
            {
                new BoyerMoore(),
                new RabinKarp(),
                new KnuthMorrisPratt()
            };
            string text;
            using (var sr = new StreamReader("anna.txt", Encoding.UTF8))
            {
                text = sr.ReadToEnd().ToLower();
            }

            int number = 100;
            Regex rg = new Regex(@"\w+");
            var bag = new HashSet<string>();
            var matches = rg.Matches(text);
            foreach (var match in matches)
            {
                bag.Add(match.ToString());
                if (bag.Count > number) break;
            }
            foreach (var pattern in bag)
            {
                var BF = new BruteForce();
                var expected = BF.IndexesOf(pattern, text);
                foreach (var algm in algms)
                {
                    var actual = algm.IndexesOf(pattern, text);
                    CollectionAssert.AreEqual(expected, actual);
                }
            }
        }
        [TestMethod]
        public void FindSublineNoExist()
        {
            var algms = new List<ISubstringSearch>()
            {
                new BoyerMoore(),
                new BruteForce(),
                new RabinKarp(),
                new KnuthMorrisPratt()
            };
            string text = "aaaaaaaaaa"; //10
            string pattern = "ab";
            foreach (var algm in algms)
            {
                var actual = algm.IndexesOf(pattern, text);
                Assert.AreEqual(0, actual.Count());
            }
        }
    }
}
