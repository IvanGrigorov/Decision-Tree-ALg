using Decision_Tree_ALg.TreeStructures;
using System.Text.RegularExpressions;

namespace Decision_Tree_ALg.AlgLogicLib
{
    abstract class ATreeExporter : ITreeExporter
    {
        protected Regex Regex { get { return new Regex(this.RegExPattern); } }         

        private string RegExPattern { get; } = @"(<|>|:|\||""|\\|\?|\*| )";

        public abstract void exportTree(string pathFileToSaveTheExport, TreeNode node); 
               
    }
}