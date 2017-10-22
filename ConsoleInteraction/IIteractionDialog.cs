using System.Collections.Generic;

namespace ConsoleInteraction
{
    internal interface IIteractionDialog
    {
        Queue<string> GetCommand();
        void PrintSuccessfullMessage(string message);
        void PrintAllertMessage(string message);
        void PrintErrorMessage(string message);
        void PrintMessage(string message);
    }
}