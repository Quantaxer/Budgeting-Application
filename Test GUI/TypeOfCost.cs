using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_GUI
{
    [Serializable]
    //This class is responsible as acting as a list of months a user may have as either an individual expense or revenue. 
    public class TypeOfCost
    {
        private Dictionary<string, double> listOfElements;
        private string type;

        public TypeOfCost()
        {
            listOfElements = new Dictionary<string, double>();
            type = "";
        }

        //Overloaded constructor
        public TypeOfCost(string x)
        {
            listOfElements = new Dictionary<string, double>();
            type = x;
        }

        //Overridden tostring
        public override string ToString()
        {
            return type;
        }

        //Getter for the name of the item
        public string getType()
        {
            return type;
        }

        //Setter for the name of the item
        public void setType(string x)
        {
            type = x;
        }

        //Utility function to add an item to the list
        public void addItem(string month, double value)
        {
            listOfElements.Add(month, value);
        }

        //Utility function to remove an item from the list
        public void removeItem(string month)
        {
            listOfElements.Remove(type);
        }

        //Utility function to find an element in the list. Returns -1 if it does not exist. Takes in the month and returns the value associated.
        public double findVal(string month)
        {
            if (listOfElements.ContainsKey(month))
            {
                return listOfElements[month];
            }
            return -1;
        }

        //Function which returns the dictionary to be used by the forms
        public Dictionary<string, double> returnDict()
        {
            return listOfElements;
        }

        //Utility function which calulates the average cost of each month
        public double calculateAverage()
        {
            double total = 0;
            //Returns 0 if there are no elements in the dictionary
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