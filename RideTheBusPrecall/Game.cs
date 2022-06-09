using System;
using System.Collections.Generic;
using System.Linq;
using Bus.Helpers;
using Bus.Services;

namespace Bus
{
    public class Game
    {
        private readonly int _maxShuffles;
        private readonly string[] _possibleGuesses;
        private readonly PercentageSuccessOneDeckService _percentageSuccessOneDeckService;
        private readonly FailureRateMultiDeckService _failureRateMultiDeckService;
        private readonly Dictionary<string, int> _percentageWinOneDeckStats;
        private readonly Dictionary<string, long> _failureCountsMultiDeck;
        CardConstraintsSingleton _availableCards = CardConstraintsSingleton.Instance;

        public Game(int sampleSize, int maxShuffles)
        {
            _maxShuffles = maxShuffles;
            _percentageSuccessOneDeckService = new PercentageSuccessOneDeckService(sampleSize);
            _failureRateMultiDeckService = new FailureRateMultiDeckService(sampleSize, maxShuffles);
            _percentageWinOneDeckStats = new Dictionary<string, int>();
            _failureCountsMultiDeck = new Dictionary<string, long>();
            _possibleGuesses = new[]
            {
                "rhis", "rhid", "rhos", "rhod",
                "rlis", "rlid", "rlos", "rlod",
                "bhis", "bhid", "bhos", "bhod",
                "blis", "blid", "blos", "blod"
            };
        }

        /// <summary>
        /// This method will run all possible precall hands against a shuffled deck of cards once and print the success rates as percentages.
        /// </summary>
        public void GetPercentageSuccessOneDeckFullSim()
        {
            foreach (var guess in _possibleGuesses)
            {
                var result = _percentageSuccessOneDeckService.GetPercentageSuccessOneDeck(guess);
                _percentageWinOneDeckStats.Add(result.Key, result.Value);
            }

            var sortedDict = from entry in _percentageWinOneDeckStats orderby entry.Value descending select entry;
            
            Console.WriteLine("Percentage success rates against one deck:");
            foreach (KeyValuePair<string, int> kvp in sortedDict)
            {
                Console.WriteLine("Guess = {0}, Percentage success = {1}%", kvp.Key, kvp.Value);
            }
        }
        /// <summary>
        /// This method will run all possible precall hands against a shuffled deck of cards, reshuffling and restarting upon the deck running out. This reshuffling will occur `maxShuffles` 
        /// amount of times and this method will return the average of how many failures it took before the hand passed. 
        /// </summary>
        public void GetFailureRateAverageMultiDeckFullSim()
        {
            foreach (var guess in _possibleGuesses)
            {
                var result = _failureRateMultiDeckService.GetFailureRateMultiDeck(guess);
                _failureCountsMultiDeck.Add(result.Key, result.Value);
            }

            var sortedDict = from entry in _failureCountsMultiDeck orderby entry.Value ascending select entry;
            
            Console.WriteLine($"Failure counts against {_maxShuffles} deck:");
            foreach (KeyValuePair<string, long> kvp in sortedDict)
            {
                Console.WriteLine("Guess = {0}, Failures = {1}", kvp.Key, kvp.Value);
            }
        }
        
    }
}