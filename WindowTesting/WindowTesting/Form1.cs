using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowTesting
{
    public partial class Form1 : Form
    {

        private string experimentPath;
        private bool isSelected;

        public Form1()
        {
            InitializeComponent();
            experimentPath = "";
            isSelected = false;
        }

        public string getSelectedPath()
        {
            return experimentPath;
        }

        public bool pathSelected()
        {
            return isSelected;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog expLoc = new FolderBrowserDialog();

            if (expLoc.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                experimentPath = expLoc.SelectedPath;

                isSelected = true;
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
