using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleInteraction
{
    sealed class  RegexContract
    {

        public readonly static IDictionary<string, string> regexCollection = new Dictionary<string, string>()
        {
            {"Exit", @"^exit\s*$" },
            {"Help", @"^help\s*$" },
            {"LoadExamples", @"^(load examples\s+)(\S+.*\s*)$" },
            {"TestMetrics", @"^test metrics with \S+\s*$" },
            {"Generate", @"^(generate to\s+)(\S+.*\s*)$" },
            {"Minify", @"^minify\s*$" },
            {"Maxify", @"^maxify\s*$" },
            {"LoadTestExamples", @"^(load test examples\s+)(\S+.*\s*)$" },
            {"Test", @"^test\s*$" },
            {"GenerateConfig", @"^(generate config\s+)(\S+.*\s*)$" }
        };

        // TODO: generate Regex es for suggestions 

        public readonly static Regex[] validationRegexCollectionToBeMatched = new Regex[]
        {
            
            // Test if there is at least one value after the node name see example below: 
            /* 
             * <NodeName>, <nodeValuew> -> at least one -> valid
             * <NodeName>,              -> no node values -> invalid
             * The Node name must have at least one value after the name 
            */
            new Regex(@"^\w+ *, *\w+")
        };

        public readonly static Regex[] validationRegexCollectionNotToBeMatched = new Regex[]
        {
            // This regex is used to validate for unaccepted symbols in a file -> !@#$%^&*(_)_- and TAB and numbers
            new Regex(@"[^A-Za-z ]")
        };
   }
}