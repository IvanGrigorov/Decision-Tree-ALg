using Decision_Tree_ALg.AlgLogicLib;
using Decision_Tree_ALg.Config;
using Decision_Tree_ALg.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Decision_Tree_ALg.TreeStructures
{
    public class TreeNode : ItreeNode
    {   

        public double Entropy { get; set; }
        public double InfGain { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string>[] RouteToNode { get; set; }
        public IDataEntity[] UsedExamplesSoFar { get; set; } = InitialConfig.GetInstance().InitialExamples;
        public string[] PossibleOutcomes { get; set; }
        public ICollection<ItreeNode> Children { get; set; }
        public string ParentTransition { get; set; }
        public IDictionary<string, string> LeafInf { get; set; }
        public bool IsUsed { get; set; } = false;


        // Initially entropy and infGain can be 0
        public TreeNode(string name, string[] outcomes, ICollection<ItreeNode> children, IDictionary<string, string> leafInf, IDictionary<string, string>[] routeToNode = null,
            double entropy = 0, double infGain = 0, bool isUsed = false)
        {
            this.LeafInf = leafInf;
            this.Entropy = entropy;
            this.InfGain = infGain;
            this.IsUsed = isUsed;
            this.Name = name;
            this.RouteToNode = routeToNode;
            this.PossibleOutcomes = outcomes;
            this.Children = children;
        }

        public void UpdateEntropy(int[] arrayWithAmountsOfDifferentClassificationValues,
            int amountOfTrainingExamples)
        {
            this.Entropy = DecisionTreeCalculator.CalculateEntropy(arrayWithAmountsOfDifferentClassificationValues,
                amountOfTrainingExamples);
        }
        public void UpdateInfGain(double entropyForClassificationFactor, int[] afterClassificationVlaues, int amountOfExamplesUsed,
            int[][] diferentOutcomesafterClassValues)
        {
            //TODO: complete the method
            this.InfGain = DecisionTreeCalculator.CalculateInformationGain(entropyForClassificationFactor, afterClassificationVlaues, amountOfExamplesUsed,
            diferentOutcomesafterClassValues);
        }



        public int CalculateAmountOfExmplesWithSpecificProperties(string nameOfFactor, string outcome)
        {            
            int count = 0;
            foreach (var example in this.UsedExamplesSoFar)
            {
                if (example[nameOfFactor] == null)
                {
                    throw new NullReferenceException("Trying to invoke property which doesn't exist. Check your parameter value to correspond with DataEntity properties.");
                }
                string[] possibleOutcomesForExample;
                InitialConfig.GetInstance().FeatureOutcomes.TryGetValue(nameOfFactor, out possibleOutcomesForExample);
                if (!possibleOutcomesForExample.Any(s => s.Equals(outcome)))
                {
                    throw new ArgumentException("The outcome passed as parameter is not valid for the specified feature(Attribute).");
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
                if ((string)example[feature] == outcome)
                {
                    tmpListOfExamples.Add(example);
                }
            }
            return tmpListOfExamples.ToArray();
        }

        public string DeclareLeafInf(int[] amountsForDifferentExamples)
        {
            if (amountsForDifferentExamples.Any(amount => amount < 0))
            {
                throw new ArgumentOutOfRangeException("There is no possibility positive or negative examples amounts to be negative.");
            }
            if (amountsForDifferentExamples.All(amount => amount == 0))
            {
                throw new ArgumentException("There is no possibility both positive and negative examples amounts to be zero.");
            }
            if (amountsForDifferentExamples.Count(amount => amount != 0) == 1)
            {
                var classificationValues = InitialConfig.GetInstance().FeatureOutcomes.First(outcome => outcome.Key == InitialConfig.NameOfClassifiedFeature);
                var indexOfNeededOutcome = Array.FindIndex(amountsForDifferentExamples, value => value > 0);
                return classificationValues.Value[indexOfNeededOutcome];
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
