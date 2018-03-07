using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEN
{
    class State
    {
        public int RCWS_EXPT; // Exposure time for the RCWS [microseconds]
        public int SHA_EXPT; // Exposure time for the SHA [microseconds]
        public int RCWS_DFORE; // RCWS Foreward defocus distance [micro-meters] 
        public int RCWS_DAFT; // RCWS Aftward defocus distance [micro-meters]
        public int MA_X; // Mirror 2 x-displacement [arc-seconds]
        public int MA_Y; // Mirror 2 y-displacement [arc-seconds]
        public string cameraInUse;

        public State(int[] parameters, string selectedCamera)
        {
            // Collecting the state parameters from the input array
            RCWS_EXPT = parameters[0];
            SHA_EXPT = parameters[1];
            RCWS_DFORE = parameters[2];
            RCWS_DAFT = parameters[3];
            MA_X = parameters[4];
            MA_Y = parameters[5];
            cameraInUse = selectedCamera;
        }

        public void RunState()
        {
            // Here's where we call the other methods
            // ASEN_RCWS
            // ASEN_SHA
            // ASEN_MotorControl
            // ASEN_Environmental
        }

        public void SaveState()
        {

        }


    }
}
