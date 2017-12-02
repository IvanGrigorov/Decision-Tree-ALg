using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree_ALg.DataEntities
{
    public class DataEntity : ADataEntity, IDataEntity
    {
        public DataEntity (ExpandoObject generatedObject)
        {
            this.EpandedObject = generatedObject;
        }     
    }
}
