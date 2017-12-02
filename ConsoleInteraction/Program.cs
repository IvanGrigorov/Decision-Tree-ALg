using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInteraction
{
    class Program
    {
        static void Main(string[] args)
        {
            InteractionConfig iteractionConfig = InteractionConfig.LazyInteractionConfig;
            Console.WriteLine("Hello, the engine is working");
            Console.WriteLine("Do you want to see the tutorial ('yes' or 'no') ?");
            String tutorialInput = Console.ReadLine();
            String pattern = @"^(yes)$|^(no)$";
            tutorialInput = tutorialInput.Trim();
            // May remove second Trim 
            while (!Regex.IsMatch(tutorialInput.Trim(), pattern ))
            {
                Console.WriteLine("Please, answer with 'yes' or 'no' ");
                tutorialInput = Console.ReadLine();
            }

            if (tutorialInput.Equals("no"))
            {
                Console.WriteLine("You can turn on the tutorial by typing 'set tutorial on' in the platform menu");
                InteractionConfig.LazyInteractionConfig.MoveToNextCommand();
            }
            else
            {
                //iteractionConfig.TutorialLoader.StartTutorial();
                Console.WriteLine("Sorry the tutorial is not implemented yet. You can use the help command to see the available functionalities");
                Console.WriteLine("You can either visit https://github.com/IvanGrigorov/Decision-Tree-ALg/blob/master/Sep%2010%202017%206-20%20PM.gif ");
                Console.WriteLine("For more information you can write to ivangrigorov9 at gmail dot com");
                InteractionConfig.LazyInteractionConfig.MoveToNextCommand();
            }

        }
    }
}
