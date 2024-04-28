using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Searching_For_Substring_In_String
{
    public interface ISubstringSearch
    {
        List<int> IndexesOf(string pattern, string text);
        string Name { get;}
    }
    public class RabinKarp : ISubstringSearch
    {
        public string Name => "RabinKarp";

        public List<int> IndexesOf(string pattern, string text)
        {
            List<int> res = new List<int>();
            int Mod = 7;
            int Radix = 13;
            int textLength = text.Length;
            int patternLength = pattern.Length;
            if (textLength == 0 || patternLength == 0) return res;
            int h = (int)Math.Pow(Radix, patternLength - 1) % Mod;
            int p = 0;
            int ts = 0;
            for (int i = 0; i < patternLength; i++)
            {
                p = (p * Radix + pattern[i]) % Mod;
                ts = (ts * Radix + text[i]) % Mod;
            }
            p = (p + Mod) % Mod;
            ts = (ts + Mod) % Mod;

            for (int i = 0; i <= textLength - patternLength; i++)
            {
                if (p == ts)
                {
                    bool flag = true;
                    for (int j = i; j < i + patternLength; j++)
                    {
                        if (pattern[j - i] != text[j])
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag) res.Add(i);
                }
                else
                {
                    ts = (Radix * (ts - text[i] * h) + text[i + patternLength]) % Mod;
                    ts = (ts + Mod) % Mod;
                }
            }

            return res;
        }
    }
    public class KnuthMorrisPratt : ISubstringSearch
    {
        public string Name => "KnuthMorrisPratt";
        public List<int> IndexesOf(string pattern, string text)
        {
            List<int> indexs = new List<int>();
            if (text.Length == 0 || pattern.Length == 0) return indexs;
            int[] table = CreateTable(pattern);
            int i = 0;
            int j = 0;
            while(i < text.Length)
            {
                if (text[i] == pattern[j])
                {
                    i++;
                    j++;
                    if (j == pattern.Length)
                    {
                        indexs.Add(i - j);
                        j = 0;
                        i = i - pattern.Length + 1;
                    }
                }
                else
                {
                    if (j > 0)
                    {
                        j = table[j - 1];
                    }
                    else
                    {
                        i += 1;
                    }
                }
            }
            return indexs;
        }
        private int[] CreateTable(string pattern)
        {
            int[] table = new int[pattern.Length];
            table[0] = 0;
            int j = 0;
            int i = 1;
            while(i < pattern.Length)
            {
                if (pattern[j] == pattern[i])
                {
                    table[i] = j + 1;
                    i++;
                    j++;
                }
                else
                {
                    if (j == 0)
                    {
                        table[i] = 0;
                        i++;
                    }
                    else
                    {
                        j = table[j - 1];
                    }
                }
            }

            return table;
        }
    }
    public class BoyerMoore : ISubstringSearch
    {
        public string Name => "BoyerMoore";
        public List<int> IndexesOf(string pattern, string text)
        {
            List<int> indexs = new List<int>();
            if (text.Length == 0 || pattern.Length == 0) return indexs;
            Dictionary<char, int> dictionary = CreateTable(pattern);
            int shift = 1;
            for (int i = pattern.Length - 1; i < text.Length; i += shift)
            {
                if (pattern[pattern.Length - 1] == text[i])
                {
                    bool flag = true;
                    int length = pattern.Length - 2;
                    for (int j = i - 1; j > i - pattern.Length; j--)
                    {
                        if (pattern[length] != text[j])
                        {
                            shift = dictionary[pattern[length]];
                            flag = false;
                            break;
                        }
                        length--;
                    }
                    if (flag)
                    {
                        indexs.Add(i + 1 - pattern.Length);
                        shift = 1;
                    }
                }
                else
                {
                    if (!dictionary.ContainsKey(text[i]))
                    {
                        shift = pattern.Length;
                    }
                    else
                    {
                        shift = dictionary[text[i]];
                    }
                }
            }
            return indexs;
        }
        private Dictionary<char, int> CreateTable(string pattern)
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            for(int i = pattern.Length - 2; i >= 0; i--)
            {
                if (!dictionary.ContainsKey(pattern[i]))
                {
                    dictionary.Add(pattern[i], pattern.Length - i - 1);
                }
            }
            if (!dictionary.ContainsKey(pattern[pattern.Length - 1]))
            {
                dictionary.Add(pattern[pattern.Length - 1], pattern.Length);
            }
            return dictionary;
        }
    }
    public class BruteForce : ISubstringSearch
    {
        public string Name => "BruteForce";
        public List<int> IndexesOf(string pattern, string text)
        {
            List<int> indexs = new List<int>();
            if (text.Length == 0 || pattern.Length == 0) return indexs;
            for (int i = 0; i < text.Length - pattern.Length + 1; i++)
            {
                bool SubStringIsEqual = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (pattern[j] != text[i + j])
                    {
                        SubStringIsEqual = false;
                        break;
                    }
                }

                if (SubStringIsEqual)
                {
                    indexs.Add(i);
                }
            }
            return indexs;
        }
    }
}
