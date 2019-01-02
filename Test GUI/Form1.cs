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

        private void form1_Load(object sender, EventArgs e)
        {
            //Read from file the list of all users
            IFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead("userList.data"))
            {
                listOfUsers = (AllUsers)formatter.Deserialize(stream);
            }
        }

        private bool isOnlyDigit(string s)
        {
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
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

        //Button to create a new user
        private void createNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User user = new User(Interaction.InputBox("Please enter your first name"), Interaction.InputBox("Please enter your last name"));
            listOfUsers.addUser(user);
            currentUser = user;
            updateAll();
        }

        //Button to switch the current user
        private void switchUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentUser = listOfUsers.findUser(Interaction.InputBox("Please enter your first name"), Interaction.InputBox("Please enter your last name"));
            updateAll();
        }

        //Button to delete current user
        private void deleteCurrentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listOfUsers.removeUser(currentUser);
            currentUser = null;
            updateAll();
        }

        //Utility function to update the main screen
        private void updateAll()
        {
            if (currentUser != null)
            {
                label1.Text = "Welcome " + currentUser.ToString() + ".";
                //This chunk of code determines what the user's budget is.

                //This checks if the user currently has no expenses and revenues
                if ((currentUser.getBudget().getAllExpenses().Count == 0) && (currentUser.getBudget().getAllRevenue().Count == 0))
                {
                    label5.Text = "$0.00";

                }
                //This checks if the user only has a revenue but no expenses
                else if ((currentUser.getBudget().getAllExpenses().Count == 0) && (currentUser.getBudget().getAllRevenue().Count != 0)) {
                    //Calculate the total revenue and update the text
                    double val = currentUser.getBudget().calculateTotalRevenue();
                    if (currentUser.getBudget().getPercentTaken() != 0)
                    {
                        val = val - val * currentUser.getBudget().getPercentTaken();
                    }
                    label5.Text = "$" + val.ToString("f2");
                }
                //This checks if the user only has expenses, but no revenues
                else if ((currentUser.getBudget().getAllExpenses().Count != 0) && (currentUser.getBudget().getAllRevenue().Count == 0))
                {
                    label5.Text = "$-" + currentUser.getBudget().calculateTotalExpenses().ToString("f2");
                }
                //This is when the user has both expenses and revenues
                else
                {
                    label5.Text = "$" + currentUser.getBudget().calculateBudgetAfterPercentage().ToString("f2");
                }

                //This text updates the user's total revenues
                if (currentUser.getBudget().getAllExpenses().Count == 0) 
                {
                    label7.Text = "$0.00";
                }
                else
                {
                    label7.Text = "$" + currentUser.getBudget().calculateTotalExpenses().ToString("f2");
                }

                //This text updates the user's total expenses
                if (currentUser.getBudget().getAllRevenue().Count == 0)
                {
                    label6.Text = "$0.00";
                }
                else
                {
                    label6.Text = "$" + currentUser.getBudget().calculateTotalRevenue().ToString("f2");
                }

                //This for loop updates the listView for the expenses.
                listView2.Items.Clear();
                foreach (TypeOfCost item in currentUser.getBudget().getAllExpenses())
                {
                    //Add the name of the expense
                    ListViewItem lvi = new ListViewItem(item.getType());
                    //Add the total average cost of the expense
                    lvi.SubItems.Add("$" + Convert.ToString(item.calculateAverage().ToString("f2")));
                    //Add the percentage of total expenses
                    if (currentUser.getBudget().calculateTotalExpenses() == 0)
                    {
                        lvi.SubItems.Add("0.00%");
                    }
                    else
                    {
                        lvi.SubItems.Add((item.calculateAverage() / currentUser.getBudget().calculateTotalExpenses() * 100).ToString("f2") + "%");
                    }
                    //Add the item to the list
                    lvi.Tag = item;
                    listView2.Items.Add(lvi);
                }

                //This for loop updates the listView for the revenues
                listView3.Items.Clear();
                foreach (TypeOfCost item in currentUser.getBudget().getAllRevenue())
                {
                    //Add the name of the revenue
                    ListViewItem lvi2 = new ListViewItem(item.getType());
                    //Add the total average revenue
                    lvi2.SubItems.Add("$" + Convert.ToString(item.calculateAverage().ToString("f2")));
                    //Add the percentage of revenues
                    if (currentUser.getBudget().calculateTotalRevenue() == 0)
                    {
                        lvi2.SubItems.Add("0.00%");
                    }
                    else
                    {
                        lvi2.SubItems.Add((item.calculateAverage() / currentUser.getBudget().calculateTotalRevenue() * 100).ToString("f2") + "%");
                    }
                    //Add the item to the list
                    lvi2.Tag = item;
                    listView3.Items.Add(lvi2);
                }

                //This for loop updates the pie chart for all expenses
                string[] expenseNameList = new string[currentUser.getBudget().getAllExpenses().Count()];
                double[] expensePercentList = new double[currentUser.getBudget().getAllExpenses().Count()];
                int i = 0;
                //Initialize the lists and append the values
                foreach (TypeOfCost item in currentUser.getBudget().getAllExpenses())
                {
                    expenseNameList[i] = item.getType();
                    expensePercentList[i] = item.calculateAverage() / currentUser.getBudget().calculateTotalExpenses() * 100;
                    i++;
                }
                //Bind the values to the pie chart, then change the labels
                chart1.Series["Type of Expense"].Points.DataBindXY(expenseNameList, expensePercentList);
                chart1.Series["Type of Expense"].Label = "#PERCENT{0.00%}";
                chart1.Series["Type of Expense"].LegendText = "#AXISLABEL";

                //This for loop updates the pie chart of all revenues
                string[] revenueNameList = new string[currentUser.getBudget().getAllRevenue().Count()];
                double[] revenuePercentList = new double[currentUser.getBudget().getAllRevenue().Count()];
                i = 0;
                //Initialize the lists and append the values
                foreach (TypeOfCost item in currentUser.getBudget().getAllRevenue())
                {
                    revenueNameList[i] = item.getType();
                    revenuePercentList[i] = item.calculateAverage() / currentUser.getBudget().calculateTotalExpenses() * 100;
                    i++;
                }
                //Bind the values to the pie chart and update the labels
                chart2.Series["Type of Revenue"].Points.DataBindXY(revenueNameList, revenuePercentList);
                chart2.Series["Type of Revenue"].Label = "#PERCENT{0.00%}";
                chart2.Series["Type of Revenue"].LegendText = "#AXISLABEL";
            }
            else
            {
                label1.Text = "No current user";
                listView2.Items.Clear();
                listView3.Items.Clear();
            }
        }

        //Rename element from the expenses list
        private void button2_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                string toEdit = Interaction.InputBox("Enter the element you want to rename");
                foreach (TypeOfCost item in currentUser.getBudget().getAllExpenses())
                {
                    if (item.getType().Equals(toEdit))
                    {
                        item.setType(Interaction.InputBox("Enter the new name of the element"));
                        updateAll();
                        return;
                    }
                }
                MessageBox.Show("ERROR: item not found");
                updateAll();
            }
        }

        //Rename element from the revenues list
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

        //Button to change the amount of money to be put away in savings
        private void button5_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                double percentage = 0.0;
                string s = Interaction.InputBox("Please enter the percentage you would like to be put away (format: 15 = 15%)");
                if (isOnlyDigit(s))
                {
                    percentage = Convert.ToDouble(s);
                    if ((percentage <= 100) && (percentage >= 0))
                    {
                        currentUser.getBudget().setPercentTaken(percentage);
                        updateAll();
                    }
                    else
                    {
                        MessageBox.Show("ERROR: not a valid percentage");
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: not a valid number");
                }
            }
        }

        //Shows a new form of all the available users in the database
        private void showListOfAllUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllUsers tempUsers = listOfUsers;

            allUserForm userForm = new allUserForm(listOfUsers);
            userForm.Show();
        }

        //Delete an expense from the list
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

        //Delete a revenue from the list
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

        //Show form for editing a specific type of expense
        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                TypeOfCost foo = (TypeOfCost)listView2.SelectedItems[0].Tag;
                TypeOfCostForm temp = new TypeOfCostForm(foo);
                temp.Show();
            }
        }

        //Show form for editing a specific type of revenue
        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                TypeOfCost foo = (TypeOfCost)listView3.SelectedItems[0].Tag;
                TypeOfCostForm temp = new TypeOfCostForm(foo);
                temp.Show();
            }
        }

        //Button that saves the state of the program to a file
        private void saveStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Create("userList.data"))
            {
                formatter.Serialize(stream, listOfUsers);
            }
        }

        //Button that loads the state of the program from a file
        private void loadStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead("userList.data"))
            {
                listOfUsers = (AllUsers)formatter.Deserialize(stream);
            }
        }

        //Resets the program
        private void resetStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listOfUsers.resetState();
        }
    }
}
