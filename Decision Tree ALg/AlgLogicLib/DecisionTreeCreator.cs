using Decision_Tree_ALg.Config;
using Decision_Tree_ALg.DataEntities;
using Decision_Tree_ALg.TreeStructures;
using System;
using System.Linq;

namespace Decision_Tree_ALg.AlgLogicLib
{
    /// <summary>
    /// The class is responsible for creating all tree branches, nodes and leaves
    /// It also contains methods to calculate the entropy and information gain for all nodes
    /// </summary>
    public class DecisionTreeCreator
    {
        public ItreeNode RootNode { get; set; }

        public void InsertRootNode(ItreeNode[] allPossibleNodes)
        {
            ItreeNode root = allPossibleNodes.OrderBy(node => node.InfGain).Last();
            root.IsUsed = true;
            this.RootNode = root;
        }

        public void StartBuilding() 
        {
            var dataExamples = InitialConfig.GetInstance().InitialExamples;
            /// First update of Inf Gain 
            foreach (var node in InitialConfig.GetInstance().NodesToBeInserted)
            {
                node.UpdateEntropy(this.CalculateAmountOfExamplesWithDifferentClassifications(dataExamples), dataExamples.Length);

                /// calculate the examples which contain specific value
                int[] storeAmountOfOutcomeValueHits = new int[node.PossibleOutcomes.Length];
                int[][] classificatedeExamplesAfterSpecificValue = new int[node.PossibleOutcomes.Length][];
                for (int i = 0; i < node.PossibleOutcomes.Length; i++)
                {
                    storeAmountOfOutcomeValueHits[i] = node.CalculateAmountOfExmplesWithSpecificProperties(node.Name, node.PossibleOutcomes[i]);
                    var examplesAfterOutcomeValue = node.ReturnAllExamplesAfterValueCondition(node.Name, node.PossibleOutcomes[i]); 
                    classificatedeExamplesAfterSpecificValue[i] = CalculateAmountOfExamplesWithDifferentClassifications(examplesAfterOutcomeValue);
                }

                node.UpdateInfGain(node.Entropy, storeAmountOfOutcomeValueHits, node.UsedExamplesSoFar.Length, classificatedeExamplesAfterSpecificValue);
                Console.WriteLine();
            }

            // Initially all nodes are ready to be inserted
            InsertRootNode(InitialConfig.GetInstance().NodesToBeInserted);
            ContinueFillingTheTreeRecursivly(this.RootNode, InitialConfig.GetInstance().IsMemoryOptimized);
        }

        public void ContinueFillingTheTreeRecursivly(ItreeNode currentNode, bool isMemoryOptimized)
        {
            // With more complex and bigger amount of possible Nodes it might be better
            // To optimize this by splitting used and unused nodes in order not to 
            // iterate in big sized array every time 
            // (for smaller sets it is not that necessary because of memory space optimization)
            var availableNodes = InitialConfig.GetInstance().NodesToBeInserted.Where(tmpNode => !tmpNode.IsUsed);
            
            foreach (var outcome in currentNode.PossibleOutcomes)
            {
                string leafInf = string.Empty;
                int[] clasificatedExamplesAfterOutcome = CalculateAmountOfExamplesWithDifferentClassifications(currentNode.ReturnAllExamplesAfterValueCondition(currentNode.Name, outcome));
                try
                {
                    leafInf = currentNode.DeclareLeafInf(clasificatedExamplesAfterOutcome);
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    Environment.Exit(0);
                }
                catch (ArgumentException)
                {
                    leafInf = currentNode.DeclareLeafInf(CalculateAmountOfExamplesWithDifferentClassifications(currentNode.UsedExamplesSoFar));

                }
                if (leafInf != "Node")
                {
                    currentNode.LeafInf.Add(outcome, leafInf);
                    continue;
                }
                if (availableNodes.ToArray().Length == 0)
                {
                    int indexOfPossibleOutcomes = Array.IndexOf(clasificatedExamplesAfterOutcome, clasificatedExamplesAfterOutcome.Max());
                    var outcomes = InitialConfig.GetInstance().FeatureOutcomes.First(tmpOutcome => tmpOutcome.Key == InitialConfig.NameOfClassifiedFeature);
                    currentNode.LeafInf.Add(outcome, outcomes.Value[indexOfPossibleOutcomes]);
                    continue;
                }

                var dataExamples = currentNode.UsedExamplesSoFar;

                foreach (var node in availableNodes)
                {
                    if (!node.IsUsed)
                    {
                        node.UsedExamplesSoFar = dataExamples;
                        node.UsedExamplesSoFar = node.ReturnAllExamplesAfterValueCondition(currentNode.Name, outcome);
                        node.UpdateEntropy(this.CalculateAmountOfExamplesWithDifferentClassifications(node.UsedExamplesSoFar), node.UsedExamplesSoFar.Length);

                        // calculate the examples which contain specific value
                        int[] storeAmountOfOutcomeValueHits = new int[node.PossibleOutcomes.Length];
                        int[][] classificatedExamplesAfterSpecificValue = new int[node.PossibleOutcomes.Length][];
                        for (int i = 0; i < node.PossibleOutcomes.Length; i++)
                        {
                            storeAmountOfOutcomeValueHits[i] = node.CalculateAmountOfExmplesWithSpecificProperties(node.Name, node.PossibleOutcomes[i]);
                            var examplesAfterOutcomeValue = node.ReturnAllExamplesAfterValueCondition(node.Name, node.PossibleOutcomes[i]);
                            classificatedExamplesAfterSpecificValue[i] = CalculateAmountOfExamplesWithDifferentClassifications(examplesAfterOutcomeValue);
                        }

                        node.UpdateInfGain(node.Entropy, storeAmountOfOutcomeValueHits, node.UsedExamplesSoFar.Length, classificatedExamplesAfterSpecificValue);                   
                    }
                }

                ItreeNode nextNode = availableNodes.OrderBy(tmpNode => tmpNode.InfGain).Last();
                nextNode.IsUsed = true;
                nextNode.ParentTransition = outcome;
                currentNode.Children.Add(nextNode);
            }
            foreach (var child in currentNode.Children)
            {
                ContinueFillingTheTreeRecursivly(child, isMemoryOptimized);
            }       
            if (isMemoryOptimized)
            {
                currentNode.UsedExamplesSoFar = null; 
            }
        }

        public int[] CalculateAmountOfExamplesWithDifferentClassifications(IDataEntity[] examples)
        {
           // int negativeExamples = 0;
           // int positiveExamples = 0;

            string[] possibleClassifications = InitialConfig.GetInstance().FeatureOutcomes.First(outcome => outcome.Key == InitialConfig.NameOfClassifiedFeature).Value;

            int amountOfPissibleClassifications = possibleClassifications.Length;
            int[] result = new int[amountOfPissibleClassifications];

            foreach (var dataExample in examples)
            {
                for (int i = 0; i < amountOfPissibleClassifications; i++)
                {
                    if  (dataExample.ClassifiedResult == possibleClassifications[i])
                    {
                        result[i]++;
                    }
                }
            }
            return result;
                //new int[] { negativeExamples, positiveExamples };
        }

        //public createLeavesFor
    }
}
