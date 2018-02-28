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
            WindowTesting.Form1 formTest = new WindowTesting.Form1();

            formTest.ShowDialog();

            string userPath = formTest.getSelectedPath();

            Console.WriteLine(formTest.getSelectedPath());
           
        }
    }

}

