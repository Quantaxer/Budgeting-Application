using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_GUI
{
    [Serializable]
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

        public User(String first, String last)
        {
            firstName = first;
            lastName = last;
            userBudget = new Budget();
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }

        public String GetFirstName()
        {
            return firstName;
        }

        public String GetLastName()
        {
            return lastName;
        }

        public void SetFirstName(String first)
        {
            firstName = first;
        }

        public void SetLastName(String last)
        {
            lastName = last;
        }

        public void setBudget(Budget b)
        {
            userBudget = b;
        }

        public Budget getBudget()
        {
            return userBudget;
        }
    }
}
