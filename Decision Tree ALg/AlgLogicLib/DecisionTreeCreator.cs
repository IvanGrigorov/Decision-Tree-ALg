﻿using Decision_Tree_ALg.DataEntities;
using System.Collections.Generic;
using Decision_Tree_ALg.PrepareDataLib;
using Decision_Tree_ALg.TreeStructures;
using Decision_Tree_ALg.Config;
using System.Linq;
using System; 

namespace Decision_Tree_ALg.AlgLogicLib
{
    class DecisionTreeCreator
    {

        public TreeNode RootNode { get; set; }

        public static double calculateEntropy(int[] arrayWithAmountsOfDifferentClassificationValues, 
            int amountOfTrainingExamples)
        {
            double entropy = 0;
            int amountsOfValuesInArray = arrayWithAmountsOfDifferentClassificationValues.Length;
            foreach (var classValue in arrayWithAmountsOfDifferentClassificationValues)
            {
                double numberOfLogarithm = (double)classValue / (double)amountOfTrainingExamples; 
                if (numberOfLogarithm == 0)
                {
                    return 0; 
                }
                entropy -= numberOfLogarithm * Math.Log(numberOfLogarithm, amountsOfValuesInArray);
            }
            return entropy;
        } 

        public static double calculateInformationGain(double entropyForClassificationFactor, int[] afterClassificationVlaues, int amountOfExamplesUsed, 
            int[][] diferentOutcomesafterClassValues )        
        {
            double informationGain = entropyForClassificationFactor;

            for (int index = 0; index < afterClassificationVlaues.Length; index++)
            { 

                informationGain -= (double)afterClassificationVlaues[index] / (double)amountOfExamplesUsed * calculateEntropy(diferentOutcomesafterClassValues[index], afterClassificationVlaues[index]);
            }

            return informationGain;
        }

        public void InsertRootNode(TreeNode[] allPossibleNodes)
        {
            TreeNode root = allPossibleNodes.OrderBy(node => node.InfGain).Last();
            root.IsUsed = true;
            this.RootNode = root;
        }

        public void StartBuilding() 
        {
            var dataExamples = InitialConfig.GetInstance().InitialExamples;
            // First update of Inf Gain 
            foreach (var node in InitialConfig.GetInstance().NodesToBeInserted)
            {
                node.UpdateEntropy(this.CalculateNegativeAndPositiveAmountOfExamples(dataExamples), dataExamples.Length);

                // calculate the examples which contain specific value
                int[] storeAmountOfOutcomeValueHits = new int[node.PossibleOutcomes.Length];
                int[][] positiveAndNegativeExamplesAfterSpecificValue = new int[node.PossibleOutcomes.Length][];
                for (int i = 0; i < node.PossibleOutcomes.Length; i++)
                {
                    storeAmountOfOutcomeValueHits[i] = node.calculateAmountOfExmplesWithSpecificProperties(node.Name, node.PossibleOutcomes[i]);
                    var examplesAfterOutcomeValue = node.ReturnAllExamplesAfterValueCondition(node.Name, node.PossibleOutcomes[i]); 
                    positiveAndNegativeExamplesAfterSpecificValue[i] = CalculateNegativeAndPositiveAmountOfExamples(examplesAfterOutcomeValue);
                }

                node.UpdateInfGain(node.Entropy, storeAmountOfOutcomeValueHits, node.UsedExamplesSoFar.Length, positiveAndNegativeExamplesAfterSpecificValue);
                Console.WriteLine();
            }

            // Initially all nodes are ready to be inserted
            InsertRootNode(InitialConfig.GetInstance().NodesToBeInserted);
            ContinueFillingTheTreeRecursivly(this.RootNode);
        }

        public void ContinueFillingTheTreeRecursivly(TreeNode currentNode)
        {
            // With more complex and bigger amount of possible Nodes it might be better
            // To optimize this by splitting used and unused nodes in order not to 
            // iterate in big sized array every time 
            // (for smaller sets it is not that necessary because of memory space optimization)
            var availableNodes = InitialConfig.GetInstance().NodesToBeInserted.Where(tmpNode => !tmpNode.IsUsed);
            
            foreach (var outcome in currentNode.PossibleOutcomes)
            {
                int[] possitiveAndNegativeExamplesAfterOutcome = CalculateNegativeAndPositiveAmountOfExamples(currentNode.ReturnAllExamplesAfterValueCondition(currentNode.Name, outcome));
                string leafInf = currentNode.DeclareLeafInf(possitiveAndNegativeExamplesAfterOutcome);
                if (leafInf != "Node")
                {
                    currentNode.LeafInf.Add(outcome, leafInf);
                    continue;
                }
                if (availableNodes.ToArray().Length == 0)
                {
                    if (possitiveAndNegativeExamplesAfterOutcome[0] <= possitiveAndNegativeExamplesAfterOutcome[1])
                    {
                        currentNode.LeafInf.Add(outcome, "Yes");
                        continue;
                    }
                    else
                    {
                        currentNode.LeafInf.Add(outcome, "No");
                        continue;

                    }
                }

                var dataExamples = currentNode.UsedExamplesSoFar;

                foreach (var node in availableNodes)
                {
                    if (!node.IsUsed)
                    {
                        node.UsedExamplesSoFar = dataExamples;
                        node.UsedExamplesSoFar = node.ReturnAllExamplesAfterValueCondition(currentNode.Name, outcome);
                        node.UpdateEntropy(this.CalculateNegativeAndPositiveAmountOfExamples(node.UsedExamplesSoFar), node.UsedExamplesSoFar.Length);

                        // calculate the examples which contain specific value
                        int[] storeAmountOfOutcomeValueHits = new int[node.PossibleOutcomes.Length];
                        int[][] positiveAndNegativeExamplesAfterSpecificValue = new int[node.PossibleOutcomes.Length][];
                        for (int i = 0; i < node.PossibleOutcomes.Length; i++)
                        {
                            storeAmountOfOutcomeValueHits[i] = node.calculateAmountOfExmplesWithSpecificProperties(node.Name, node.PossibleOutcomes[i]);
                            var examplesAfterOutcomeValue = node.ReturnAllExamplesAfterValueCondition(node.Name, node.PossibleOutcomes[i]);
                            positiveAndNegativeExamplesAfterSpecificValue[i] = CalculateNegativeAndPositiveAmountOfExamples(examplesAfterOutcomeValue);
                        }

                        node.UpdateInfGain(node.Entropy, storeAmountOfOutcomeValueHits, node.UsedExamplesSoFar.Length, positiveAndNegativeExamplesAfterSpecificValue);                   
                    }
                }
                
                TreeNode nextNode = availableNodes.OrderBy(tmpNode => tmpNode.InfGain).Last();
                nextNode.IsUsed = true;
                nextNode.ParentTransition = outcome;
                currentNode.Children.Add(nextNode);
            }

            foreach (var child in currentNode.Children)
            {
                ContinueFillingTheTreeRecursivly(child);
            }
            
        }

        public int[] CalculateNegativeAndPositiveAmountOfExamples(IDataEntity[] examples)
        {
            int negativeExamples = 0;
            int positiveExamples = 0;

            foreach (var dataExample in examples)
            {
                if (dataExample.ClassifiedResult == "No")
                {
                    negativeExamples++;
                }
                else if (dataExample.ClassifiedResult == "Yes")
                {
                    positiveExamples++;
                }
            }
            return new int[] { negativeExamples, positiveExamples };

        }

        //public createLeavesFor

    }
}
