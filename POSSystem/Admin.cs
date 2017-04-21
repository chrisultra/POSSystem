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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();

            CatInfo ci = CatInfo.Instance();
            ci.Load();
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            CatInfo ci = CatInfo.Instance();
            String catName = catNameTextBox.Text;
            ci.AddCategory(catName);
            ci.Save();
        }
    }
}
