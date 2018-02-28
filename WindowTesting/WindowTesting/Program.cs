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
            WindowTesting.ExperimentDialog formTest = new WindowTesting.ExperimentDialog();

            formTest.ShowDialog();

            string userPath = formTest.getSelectedPath();

            WindowTesting.GetExperiment enterTest = new WindowTesting.GetExperiment();

            enterTest.ShowDialog();

            string experimentName = enterTest.getEnteredExperiment();

            
           
        }
    }

}

