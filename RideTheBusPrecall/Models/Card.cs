using System;
using System.Linq;
using Bus.Constants;
using Bus.Enums;
namespace Bus.Models
{
    //TODO convert the card to store the int value and use the letters as a presentation only
    public class Card
    {
        public readonly Suit Suit;
        private readonly string _value;

        public Card(Suit suit, string value)
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
            switch (_value)
            {
                case "A":
                    return 1;
                case "J":
                    return 11;
                case "Q":
                    return 12;
                case "K":
                    return 13;
                default:
                    return Int32.Parse(_value);
            }
        }

        public Suit GetSuit()
        {
            return Suit;
        }
    }
}