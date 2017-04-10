using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree_ALg.DataEntities
{
    class DataEntity : IDataEntity
    {
             
        public string WindClassificated { get;  set; }
        public string HumidClassificated { get; set; }
        public string RainTypeClassificated { get; set; }
        public string TempClassificated { get; set; }
        public string ClassifiedResult { get; set; }


        public DataEntity(string rain, string temp, string humid, string wind, string result)
        {
            this.RainTypeClassificated = rain;
            this.TempClassificated = temp;
            this.HumidClassificated = humid;
            this.WindClassificated = wind;
            this.ClassifiedResult = result;
        }

        // Get Property by indexing it with string
        public object this[string nameOfProperty]
        {
            get
            {
                PropertyInfo property = GetType().GetProperty(nameOfProperty);
                return property.GetValue(this, null);
            }
            set
            {
                PropertyInfo property = GetType().GetProperty(nameOfProperty);
                property.SetValue(this, value, null);
            }
        }
    }
}
