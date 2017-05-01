using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static System.Console;
using System.Collections;
using System.Globalization;

namespace POSSystem
{
    public partial class MainUI : Form
    {
        // The data files 
        const string CATSFILE = "categories.dat";
        const string ITEMSFILE = "items.dat";

        // Object lists
        List<Button> catButtonList = new List<Button>();       
        List<Button> itemButtonList = new List<Button>();       
        List<Item> itemList = new List<Item>();
        List<Item> catItemList;
        List<ShCartItem> shoppingCart = new List<ShCartItem>();

        // String list for categories
        List<string> cats = new List<string>();

        string listEntry;

        // Counting variable for scrolling category button text
        int shiftCount = 0;

        // Variable to read break in data files
        const char DELIM = ',';

        // Variables for calculating shopping cart totals
        double taxDub = 0;
        double subDub = 0;
        double totDub = 0;

        string numVar = null;
        string idVar = null;
        string nameVar = null;
        string qtyVar = null;
        string costVar = null;

        int test = 0;

        int shopNum = 0;



        public MainUI()
        {
            // Call initial load methods
            InitializeComponent();
            loadCatButtons();
            loadItemButtons();
            loadMainLVColumnHeaders();
            loadEventHandlers();

            // Disable scroll left button until needed
            if (shiftCount == 0)
            {
                scrollLeftButton.Enabled = false;
            }
        }



        void catButtonClick(object sender, EventArgs e)
        {
            Deselector();

            Button button = sender as Button;

            // Get categories text name for button
            catItemList = itemList.Where(item => item.category == button.Text).ToList();

            // Enable all item buttons
            for (int i = 0; i < itemButtonList.Count; i++)
            {
                itemButtonList[i].Enabled = true;
                itemButtonList[i].Text = "";          
            }
            
            // If there are 16 or more categories, fill all button text values
            if (catItemList.Count >= 16)
            {

                for (int i = 0; i < itemButtonList.Count; i++)
                {
                    listEntry = catItemList[i].name;
                    itemButtonList[i].Text = listEntry;
                }
            }

            // Else just fill the category button texts that exist
            else
            {
                for (int i = 0; i < catItemList.Count; i++)
                {
                    listEntry = catItemList[i].name;
                    itemButtonList[i].Text = listEntry;
                }
            }

            // If button is empty, disable it
            for (int i = 0; i < itemButtonList.Count; i++)
            {
                if ((itemButtonList[i].Text == "") || (itemButtonList[i].Text == null))
                {
                    itemButtonList[i].Enabled = false;
                }
            }
        }



        void itemButtonClick(object sender, EventArgs e)
        {


            Deselector();

            Button button = sender as Button;

            // Match and Get the item text for clicked item button
            catItemList = itemList.Where(item => item.category == button.Text).ToList();

            // Get the data for the item
            Item butname = itemList.Find(item => item.name.Equals(button.Text));

            string id = butname.id;
            string name = butname.name;
            string cost = butname.cost;
            string category = butname.category;

            // Get tax rate from property settings
            double taxRate = Properties.Settings.Default.TaxRate;

            shopNum = shopNum + 1;
            string shopNumString = shopNum.ToString();

            // Add item to shopping cart and listview
            shoppingCart.Add(new ShCartItem() { num = shopNumString, id = id, name = name, cost = cost, qty = "1", category = category });


            mainListView.Items.Add(new ListViewItem(new string[] { shopNumString, id, name, "1", cost }));

            // Calculate subtotal 
            double costDub = double.Parse(cost, CultureInfo.InvariantCulture);
            subDub = subDub + costDub;
            subTotalLabel.Text = subDub.ToString("C");

            // Calculate tax
            double taxTemp = costDub * taxRate;
            taxDub = taxTemp + taxDub;
            taxLabel.Text = taxDub.ToString("C");

            // Calculate total sale
            double totTemp = costDub + taxTemp;
            totDub = totTemp + totDub;
            totalLabel.Text = totDub.ToString("C");



            test = shoppingCart.Count();
            Console.Write(test);

        }

        public void loadEventHandlers()
        {
            // Create new event handlers for category and item buttons
            itemButton1.Click += new EventHandler(this.itemButtonClick);
            itemButton2.Click += new EventHandler(this.itemButtonClick);
            itemButton3.Click += new EventHandler(this.itemButtonClick);
            itemButton4.Click += new EventHandler(this.itemButtonClick);
            itemButton5.Click += new EventHandler(this.itemButtonClick);
            itemButton6.Click += new EventHandler(this.itemButtonClick);
            itemButton7.Click += new EventHandler(this.itemButtonClick);
            itemButton8.Click += new EventHandler(this.itemButtonClick);
            itemButton9.Click += new EventHandler(this.itemButtonClick);
            itemButton10.Click += new EventHandler(this.itemButtonClick);
            itemButton11.Click += new EventHandler(this.itemButtonClick);
            itemButton12.Click += new EventHandler(this.itemButtonClick);
            itemButton13.Click += new EventHandler(this.itemButtonClick);
            itemButton14.Click += new EventHandler(this.itemButtonClick);
            itemButton15.Click += new EventHandler(this.itemButtonClick);
            itemButton16.Click += new EventHandler(this.itemButtonClick);

            catButton1.Click += new EventHandler(this.catButtonClick);
            catButton2.Click += new EventHandler(this.catButtonClick);
            catButton3.Click += new EventHandler(this.catButtonClick);
            catButton4.Click += new EventHandler(this.catButtonClick);
        }


        public void loadCatButtons()
        {
            // Read category file
            cats = File.ReadAllLines(CATSFILE).ToList();

            // Add each button to the list (put this in form load method)
            catButtonList.Add(catButton1);
            catButtonList.Add(catButton2);
            catButtonList.Add(catButton3);
            catButtonList.Add(catButton4);

            // Loop and fill button text values
            for (int i = 0; i < catButtonList.Count; i++)
            {
                listEntry = cats[i];
                catButtonList[i].Text = listEntry;
            }
        }
        
        public void loadItemButtons()
        {
            // If the file doesnt exist, the application will crash
            if (File.Exists("items.dat"))
            {
                using (FileStream file = new FileStream(ITEMSFILE, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(file))
                {
                    string recordIn;
                    string[] fields;

                    while (!reader.EndOfStream)
                    {
                        recordIn = reader.ReadLine();
                        fields = recordIn.Split(DELIM);
                        itemList.Add(new Item() { id = fields[0], name = fields[1], cost = fields[2], category = fields[3] });
                    }                  
                }
            
            }
            else
            {
                return;
            }

            catItemList = itemList.Where(item => item.category == catButton1.Text).ToList();

        
            // Add each item button to the itemButtonList
            itemButtonList.Add(itemButton1);
            itemButtonList.Add(itemButton2);
            itemButtonList.Add(itemButton3);
            itemButtonList.Add(itemButton4);
            itemButtonList.Add(itemButton5);
            itemButtonList.Add(itemButton6);
            itemButtonList.Add(itemButton7);
            itemButtonList.Add(itemButton8);
            itemButtonList.Add(itemButton9);
            itemButtonList.Add(itemButton10);
            itemButtonList.Add(itemButton11);
            itemButtonList.Add(itemButton12);
            itemButtonList.Add(itemButton13);
            itemButtonList.Add(itemButton14);
            itemButtonList.Add(itemButton15);
            itemButtonList.Add(itemButton16);

            // If there are more then 16 items in category, load them 
            if (catItemList.Count >= 16)
            {

                for (int i = 0; i < itemButtonList.Count; i++)
                {
                    listEntry = catItemList[i].name;
                    itemButtonList[i].Text = listEntry;                
                }
            }

            // Else, load the items that exist
            else
            {
                for (int i = 0; i < catItemList.Count; i++)
                {
                    listEntry = catItemList[i].name;
                    itemButtonList[i].Text = listEntry;
                }
            }

            // If a button is empty, disbale it
            for (int i = 0; i < itemButtonList.Count; i++)
            {
                if ((itemButtonList[i].Text == "") || (itemButtonList[i].Text == null))
                {
                    itemButtonList[i].Enabled = false;
                }
            }

        }



        private void adminButton_Click(object sender, EventArgs e)
        {
            Deselector();

            // Open the admin form
            Admin adminForm = new Admin(this);
            adminForm.ShowDialog();       
        }

    

        private void scrollRightButton_Click(object sender, EventArgs e)
        {
            Deselector();

            try
            {
                // set threshold to avoid outofbounds
                int threshhold = cats.Count - 4;
            
                if (shiftCount < threshhold)
                {                  
                    shiftCount++;
                    Console.Write(shiftCount);

                    // Shift view of cateogry buttons to the right
                    for (int i = 0; i < catButtonList.Count; i++)
                    {
                        listEntry = cats[i + shiftCount];
                        catButtonList[i].Text = listEntry;
                    }
                    if (shiftCount >= 1)
                    {
                        scrollLeftButton.Enabled = true;
                    }
                    if (shiftCount == cats.Count - 4)
                    {
                        scrollRightButton.Enabled = false;
                    }

                }
            }
            catch
            {
                // Empty but needed so no out of range exception is thrown
                // when user clicks scroll buttons fast
            }
        }



        private void scrollLeftButton_Click(object sender, EventArgs e)
        {
            Deselector();

            try
            {
                if (shiftCount >= 1)
                {
                    shiftCount--;
                    Console.Write(shiftCount);

                    for (int i = 0; i < catButtonList.Count; i++)
                    {
                        listEntry = cats[i + shiftCount];
                        catButtonList[i].Text = listEntry;
                    }

                    if (shiftCount == 0)
                    {
                        scrollLeftButton.Enabled = false;
                        scrollRightButton.Enabled = true;
                    }
                    if (shiftCount == cats.Count - 5)
                    {
                        scrollRightButton.Enabled = true;
                    }
                }
            }
            catch
            {
                // Empty but needed so no out of range exception is thrown
                // when user clicks scroll buttons fast
            }
        }





        public void loadMainLVColumnHeaders()
        {
            // Declare and construct the ColumnHeader objects.
            ColumnHeader header1, header2, header3, header4, header5;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();
            header5 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "#";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 50;

            header2.Text = "ID";
            header2.TextAlign = HorizontalAlignment.Left;
            header2.Width = 65;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "Item Name";
            header3.Width = 175;

            header4.TextAlign = HorizontalAlignment.Right;
            header4.Text = "Qty.";
            header4.Width = 50;

            header5.TextAlign = HorizontalAlignment.Right;
            header5.Text = "Cost";
            header5.Width = 115;

            // Add the headers to the ListView control.
            mainListView.Columns.Add(header1);
            mainListView.Columns.Add(header2);
            mainListView.Columns.Add(header3);
            mainListView.Columns.Add(header4);
            mainListView.Columns.Add(header5);

            // Specify that each item appears on a separate line.
            mainListView.View = View.Details;

            // Make blue listview bar highlight entire row
            mainListView.FullRowSelect = true;

            // Align cost to the right
            mainListView.Columns[3].TextAlign = HorizontalAlignment.Right;
        }



        private void clearButton_Click(object sender, EventArgs e)
        {
            Deselector();

            if (mainListView.Items.Count == 0)
            {
                MessageBox.Show("There are no items to clear", "Advisory");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this sale?", "Advisory", MessageBoxButtons.YesNo);


                if (dialogResult == DialogResult.Yes)
                {
                    // Clear these variables in order to start new shopping list
                    mainListView.Items.Clear();
                    subTotalLabel.Text = "$0.00";
                    subDub = 0;
                    taxLabel.Text = "$0.00";
                    taxDub = 0;
                    totDub = 0;
                    totalLabel.Text = "$0.00";
                    shoppingCart.Clear();
                    removeItemButton.Enabled = false;
                    shopNum = 0;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
        }


        // 
        private void mainListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mainListView.SelectedItems.Count == 0)
                {
                    Deselector();

                    return;
                }



                    ListViewItem item = mainListView.SelectedItems[0];


                   // Fill the variables
                    numVar = item.SubItems[0].Text;
                    idVar = item.SubItems[1].Text;
                    nameVar = item.SubItems[2].Text;
                    qtyVar = item.SubItems[3].Text;
                    costVar = item.SubItems[4].Text;






                removeItemButton.Enabled = true;
                qtyButton.Enabled = true;
            }
            catch
            {

            }
        }


        public void Deselector()
        {
            removeItemButton.Enabled = false;
            qtyButton.Enabled = false;
        }

        private void removeItemButton_Click(object sender, EventArgs e)
        {



            if (mainListView.SelectedItems.Count == 0)
            {
                return;     
            }

            mainListView.SelectedItems[0].Remove();

            Console.Write("\nShopping Cart # : " + numVar + "\n");
            Console.Write("Item ID # : " + idVar + "\n");
            Console.Write("Item Name : " + nameVar + "\n");
            Console.Write("Quantity : " + qtyVar + "\n");
            Console.Write("Cost : " + costVar + "\n");
            Console.Write("\n");

            var itemToRemove = shoppingCart.SingleOrDefault(r => r.num == numVar);
            if (itemToRemove != null)
                shoppingCart.Remove(itemToRemove);


            shopNum = shopNum - 1;

            test = shoppingCart.Count();
            Console.Write("Shopping Cart Count : " + test + "\n");

            subDub = 0;
            
            
            for (var i = 0; i < shoppingCart.Count; i++)
            {
                string numString = shoppingCart[i].num;
                string costLoopString = shoppingCart[i].cost;
                string qtyString = shoppingCart[i].qty;
                double costLoopDub = double.Parse(costLoopString, CultureInfo.InvariantCulture);
                double qtyDub = double.Parse(qtyString, CultureInfo.InvariantCulture);
                subDub = subDub + (costLoopDub * qtyDub);        
                Console.WriteLine("# is {0}, Amount is {1} and quantity is {2}", shoppingCart[i].num, shoppingCart[i].cost, shoppingCart[i].qty);
                int j = i + 1;
                shoppingCart[i].num = j.ToString();
                mainListView.Items[i].SubItems[0].Text = j.ToString();
                Console.WriteLine("# is {0}, Amount is {1} and quantity is {2}", shoppingCart[i].num, shoppingCart[i].cost, shoppingCart[i].qty);
            }

            subTotalLabel.Text = subDub.ToString("C");

            // Get tax rate from property settings
            double taxRate = Properties.Settings.Default.TaxRate;

            taxDub = 0;
            totDub = 0;

            // Calculate tax
            double taxTemp = subDub * taxRate;
            taxDub = taxTemp;
            taxLabel.Text = taxDub.ToString("C");

            // Calculate total sale
            double totTemp = subDub + taxTemp;
            totDub = totTemp + totDub;
            totalLabel.Text = totDub.ToString("C");

            Deselector();
        }

        private void payButton_Click(object sender, EventArgs e)
        {
            // Open the pay form
            Pay payForm = new Pay();
            payForm.ShowDialog();
        }

        private void qtyButton_Click(object sender, EventArgs e)
        {
            // Open the quantity form
            Quantity qtyForm = new Quantity();
            qtyForm.ShowDialog();
        }
    }
}
