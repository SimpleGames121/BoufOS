using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoufOS.Programs
{
    internal class GuessTheNumber
    {
        public static void Run()
        {
            Random rand = new Random();
            int tries = 1;
            int number = rand.Next(100);
            Console.WriteLine("Guess the hidden number!(0-100)");
            int input = 0;
            Console.Write(">");
            Int32.TryParse(Console.ReadLine(), out input);

            while(number != input)
            {
                if(number > input)
                {
                    Console.WriteLine("Give a larger number!");
                }
                else
                {
                    Console.WriteLine("Give a smaller number!");
                }
                Console.Write(">");
                Int32.TryParse(Console.ReadLine(), out input);
                tries++;
            }

            Console.Write("You found it in ");
            Console.Write(tries.ToString());
            Console.WriteLine(" tries!");
        }
    }
}
