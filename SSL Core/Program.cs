using System;
using System.Collections.Generic;
using SSL_Core.item.items;
using SSL_Core.player;
using SSL_Core.utils;

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

            ItemFood snack = new ItemFood("test", "TEST", 4);
            Serializer serializer = new Serializer();
            Console.Write(serializer.Serialize(snack));
            
        }
    }
}