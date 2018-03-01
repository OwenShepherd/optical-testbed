using System;
using System.IO;
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
    public partial class ExperimentDialog : Form
    {

        private string experimentPath;
        private string schedulePath;
        private bool isSelected;

        public ExperimentDialog()
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

            ExperimentDirectory.Text = experimentPath;
        }

        private void ScheduleBrowse_Click(object sender, EventArgs e)
        {
            // Opening a dialog for the user to browse for the experiment file
            Stream myStream = null;
            OpenFileDialog scheduleDialog = new OpenFileDialog();

            scheduleDialog.InitialDirectory = "c:\\";
            scheduleDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            scheduleDialog.FilterIndex = 1;
            scheduleDialog.RestoreDirectory = true;

            if (scheduleDialog.ShowDialog() == DialogResult.OK)
            {
                schedulePath = scheduleDialog.FileName;
            }

            ScheduleDirectory.Text = schedulePath;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ExperimentDirectory_Click(object sender, EventArgs e)
        {

        }

       
    }
}
