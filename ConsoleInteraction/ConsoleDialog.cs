using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInteraction
{
    class ConsoleDialog : IIteractionDialog
    {
        private readonly ConsoleColor defaultColor = Console.ForegroundColor;
        public Queue<String> GetCommand()
        {
            String command = Console.ReadLine();
            Queue<String> parsedCommand;
            try
            {
                parsedCommand = CommandParser.ParseCommand(command);

            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Unexpected command");
            }
            return parsedCommand;
        }

        public void PrintAllertMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            //Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = this.defaultColor;


        }

        public void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = this.defaultColor;


        }

        public void PrintMessage(string message)
        {
            //Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
        }

        public void PrintSuccessfullMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = this.defaultColor;

        }
    }
}
