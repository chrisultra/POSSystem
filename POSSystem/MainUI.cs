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
        const string CATSFILE = "categories.dat";

        List<Button> buttonList = new List<Button>();

        string listEntry;

        List<string> cats = File.ReadAllLines(CATSFILE).ToList();

        int catClicker = 1;




        public MainUI()
        {
            InitializeComponent();
            load();
          

            // add each button to the list (put this in form load method)
            buttonList.Add(catButton1);
            buttonList.Add(catButton2);
            buttonList.Add(catButton3);
            buttonList.Add(catButton4);

            //int test = buttonList.Count;
            //Console.Write(test);

            for (int i = 0; i < buttonList.Count; i++)
            {
                listEntry = cats[i];
                buttonList[i].Text = listEntry;
            }
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            Admin adminForm = new Admin();
            adminForm.ShowDialog();
          
        }



        public void load()
        {
            using (FileStream fs = new FileStream(CATSFILE, FileMode.Open, FileAccess.Read))
            using (StreamReader r = new StreamReader(fs))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    //catButton1.Text = line;
                }    
            }
        }



        private void scrollRightButton_Click(object sender, EventArgs e)
        {
            int threshhold = cats.Count - 3;

            if (catClicker < threshhold)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    listEntry = cats[i + catClicker];
                    buttonList[i].Text = listEntry;
                }
                catClicker++;
  

                Console.Write(catClicker);
 
            }
        }



        private void scrollLeftButton_Click(object sender, EventArgs e)
        {

            if (catClicker >= 2)
            {


                int cc = catClicker-1;
                Console.Write(cc);

                for (int i = 0; i < buttonList.Count; i++)
                {
                    listEntry = cats[i + cc];
                    buttonList[i].Text = listEntry;
                }
                catClicker--;
            }
        }
    }
}
