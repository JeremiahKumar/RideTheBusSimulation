using System;
using System.Linq;
using Bus.Constants;
using Bus.Enums;
namespace Bus.Models
{
    public class Card
    {
        public readonly Suit Suit;
        private readonly int _value;

        public Card(Suit suit, int value)
        {
            if (!ApplicationConstants.PossibleCardValues.Contains(value))
            {
                throw new Exception($"value {value} is not accepted as a possible card value");
            }
            
            Suit = suit;
            _value = value;
        }
        public override string ToString()
        {
            return $"{_value} of {Suit}";
        }

        public int GetValue()
        {
            return _value;
        }

        public Suit GetSuit()
        {
            return Suit;
        }
    }
}