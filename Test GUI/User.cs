using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_GUI
{
    [Serializable]
    //This class is a specific user in the database. They have a first and last name, and a specific budget plan.
    public class User
    {
        private String firstName;
        private String lastName;
        private Budget userBudget;
        
        public User()
        {
            firstName = "";
            lastName = "";
            userBudget = new Budget();
        }

        //Overloaded constructor
        public User(String first, String last)
        {
            firstName = first;
            lastName = last;
            userBudget = new Budget();
        }

        //Overridden tostring function
        public override string ToString()
        {
            return firstName + " " + lastName;
        }

        //Getter for the first name
        public String getFirstName()
        {
            return firstName;
        }

        //Getter for the last name
        public String getLastName()
        {
            return lastName;
        }

        //Setter for the first name
        public void setFirstName(String first)
        {
            firstName = first;
        }

        //Getter for the last name
        public void setLastName(String last)
        {
            lastName = last;
        }

        //Setter for the user's budget
        public void setBudget(Budget b)
        {
            userBudget = b;
        }

        //Getter for the user's budget
        public Budget getBudget()
        {
            return userBudget;
        }
    }
}
