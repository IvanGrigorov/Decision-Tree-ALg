using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using Decision_Tree_ALg.TreeStructures;

namespace Decision_Tree_ALg.DataEntities
{
    public abstract class ADataEntity : IDataEntity
    {
             
        public string ClassifiedResult { get; set; }
        protected ExpandoObject EpandedObject { get; set; }
        // Get Property by indexing it with string
        public object this[string nameOfProperty]
        {
            get
            {
                
                PropertyInfo property = this.EpandedObject.GetType().GetProperty(nameOfProperty);
                return ((IDictionary<string, object>)this.EpandedObject)[nameOfProperty];
                //return property.GetValue(this.EpandedObject, null);
            }
            set
            {
                PropertyInfo property = this.EpandedObject.GetType().GetProperty(nameOfProperty);
                property.SetValue(this.EpandedObject, value, null);
            }
        }

        public string GetOutcomeForEntity(ItreeNode tree)
        {
            if (tree.LeafInf.Keys.Contains((string) (this[tree.Name])))
            {
                return tree.LeafInf[(string)this[tree.Name]];
            }
            else
            {
                foreach (var treeNode in tree.Children)
                {
                    return this.GetOutcomeForEntity(treeNode);
                }
            }
            throw new ArgumentException("Entity not classified");
            
        }
    }
}
