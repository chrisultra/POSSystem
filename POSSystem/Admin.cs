using System;
using static System.Console;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace POSSystem
{
    public partial class Admin : Form
    {

        const string CATSFILE = "categories.dat";
        const string ITEMSFILE = "items.dat";

        List<Item> itemList = new List<Item>();
        List<Item> itemC = new List<Item>();

        const char DELIM = ',';
        FileStream file = null;
        StreamReader reader = null;
        


        public Admin()
        {
            InitializeComponent();

            load();
            load2();
            List<string> lines = File.ReadAllLines(CATSFILE).ToList();

            catComboBox.DataSource = lines;

            //itemsListView.View = View.Details;

            //itemsListView.Columns.Add("ID");
            //itemsListView.Columns.Add("Item Name");
            //itemsListView.Columns.Add("Cost");
            //itemsListView.Columns.Add("Category");



            //itemsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //itemsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


            // Declare and construct the ColumnHeader objects.
            ColumnHeader header1, header2, header3, header4;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "ID";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 80;

            header2.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Item Name";
            header2.Width = 250;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "Cost";
            header3.Width = 100;

            header4.TextAlign = HorizontalAlignment.Left;
            header4.Text = "Category";
            header4.Width = 180;

            // Add the headers to the ListView control.
            itemsListView.Columns.Add(header1);
            itemsListView.Columns.Add(header2);
            itemsListView.Columns.Add(header3);
            itemsListView.Columns.Add(header4);

            // Specify that each item appears on a separate line.
            itemsListView.View = View.Details;

            //foreach (ListViewItem item in itemsListView.Items)
            //{
            //    item.ForeColor = Color.Red;
            //}
            itemsListView.FullRowSelect = true;

        }


        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(CATSFILE, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                String catName = catNameTextBox.Text;

                if (string.IsNullOrEmpty(catName) == false)
                {
                    catName = FirstCharToUpper(catName);
                    sw.WriteLine(catName);
                    catListBox.Items.Add(catName);
                    catNameTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Please enter a category",
                        "Advisory");
                }
            }
        }


        public void load()
        {
            using (FileStream fs = new FileStream(CATSFILE, FileMode.Open, FileAccess.Read))
            using (StreamReader r = new StreamReader(fs))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    catListBox.Items.Add(line);
                }
            }     
        }


        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please fill in all fields!", "Advisory");
                return input;
            }
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }


        private void addItemButton_Click(object sender, EventArgs e)
        {

            List<string> itemC = File.ReadAllLines(ITEMSFILE).ToList();
                           
            using (FileStream fsItems = new FileStream(ITEMSFILE, FileMode.Append, FileAccess.Write))
            using (StreamWriter swItems = new StreamWriter(fsItems))
            {
                int tempint = itemC.Count + 1;
                String id = tempint.ToString();
                String itemName = itemNameTextBox.Text;
                String catCB = catComboBox.Text;
                String itemCost = costTextBox.Text;

                if (string.IsNullOrEmpty(itemName) == false)
                {
                    itemName = FirstCharToUpper(itemName);
                    swItems.WriteLine(id + DELIM + itemName + DELIM + itemCost + DELIM + catCB);

                    itemsListView.Items.Add(new ListViewItem(new string[] { id, itemName, itemCost, catCB }));
                    itemList.Add(new Item() { id = id, name = itemName, cost = itemCost, category = catCB });
                    itemNameTextBox.Clear();
                    costTextBox.Clear();
                    WriteToConsole(itemList);
                }
                else
                {
                    MessageBox.Show("Please complete all fields",
                        "Advisory");
                }
                
            }
        }



        public void WriteToConsole(IEnumerable item)
        {
            foreach (object o in item)
            {
                Console.WriteLine(o);
            }
        }


        public void load2()
        {

            if (File.Exists("items.dat"))
            {
             
                using (FileStream file = new FileStream(ITEMSFILE, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(file))
                {
                    string recordIn;
                    string[] fields;
                    itemsListView.Items.Clear();

                        while (!reader.EndOfStream)
                        {
                            
                            recordIn = reader.ReadLine();
                            fields = recordIn.Split(DELIM);
                            itemList.Add(new Item() { id = fields[0], name = fields[1], cost = fields[2], category = fields[3] });
                            itemsListView.Items.Add(new ListViewItem(new string[] { fields[0], fields[1], fields[2], fields[3] }));
                   
                        }

                    WriteToConsole(itemList);

                }
            }
            else {

                return;
            }

        }
    }
}
