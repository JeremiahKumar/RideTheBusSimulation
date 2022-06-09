using System;
using System.Collections.Generic;
using Bus.Models;
using Bus.Validators;

namespace Bus.Services
{
    public class PercentageSuccessOneDeckService
    {
        private readonly int _sampleSize;
        private readonly Deck _deck;
        private readonly ICardValidator[] _cardValidators;
        private readonly GuessValidator _guessValidator;
        private Card[] _currentGameState = new Card[4];
        
        public PercentageSuccessOneDeckService(int sampleSize)
        {
            _sampleSize = sampleSize;
            _deck = new Deck();
            _guessValidator = new GuessValidator();
            _cardValidators = new ICardValidator[]
            {
                new RedBlackValidator(),
                new HigherLowerValidator(),
                new InsideOutsideValidator(),
                new SameDifferentValidator()
            };
        }
        
        /// <summary>
        /// This method will go through a deck one time and add to a count if a guess was successful. It repeats this '_sampleSize' amount of times and returns
        /// the results 
        /// </summary>
        /// <param name="guess"></param>
        /// <returns>KeyValuePair of guess and success rate</returns>
        /// <exception cref="Exception"></exception>
        public KeyValuePair<string,int> GetPercentageSuccessOneDeck(string guess)
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
            
            return new KeyValuePair<string,int>(guess, percentSuccess);
        }

        /// <summary>
        /// This method will go through the Deck one time, checking all validation rules in order and moving the iterator through the deck at a success.
        /// If all validators pass this will exit early as a match has been found. If not, continue trying until there is no more space in the deck.
        /// 
        /// Current game state will continuously update to match validation status.
        /// 
        /// Since this method goes through the deck only once it stops X-1 indices away from the Deck length (where X is the amount of validators)
        /// This is because if it kept going, there would be X validators but only X-1 remaining cards and a win is impossible.
        /// </summary>
        /// <param name="guess"></param>
        /// <returns></returns>
        private bool RunThroughDeckOnce(string guess)
        {
            var finalDeckIndex = _deck.GetDeckLength() - _cardValidators.Length;
            
            for (int i = 0; i < finalDeckIndex; i++)
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