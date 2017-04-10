using System;
using System.Collections.Generic;

using Decision_Tree_ALg.DataEntities;

namespace Decision_Tree_ALg.PrepareDataLib
{
    class DataPreparator
    { 
        public static String[] splitDataExamples(string fileLineWithExample)
        {
            String[] splitedDataExample = fileLineWithExample.Split(new char[] { ',' });
            return splitedDataExample;
        }
    }
}
