using System;
using System.Collections.Generic;
using System.Linq;
using Bus.Enums;
using Bus.Models;

namespace Bus
{
    public class Game
    {
        private readonly int sample_size;
        private readonly Deck deck;
        private Card[] _currentGameState = new Card[4];
        private Dictionary<String, int> full_sim_stats;

        public Game(int sampleSize)
        {
            sample_size = sampleSize;
            deck = new Deck();
            full_sim_stats = new Dictionary<string, int>();
        }

        public void FullSimulation()
        {
            var sortedDict = from entry in full_sim_stats orderby entry.Value ascending select entry;
            
            foreach (KeyValuePair<string, int> kvp in sortedDict)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
        }

        public void SimulateHand(RedBlack rb, HigherLower hl, InsideOutside io, SameDifferent sd)
        {
            deck.Shuffle();
            var count = 0;
            for (int i = 0; i < sample_size; i++)
            {
                if (RunThroughDeckOnce(rb, hl, io, sd))
                {
                    count++;
                }
                deck.Shuffle();
            }

            var key = rb.ToString() + hl.ToString() + io.ToString() + sd.ToString();
            var value = (int)Math.Round((double)(100 * count) / sample_size);
            full_sim_stats.Add(key,value);

            // Console.WriteLine( $"Ran guess {key} {sample_size} times. " +
            //        $"{count} times won, this is {value} percent.");
        }

        private bool RunThroughDeckOnce(RedBlack rb, HigherLower hl, InsideOutside io, SameDifferent sd)
        {
            for (int i = 0; i < deck.GetDeckLength()-4; i++)
            {
                if (CheckRedBlack(deck.GetCardAtIndex(i),rb))
                {
                    _currentGameState[0] = deck.GetCardAtIndex(i);
                    i++;
                }
                else
                {
                    _currentGameState = new Card[4];
                    continue;
                }

                if (CheckHigherLower(deck.GetCardAtIndex(i), hl))
                {
                    _currentGameState[1] = deck.GetCardAtIndex(i);
                    i++;
                }
                else
                {
                    _currentGameState = new Card[4];
                    continue;
                }
                
                if (CheckInsideOutside(deck.GetCardAtIndex(i),io))
                {
                    _currentGameState[2] = deck.GetCardAtIndex(i);
                    i++;
                }
                else
                {
                    _currentGameState = new Card[4];
                    continue;
                }
                
                if (CheckSameDifferent(deck.GetCardAtIndex(i),sd))
                {
                    return true;
                }
                else
                {
                    _currentGameState = new Card[4];
                }
            }

            return false;
        }

        private bool CheckRedBlack(Card card, RedBlack rb)
        {
            var cardColor = RedBlack.Red;
            
            if (card.Suit == Suit.Spades || card.Suit == Suit.Clubs)
            {
                cardColor = RedBlack.Black;
            }

            return cardColor == rb;
        }
        
        private bool CheckHigherLower(Card card, HigherLower hl)
        {
            var cardHigherLower = HigherLower.Higher;
            var currentValue = card.GetValue();

            if (currentValue == _currentGameState[0].GetValue())
            {
                return false;
            }
            
            if (currentValue < _currentGameState[0].GetValue())
            {
                cardHigherLower = HigherLower.Lower;
            }

            return cardHigherLower == hl;
        }
        
        private bool CheckInsideOutside(Card card, InsideOutside io)
        {
            var cardInsideOutside = InsideOutside.Inside;
            var currentValue = card.GetValue();

            if (currentValue == _currentGameState[0].GetValue() || currentValue == _currentGameState[1].GetValue())
            {
                return false;
            }

            int lowerValue = Math.Min(_currentGameState[0].GetValue(), _currentGameState[1].GetValue());
            int higherValue = Math.Max(_currentGameState[0].GetValue(), _currentGameState[1].GetValue());
            
            if (currentValue < lowerValue || currentValue > higherValue)
            {
                cardInsideOutside = InsideOutside.Outside;
            }

            return cardInsideOutside == io;
        }
        
        private bool CheckSameDifferent(Card card, SameDifferent sd)
        {
            var cardSameDifferent = SameDifferent.Same;
            
            if (card.Suit != _currentGameState[0].GetSuit() 
                && card.Suit != _currentGameState[1].GetSuit() 
                && card.Suit != _currentGameState[2].GetSuit())
            {
                cardSameDifferent = SameDifferent.Different;
            }

            return cardSameDifferent == sd;
        }

    }
}