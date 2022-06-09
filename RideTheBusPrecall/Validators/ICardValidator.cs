using Bus.Models;

namespace Bus.Validators
{
    public interface ICardValidator
    {
        bool Validate(Card card, string guess, Card[] currentState = null);
    }
}