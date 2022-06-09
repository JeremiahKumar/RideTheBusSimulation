using Bus.Models;

namespace Bus.Validators
{
    public class SameDifferentValidator : ICardValidator
    {
        public bool Validate(Card card, string guess, Card[] currentState)
        {
            var cardSameDifferent = 's';
            
            if (card._suit != currentState[0].GetSuit() 
                && card._suit != currentState[1].GetSuit() 
                && card._suit != currentState[2].GetSuit())
            {
                cardSameDifferent = 'd';
            }

            return cardSameDifferent.Equals(guess[3]);
        }
    }
}