using System;
using Bus.Models;

namespace Bus.Validators
{
    public class InsideOutsideValidator : ICardValidator
    {
        public bool Validate(Card card, string guess, Card[] currentState)
        {
            var cardInsideOutside = 'i';
            var currentValue = card.GetValue();

            if (currentValue == currentState[0].GetValue() || currentValue == currentState[1].GetValue())
            {
                return false;
            }

            int lowerValue = Math.Min(currentState[0].GetValue(), currentState[1].GetValue());
            int higherValue = Math.Max(currentState[0].GetValue(), currentState[1].GetValue());
            
            if (currentValue < lowerValue || currentValue > higherValue)
            {
                cardInsideOutside = 'o';
            }

            return cardInsideOutside.Equals(guess[2]);
        }
    }
}