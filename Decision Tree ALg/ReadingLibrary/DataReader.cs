using Decision_Tree_ALg.DataEntities;
using System.Collections.Generic;
using Decision_Tree_ALg.PrepareDataLib;
using System;
using Decision_Tree_ALg.Config;

namespace Decision_Tree_ALg.ReadingLibrary
{
    class DataReader
    {
        public static IDataEntity[] ReturnAllExamplesFromFile(string dataFilePath)
        {
            Dictionary<int, IDataEntity> dataEntities = new Dictionary<int, IDataEntity>();

            string line;
            int lineIndex = 0;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(dataFilePath);
            while ((line = file.ReadLine()) != null)
            {
                string[] splitedData = DataPreparator.splitDataExamples(line);
                var entity = (IDataEntity) Activator.CreateInstance(InitialConfig.TypeOfObject, splitedData);
                dataEntities.Add(lineIndex, entity);
                //dataEntities.Add(lineIndex, line);
                lineIndex++;
            }

            file.Close();
            IDataEntity[] dataEntitiesAsArray = new IDataEntity[dataEntities.Count];
            dataEntities.Values.CopyTo(dataEntitiesAsArray, 0);
            return dataEntitiesAsArray;
        }
    }
}
