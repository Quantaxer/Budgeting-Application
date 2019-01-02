using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_GUI
{
    [Serializable]
    //This class serves as a collection of users
    public class AllUsers
    {
        private List<User> listOfAllUsers;
        public AllUsers()
        {
            listOfAllUsers = new List<User>();
        }

        //Overridden tostring method
        public override string ToString()
        {
            return string.Join(",", listOfAllUsers.ToString());
        }

        //Utility function to add a user
        public void addUser(User x)
        {
            listOfAllUsers.Add(x);
        }

        //Utility function to remove a user
        public void removeUser(User x)
        {
            if (listOfAllUsers.Contains(x))
            {
                listOfAllUsers.Remove(x);
            }
        }

        //Utility function to fins a user in the list. Returns null if it doesn't exist
        public User findUser(String first, String last)
        {
            foreach(User x in listOfAllUsers)
            {
                if ((x.getFirstName().Equals(first)) && (x.getLastName().Equals(last)))
                {
                    return x;
                }
            }
            return null;
        }

        //Getter for the list of all users
        public List<User> getList()
        {
            return listOfAllUsers;
        }

        //Utility function to reset the state of the program
        public void resetState()
        {
            listOfAllUsers.Clear();
        }
    }
}
