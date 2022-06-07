using Bus.Enums;
using Bus.Models;

namespace Bus.Validators
{
    public class RedBlackValidator : ICardValidator
    {
        public bool Validate(Card card, string guess, Card[] currentState = null)
        {
            var cardColor = 'r';
            
            if (card.Suit == Suit.Spades || card.Suit == Suit.Clubs)
            {
                cardColor = 'b';
            }

            return cardColor.Equals(guess[0]);
        }

    }

}