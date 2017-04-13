using Decision_Tree_ALg.TreeStructures;
using System;
using System.Text.RegularExpressions;

namespace Decision_Tree_ALg.AlgLogicLib
{
    interface ITreeExporter
    {
        void exportTree(string pathFileToSaveTheExport, TreeNode node);
    }
}
