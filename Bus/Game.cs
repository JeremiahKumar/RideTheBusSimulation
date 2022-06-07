using System;
using System.Collections.Generic;
using System.Linq;
using Bus.Models;
using Bus.Validators;

namespace Bus
{
    public class Game
    {
        private readonly int _sampleSize;
        private readonly Deck _deck;
        private Card[] _currentGameState = new Card[4];
        private readonly Dictionary<String, int> _percentageWinOneDeckStats;
        private readonly ICardValidator[] _cardValidators;
        private readonly GuessValidator _guessValidator;
        private readonly string[] _possibleGuesses;

        public Game(int sampleSize)
        {
            _sampleSize = sampleSize;
            _deck = new Deck();
            _percentageWinOneDeckStats = new Dictionary<string, int>();
            _guessValidator = new GuessValidator();
            
            _cardValidators = new ICardValidator[]
            {
                new RedBlackValidator(),
                new HigherLowerValidator(),
                new InsideOutsideValidator(),
                new SameDifferentValidator()
            };

            _possibleGuesses = new[]
            {
                "rhis", "rhid", "rhos", "rhod",
                "rlis", "rlid", "rlos", "rlod",
                "bhis", "bhid", "bhos", "bhod",
                "blis", "blid", "blos", "blod"
            };
        }

        public void GetPercentageSuccessOneDeckFullSim()
        {
            foreach (var guess in _possibleGuesses)
            {
                GetPercentageSuccessOneDeck(guess);
            }

            var sortedDict = from entry in _percentageWinOneDeckStats orderby entry.Value descending select entry;
            
            foreach (KeyValuePair<string, int> kvp in sortedDict)
            {
                Console.WriteLine("Guess = {0}, Percentage success = {1}%", kvp.Key, kvp.Value);
            }
        }

        private void GetPercentageSuccessOneDeck (string guess)
        {
            if (!_guessValidator.ValidateGuess(guess))
            {
                throw new Exception($"Guess {guess} is incorrectly formatted, please fix");
            }
            
            _deck.Shuffle();
            var count = 0;
            for (int i = 0; i < _sampleSize; i++)
            {
                if (RunThroughDeckOnce(guess))
                {
                    count++;
                }
                _deck.Shuffle();
            }
            
            var percentSuccess = (int)Math.Round((double)(100 * count) / _sampleSize);
            _percentageWinOneDeckStats.Add(guess, percentSuccess);
            
        }

        /// <summary>
        /// This method will go through the Deck one time, checking all validation rules in order and moving the iterator through the deck at a success.
        /// If all validators pass this will exit early as a match has been found.
        /// 
        /// Current game state will continuously update to match validation status.
        /// 
        /// Since this method goes through the deck only once, it stops 4 indices away from the Deck length (as there would only be three remaining cards and
        /// a win is impossible).
        /// </summary>
        /// <param name="guess"></param>
        /// <returns></returns>
        private bool RunThroughDeckOnce(string guess)
        {
            for (int i = 0; i < _deck.GetDeckLength()-4; i++)
            {
                for (int j = 0; j < _cardValidators.Length; j++)
                {
                    if (_cardValidators[j].Validate(_deck.GetCardAtIndex(i), guess, _currentGameState))
                    {
                        // last validator has passed
                        if (j == 3)
                        {
                            return true;
                        }
                        _currentGameState[j] = _deck.GetCardAtIndex(i);
                        i++;
                    } 
                    else
                    { 
                        // A validator failed, reset game state and stop validation loop
                        _currentGameState = new Card[4];
                        break;
                    }
                }
            }
            return false;
        }
    }
}