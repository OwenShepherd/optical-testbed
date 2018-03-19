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
        public decimal RCWS_DFORE; // RCWS Foreward defocus distance [micro-meters] 
        public decimal RCWS_DAFT; // RCWS Aftward defocus distance [micro-meters]
        public decimal MA_X; // Mirror 2 x-displacement [arc-seconds]
        public decimal MA_Y; // Mirror 2 y-displacement [arc-seconds]
        public string path; // Path of the state root folder
        public string cameraInUse;
        private string rootPath;
        private string[] serials;
        private ASEN_SHA currentSHA; // The object that controls interactions with the SHA
        private ASEN_ENV teensy; // The object that controls interactions with the teensy
        private ASEN_RCWS currentRCWS;
        private ASEN_MotorControl motor1;
        private object teensyLock;
        private bool READ;
        public int velocity;
        private string teensyPort;


        public State(double[] parameters, string selectedCamera, string statePath, string[] serials, string COMPort)
        {
            // Collecting the state parameters from the input array
            RCWS_EXPT = parameters[0];
            SHA_EXPT = parameters[1];
            RCWS_DFORE = (decimal)parameters[2];
            RCWS_DAFT = (decimal)parameters[3];
            MA_X = (decimal)parameters[4];
            MA_Y = (decimal)parameters[5];
            cameraInUse = selectedCamera;
            this.serials = serials;
            this.velocity = 3200; // Velocity units are unknown / stupid...
            this.path = statePath;
            this.teensyLock = new object();
            this.teensyPort = COMPort;
        }

        public void RunState()
        {
            // Here's where we call the other methods

            // ASEN_SHA


            
            // ASEN_MotorControl Set Up
            this.motor1 = new ASEN_MotorControl(serials[0], this.velocity);
            //ASEN_MotorControl motor2 = new ASEN_MotorControl(serials[1], this.velocity);
            //ASEN_MotorControl motor3 = new ASEN_MotorControl(serials[2], this.velocity);

            Dictionary<string, string> SettingValues = new Dictionary<string, string>();

            


            MotorConfiguration motorConfig = new MotorConfiguration(serials[0]);

            motorConfig = motor1.GetMotorConfiguration(serials[0], 1);




            // Initializing each motor one-by-one
            motor1.InitializeMotor();
            //motor2.InitializeMotor();
            //motor3.InitializeMotor();

            // Homing the motors first
            motor1.HomeMotor();
            //motor2.HomeMotor();
            //motor3.HomeMotor();

            // Now we move to whatever positon we desire
            motor1.MoveMotor(RCWS_DFORE);
            //motor2.MoveMotor(MA_X);
            //motor3.MoveMotor(MA_Y);
            

            
            // ASEN_RCWS Initializing Camera
            this.currentRCWS = new ASEN_RCWS(cameraInUse);
            currentRCWS.InitializeCamera();
            

            /*
            // ASEN_SHA Initializing Device
            this.currentSHA = new ASEN_SHA();
            currentSHA.CameraConnectionAndSetup();
            

            
            // ASEN_Environmental
            this.teensy = new ASEN_ENV(this.teensyPort, path);
            this.READ = true;
            

            
            Task teensyRead = Task.Factory.StartNew(() => TeensyParallel(ref this.teensyLock));
            Task imageRead = Task.Factory.StartNew(() => ImagingParallel());

            Task.WaitAll(imageRead);

            // After the image is done reading, we have to set READ to false.
            // Note that we have to block access to this variable as it is also shared by teensy read
            lock (this.teensyLock)
            {
                this.READ = false;
            }

            Task.WaitAll(teensyRead);
            
            currentRCWS.Disconnect();
            //currentSHA.CloseCamera();
            */


        }

        // Guessing that I would want to pass some file paths in here
        public void SaveState()
        {

        }

        // ------------------ Functions for Parallel Threads -----------------------------
        private void TeensyParallel(ref object teensyLock)
        {

            int dataCount = 0;
            bool localREAD;

            // Storing a local variable so that we only have to lock during the updating of the bool
            lock (teensyLock)
            {
                localREAD = this.READ;
            }

            while (localREAD)
            {

                teensy.BeginTeensyRead(ref dataCount);

                // Locking to see if the image read has completed
                lock (teensyLock)
                {
                    localREAD = this.READ;
                }

            }
        }

        private void ImagingParallel()
        {
            
            // Here we save the image from the RCWS
            int[,] RCWSImageFORE = new int[currentRCWS.width,currentRCWS.height];

            RCWSImageFORE = currentRCWS.Capture(RCWS_EXPT,false);
            // currentRCWS.saveImage(); // Not implemented yet

            // Here we save the image from the SHA
            //byte[] byteData = currentSHA.GatherCameraData(SHA_EXPT);
            //float[] zerinkes = currentSHA.ProcessCameraData();
            

            
            // Moving the RCWS to the aft defocus distance
            motor1.MoveMotor(RCWS_DAFT);

            // Taking the image again
            int[,] RCWSImageAFT = new int[currentRCWS.width, currentRCWS.height];

            RCWSImageAFT = currentRCWS.Capture(RCWS_EXPT,false);
            // currentRCWS.saveImage();

            // Here we save the image from the SHA
            //byteData = currentSHA.GatherCameraData(SHA_EXPT);
            //zerinkes = currentSHA.ProcessCameraData();
            
        }


    }
}
