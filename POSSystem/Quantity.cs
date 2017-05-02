using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSSystem
{
    public partial class Quantity : Form
    {
       
        StringBuilder sb = new StringBuilder();



        public Quantity()
        {
            InitializeComponent();

            loadEventHandlers();

        }




        public void loadEventHandlers()
        {
            // Create new event handlers for category and item buttons
            digitButton0.Click += new EventHandler(this.digitButtonClick);
            digitButton1.Click += new EventHandler(this.digitButtonClick);
            digitButton2.Click += new EventHandler(this.digitButtonClick);
            digitButton3.Click += new EventHandler(this.digitButtonClick);
            digitButton4.Click += new EventHandler(this.digitButtonClick);
            digitButton5.Click += new EventHandler(this.digitButtonClick);
            digitButton6.Click += new EventHandler(this.digitButtonClick);
            digitButton7.Click += new EventHandler(this.digitButtonClick);
            digitButton8.Click += new EventHandler(this.digitButtonClick);
            digitButton9.Click += new EventHandler(this.digitButtonClick);
        }

        public void digitButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;

       
            this.sb.Append(button.Text);

            Console.WriteLine(button.Text);
            this.digitTextBox.Text = sb.ToString();

        }

        private void enterButton_Click(object sender, EventArgs e)
        {

            string sbString = sb.ToString();

            //initialize a Form1, refer back to the .Owner properties 
            MainUI parent = (MainUI)this.Owner;

            //call the updatetext method
            parent.updateQuantity(sbString);

            //closing the form is optional.
            this.Close();
        }
    }
}

