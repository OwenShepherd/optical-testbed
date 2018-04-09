using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEN
{
    class Experiment
    {
        public const int numData = 6;
        public string csvPath; // Path to the CSV scheduler
        public string experimentPath; // Path to the experiment top-level directory
        private State currentState; // The current state
        private string cameraInUse; // The string for the driver of whichever camera (ASI vs. QHY) is being used
        private string serialNo1;
        private string serialNo2;
        private string serialNo3;
        private string COMPort;
        private string[] serials;
        private string ipyPath;
        private string pyPath;
        public bool ISASI;

        private struct StateOrganizer
        {
            public int originalState;
            public int newState;
        }


        public Experiment(string schedulerPath, string experimentPath, string selectedCamera, string teensyPort, string pythonPath, string ipythonPath)
        {
            this.serials = new string[3];
            this.csvPath = schedulerPath;
            this.experimentPath = experimentPath;
            //this.cameraInUse = selectedCamera;
            this.serialNo1 = "27501994"; // Set to "motor 1" serial number
            this.serialNo2 = "27002310"; // Set to "motor 2" serial number
            this.serialNo3 = "27002528"; // Set to "motor 3" serial number
            this.COMPort = teensyPort;
            this.serials[0] = this.serialNo1;
            this.serials[1] = this.serialNo2;
            this.serials[2] = this.serialNo3;
            this.pyPath = pythonPath;
            this.ipyPath = ipythonPath;
            //ISASI = selectedCamera;
            this.cameraInUse = selectedCamera;
        }

        private void ExperimentReader()
        {
            // Read the entire file at once
            string[] allLines = File.ReadAllLines(this.csvPath);
            string[] valInput = new string[numData];
            double[] inputLine = new double[numData];


            // Let's start collecting data on which states are the same
            // We're going to make a new array
            for (int i = 1; i < allLines.Length; i++)
            {
                valInput = allLines[i].Split(',');
                inputLine = Array.ConvertAll(valInput, double.Parse);
            }


        }


        public void StartExperiment()
        {
            // Most of what happens in the class will happen within the constructor, I think
            ASEN.ExperimentDirectory initialDirectory = new ASEN.ExperimentDirectory(experimentPath);
            string statePath = "";
            int csvCount = 0;

            using (StreamReader sr = new StreamReader(csvPath))
            {
                string currentLine;
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (csvCount == 0) { }
                    else
                    {
                        // Parsing data from the string
                        string[] valInput = currentLine.Split(',');
                        double[] inPut = Array.ConvertAll(valInput, double.Parse);

                        // Adding a new state to the directory
                        statePath = initialDirectory.CreateNewState();

                        // calling the state constructor
                        currentState = new State(inPut, cameraInUse, statePath, this.serials, this.COMPort, this.pyPath, this.ipyPath);
                        currentState.RunState();

                    }

                    csvCount++;

                }
            }
        }



    }
}
