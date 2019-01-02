using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_GUI
{
    [Serializable]
    public class AllUsers
    {
        private List<User> listOfAllUsers;
        public AllUsers()
        {
            listOfAllUsers = new List<User>();
        }

        public override string ToString()
        {
            return string.Join(",", listOfAllUsers.ToString());
        }

        public void addUser(User x)
        {
            listOfAllUsers.Add(x);
        }

        public void removeUser(User x)
        {
            if (listOfAllUsers.Contains(x))
            {
                listOfAllUsers.Remove(x);
            }
        }

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

        public List<User> getList()
        {
            return listOfAllUsers;
        }

        public void resetState()
        {
            listOfAllUsers.Clear();
        }
    }
}
