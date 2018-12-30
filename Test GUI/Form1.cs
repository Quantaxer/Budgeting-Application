using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Test_GUI
{
    [Serializable]
    public partial class Form1 : Form
    {
        AllUsers listOfUsers = new AllUsers();
        User currentUser = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead("userList.data"))
            {
                listOfUsers = (AllUsers)formatter.Deserialize(stream);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void createNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User user = new User(Interaction.InputBox("Please enter your first name"), Interaction.InputBox("Please enter your last name"));
            listOfUsers.AddUser(user);
            currentUser = user;
            updateAll();
        }

        private void switchUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentUser = listOfUsers.FindUser(Interaction.InputBox("Please enter your first name"), Interaction.InputBox("Please enter your last name"));
            updateAll();
        }

        private void deleteCurrentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listOfUsers.RemoveUser(currentUser);
            currentUser = null;
            updateAll();
        }

        private void updateAll()
        {
            if (currentUser != null)
            {
                label1.Text = "Welcome " + currentUser.ToString() + ".";
                if ((currentUser.getBudget().getAllExpenses().Count == 0) && (currentUser.getBudget().getAllRevenue().Count == 0))
                {
                    label5.Text = "$0.00";

                }
                else if ((currentUser.getBudget().getAllExpenses().Count == 0) && (currentUser.getBudget().getAllRevenue().Count != 0)) {
                    double val = currentUser.getBudget().calculateTotalRevenue();
                    if (currentUser.getBudget().getPercentTaken() != 0)
                    {
                        val = val - val * currentUser.getBudget().getPercentTaken();
                    }
                    label5.Text = "$" + val.ToString("f2");
                }
                else if ((currentUser.getBudget().getAllExpenses().Count != 0) && (currentUser.getBudget().getAllRevenue().Count == 0))
                {
                    label5.Text = "$-" + currentUser.getBudget().calculateTotalExpenses().ToString("f2");
                }
                else
                {
                    label5.Text = "$" + currentUser.getBudget().calculateBudgetAfterPercentage().ToString("f2");

                }

                if (currentUser.getBudget().getAllExpenses().Count == 0) 
                {
                    label7.Text = "$0.00";
                }
                else
                {
                    label7.Text = "$" + currentUser.getBudget().calculateTotalExpenses().ToString("f2");
                }

                if (currentUser.getBudget().getAllRevenue().Count == 0)
                {
                    label6.Text = "$0.00";
                }
                else
                {
                    label6.Text = "$" + currentUser.getBudget().calculateTotalRevenue().ToString("f2");
                }


                listView2.Items.Clear();
                foreach (TypeOfCost item in currentUser.getBudget().getAllExpenses())
                {
                    ListViewItem lvi = new ListViewItem(item.getType());
                    lvi.SubItems.Add("$" + Convert.ToString(item.calculateAverage().ToString("f2")));
                    if (currentUser.getBudget().calculateTotalExpenses() == 0)
                    {
                        lvi.SubItems.Add("0.00%");
                    }
                    else
                    {
                        lvi.SubItems.Add((item.calculateAverage() / currentUser.getBudget().calculateTotalExpenses() * 100).ToString("f2") + "%");
                    }
                    lvi.Tag = item;
                    listView2.Items.Add(lvi);
                }

                listView3.Items.Clear();
                foreach (TypeOfCost item in currentUser.getBudget().getAllRevenue())
                {
                    ListViewItem lvi2 = new ListViewItem(item.getType());
                    lvi2.SubItems.Add("$" + Convert.ToString(item.calculateAverage().ToString("f2")));
                    if (currentUser.getBudget().calculateTotalRevenue() == 0)
                    {
                        lvi2.SubItems.Add("0.00%");
                    }
                    else
                    {
                        lvi2.SubItems.Add((item.calculateAverage() / currentUser.getBudget().calculateTotalRevenue() * 100).ToString("f2") + "%");
                    }
                    lvi2.Tag = item;
                    listView3.Items.Add(lvi2);
                }
            }
            else
            {
                label1.Text = "No current user";
                listView2.Items.Clear();
                listView3.Items.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                string toEdit = Interaction.InputBox("Enter the element you want to rename");
                foreach (TypeOfCost item in currentUser.getBudget().getAllExpenses())
                {
                    if (item.getType().Equals(toEdit))
                    {
                        item.setType(Interaction.InputBox("Enter the newname of the element"));
                        updateAll();
                        return;
                    }
                }
                MessageBox.Show("ERROR: item not found");
                updateAll();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                string toEdit = Interaction.InputBox("Enter the element you want to rename");
                foreach (TypeOfCost item in currentUser.getBudget().getAllRevenue())
                {
                    if (item.getType().Equals(toEdit))
                    {
                        item.setType(Interaction.InputBox("Enter the newname of the element"));
                        updateAll();
                        return;
                    }
                }
                MessageBox.Show("ERROR: item not found");
                updateAll();
            }
        }

        //Create new expense
        private void button1_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                TypeOfCost temp = new TypeOfCost(Interaction.InputBox("Please enter the name of the expense"));
                currentUser.getBudget().getAllExpenses().Add(temp);
                updateAll();
            }
        }

        //Create new revenue
        private void button3_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                TypeOfCost temp = new TypeOfCost(Interaction.InputBox("Please enter the name of the revenue"));
                currentUser.getBudget().getAllRevenue().Add(temp);
                updateAll();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                double percentage = Convert.ToDouble(Interaction.InputBox("Please enter the percentage you would like to be put away (format: 15 = 15%)"));
                if ((percentage <=100) && (percentage >= 0))
                {
                    currentUser.getBudget().setPercentTaken(percentage);
                    updateAll();
                }
                else
                {
                    MessageBox.Show("ERROR: not a valid percentage");
                }
            }
        }

        private void showListOfAllUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllUsers tempUsers = listOfUsers;

            allUserForm userForm = new allUserForm(listOfUsers);
            userForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                string toRemove = Interaction.InputBox("Please enter the expense you want to delete");
                foreach (TypeOfCost item in currentUser.getBudget().getAllExpenses())
                {
                    if (item.getType().Equals(toRemove))
                    {
                        currentUser.getBudget().getAllExpenses().Remove(item);
                        updateAll();
                        return;
                    }
                }
                MessageBox.Show("ERROR: item not found");
                updateAll();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                string toRemove = Interaction.InputBox("Please enter the revenue you want to delete");
                foreach (TypeOfCost item in currentUser.getBudget().getAllRevenue())
                {
                    if (item.getType().Equals(toRemove))
                    {
                        currentUser.getBudget().getAllRevenue().Remove(item);
                        updateAll();
                        return;
                    }
                }
                MessageBox.Show("ERROR: item not found");
                updateAll();
            }
        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                TypeOfCost foo = (TypeOfCost)listView3.SelectedItems[0].Tag;
                TypeOfCostForm temp = new TypeOfCostForm(foo);
                temp.Show();
            }
        }

        private void saveStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Create("userList.data"))
            {
                formatter.Serialize(stream, listOfUsers);
            }
        }

        private void loadStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead("userList.data"))
            {
                listOfUsers = (AllUsers)formatter.Deserialize(stream);
            }
        }

        private void resetStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listOfUsers.resetState();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                TypeOfCost foo = (TypeOfCost)listView2.SelectedItems[0].Tag;
                TypeOfCostForm temp = new TypeOfCostForm(foo);
                temp.Show();
            }
        }
    }
}
