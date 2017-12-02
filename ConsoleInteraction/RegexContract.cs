using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleInteraction
{
    sealed class RegexContract
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

        public readonly static IDictionary<string, string> commandRecommendations = new Dictionary<string, string>()
        {
            // Make use of the way minify and maxify are created and apply them to others 
            {
                "Maybe you mean to use <<load examples>>" ,
                @"(((lod|lad|[^l]oad|loa) ?(examples|example|xamples|eamples|exmples|exaples|examles|exampes|exampls))|((lod|lad|oad|loa|load) ?(example|xamples|eamples|exmples|exaples|examles|exampes|exampls)$)|loadexamples)"
            },
            {
                "Maybe you mean to use <<exit>>" ,
                @"(( +((ext +|ext$)|(xit +|xit$)|(exi +|exi$)|(eit +|eit$)))|^((ext +|ext$)|(xit +|xit$)|(exi +|exi$)|(eit +|eit$)))"
            },
            {
                "Maybe you mean to use <<help>>" ,
                @"(( +((hel +|hel$)|(elp +|elp$)|(hlp +|hlp$)|(hep +|hep$)))|^((hel +|hel$)|(elp +|elp$)|(hlp +|hlp$)|(hep +|hep$)))"
            },
            {
                "Maybe you mean to use <<minify>>",
                @"^ *(minif|miniy|minfy|miify|mnify|m[^i]{1}nify|mi[^n]{1}ify|min[^i]{1}fy|mini[^f]{1}y|minif[^y]{1}) *$"
            },
            {
                "Maybe you mean to use <<maxify>>",
                @"^ *(maxif|mxify|maify|maxfy|maxiy|m[^a]{1}xify|ma[^x]{1}ify|max[^i]{1}fy|maxi[^f]{1}y|maxif[^y]{1}) *$"
            },
            {
                "Maybe you mean to use <<generate>>",
                @"^ *(generat|enerate|gnerate|geerate|genrate|geneate|generte|generae|[^g]{1}eneratе|g[^e]{1}neratе|ge[^n]{1}eratе|gen[^e]{1}ratе|gene[^r]{1}atе|gener[^a]{1}tе|genera[^t]{1}е|generat[^e]{1}) *$"
            },
            {
                "Maybe you mean to use <<test>>",
                @"^ *(tes|tet|tst|est|tes[^t]{1}|te[^s]{1}t|t[^e]{1}st|[^t]{1}est) *$"

            }
        };

    }
}