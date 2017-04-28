using Decision_Tree_ALg.DataEntities;
using System.Collections.Generic;
using Decision_Tree_ALg.AlgLogicLib;
using Decision_Tree_ALg.Config;
using System;
using System.Collections;

namespace Decision_Tree_ALg.TreeStructures
{
    public interface ItreeNode
    {

        double Entropy { get; set; }
        double InfGain { get; set; }
        string Name { get; set; }
        IDictionary<string, string>[] RouteToNode { get; set; }
        IDataEntity[] UsedExamplesSoFar { get; set; }
        string[] PossibleOutcomes { get; set; }
        ICollection<ItreeNode> Children { get; set; }
        string ParentTransition { get; set; }
        IDictionary<string, string> LeafInf { get; set; }
        bool IsUsed { get; set; }


        void UpdateEntropy(int[] arrayWithAmountsOfDifferentClassificationValues,
            int amountOfTrainingExamples);

        void UpdateInfGain(double entropyForClassificationFactor, int[] afterClassificationVlaues, int amountOfExamplesUsed,
            int[][] diferentOutcomesafterClassValues);

        int CalculateAmountOfExmplesWithSpecificProperties(string nameOfFactor, string outcome);


        IDataEntity[] ReturnAllExamplesAfterValueCondition(string feature, string outcome);


        string DeclareLeafInf(int[] negativeAndPositiveAmount);


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
