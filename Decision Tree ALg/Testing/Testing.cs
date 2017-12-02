using Decision_Tree_ALg;
using Decision_Tree_ALg.Config;
using Decision_Tree_ALg.DataEntities;
using Decision_Tree_ALg.ReadingLibrary;
using Decision_Tree_ALg.TreeStructures;
using System;

namespace Decision_Tree_Alg.Testing
{
    sealed public class Testing
    {
    
        private IDataEntity[] DataExamplesForTesting { get; set; }
        //public int InitConfig { get; private set; }

        public Testing()
        {
          
        }

        public double ReturnSuccessfullPercentageRateAfterTesting()
        {
            return (((double)this.ReturnAmountOfSuccessfullPredictions() / (double)this.DataExamplesForTesting.Length)) * 100;

            //return (((double) this.DataExamplesForTesting.Length / (double) this.ReturnAmountOfSuccessfullPredictions())) * 100;
        }

        private int ReturnAmountOfSuccessfullPredictions()
        {
            int successfullMatchesNumber = 0;
            foreach (var entity in this.DataExamplesForTesting)
            {
                // There should be another check in case if StartUp.GeneratedTree throws and exception 
                // Not only in console project 
                if (((string) entity[InitialConfig.NameOfClassifiedFeature]).Equals(entity.GetOutcomeForEntity(StartUp.GeneratedTree.RootNode))) {
                    successfullMatchesNumber++;
                }
            }
            return successfullMatchesNumber;
        }

        public void LoadDataTestExamples(string pathFile)
        {
            this.DataExamplesForTesting = DataReader.ReturnAllExamplesFromFile(pathFile);
        }
    }
}
