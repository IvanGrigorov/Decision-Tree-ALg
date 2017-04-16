using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision_Tree_ALg.ReadingLibrary;
using Decision_Tree_ALg.AlgLogicLib;

namespace Decision_Tree_ALg
{
    class StartUp
    {
        static void Main(string[] args)
        {
            // var examples = DataReader.ReturnAllExamplesFromFile("../../TrainingData/DataExamples.txt");
            // Console.WriteLine(examples);
            DecisionTreeCreator treeCreator = new DecisionTreeCreator();
            treeCreator.StartBuilding();
            Console.WriteLine(treeCreator);
            ITreeExporter exporter = new JSONTreeExporter();
            DateTime dateTime = DateTime.Now;

            // Add this option in configuration 

            String resultsPathFile = "../../Results/Tree-" + dateTime.ToString().Replace("/", "-") + ".txt";
            exporter.exportTree(resultsPathFile, treeCreator.RootNode);
        }
    }
}
