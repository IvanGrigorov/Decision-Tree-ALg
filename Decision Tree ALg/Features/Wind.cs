using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree_ALg.Features
{
    class Wind
    {
        private String windValueClassificated;
        private double windSpeed;

        public String WindValueClassificated { get; private set; }


        public double WindSpeed
        {
            get { return windSpeed; }
            set
            {
                windSpeed = value;
                this.windValueClassificated = SetClassification(value);
            }
        }

        public Wind(double windSpeed)
        {
            this.windSpeed = windSpeed;
        }

        public string SetClassification(double windSpeed)
        {
            if (windSpeed < 5)
            {
                return "low";
            }
            else if (windSpeed < 10)
            {
                return "normal";
            }
            else
            {
                return "high";
            }
        }
    }
}
