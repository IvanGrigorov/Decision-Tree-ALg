using System;

namespace Decision_Tree_ALg.AlgLogicLib
{
    public class DecisionTreeCalculator
    {
        /// <summary>
        /// The Method calculates the entropy for current node(Feature or Attribute)
        /// </summary>
        /// <param name="arrayWithAmountsOfDifferentClassificationValues">The array should have length equal to the possible outcomes
        /// value[0] - amount of examples with first outcome, etc. </param>
        /// <param name="amountOfTrainingExamples">Gives the amount of all examples used so far</param>
        /// <returns>The Entropy for the node</returns>
        public static double CalculateEntropy(
            int[] arrayWithAmountsOfDifferentClassificationValues,
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

        /// <summary>
        /// The Method calculates the Information Gain for current node(Feature or Attribute)
        /// </summary>
        /// <param name="entropyForClassificationFactor">Entropy based on examples used so far (before the new node)</param>
        /// <param name="afterClassificationVlaues">Array with length equal to the amount of possible outcomes - each[i] contains 
        /// the amount of examples with this outcome (based on examples used so far)</param>
        /// <param name="amountOfExamplesUsed">Amount of examples used so far</param>
        /// <param name="diferentOutcomesafterClassValues">Matrix[i] - All possible outcomes - Matrix[i][0] - Contains amount of positive examples after this outcome(i)
        /// Matrix[i][1] - Contains amount of negative examples after this outcome(i)</param>
        /// <returns>The Information Gain for the Node</returns>
        public static double CalculateInformationGain(
            double entropyForClassificationFactor, 
            int[] afterClassificationVlaues, 
            int amountOfExamplesUsed,
            int[][] diferentOutcomesafterClassValues)
        {
            double informationGain = entropyForClassificationFactor;

            for (int index = 0; index < afterClassificationVlaues.Length; index++)
            {
                informationGain -= (double)afterClassificationVlaues[index] / (double)amountOfExamplesUsed * CalculateEntropy(diferentOutcomesafterClassValues[index], afterClassificationVlaues[index]);
            }

            return informationGain;
        }
    }
}