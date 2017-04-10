using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_Tree_ALg.Features
{
    class Temp
    {
        private String tempValueClassificated;
        private double tempInCelsius;

        public String TempValueClassified { get; private set;}


        public double TempInCelsius { get { return tempInCelsius; }
            set
            {
                tempInCelsius = value;
                tempValueClassificated = SetClassification(value); 
            }
        }

        public Temp(double tempInCelsius)
        {
            this.tempInCelsius = tempInCelsius;
        }

        public string SetClassification(double valueInCelsius)
        {
            if (valueInCelsius < 5)
            {
                return "cold";
            }
            else if (valueInCelsius < 25)
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
