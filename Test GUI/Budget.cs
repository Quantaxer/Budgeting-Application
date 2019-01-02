using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_GUI
{
    [Serializable]
    //This class is meant to be the user's budget, which involves a list of expenses and revenues, as well as their total budget per month
    public class Budget
    {
        private List<TypeOfCost> allExpenses;
        private List<TypeOfCost> allRevenue;
        private double totalBudget;
        private double percentTaken;
        
        public Budget()
        {
            allExpenses = new List<TypeOfCost>();
            allRevenue = new List<TypeOfCost>();
            totalBudget = 0.0;
            percentTaken = 0.0;
        }

        //Overridden tostring method
        public override string ToString()
        {
            return calculateExpenses() + ", " + calculateRevenue() + ": " + calculateBudgetAfterPercentage();
        }

        //Getter for the list of all expenses a user may have
        public List<TypeOfCost> getAllExpenses()
        {
            return allExpenses;
        }

        //Getter for a list of all revenues a user may have
        public List<TypeOfCost> getAllRevenue()
        {
            return allRevenue;
        }

        //Setter for the total percentage taken away to be put in their savings
        public void setPercentTaken(double percentage)
        {
            percentTaken = percentage / 100;
        }

        //Getter for the total percentage taken away to be put in their savings
        public double getPercentTaken()
        {
            return percentTaken;
        }

        //Utility method to calculate the total average expenses
        public double calculateTotalExpenses()
        {
            double total = 0;
            foreach (TypeOfCost item in allExpenses)
            {
                total += item.calculateAverage();
            }
            return total;
        }

        //Utility function which calculates the total expenses without taking into account the average
        public double calculateExpenses()
        {
            double total = 0;
            foreach (TypeOfCost item in allExpenses)
            {
                total += item.calculateAverage();
            }
            return total / allExpenses.Count;
        }

        //Utility method to calculate the total average revenues
        public double calculateTotalRevenue()
        {
            double total = 0;
            foreach (TypeOfCost item in allRevenue)
            {
                total += item.calculateAverage();
            }
            return total;
        }

        //Utility function which calculates the total revenue without taking into account the average
        public double calculateRevenue()
        {
            double total = 0;
            foreach (TypeOfCost item in allRevenue)
            {
                total += item.calculateAverage();
            }
            return total / allRevenue.Count;
        }

        //Utility function to calculate the total budget without taking into account the amount of money to be put away
        public double calculateBudgetBeforePercentage()
        {
            return calculateTotalRevenue() - calculateTotalExpenses();
        }

        //Utility function to calulate the total budget
        public double calculateBudgetAfterPercentage()
        {
            totalBudget = calculateBudgetBeforePercentage() - calculateBudgetBeforePercentage() * percentTaken;
            return totalBudget;
        }
    }
}
