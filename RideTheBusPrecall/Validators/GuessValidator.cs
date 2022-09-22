namespace Bus.Validators
{
    public class GuessValidator
    {
        public static bool ValidateGuess(string guess)
        {
            if (guess.Length != 4)
            {
                return false;
            }
            var lowercaseGuess = guess.ToLower();

            if ( (lowercaseGuess[0] != 'r' && lowercaseGuess[0] != 'b') 
                || (lowercaseGuess[1] != 'h' && lowercaseGuess[1] != 'l') 
                || (lowercaseGuess[2] != 'i' && lowercaseGuess[2] != 'o') 
                || (lowercaseGuess[3] != 's' && lowercaseGuess[3] != 'd'))
            {
                return false;
            }

            return true;
        }
    }
}