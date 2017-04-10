using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree_ALg.Classifications
{
    class Temperature
    {
        private double tempInCelsius;
        private string tempClassificated;

        public double TempInCelsius
        {
            get { return this.tempInCelsius; }
            set
            {
                if (value < 5)
                {
                    this.tempClassificated = "low";
                }
                else if (value < 25)
                {
                    this.tempClassificated = "normal";
                }
                else
                {
                    this.tempClassificated = "high";
                }
            }
        }

        public string TempClassificated { get; private set; }
    }
}
