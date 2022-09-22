using System;
using System.Collections.Generic;
using System.Linq;
using Bus.Services;

namespace Bus
{
    public class Game
    {
        private readonly string[] _possibleGuesses =
        {
            "rhis", "rhid", "rhos", "rhod",
            "rlis", "rlid", "rlos", "rlod",
            "bhis", "bhid", "bhos", "bhod",
            "blis", "blid", "blos", "blod"
        };
        
        private readonly Dictionary<string, int> _percentageWinOneDeckStats = new Dictionary<string, int>();
        private readonly Dictionary<string, long> _failureCountsMultiDeck = new Dictionary<string, long>();
        
        private readonly int _maxShuffles;
        private readonly PercentageSuccessOneDeckService _percentageSuccessOneDeckService;
        private readonly PercentageFailureMultiDeckService _percentageFailureMultiDeckService;
        
        public Game(int sampleSize, int maxShuffles)
        {
            _maxShuffles = maxShuffles;
            _percentageSuccessOneDeckService = new PercentageSuccessOneDeckService(sampleSize);
            _percentageFailureMultiDeckService = new PercentageFailureMultiDeckService(sampleSize, maxShuffles);
        }

        /// <summary>
        /// This method will run all possible precall hands against a shuffled deck of cards once and print the success rates as percentages.
        /// When there are not enough cards left to continue the game will end.
        /// </summary>
        public void GetSuccessPercentagesOneDeck()
        {
            foreach (var guess in _possibleGuesses)
            {
                var result = _percentageSuccessOneDeckService.GetSuccessPercentageForGuess(guess);
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
        /// This method will run all possible precall hands against a shuffled deck of cards, reshuffling and restarting upon the deck running out.
        /// This reshuffling will occur `maxShuffles` amount of times and this method will return the average of how many failures it took before the hand passed. 
        /// </summary>
        public void GetFailurePercentagesMultiDeck()
        {
            foreach (var guess in _possibleGuesses)
            {
                var result = _percentageFailureMultiDeckService.GetFailureCountForGuess(guess);
                _failureCountsMultiDeck.Add(result.Key, result.Value);
            }

            var sortedDict = from entry in _failureCountsMultiDeck orderby entry.Value ascending select entry;
            
            Console.WriteLine($"Failure counts against {_maxShuffles} deck:");
            foreach (KeyValuePair<string, long> kvp in sortedDict)
            {
                Console.WriteLine("Guess = {0}, Percentage failure  = {1}", kvp.Key, kvp.Value);
            }
        }
        
    }
}