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
        private LevelService _levelService;
        private SpawnService _spawnService;
        private Assembly _executingAssembly;
        private char[] _currentLetters;
        private readonly Random _random = new Random();
        private readonly List<string> _words = new List<string>();

        private readonly Dictionary<char, int> _letters = new Dictionary<char, int>
        {
            {'A', 1}, {'E', 1}, {'I', 1}, {'O', 1}, {'U', 1}, {'N', 1}, {'R', 1}, {'T', 1}, {'L', 1}, {'S', 1}, {'D', 2}, {'G', 2}, {'B', 3},
            {'C', 3}, {'M', 3}, {'P', 3}, {'F', 4}, {'H', 4}, {'W', 4}, {'V', 4}, {'Y', 4}, {'K', 5}, {'J', 8}, {'X', 8}, {'Q', 10}, {'Z', 10}
        };

        private readonly Dictionary<char, int> _vowels = new Dictionary<char, int> {{'A', 1}, {'E', 1}, {'I', 1}, {'O', 1}, {'U', 1}};

        private readonly Dictionary<char, int> _consonants = new Dictionary<char, int>
        {
            {'N', 1}, {'R', 1}, {'T', 1}, {'L', 1}, {'S', 1}, {'D', 2}, {'G', 2}, {'B', 3}, {'C', 3}, {'M', 3}, {'P', 3},
            {'F', 4}, {'H', 4}, {'W', 4}, {'V', 4}, {'Y', 4}, {'K', 5}, {'J', 8}, {'X', 8}, {'Q', 10}, {'Z', 10}
        };

        public override void _Ready()
        {
            _executingAssembly = Assembly.GetExecutingAssembly();
            LoadWords();
            _levelService = GetNode<LevelService>("/root/LevelService");
            _spawnService = GetNode<SpawnService>("/root/SpawnService");
        }

        private void LoadWords()
        {
            if (_words.Any())
                return;

            var wordsStream = _executingAssembly.GetManifestResourceStream("GameOff2020.Resources.words.txt");
            using var streamReader = new StreamReader(wordsStream!);

            var csvReader = new CsvReader(streamReader);
            while (csvReader.Read())
                _words.Add(csvReader[0]);
        }

        public int CalculateScore(string word) => word.ToUpper().Sum(c => _letters[c]);

        public bool IsValidWord(string word) => _words.Contains(word.ToUpper()) && word.All(_currentLetters.Contains);

        public char[] GetRandomLetters(int amount, bool fullRandom = false)
        {
            if (fullRandom)
                return _letters.Keys.OrderBy(x => _random.Next()).Take(amount).ToArray();

            var consonants = _consonants.Keys.OrderBy(x => _random.Next()).Take(amount - 5).ToArray();
            _currentLetters = consonants.Concat(_vowels.Keys).OrderBy(x => _random.Next()).ToArray();
            return _currentLetters;
        }

        private readonly string[] _aiSpyWords =
        {
            "SPACE", "EARTH", "MARS", "MOON", "SOLAR", "JUPITER", "NEPTUNE", "MERCURY", "SATURN", "PLUTO", "VENUS", "URANUS", "PLANET", "ASTEROID",
            "ASTRONAUT", "ASTRONAUTS", "ASTRONOMER", "ASTRONOMICAL", "ASTRONOMY", "CONSTELLATION", "COSMOS", "COSMIC", "CRATER", "DUST", "EQUINOX",
            "ECLIPSE", "GALAXY", "LUNAR", "METEOR", "METEORITE", "METEOROID", "GRAVITY", "INERTIA", "MAGNITUDE", "MASS", "NEBULA", "ORBIT", "ROCKET",
            "EXPLORATION", "SOLSTICE", "STAR", "UMBRA", "SKY", "SATELLITE", "PENUMBRA", "PHASE", "OBSERVATORY", "UNIVERSE", "SUN", "STARLIGHT",
            "TELESCOPE", "INTERSTELLAR", "SPACECRAFT", "COMET", "AEROSPACE", "SPACEMAN", "SPACESHIP", "INTERPLANETARY", "GALACTIC", "HELIOSPHERE",
            "MICROGRAVITY", "GRAVITATION", "ATMOSPHERE", "MOONSHINE", "SPACEFLIGHT", "SPUTNIK", "GAGARIN", "NASA", "APOLLO", "SOYUZ", "ORIENTATION",
            "ARMSTRONG", "KENNEDY", "ARTEMIS", "MOONBEAM"
        };

        public string GetRandomAISpyWord()
        {
            if (_random.NextDouble() < _levelService.LevelData.AISpyRandomWordChance)
                return new string(GetRandomLetters(_random.Next(4, 8), true));

            return _aiSpyWords[_random.Next(0, _aiSpyWords.Length)];
        }

        public bool IsValidAISpyWord(string word) => _spawnService.AISpies.Any(spy => string.Equals(spy.Word, word));
    }
}
