namespace Bus.Models
{
    public class CardConstraints
    {
        protected string[] _possibleValues;

        public CardConstraints()
        {
            _possibleValues = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        }
    }
}