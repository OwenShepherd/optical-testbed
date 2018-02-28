using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowTesting
{
    class ExperimentDirectory
    {
        private string parentPath; // Parent directory path in string
        private DirectoryInfo parent;

        private int stateCount;  // Counts how many states have been created so far

        public ExperimentDirectory(string path)
        {
            // This is the experiment directory, or the parent directory for the experiment
            // This will serve as the parent directory...all further states will be stored in the folder
            parentPath = path;

            // Creating the directory
            parent = Directory.CreateDirectory(parentPath);

            // Initializing state count
            stateCount = 1;
        }

        private string zeroAdder(int input)
        {
            if (input < 10)
            {
                return "0" + input.ToString();
            }
            else
            {
                return input.ToString();
            }
        }

        public string CreateNewState()
        {
            // Getting the current time
            DateTime currentTime = DateTime.Now;

            // Creating the new state folder name
            string currDate = zeroAdder(currentTime.Year) + "_" + zeroAdder(currentTime.Month) + "_" + zeroAdder(currentTime.Day) + "_";
            string currTime = zeroAdder(currentTime.Hour) + "_" + zeroAdder(currentTime.Minute) + "_" + zeroAdder(currentTime.Second) + "_";
            string currState = "STATE" + stateCount.ToString();

            // The state path
            string statePath = parentPath + "\\" + currDate + currTime + currState;

            // Keeping track of the state count
            stateCount++;

            // creating the directory
            DirectoryInfo state = Directory.CreateDirectory(statePath);

            return statePath;
        }

        
    }
}
