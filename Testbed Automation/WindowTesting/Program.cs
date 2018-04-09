using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace ASEN
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
            //ASEN.ExperimentDialog formTest = new ASEN.ExperimentDialog();
            //Application.Run(formTest);

            ASEN_MotorControl motorTest = new ASEN_MotorControl("27501994", 3200);
            motorTest.InitializeMotor();
            motorTest.HomeMotor();
            motorTest.MoveMotorLinear(1);
            motorTest.DisconnectMotor();
            

        }
    }
}

