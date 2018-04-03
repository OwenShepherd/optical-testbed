using System;
using System.IO;
using ASCOM.DriverAccess;


namespace ASEN
{
    class ASEN_RCWS : Camera
    {
        public string ID;
        public int width;
        public int height;
        
        public ASEN_RCWS(string driverID) : base(driverID) {

            // Using the API to create an instance of the camera class
            // And setting instance variable "ID" to the user-supplied camera ID
            this.ID = driverID;
            
        }

        public void Capture(double exposureTime, bool IS_LIGHT_IMAGE)
        {
            // Starting the exposure
            this.StartExposure(exposureTime, IS_LIGHT_IMAGE);

            bool wait = true;

            while(wait)
            {
                if (this.ImageReady)
                {
                    wait = false;
                }
            }

            Console.WriteLine("Exposure Complete.");
            
        }

        public void InitializeCamera()
        {
            this.Connected = true;
            // An exception will be thrown here if the camera is not connected

            this.width = this.CameraXSize;
            this.height = this.CameraYSize;
        }

        // Saving to CSV
        public void SaveImage(string outputFile)
        {
            int[,] CurrentImage = (int[,])this.ImageArray;

            using (StreamWriter outfile = new StreamWriter(outputFile))
            {
                for (int i = 0; i < this.width; i++)
                {
                    string content = "";

                    for (int j = 0; j < this.height; j++)
                    {
                        content += CurrentImage[i, j].ToString() + ",";
                    }

                    outfile.WriteLine(content);

                }
            }
        }
        

        public void Disconnect()
        {
            this.Connected = false;
            this.Dispose();
        }

    }
}
