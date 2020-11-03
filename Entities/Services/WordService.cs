using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Godot;
using NReco.Csv;

namespace GameOff2020.Entities.Services
{
    public class WordService : Node
    {
        private Assembly _executingAssembly;
        private readonly Random _random = new Random();
        private readonly List<string> _words = new List<string>();

        private readonly Dictionary<char, int> _letters = new Dictionary<char, int>
        {
            {'A', 1},
            {'E', 1},
            {'I', 1},
            {'O', 1},
            {'N', 1},
            {'R', 1},
            {'T', 1},
            {'L', 1},
            {'S', 1},
            {'U', 1},
            {'D', 2},
            {'G', 2},
            {'B', 3},
            {'C', 3},
            {'M', 3},
            {'P', 3},
            {'F', 4},
            {'H', 4},
            {'W', 4},
            {'V', 4},
            {'Y', 4},
            {'K', 5},
            {'J', 8},
            {'X', 8},
            {'Q', 10},
            {'Z', 10}
        };

        public override void _Ready()
        {
            _executingAssembly = Assembly.GetExecutingAssembly();
            ReadWords();
        }

        private void ReadWords()
        {
            var wordsStream = _executingAssembly.GetManifestResourceStream("GameOff2020.Resources.words.txt");
            using var streamReader = new StreamReader(wordsStream!);

            var csvReader = new CsvReader(streamReader);
            while (csvReader.Read())
            {
                var word = csvReader[0];
                if (!word.Contains("'"))
                    _words.Add(word.ToUpper());
            }
        }

        public int CalculateScore(string word) => word.ToUpper().Sum(c => _letters[c]);

        public bool IsValidWord(string word) => _words.Contains(word.ToUpper());

        //TODO Smarter letter generation
        public char[] GetRandomLetters(int amount) => _letters.Keys.OrderBy(x => _random.Next()).Take(amount).ToArray();
    }
}
