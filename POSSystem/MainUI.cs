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

namespace POSSystem
{
    public partial class MainUI : Form
    {
        const string FILENAME = "categories2.dat";


        List<string> lines = new List<string>();


        public MainUI()
        {
            InitializeComponent();
            load();

            // instantiate a list (at the form class level)
            List<Button> buttonList = new List<Button>();

            List<string> lines = File.ReadAllLines(FILENAME).ToList();

            // add each button to the list (put this in form load method)
            buttonList.Add(catButton1);
            buttonList.Add(catButton2);
            buttonList.Add(catButton3);
            buttonList.Add(catButton4);

            string listEntry = lines[0];
            buttonList[0].Text = listEntry;
            listEntry = lines[1];
            buttonList[1].Text = listEntry;
            listEntry = lines[2];
            buttonList[2].Text = listEntry;
            listEntry = lines[3];
            buttonList[3].Text = listEntry;

        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            Admin adminForm = new Admin();
            adminForm.ShowDialog();
          
        }


        public void load()
        {

            using (FileStream fs = new FileStream(FILENAME, FileMode.Open, FileAccess.Read))
            using (StreamReader r = new StreamReader(fs))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    //catButton1.Text = line;
                }
                
            }

        }

    }

}
