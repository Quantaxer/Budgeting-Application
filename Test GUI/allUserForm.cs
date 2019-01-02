using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_GUI
{
    public partial class allUserForm : Form
    {
        AllUsers temp;
        public allUserForm()
        {
            InitializeComponent();
        }

        //Overloaded constructor, taking in the list of all users in order to display it
        public allUserForm(AllUsers x)
        {
            InitializeComponent();
            temp = x;
        }

        //Initializer which displays the list of all users
        private void allUserForm_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            //Loops through the list of all users and adds them as a subitem to the listview
            foreach (User x in temp.getList())
            {
                ListViewItem lvi = new ListViewItem(x.getFirstName());
                lvi.SubItems.Add(x.getLastName());
                listView1.Items.Add(lvi);
            }
        }
    }
}
