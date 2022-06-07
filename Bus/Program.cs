using System;
using Bus.Enums;
using Bus.Models;

namespace Bus
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game(10000);
            game.SimulateHand(RedBlack.Red, HigherLower.Higher, InsideOutside.Inside, SameDifferent.Different);
        }
    }
}