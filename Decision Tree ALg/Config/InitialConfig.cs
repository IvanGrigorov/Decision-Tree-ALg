using Decision_Tree_ALg.DataEntities;
using System.Collections.Generic;
using Decision_Tree_ALg.ReadingLibrary;
using Decision_Tree_ALg.PrepareDataLib;
using Decision_Tree_ALg.TreeStructures;
using System.Linq;
using System; 

namespace Decision_Tree_ALg.Config
{
    class InitialConfig
    {
        private int NumberOfFeatures { get; set; } = 4;
        private static InitialConfig configuration;
        public IDataEntity[] InitialExamples { get; } = DataReader.ReturnAllExamplesFromFile("../../TrainingData/DataExamples.txt");



        // These can be changed according to the different training data  

        // Store all feature names and number of possible outcomes
        private Dictionary<string, int> FeatureNames = new Dictionary<string, int>()
            {
                { "RainTypeClassificated", 3},
                { "TempClassificated", 3},
                { "HumidClassificated", 3},
                { "WindClassificated", 2},
                { "ClassifiedResult", 2}

            };

        private Dictionary<string, string[]> FeatureOutcomes = new Dictionary<string, string[]>()
        {
            { "RainTypeClassificated", new string[] { "Low", "High", "Normal" } },
            { "TempClassificated", new string[] { "Hot", "Mild", "Cool" } },
            { "HumidClassificated", new string[] { "Low", "High", "Normal" } },
            { "WindClassificated", new string[] { "Strong", "Weak" } },
            { "ClassifiedResult", new string[] { "Yes", "No" } },

        };

        public static string NameOfDataClass { get; set; } = "Decision_Tree_ALg.DataEntities.DataEntity";

        public static Type TypeOfObject { get { return Type.GetType(NameOfDataClass); } }

        private InitialConfig() 
        {
        }

        public TreeNode[] NodesToBeInserted { get; set; } 

        // Create Singleton case to use only one version of the configuration
        public static InitialConfig GetInstance()
        {
            if (configuration == null)
            {
                configuration = new InitialConfig();
                configuration.NodesToBeInserted = new TreeNode[configuration.NumberOfFeatures];
                for (int index = 0; index < configuration.NumberOfFeatures; index++)
                {
                    var tempFeatureInfo = configuration.FeatureOutcomes.ElementAt(index);
                    configuration.NodesToBeInserted[index] = new TreeNode(tempFeatureInfo.Key, tempFeatureInfo.Value);
                }

            }
            return configuration;
        }
    }
}
