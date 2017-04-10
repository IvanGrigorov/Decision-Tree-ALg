using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree_ALg.Features
{
    class Humidity
    {
        private String humidValueClassificated;
        private double humidity;

        public String TempValueClassified { get; private set; }


        public double HumidityVal
        {
            get { return humidity; }
            set
            {
                humidity = value;
                humidValueClassificated = SetClassification(value);
            }
        }

        public Humidity(double humidity)
        {
            this.humidity = humidity;
        }

        public string SetClassification(double humidity)
        {
            if (humidity < 5)
            {
                return "cold";
            }
            else if (humidity < 25)
            {
                return "normal";
            }
            else
            {
                return "hot";
            }
        }
    }
}
