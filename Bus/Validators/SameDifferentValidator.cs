using Bus.Models;

namespace Bus.Validators
{
    public class SameDifferentValidator : ICardValidator
    {
        public bool Validate(Card card, string guess, Card[] currentState)
        {
            var cardSameDifferent = 's';
            
            if (card.Suit != currentState[0].GetSuit() 
                && card.Suit != currentState[1].GetSuit() 
                && card.Suit != currentState[2].GetSuit())
            {
                cardSameDifferent = 'd';
            }

            return cardSameDifferent.Equals(guess[3]);
        }
    }
}