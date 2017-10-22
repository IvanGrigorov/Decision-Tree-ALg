using Decision_Tree_ALg.TreeStructures;
using System;
using System.Text.RegularExpressions;

namespace Decision_Tree_ALg.AlgLogicLib
{
    public interface ITreeExporter
    {
        void ExportTree(string pathFileToSaveTheExport, ItreeNode node);
        Regex Regex { get; }
    }
}
