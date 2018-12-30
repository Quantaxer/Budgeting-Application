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

        public void AddUser(User x)
        {
            listOfAllUsers.Add(x);
        }

        public void RemoveUser(User x)
        {
            if (listOfAllUsers.Contains(x))
            {
                listOfAllUsers.Remove(x);
            }
        }

        public User FindUser(String first, String last)
        {
            foreach(User x in listOfAllUsers)
            {
                if ((x.GetFirstName().Equals(first)) && (x.GetLastName().Equals(last)))
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
