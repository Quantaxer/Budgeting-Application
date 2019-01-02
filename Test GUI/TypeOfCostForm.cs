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
        public TypeOfCostForm()
        {
            InitializeComponent();
        }

        //Overloaded constructor which takes in a TypeOfCost class to be used by the form
        public TypeOfCostForm(TypeOfCost x)
        {
            InitializeComponent();
            listToEdit = x.returnDict();
            label2.Text = x.getType();
            updateListView();
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
        }

        //Button which adds an element to the list
        private void button1_Click(object sender, EventArgs e)
        {
            listToEdit.Add(Interaction.InputBox("Please enter the month"), Convert.ToDouble(Interaction.InputBox("Please enter the amount of money")));
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
                    listToEdit[toEdit] = Convert.ToDouble(Interaction.InputBox("Enter the new cost"));
                    updateListView();
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
                    //IF the element was found, exit
                    return;
                }
            }
            //If the element was not found, display an error
            MessageBox.Show("ERROR: item not found");
            updateListView();
        }
    }
}
