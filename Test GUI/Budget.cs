using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_GUI
{
    [Serializable]
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

        public override string ToString()
        {
            return calculateExpenses() + ", " + calculateRevenue() + ": " + calculateBudgetAfterPercentage();
        }

        public List<TypeOfCost> getAllExpenses()
        {
            return allExpenses;
        }

        public List<TypeOfCost> getAllRevenue()
        {
            return allRevenue;
        }

        public void setPercentTaken(double percentage)
        {
            percentTaken = percentage / 100;
        }

        public double getPercentTaken()
        {
            return percentTaken;
        }

        public double calculateTotalExpenses()
        {
            double total = 0;
            foreach (TypeOfCost item in allExpenses)
            {
                total += item.calculateAverage();
            }
            return total;
        }

        public double calculateExpenses()
        {
            double total = 0;
            foreach (TypeOfCost item in allExpenses)
            {
                total += item.calculateAverage();
            }
            return total / allExpenses.Count;
        }

        public double calculateTotalRevenue()
        {
            double total = 0;
            foreach (TypeOfCost item in allRevenue)
            {
                total += item.calculateAverage();
            }
            return total;
        }
        
        public double calculateRevenue()
        {
            double total = 0;
            foreach (TypeOfCost item in allRevenue)
            {
                total += item.calculateAverage();
            }
            return total / allRevenue.Count;
        }

    public double calculateBudgetBeforePercentage()
        {
            return calculateTotalRevenue() - calculateTotalExpenses();
        }

        public double calculateBudgetAfterPercentage()
        {
            totalBudget = calculateBudgetBeforePercentage() - calculateBudgetBeforePercentage() * percentTaken;
            return totalBudget;
        }
    }
}
