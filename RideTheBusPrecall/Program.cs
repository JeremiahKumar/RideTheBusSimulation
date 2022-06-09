namespace Bus
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game(10000, 100);
            game.GetPercentageSuccessOneDeckFullSim();
            game.GetFailureRateAverageMultiDeckFullSim();
        }
    }
}