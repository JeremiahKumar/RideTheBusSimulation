namespace Bus.Helpers
{
    public sealed class CardConstraintsSingleton
    {
        private static CardConstraintsSingleton _instance;
        private static readonly object Padlock = new object();
        public static string[] PossibleValues;

        CardConstraintsSingleton()
        {
            PossibleValues = new [] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        }

        public static CardConstraintsSingleton Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new CardConstraintsSingleton();
                    }
                    return _instance;
                }
            }
        }
    }
}