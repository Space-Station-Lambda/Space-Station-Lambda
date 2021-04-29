using System;
using SSL_Core.model;
using SSL_Core.model.player;

namespace SSL_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init players
            
            Player player1 = new Player();
            Player player2 = new Player();
            Player player3 = new Player();
            Player player4 = new Player();
            
            Console.WriteLine($"{player1} joined the game");
            Console.WriteLine($"{player2} joined the game");
            Console.WriteLine($"{player3} joined the game");
            Console.WriteLine($"{player4} joined the game");
            
        }
    }
}