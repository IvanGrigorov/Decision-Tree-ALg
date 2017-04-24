using Decision_Tree_ALg.DataEntities;
using Decision_Tree_ALg.ReadingLibrary;
using Decision_Tree_ALg.TreeStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Decision_Tree_ALg.Config
{
    /// <summary>
    /// The Main File to configure all system requirements 
    /// </summary>
    class InitialConfig
    {

        /// <summary>
        /// This Section should not be modified
        /// </summary>
        private static readonly Lazy<InitialConfig> lazyConfig = new Lazy<InitialConfig>(() => new InitialConfig());
        private static string projectName = Assembly.GetCallingAssembly().GetName().Name;
        private static string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        private int NumberOfFeatures { get; set; } = 4;
        private static InitialConfig configuration;

        public static string NameOfClassifiedFeature { get; } = "ClassifiedResult";

        // public IDataEntity[] InitialExamples { get; } = DataReader.ReturnAllExamplesFromFile(solutiondir + "/Decision Tree ALg/TrainingData/DataExamples.txt");
        public IDataEntity[] InitialExamples { get; } = DataReader.ReturnAllExamplesFromEmbeddedRessource(TypeOfObject);

        /// <summary>
        /// // These can be changed according to the different training data  
        /// </summary>

        // Store all feature names and number of possible outcomes
        private IDictionary<string, int> FeatureNames = new Dictionary<string, int>()
            {
                { "RainTypeClassificated", 3},
                { "TempClassificated", 3},
                { "HumidClassificated", 3},
                { "WindClassificated", 2},
                { NameOfClassifiedFeature , 2}

            };

        public IDictionary<string, string[]> FeatureOutcomes = new Dictionary<string, string[]>()
        {
            { "RainTypeClassificated", new string[] { "Low", "High", "Normal" } },
            { "TempClassificated", new string[] { "Hot", "Mild", "Cool" } },
            { "HumidClassificated", new string[] { "Low", "High", "Normal" } },
            { "WindClassificated", new string[] { "Strong", "Weak" } },
            { NameOfClassifiedFeature, new string[] { "Yes", "No" } },

        };


        private static Type TypeOfObject { get { return typeof(DataEntity); } }

        private InitialConfig() 
        {
        }

        public TreeNode[] NodesToBeInserted { get; set; } 

        // Create Singleton case to use only one version of the configuration
        // Lazy evaluation for less memory consumption
        public static InitialConfig GetInstance()
        {
            if (configuration == null)
            {
                configuration = lazyConfig.Value;
                configuration.NodesToBeInserted = new TreeNode[configuration.NumberOfFeatures];
                for (int index = 0; index < configuration.NumberOfFeatures; index++)
                {
                    var tempFeatureInfo = configuration.FeatureOutcomes.ElementAt(index);
                    configuration.NodesToBeInserted[index] = new TreeNode(tempFeatureInfo.Key, tempFeatureInfo.Value, new List<ItreeNode>(), new Dictionary<string, string>());
                }

            }
            return configuration;
        }
    }
}
