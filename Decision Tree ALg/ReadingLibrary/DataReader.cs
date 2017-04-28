using Decision_Tree_ALg.DataEntities;
using System.Collections.Generic;
using Decision_Tree_ALg.PrepareDataLib;
using System;
using Decision_Tree_ALg.Config;
using System.Reflection;

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
            while ((line = file.ReadLine()) != null)
            {
                string[] splitedData = DataPreparator.splitDataExamples(line);
                // Simple Factory type creation using Reflection
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

        public static IDataEntity[] ReturnAllExamplesFromEmbeddedRessource()
        {
            Dictionary<int, IDataEntity> dataEntities = new Dictionary<int, IDataEntity>();

            int lineIndex = 0;
            // Read the file and display it line by line.
            var file = Properties.Resources.DataExamples;
            var streamAsLines = file.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in streamAsLines)
            {  
                string[] splitedData = DataPreparator.splitDataExamples(line);
                var entity = (IDataEntity)Activator.CreateInstance(InitialConfig.TypeOfObject, splitedData);
                dataEntities.Add(lineIndex, entity);
                //dataEntities.Add(lineIndex, line);
                lineIndex++;
            }

            IDataEntity[] dataEntitiesAsArray = new IDataEntity[dataEntities.Count];
            dataEntities.Values.CopyTo(dataEntitiesAsArray, 0);
            return dataEntitiesAsArray;
        }
    }
}
