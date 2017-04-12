using Decision_Tree_ALg.TreeStructures;
using System;
using System.IO;
using System.Web.Script.Serialization;

namespace Decision_Tree_ALg.AlgLogicLib
{
    class JSONTreeExporter : ITreeExporter
    {
        public void exportTree(string pathFileToSaveTheExport, TreeNode node)
        {
            //throw new NotImplementedException();
            using (StreamWriter writer =  File.AppendText(pathFileToSaveTheExport))
            {
                var json = new JavaScriptSerializer().Serialize(node); 
                writer.Write("Hello");
                writer.Write(json);

            }
        }
    }
}