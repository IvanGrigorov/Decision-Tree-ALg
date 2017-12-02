using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;
namespace ConsoleInteraction
{
    sealed class CommandParser
    {
        public static Queue<string> ParseCommand(string commandInput)
        {
            foreach (var regex in RegexContract.regexCollection)
            {
                if (Regex.IsMatch(commandInput, regex.Value))
                {
                    Match match = Regex.Match(commandInput, regex.Value);
                    Queue<string> commandDetails = new Queue<string>();
                    commandDetails.Enqueue(regex.Key);
                    if (!String.IsNullOrEmpty(match.Groups[2].Value))
                    {
                        commandDetails.Enqueue(match.Groups[2].Value);
                    }
                    return commandDetails;
                }
            }
            foreach (var regex in RegexContract.commandRecommendations)
            {
                if (Regex.IsMatch(commandInput, regex.Value))
                {
                    throw new ArgumentException("Mismatch - " + regex.Key);
                }
            }
            throw new ArgumentException("Unexpected command");
            // TODO: Add suggestions  
        } 

       
    }
}