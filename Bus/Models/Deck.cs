using System;
using System.Linq;
using Bus.Enums;
using Bus.Helpers;

namespace Bus.Models
{
    public class Deck
    {
        private readonly Card[] _cards = new Card[52];
        public Deck()
        {
            
            int i = 0;
            foreach (var suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
            {
                foreach(var value in CardConstraintsSingleton.PossibleValues)
                {
                    _cards[i] = new Card(suit, value);
                    i++;
                }
                
            }
        }
        /// <summary>
        ///  Fisher-Yates Shuffle algorithm goes once through list (O(n)) from back to front and swaps each element with another random element.
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random();
            for (var i = _cards.Length - 1; i > 0; i--)
            {
                var temp = _cards[i];
                var index = random.Next(0, i + 1);
                _cards[i] = _cards[index];
                _cards[index] = temp;
            }
        }

        public int GetDeckLength()
        {
            return _cards.Length;
        }

        public Card GetCardAtIndex(int i)
        {
            return _cards[i];
        }

        public void PrintDeck()
        {
            foreach (var card in _cards)
            {
                Console.WriteLine(card.ToString());
            }
        }
    } 
}