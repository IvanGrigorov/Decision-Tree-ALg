using Decision_Tree_ALg.DataEntities;
using System.Collections.Generic;
using Decision_Tree_ALg.AlgLogicLib;
using Decision_Tree_ALg.Config;
using System;


namespace Decision_Tree_ALg.TreeStructures
{
    public class TreeNode
    {

        public double Entropy { get; set; }
        public double InfGain { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string>[] RouteToNode { get; set; }
        public IDataEntity[] UsedExamplesSoFar { get; set; } = InitialConfig.GetInstance().InitialExamples; 
        public string[] PossibleOutcomes { get; set;}
        public IList<TreeNode> Children { get; set; } = new List<TreeNode>();
        public string ParentTransition { get; set; } = null;
        public IDictionary<string, string> LeafInf { get; set; } = new Dictionary<string, string>(); 
        public bool IsUsed { get; set; } = false;


        // Initially entropy and infGain can be 0
        public TreeNode(string name, string[] outcomes, Dictionary<string, string>[] routeToNode = null, 
            double entropy = 0, double infGain = 0, bool isUsed = false)
        {
            this.Entropy = entropy;
            this.InfGain = infGain;
            this.IsUsed = isUsed;
            this.Name = name;
            this.RouteToNode = routeToNode;
            this.PossibleOutcomes = outcomes;
        }

        public void UpdateEntropy(int[] arrayWithAmountsOfDifferentClassificationValues,
            int amountOfTrainingExamples)
        {
            this.Entropy =  DecisionTreeCreator.calculateEntropy(arrayWithAmountsOfDifferentClassificationValues,
                amountOfTrainingExamples);
        }
        public void UpdateInfGain(double entropyForClassificationFactor, int[] afterClassificationVlaues, int amountOfExamplesUsed,
            int[][] diferentOutcomesafterClassValues)
        {
            //TODO: complete the method
            this.InfGain = DecisionTreeCreator.calculateInformationGain(entropyForClassificationFactor, afterClassificationVlaues,  amountOfExamplesUsed,
            diferentOutcomesafterClassValues);
        }



        public int calculateAmountOfExmplesWithSpecificProperties(string nameOfFactor, string outcome)
        {
            int count = 0;
            foreach (var example in this.UsedExamplesSoFar)
            {
                if (example[nameOfFactor] == null)
                {
                    throw new NullReferenceException("Trying to invoke property which doesn't exist. Check your parameter value to correspond with DataEntity properties.");
                }
                if ((string)example[nameOfFactor] == outcome)
                {
                    count++;
                }
            }
            return count;
        }

        public IDataEntity[] ReturnAllExamplesAfterValueCondition(string feature, string outcome)
        {
            List<IDataEntity> tmpListOfExamples = new List<IDataEntity>();
            foreach (var example in this.UsedExamplesSoFar)
            {
                if ((string) example[feature] == outcome)
                {
                    tmpListOfExamples.Add(example);
                }
            }
            return tmpListOfExamples.ToArray();
        }

        public string DeclareLeafInf(int[] negativeAndPositiveAmount)
        {
            if (negativeAndPositiveAmount[0] == 0)
            {
                return "Yes";
            }
            if (negativeAndPositiveAmount[1] == 0)
            {
                return "No";
            }
            else
            {
                return "Node";
            }
        }

        //public static Dictionary<int, DataEntity> ReturnAllExamplesFromFile(string dataFilePath)
        //{
        //    Dictionary<int, DataEntity> dataEntities = new Dictionary<int, DataEntity>();

        //    string line;
        //    int lineIndex = 0;

        //    // Read the file and display it line by line.
        //    System.IO.StreamReader file =
        //       new System.IO.StreamReader(dataFilePath);
        //    while ((line = file.ReadLine()) != null)
        //    {
        //        string[] splitedData = DataPreparator.splitDataExamples(line);
        //        dataEntities.Add(lineIndex, new DataEntity(splitedData[0], splitedData[1], splitedData[2], splitedData[3], splitedData[4]));
        //        //dataEntities.Add(lineIndex, line);
        //        lineIndex++;
        //    }

        //    file.Close();

        //    return dataEntities;



        //}
    }
}
