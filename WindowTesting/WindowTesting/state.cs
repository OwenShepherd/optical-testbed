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
        private string rootPath;
        private string[] serials;
        public int velocity;


        public State(int[] parameters, string selectedCamera, string parentPath, string[] serials)
        {
            // Collecting the state parameters from the input array
            RCWS_EXPT = parameters[0];
            SHA_EXPT = parameters[1];
            RCWS_DFORE = parameters[2];
            RCWS_DAFT = parameters[3];
            MA_X = parameters[4];
            MA_Y = parameters[5];
            cameraInUse = selectedCamera;
            this.serials = serials;
            this.velocity = 3200; // Velocity units are unknown / stupid...
        }

        public void RunState()
        {
            // Here's where we call the other methods

            // ASEN_SHA


            /*
            // ASEN_MotorControl Set Up
            ASEN_MotorControl motor1 = new ASEN_MotorControl(serials[0], this.velocity);
            ASEN_MotorControl motor2 = new ASEN_MotorControl(serials[1], this.velocity);
            ASEN_MotorControl motor3 = new ASEN_MotorControl(serials[2], this.velocity);

            // Initializing each motor one-by-one
            motor1.InitializeMotor();
            motor2.InitializeMotor();
            motor3.InitializeMotor();

            // Homing the motors first
            motor1.HomeMotor();
            motor2.HomeMotor();
            motor3.HomeMotor();

            // Now we move to whatever positon we desire
            motor1.MoveMotor(RCWS_DFORE);
            motor2.MoveMotor(MA_X);
            motor3.MoveMotor(MA_Y);
            */

            /*
            // ASEN_RCWS Capturing an Image
            ASEN_RCWS currentRCWS = new ASEN_RCWS(cameraInUse);
            currentRCWS.InitializeCamera();

            // Here we save the image
            int[,] RCWSImageFORE = new int[currentRCWS.width,currentRCWS.height];

            RCWSImage = currentRCWS.Capture(RCWS_EXPT,false);
            // currentRCWS.saveImage(); // Not implemented yet
            */

            /*
            // Moving the RCWS to the aft defocus distance
            motor1.MoveMotor(RCWS_DAFT);

            // Taking the image again
            int[,] RCWSImageAFT = new int[currentRCWS.width, currentRCWS.height];

            RCWSImageAFT = currentRCWS.Capture(RCWS_EXPT,false);
            // currentRCWS.saveImage();
            */
            // ASEN_Environmental
        }

        // Guessing that I would want to pass some file paths in here
        public void SaveState()
        {

        }


    }
}
