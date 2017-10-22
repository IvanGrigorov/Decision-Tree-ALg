﻿using System.Collections.Generic;
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
            throw new ArgumentException("Command Not Found");
            // TODO: Add suggestions  
        } 

       
    }
}