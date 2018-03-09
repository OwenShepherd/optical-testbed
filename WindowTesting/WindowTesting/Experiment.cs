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
        public string csvPath; // Path to the CSV scheduler
        public string experimentPath; // Path to the experiment top-level directory
        private State currentState; // The current state
        private string cameraInUse; // The string for the driver of whichever camera (ASI vs. QHY) is being used
        private string serialNo1;
        private string serialNo2;
        private string serialNo3;
        private string[] serials;

        public Experiment(string schedulerPath, string experimentPath, string selectedCamera)
        {
            this.csvPath = schedulerPath;
            this.experimentPath = experimentPath;
            this.cameraInUse = selectedCamera;
            this.serialNo1 = "000000"; // Set to "motor 1" serial number
            this.serialNo2 = "000000"; // Set to "motor 2" serial number
            this.serialNo3 = "000000"; // Set to "motor 3" serial number
            serials[0] = serialNo1;
            serials[1] = serialNo2;
            serials[2] = serialNo3;
        }

        public void StartExperiment()
        {
            // Most of what happens in the class will happen within the constructor, I think
            ASEN.ExperimentDirectory initialDirectory = new ASEN.ExperimentDirectory(experimentPath);

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
                        int[] intPut = Array.ConvertAll(valInput, int.Parse);

                        // Adding a new state to the directory
                        initialDirectory.CreateNewState();

                        // calling the state constructor
                        currentState = new State(intPut, this.cameraInUse);
                        
                    }

                    csvCount++;

                }
            }
        }



    }
}
