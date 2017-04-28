using System.Reflection;

namespace Decision_Tree_ALg.DataEntities
{
    abstract class ADataEntity : IDataEntity
    {
             
        public string ClassifiedResult { get; set; }

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
