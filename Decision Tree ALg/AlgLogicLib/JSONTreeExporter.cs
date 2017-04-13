using Decision_Tree_ALg.TreeStructures;
using System;
using System.IO;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace Decision_Tree_ALg.AlgLogicLib
{
    class JSONTreeExporter : ATreeExporter, ITreeExporter
    {
 
        override public void exportTree(string pathFileToSaveTheExport, TreeNode node)
        {
            var validatedPath = this.Regex.Replace(pathFileToSaveTheExport, "-");
            using (StreamWriter writer =  File.AppendText(validatedPath))
            {
                var json = new JavaScriptSerializer().Serialize(node); 
                writer.Write(json);

            }
        }
    }
}