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
        public int[,] imgArray;
        
        public ASEN_RCWS(string driverID) : base(driverID) {

            // Using the API to create an instance of the camera class
            // And setting instance variable "ID" to the user-supplied camera ID
            this.ID = driverID;
            // An exception will be thrown here if the camera is not connected
            this.Connected = true;

            this.width = this.CameraXSize;
            this.height = this.CameraYSize;
        }

        public int[,] Capture(double exposureTime, bool IS_LIGHT_IMAGE)
        {
            // Starting the exposure
            this.StartExposure(exposureTime, IS_LIGHT_IMAGE);

            while(!this.ImageReady)
            {
                // Printing dots to the console while we wait for the image to be ready
                Console.Write(".");
                System.Threading.Thread.Sleep(300);
            }

            Console.WriteLine("Exposure Complete.  Downloading.");

            int[,] arr = new int[width, height];
            arr = (int[,])this.ImageArray;
            this.imgArray = arr;

            return arr;

        }

        public void InitializeCamera()
        {

            // This block simply prints out various camera parameters.  We can change
            // this at any time to reflect what we want
            Console.WriteLine("  Connected to " + ID);
            Console.WriteLine("  Description = " + this.Description);
            Console.WriteLine("  Pixel size = " + this.PixelSizeX + " * " + this.PixelSizeY);
            Console.WriteLine("  Camera size = " + this.CameraXSize + " * " + this.CameraYSize);
            Console.WriteLine("  Max Bin = " + this.MaxBinX + " * " + this.MaxBinY);
            Console.WriteLine("  Bin = " + this.BinX + " * " + this.BinY);
            Console.WriteLine("  MaxADU = " + this.MaxADU);
            Console.WriteLine("  CameraState = " + this.CameraState.ToString());
            Console.WriteLine("  CanAbortExposure = " + this.CanAbortExposure);
            Console.WriteLine("  CanAsymmetricBin = " + this.CanAsymmetricBin);
            Console.WriteLine("  CanGetCoolerPower = " + this.CanGetCoolerPower);
            Console.WriteLine("  CanPulseGuide = " + this.CanPulseGuide);
            Console.WriteLine("  CanSetCCDTemperature = " + this.CanSetCCDTemperature);
            Console.WriteLine("  CanStopExposure = " + this.CanStopExposure);
            Console.WriteLine("  CCDTemperature = " + this.CCDTemperature);
            Console.WriteLine("  ExposureMax = " + this.ExposureMax);
            Console.WriteLine("  ExposureMin = " + this.ExposureMin);
        }

        // Saving to CSV
        /*
        public void saveImage()
        {
            using (StreamWriter outfile = new StreamWriter(@".\output.csv"))
            {
                for (int x = 0; x < width; x++)
                {
                    string content = "";
                    for (int y = 0; y < height; y++)
                    {
                        content += imgArray[x, y].ToString() + ",";
                    }
                    //trying to write data to csv
                    outfile.WriteLine(content);
                }


            }
        }
        */

        public void Disconnect()
        {
            this.Connected = false;
            this.Dispose();
        }

    }
}
