using Decision_Tree_ALg.TreeStructures;
using System.Text.RegularExpressions;

namespace Decision_Tree_ALg.AlgLogicLib
{
    public abstract class ATreeExporter : ITreeExporter
    {
        protected Regex Regex
        {
            get
            {
                return new Regex(this.RegExPattern);
            }
        }    
             
        private string RegExPattern { get; } = @"(<|>|:|\||""|\\|\?|\*| )";

        public abstract void ExportTree(string pathFileToSaveTheExport, ItreeNode node);               
    }
}