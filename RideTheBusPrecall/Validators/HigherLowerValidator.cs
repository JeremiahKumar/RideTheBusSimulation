using Bus.Models;

namespace Bus.Validators
{
    public class HigherLowerValidator : ICardValidator
    {
        public bool Validate(Card card, string guess, Card[] currentGameState)
        {
            var cardHigherLower = 'h';
            var currentValue = card.GetValue();

            if (currentValue == currentGameState[0].GetValue())
            {
                return false;
            }
            
            if (currentValue < currentGameState[0].GetValue())
            {
                cardHigherLower = 'l';
            }

            return cardHigherLower.Equals(guess[1]);
        }
    }
}