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
using System.Globalization;

namespace POSSystem
{
    public partial class Admin : Form
    {

        // Declare datafiles
        const string CATSFILE = "categories.dat";
        const string ITEMSFILE = "items.dat";

        // Object lists
        List<Item> itemList = new List<Item>();
        List<Item> itemC = new List<Item>();

        // Variable to read break in data files
        const char DELIM = ',';

        FileStream file = null;
        StreamReader reader = null;

        // TODO: varaible for updating parent form (MainUI)
        MainUI _owner;

        public Admin(MainUI owner)
        {
            // Call initial load methods
            InitializeComponent();
            load();
            load2();
            loadLVColumnHeaders();

            // Read cateogry file
            List<string> lines = File.ReadAllLines(CATSFILE).ToList();

            // Populate combobox
            catComboBox.DataSource = lines;

            // Declare and save variable for setting tax rate to properties settings
            // Format it and save to string
            double taxRate = Properties.Settings.Default.TaxRate;
            double taxRateTemp = taxRate * 100;
            taxRateTextBox.Text = taxRateTemp.ToString();

            //_owner = owner;
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
        }


        // Not implemented yet
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            _owner.loadCatButtons();
        }



        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(CATSFILE, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                String catName = catNameTextBox.Text;

                if (string.IsNullOrEmpty(catName) == false)
                {
                    catName = CapitalizeWords(catName);
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
            // Load categories
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



        public static string CapitalizeWords(string input)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (input.Length == 0)
                return input;

            StringBuilder sb = new StringBuilder(input.Length);
            // Upper the first char.
            sb.Append(char.ToUpper(input[0]));
            for (int i = 1; i < input.Length; i++)
            {
                // Get the current char.
                char c = input[i];

                // Upper if after a space.
                if (char.IsWhiteSpace(input[i - 1]))
                    c = char.ToUpper(c);
                else
                    c = char.ToLower(c);

                sb.Append(c);
            }

            return sb.ToString();
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
                    itemName = CapitalizeWords(itemName);
                    swItems.WriteLine(id + DELIM + itemName + DELIM + itemCost + DELIM + catCB);

                    itemsListView.Items.Add(new ListViewItem(new string[] { id, itemName, itemCost, catCB }));
                    itemList.Add(new Item() { id = id, name = itemName, cost = itemCost, category = catCB });
                    itemNameTextBox.Clear();
                    costTextBox.Clear();
                    WriteToConsole(itemList);
                }
                else
                {
                    MessageBox.Show("Please complete all fields", "Advisory");
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
            else
            {
                return;
            }
        }


        public void loadLVColumnHeaders()
        {
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
            header4.Width = 160;

            // Add the headers to the ListView control.
            itemsListView.Columns.Add(header1);
            itemsListView.Columns.Add(header2);
            itemsListView.Columns.Add(header3);
            itemsListView.Columns.Add(header4);

            // Specify that each item appears on a separate line.
            itemsListView.View = View.Details;

            itemsListView.FullRowSelect = true;
        }






        private void saveTaxRateButton_Click(object sender, EventArgs e)
        {
            if ((taxRateTextBox.Text == "") || (taxRateTextBox.Text == null))
            {
                MessageBox.Show("Please enter a tax rate", "Advisory");
                return;
            }
            else {            
            string taxRateTemp = taxRateTextBox.Text;
            double taxRateDub = double.Parse(taxRateTemp, CultureInfo.InvariantCulture);
            double taxRate = taxRateDub / 100;
            Properties.Settings.Default.TaxRate = taxRate;
            Properties.Settings.Default.Save();

            MessageBox.Show("The tax rate has been saved", "Success");
            }
        }
    }
}
