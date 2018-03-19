using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ASEN
{
    
    public partial class ExperimentDialog : Form
    {

        private string experimentPath;
        private string schedulePath;
        private string experimentName;
        private bool isSelected;
        private static string QHY = "ASCOM.QHYCCD.Camera";
        private static string ASI = "ASCOM.ASICamera2.Camera";
        private Experiment currentExperiment;
        string[] availablePorts;

        public ExperimentDialog()
        {
            InitializeComponent();
            experimentPath = "Not Selected";
            ScheduleDirectory.Text = "Not Selected";
            CameraSelection.Items.Add("ASI");
            CameraSelection.Items.Add("QHY");

            // Get Port names
            this.availablePorts = SerialPort.GetPortNames();
            // Add port names to the selection window
            for (int i = 0; i < availablePorts.Length; i++)
            {
                teensySelection.Items.Add(availablePorts[i]);
            }


            isSelected = false;
        }

        public string getSelectedPath()
        {
            return experimentPath;
        }

        public string getExperimentName()
        {
            return experimentName;
        }

        public bool getSelected()
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

        private void StartButton_Click(object sender, EventArgs e)
        {
            
            this.experimentName = ExpDialog.Text;
            isSelected = true;

            string selectedCamera = CameraSelection.Text;

            bool ISASI = selectedCamera.Equals("ASI", StringComparison.Ordinal);

            if (ISASI) { selectedCamera = ASI; }
            else { selectedCamera = QHY; }

            string teensyPort = teensySelection.Text;

            // Getting the path that the user had selected
            string userPath = this.getSelectedPath();
            string experimentName = this.getExperimentName();

            // Getting the experiment name
            string experimentPath = userPath + "\\" + experimentName;

            // Creating our new experiment
            currentExperiment = new Experiment(schedulePath, experimentPath,selectedCamera,teensyPort);
            currentExperiment.StartExperiment();

            this.Close();
        }

        private void CameraSelection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
