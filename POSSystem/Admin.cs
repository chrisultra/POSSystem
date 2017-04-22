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

        const string DELIM = ",";
        const string FILENAME = "categories2.dat";


        int id;
        string name;



        //FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);

        List<string> lines = new List<string>();


        public Admin()
        {
            InitializeComponent();

            load();
        }


        private void addCategoryButton_Click(object sender, EventArgs e)
        {




            using (FileStream fs = new FileStream(FILENAME, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                String catName = catNameTextBox.Text;
                catName = FirstCharToUpper(catName);
                sw.WriteLine(catName);
                catListBox.Items.Add(catName);
                catNameTextBox.Clear();
                
                
            }



        }

        public void load()
        {
            
            using (FileStream fs = new FileStream(FILENAME, FileMode.Open, FileAccess.Read))
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
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }









    }
}
