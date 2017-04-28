using Decision_Tree_ALg.TreeStructures;
using System.IO;
using System.Web.Script.Serialization;

namespace Decision_Tree_ALg.AlgLogicLib
{
    public class JSONTreeExporter : ATreeExporter, ITreeExporter
    {
        override public void ExportTree(string pathFileToSaveTheExport, ItreeNode node)
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