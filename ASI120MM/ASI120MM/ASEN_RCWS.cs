using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ASCOM.DriverAccess;

namespace ASEN_RCWS
{
    class ASEN_RCWS : Camera
    {
        public string ID;
        public int width;
        public int height;
        
        public ASEN_RCWS(string driverID, int exposure) : base(driverID) {

            // Using the API to create an instance of the camera class
            // And setting instance variable "ID" to the user-supplied camera ID
            this.ID = driverID;
            this.width = this.CameraXSize;
            this.height = this.CameraYSize;
        }

        public ushort[,] capture(int exposureTime, bool IS_LIGHT_IMAGE)
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

            ushort[,] arr = new ushort[width, height];
            arr = (ushort[,])this.ImageArray;

            return arr;

        }

        public void initializeCamera()
        {
            // An exception will be thrown here if the camera is not connected
            this.Connected = true;

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

        public void disconnect()
        {
            this.Connected = false;
            this.Dispose();
        }

    }
}
