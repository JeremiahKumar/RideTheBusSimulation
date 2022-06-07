using Bus.Helpers;

namespace Bus
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            CardConstraintsSingleton availableCards = CardConstraintsSingleton.Instance;
            var game = new Game(10000);
            game.GetPercentageSuccessOneDeckFullSim();
            
            //TODO Make a version that goes through multiple decks (up to limit). This will need to count how many failures before success is hit
        }
    }
}