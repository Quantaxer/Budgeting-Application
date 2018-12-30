using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_GUI
{
    [Serializable]
    public class TypeOfCost
    {
        private Dictionary<string, double> listOfElements;
        private string type;

        public TypeOfCost()
        {
            listOfElements = new Dictionary<string, double>();
            type = "";
        }

        public TypeOfCost(string x)
        {
            listOfElements = new Dictionary<string, double>();
            type = x;
        }

        public override string ToString()
        {
            return type;
        }

        public string getType()
        {
            return type;
        }

        public void setType(string x)
        {
            type = x;
        }

        public void addItem(string month, double value)
        {
            listOfElements.Add(month, value);
        }

        public void removeItem(string month)
        {
            listOfElements.Remove(type);
        }

        public double findVal(string month)
        {
            if (listOfElements.ContainsKey(month))
            {
                return listOfElements[month];
            }
            return -1;
        }

        public Dictionary<string, double> returnDict()
        {
            return listOfElements;
        }

        public double calculateAverage()
        {
            double total = 0;
            if (listOfElements.Count == 0)
            {
                return 0;
            }
            foreach (KeyValuePair<string, double> item in listOfElements)
            {
                total += item.Value;
            }
            return total / listOfElements.Count;
        }
    }
}