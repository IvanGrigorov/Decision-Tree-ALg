using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision_Tree_ALg.ReadingLibrary;
using Decision_Tree_ALg.AlgLogicLib;

namespace Decision_Tree_ALg
{
    class Program
    {
        static void Main(string[] args)
        {
            // var examples = DataReader.ReturnAllExamplesFromFile("../../TrainingData/DataExamples.txt");
            // Console.WriteLine(examples);
            DecisionTreeCreator treeCreator = new DecisionTreeCreator();
            treeCreator.StartBuilding();
            Console.WriteLine(treeCreator);
            ITreeExporter exporter = new JSONTreeExporter();
            exporter.exportTree("../../Results/Tree.txt", treeCreator.RootNode);
        }
    }
}
