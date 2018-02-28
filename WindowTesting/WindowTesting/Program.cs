using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // This creates a form that prompts the user to select a directory
            WindowTesting.ExperimentDialog formTest = new WindowTesting.ExperimentDialog();
            formTest.ShowDialog();

            // Getting the path that the user had selected
            string userPath = formTest.getSelectedPath();

            // Creating a new form to ask the user for an experiment name
            WindowTesting.GetExperiment enterTest = new WindowTesting.GetExperiment();
            enterTest.ShowDialog();

            // Getting the experiment name
            string experimentName = enterTest.getEnteredExperiment();
            string experimentPath = userPath + "\\" + experimentName;

            WindowTesting.ExperimentDirectory initialDirectory = new WindowTesting.ExperimentDirectory(experimentPath);

            string newState = initialDirectory.CreateNewState();
           
        }
    }

}

