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

namespace Test_GUI
{
    public partial class TypeOfCostForm : Form
    {
        Dictionary<string, double> listToEdit;
        private Form1 parentForm;
        public TypeOfCostForm()
        {
            InitializeComponent();
        }

        //Overloaded constructor which takes in a TypeOfCost class to be used by the form
        public TypeOfCostForm(TypeOfCost x, Form1 parent)
        {
            InitializeComponent();
            listToEdit = x.returnDict();
            parentForm = parent;
            label2.Text = x.getType();
            updateListView();
        }

        //Helper method for error trapping input boxes
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

        //Helper function which updates the screen to show the correct information
        public void updateListView()
        {
            listView1.Items.Clear();
            foreach (KeyValuePair<string, double> item in listToEdit)
            {
                ListViewItem lvi = new ListViewItem(item.Key);
                lvi.SubItems.Add(Convert.ToString(item.Value));
                listView1.Items.Add(lvi);
            }
            parentForm.updateAll();
        }

        //Button which adds an element to the list
        private void button1_Click(object sender, EventArgs e)
        {
            string month = Interaction.InputBox("Please enter the month");
            string amount = Interaction.InputBox("Please enter the amount of money");
            //Checks whether or not the number is actually a number
            if (isOnlyDigit(amount))
            {
                listToEdit.Add(month, Convert.ToDouble(amount));
            }
            //Otherwise display an error
            else
            {
                MessageBox.Show("ERROR: Not a valid number");
            }
            updateListView();
        }

        //Button which edits an element in the list
        private void button2_Click(object sender, EventArgs e)
        {
            string toEdit = Interaction.InputBox("Enter the element you want to change the cost of");
            foreach (KeyValuePair<string, double> item in listToEdit)
            {
                if (item.Key.Equals(toEdit))
                {
                    string amount = Interaction.InputBox("Enter the new cost");
                    //Check if the user inputted a valid number
                    if (isOnlyDigit(amount))
                    {
                        listToEdit[toEdit] = Convert.ToDouble(amount);
                        updateListView();
                    }
                    //Otherwise display an error
                    else
                    {
                        MessageBox.Show("ERROR: Not a valid number");
                    }
                    //Quits if the item was found
                    return;
                }
            }
            //If the item was not found, display an error
            MessageBox.Show("ERROR: element not found");
            updateListView();
        }

        //Deletes an element from the list
        private void button3_Click(object sender, EventArgs e)
        {
            string toRemove = Interaction.InputBox("Please enter the item you want to delete");
            foreach (KeyValuePair<string, double> item in listToEdit)
            {
                if (item.Key.Equals(toRemove))
                {
                    listToEdit.Remove(item.Key);
                    updateListView();
                    //If the element was found, exit
                    return;
                }
            }
            //If the element was not found, display an error
            MessageBox.Show("ERROR: item not found");
            updateListView();
        }
    }
}