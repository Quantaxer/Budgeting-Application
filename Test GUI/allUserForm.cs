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

        public allUserForm(AllUsers x)
        {
            InitializeComponent();
            temp = x;
        }

        private void allUserForm_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            foreach (User x in temp.getList())
            {
                ListViewItem lvi = new ListViewItem(x.GetFirstName());
                lvi.SubItems.Add(x.GetLastName());
                listView1.Items.Add(lvi);
            }
        }
    }
}
