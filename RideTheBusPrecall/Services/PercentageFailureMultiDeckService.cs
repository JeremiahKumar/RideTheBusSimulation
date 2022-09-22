using System;
using System.Collections.Generic;
using Bus.Models;
using Bus.Validators;

namespace Bus.Services
{
    public class PercentageFailureMultiDeckService
    {
        private readonly int _sampleSize;
        private readonly int _maxShuffles;
        private readonly Deck _deck;
        private readonly ICardValidator[] _cardValidators;
        private Card[] _currentGameState = new Card[4];
        
        /// <summary>
        /// This class will simulate the amount of failures it takes before a guess succeeds over multiple deck runs.
        /// </summary>
        /// <param name="sampleSize"></param>
        /// <param name="maxShuffles"></param>
        public PercentageFailureMultiDeckService(int sampleSize, int maxShuffles)
        {
            _sampleSize = sampleSize;
            _maxShuffles = maxShuffles;
            _deck = new Deck();
            _cardValidators = new ICardValidator[]
            {
                new RedBlackValidator(),
                new HigherLowerValidator(),
                new InsideOutsideValidator(),
                new SameDifferentValidator()
            };
        }
        
        /// <summary>
        /// This method will go through a deck up to _maxShuffle times counting how many failures it takes before a guess is reached.It will do this process
        /// _sampleSize amount of times and average the results and return it.
        /// </summary>
        /// <param name="guess"></param>
        /// <returns>KeyValuePair of guess and failure rate</returns>
        /// <exception cref="Exception"></exception>
        public KeyValuePair<string,long> GetFailureCountForGuess(string guess)
        {
            if (!GuessValidator.ValidateGuess(guess))
            {
                throw new Exception($"Guess {guess} is incorrectly formatted, please fix");
            }
            
            _deck.Shuffle();
            long failureSum = 0;
            
            for (int i = 0; i < _sampleSize; i++)
            {
                failureSum += RunThroughMultiDecksUntilSuccess(guess);
                _deck.Shuffle();
            }
            
            var percentFailure = failureSum / _sampleSize;
            
            return new KeyValuePair<string,long>(guess, percentFailure);
        }
        
        /// <summary>
        /// This method will go through the Deck multiple times, checking all validation rules in order and moving the iterator through the deck at a success.
        /// If all validators pass this will exit early as a match has been found. If not, continue trying with _maxShuffles deck shuffles allowed when the index
        /// is at the end of the deck.
        /// 
        /// Since this method goes through the deck multiple times it uses an if statement at the start of the loop to check if it is in an impossible index. If so
        /// that means we need to reset the index and shuffle the cards and continue. This continues until the max shuffle count is reached.
        ///
        /// Note that if a validator succeeded on index 51, the code will still shuffle the deck on 52 and current game state will be wiped and restarted. This is consistent
        /// with the game rules outlined on the README
        /// </summary>
        /// <param name="guess"></param>
        /// <returns></returns>
        private int RunThroughMultiDecksUntilSuccess(string guess)
        {
            var shuffleCount = 0;
            var failureCount = 0;
            
            // For each card in the deck
            for (int i = 0; i <= _deck.GetDeckLength(); i++)
            {
                // If a Validator has failed and we are starting again at index 52, reset the deck and deck iterator.
                if (i == _deck.GetDeckLength())
                {
                    HandleShuffle(shuffleCount,guess,failureCount);
                    i = 0;
                    shuffleCount++;
                }
                
                // Go through all the validators
                for (int j = 0; j < _cardValidators.Length; j++)
                {
                    // If a Validator has succeeded but we are starting again at index 52, reset the deck and deck iterator,
                    // add to the failure count and restart the game state.
                    if (i == _deck.GetDeckLength())
                    {
                        HandleShuffle(shuffleCount,guess,failureCount);
                        i = 0;
                        shuffleCount++;
                        failureCount++;
                        _currentGameState = new Card[4];
                        break;
                    }
                    
                    if (_cardValidators[j].Validate(_deck.GetCardAtIndex(i), guess, _currentGameState))
                    {
                        // Last validator has passed, success!
                        if (j == 3)
                        {
                            return failureCount;
                        }
                        _currentGameState[j] = _deck.GetCardAtIndex(i);
                        i++;
                    } 
                    else
                    { 
                        // A validator failed, reset game state and stop validation loop
                        failureCount++;
                        _currentGameState = new Card[4];
                        break;
                    }
                }
            }
            return failureCount;
        }

        private void HandleShuffle(int shuffleCount, string guess, int failureCount)
        {
            if (shuffleCount == _maxShuffles)
            {
                throw new Exception($"Guess {guess} did not succeed after {_maxShuffles} shuffles. Failure count was {failureCount}");
                
            }
            _deck.Shuffle();
        }
    }
}