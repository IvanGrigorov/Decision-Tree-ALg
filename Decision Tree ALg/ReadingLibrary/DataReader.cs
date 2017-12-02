using Decision_Tree_ALg.Config;
using Decision_Tree_ALg.DataEntities;
using Decision_Tree_ALg.PrepareDataLib;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Decision_Tree_ALg.ReadingLibrary
{
    public class DataReader
    {
        public static IDataEntity[] ReturnAllExamplesFromFile(string dataFilePath)
        {
            IDictionary<int, IDataEntity> dataEntities = new Dictionary<int, IDataEntity>();

            string line;
            int lineIndex = 0;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(dataFilePath);
            String[] propertyInfo = DataPreparator.splitDataExamples(File.ReadLines(dataFilePath).First()); // gets the first line from file.
            int lastColumnIndex = propertyInfo.Length;
            InitialConfig.NameOfClassifiedFeature = propertyInfo[lastColumnIndex - 1];

            while ((line = file.ReadLine()) != null)
            {
                if (lineIndex == 0)
                {
                    lineIndex++;
                    continue;
                }
                lineIndex++;

                var newObject = new ExpandoObject();
                string[] splitedData = DataPreparator.splitDataExamples(line);

                // Check for same  length
                if (splitedData.Length  != InitialConfig.NumberOfFeatures + 1)
                {
                    throw new InvalidDataException("There is a mismatch from the amount of results and the amount of features from the config on line " + lineIndex);
                }
                for (int i = 0; i < propertyInfo.Length; i++)
                {
                    ((IDictionary<string, object>)newObject).Add(propertyInfo[i], splitedData[i]);
                }

                var entity = new DataEntity(newObject);
                dataEntities.Add(lineIndex, entity);
                //dataEntities.Add(lineIndex, line);
                lineIndex++;
            }

            file.Close();
            IDataEntity[] dataEntitiesAsArray = new IDataEntity[dataEntities.Count];
            dataEntities.Values.CopyTo(dataEntitiesAsArray, 0);
            return dataEntitiesAsArray;
        }

        public static void ReadTreeNodeConfigFromFile(string pathFile)
        {
            if (!File.Exists(pathFile))
            {
                throw new ArgumentException("File not found at {0}", pathFile);
            }
            else
            {
                StreamReader file = new StreamReader(pathFile);
                string line;
                int lineIndex = 0;
                while ((line = file.ReadLine()) != null)
                {
                    lineIndex++;
                    if (DataReader.validationRegexCollectionNotToBeMatched.Any(regex => regex.IsMatch(line))
                        || DataReader.validationRegexCollectionToBeMatched.Any(regex => !regex.IsMatch(line)))
                    {
                        throw new InvalidDataException("There is an error in the file on line " + lineIndex + " at " + line);
                    }
                    IList<String> nodeData = line.Split(',').Select(param => param.Trim()).ToList();
                    nodeData = nodeData.Where(param => !String.IsNullOrWhiteSpace(param)).ToList();
                    String featureName = nodeData.ElementAt(0);
                    // This is not time consuming operation because we always remove the first element and it is a list
                    nodeData.RemoveAt(0);
                    InitialConfig.FeatureOutcomes.Add(featureName, nodeData.ToArray());
                }
                // Save the number of features used (The last one is the classification so therefor we use -1 ) 
                InitialConfig.NumberOfFeatures = lineIndex - 1;
            }
        }

        public static IDataEntity[] ReturnAllExamplesFromEmbeddedRessource(Type typeOfClassToCreate)
        {
            IDictionary<int, IDataEntity> dataEntities = new Dictionary<int, IDataEntity>();

            int lineIndex = 0;
            // Read the file and display it line by line.
            var file = Properties.Resources.DataExamples;
            var streamAsLines = file.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in streamAsLines)
            {
                string[] splitedData = DataPreparator.splitDataExamples(line);
                if (typeOfClassToCreate.IsAbstract || typeOfClassToCreate.IsInterface)
                {
                    throw new ArgumentException("You cannot instantiate an Interface or an Abstract Class. Change the type in arguments.");
                }
                var entity = (IDataEntity)Activator.CreateInstance(typeOfClassToCreate, splitedData);
                dataEntities.Add(lineIndex, entity);
                //dataEntities.Add(lineIndex, line);
                lineIndex++;
            }

            IDataEntity[] dataEntitiesAsArray = new IDataEntity[dataEntities.Count];
            dataEntities.Values.CopyTo(dataEntitiesAsArray, 0);
            return dataEntitiesAsArray;
        }

        private readonly static Regex[] validationRegexCollectionToBeMatched = new Regex[]
        {
            
            // Test if there is at least one value after the node name see example below: 
            /* 
             * <NodeName>, <nodeValuew> -> at least one -> valid
             * <NodeName>,              -> no node values -> invalid
             * The Node name must have at least one value after the name 
            */
            new Regex(@"^\w+ *, *\w+")
        };

        private readonly static Regex[] validationRegexCollectionNotToBeMatched = new Regex[]
        {
            // This regex is used to validate for unaccepted symbols in a file -> !@#$%^&*(_)_- and TAB and numbers
            new Regex(@"[^A-Za-z ,]")

        };
    }
}
