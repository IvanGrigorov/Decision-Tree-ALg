using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree_ALg.Classifications
{
    class Humidity
    {
        private double humidInPercent;
        private string humidClassificated;

        public double HumidInPercent
        {
            get { return this.humidInPercent; }
            set
            {
                if (value < 15)
                {
                    this.humidClassificated = "low";
                }
                else if (value < 40)
                {
                    this.humidClassificated = "normal";
                }
                else
                {
                    this.humidClassificated = "high";
                }
            }
        }

        public string HumidClassificated { get; private set; }
    }
}
