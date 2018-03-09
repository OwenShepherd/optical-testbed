using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ASEN
{
    class State
    {
        public double RCWS_EXPT; // Exposure time for the RCWS [microseconds]
        public double SHA_EXPT; // Exposure time for the SHA [microseconds]
        public double RCWS_DFORE; // RCWS Foreward defocus distance [micro-meters] 
        public double RCWS_DAFT; // RCWS Aftward defocus distance [micro-meters]
        public double MA_X; // Mirror 2 x-displacement [arc-seconds]
        public double MA_Y; // Mirror 2 y-displacement [arc-seconds]
        public string path; // Path of the state root folder
        public string cameraInUse;
        private string rootPath;
        private string[] serials;
        private ASEN_SHA currentSHA; // The object that controls interactions with the SHA
        private ASEN_ENV teensy; // The object that controls interactions with the teensy
        private ASEN_RCWS currentRCWS;
        private ASEN_MotorControl motor1;
        public int velocity;


        public State(double[] parameters, string selectedCamera, string statePath, string[] serials)
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
            this.path = statePath;
        }

        public void RunState()
        {
            // Here's where we call the other methods

            // ASEN_SHA


            
            // ASEN_MotorControl Set Up
            this.motor1 = new ASEN_MotorControl(serials[0], this.velocity);
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
            

            
            // ASEN_RCWS Initializing Camera
            this.currentRCWS = new ASEN_RCWS(cameraInUse);
            currentRCWS.InitializeCamera();
            

            
            // ASEN_SHA Initializing Device
            this.currentSHA = new ASEN_SHA();
            currentSHA.CameraConnectionAndSetup();
            

            
            // ASEN_Environmental
            this.teensy = new ASEN_ENV("COM3", path);
            teensy.PrepRead();
            

            
            Task teensyRead = Task.Factory.StartNew(() => TeensyParallel());
            Task imageRead = Task.Factory.StartNew(() => ImagingParallel());

            Task.WaitAll(teensyRead, imageRead);


            
            
            // Turning off the teensy read
            teensy.EndRead();
            currentRCWS.Disconnect();
            currentSHA.CloseCamera();
            


        }

        // Guessing that I would want to pass some file paths in here
        public void SaveState()
        {

        }

        // ------------------ Functions for Parallel Threads -----------------------------
        private void TeensyParallel()
        {
            teensy.BeginTeensyRead();
        }

        private void ImagingParallel()
        {
            
            // Here we save the image from the RCWS
            int[,] RCWSImageFORE = new int[currentRCWS.width,currentRCWS.height];

            RCWSImageFORE = currentRCWS.Capture(RCWS_EXPT,false);
            // currentRCWS.saveImage(); // Not implemented yet

            // Here we save the image from the SHA
            byte[] byteData = currentSHA.GatherCameraData(SHA_EXPT);
            float[] zerinkes = currentSHA.ProcessCameraData();
            

            
            // Moving the RCWS to the aft defocus distance
            motor1.MoveMotor(RCWS_DAFT);

            // Taking the image again
            int[,] RCWSImageAFT = new int[currentRCWS.width, currentRCWS.height];

            RCWSImageAFT = currentRCWS.Capture(RCWS_EXPT,false);
            // currentRCWS.saveImage();

            // Here we save the image from the SHA
            byteData = currentSHA.GatherCameraData(SHA_EXPT);
            zerinkes = currentSHA.ProcessCameraData();
            
        }


    }
}
