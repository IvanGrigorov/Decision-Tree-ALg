using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision_Tree_ALg.ReadingLibrary;
using Decision_Tree_ALg.AlgLogicLib;
using Decision_Tree_ALg.Config;
using Decision_Tree_ALg.TreeStructures;

namespace Decision_Tree_ALg
{
    public class StartUp
    {
        private static DecisionTreeCreator decisionTreeCreator;
        public static DecisionTreeCreator GeneratedTree {
            get
            {
                if (decisionTreeCreator == null)
                {
                    throw new ArgumentNullException("Tree is empty. Cannot use empty tree. Please use method Start in StartUP class to generate a tree.");
                }
                else
                {
                    return decisionTreeCreator;
                }
            }
            private set
            {
                decisionTreeCreator = value;
            }
        }

        public static void Start()
        {
            // var examples = DataReader.ReturnAllExamplesFromFile("../../TrainingData/DataExamples.txt");
            // Console.WriteLine(examples);
            DecisionTreeCreator treeCreator = new DecisionTreeCreator();
            treeCreator.StartBuilding();
            //Console.WriteLine(treeCreator);
            ITreeExporter exporter = new JSONTreeExporter();
            DateTime dateTime = DateTime.Now;

            // Add this option in configuration 
            decisionTreeCreator = treeCreator;
            String folderPath = InitialConfig.GetInstance().ResultDirectory;
            String resultsPathFile =  folderPath + "/Tree-" + exporter.Regex.Replace(dateTime.ToString(), "-") + ".txt";
            exporter.ExportTree(resultsPathFile, treeCreator.RootNode);
        }
    }
}
