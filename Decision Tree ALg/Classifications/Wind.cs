using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree_ALg.Classifications
{
    class Wind
    {
        private double windSpeedInKm;
        private string windSpeedClassificated;

        public double WindSpeedInKm
        {
            get { return this.windSpeedInKm; }
            set
            {
                if (value < 5)
                {
                    this.windSpeedClassificated = "low";
                }
                else if (value < 25)
                {
                    this.windSpeedClassificated = "normal";
                }
                else
                {
                    this.windSpeedClassificated = "high";
                }

            }
        }

        public string WindSpeedClassificated { get; private set; }
    }
}
