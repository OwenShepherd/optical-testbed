// This implements a console application that can be used to test an ASCOM driver
//

// This is used to define code in the template that is specific to one class implementation
// unused code can be deleted and this definition removed.

#define Camera
// remove this to bypass the code that uses the chooser to select the driver
//#define UseChooser

using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASCOM.DriverAccess;


namespace ASCOM
{
    class Program
    {
        
        static void Main(string[] args)
        {

            // Put the driver name here.  You can figure out the driver name by uncommenting the below code
            string driverID = "ASCOM.ASICamera2.Camera";

            // Uncomment the code that's required
#if UseChooser
            // choose the device
            string id = ASCOM.DriverAccess.Camera.Choose("");
            if (string.IsNullOrEmpty(id))
                return;
            // create this device
            Console.WriteLine("ID: " + id);
            ASCOM.DriverAccess.Camera device = new ASCOM.DriverAccess.Camera(id);
#else
            // this can be replaced by this code, it avoids the chooser and creates the driver class directly.
            //ASCOM.DriverAccess.Camera device = new ASCOM.DriverAccess.Camera(driverID);
#endif
            #region Camera
            Console.WriteLine("\r\nCamera:");
            string progID = driverID;
            if (progID != "")
            {
                Camera C = new Camera(progID);
                C.Connected = true;
                Console.WriteLine("  Connected to " + progID);
                Console.WriteLine("  Description = " + C.Description);
                Console.WriteLine("  Pixel size = " + C.PixelSizeX + " * " + C.PixelSizeY);
                Console.WriteLine("  Camera size = " + C.CameraXSize + " * " + C.CameraYSize);
                Console.WriteLine("  Max Bin = " + C.MaxBinX + " * " + C.MaxBinY);
                Console.WriteLine("  Bin = " + C.BinX + " * " + C.BinY);
                Console.WriteLine("  MaxADU = " + C.MaxADU);
                Console.WriteLine("  CameraState = " + C.CameraState.ToString());
                Console.WriteLine("  CanAbortExposure = " + C.CanAbortExposure);
                Console.WriteLine("  CanAsymmetricBin = " + C.CanAsymmetricBin);
                Console.WriteLine("  CanGetCoolerPower = " + C.CanGetCoolerPower);
                Console.WriteLine("  CanPulseGuide = " + C.CanPulseGuide);
                Console.WriteLine("  CanSetCCDTemperature = " + C.CanSetCCDTemperature);
                Console.WriteLine("  CanStopExposure = " + C.CanStopExposure);
                Console.WriteLine("  CCDTemperature = " + C.CCDTemperature);
                if (C.CanGetCoolerPower)
                    Console.WriteLine("  CoolerPower = " + C.CoolerPower);
                Console.WriteLine("  ElectronsPerADU = " + C.ElectronsPerADU);
                Console.WriteLine("  FullWellCapacity = " + C.FullWellCapacity);
                Console.WriteLine("  HasShutter = " + C.HasShutter);
                //Console.WriteLine("  HeatSinkTemperature = " + C.HeatSinkTemperature);
                if (C.CanPulseGuide)
                    Console.WriteLine("  IsPulseGuiding = " + C.IsPulseGuiding);
                Console.Write("  Take 15 second image");
                C.StartExposure(1.0, true);
                while (!C.ImageReady)
                {
                    Console.Write(".");
                    //U.WaitForMilliseconds(300);
                    System.Threading.Thread.Sleep(300);
                }
                Console.WriteLine("\r\n  Exposure complete, ready for download.");
                Console.WriteLine("  CameraState = " + C.CameraState.ToString());
                //Console.WriteLine("  LastExposureDuration = " + C.LastExposureDuration);
                //Console.WriteLine("  LastExposureStartTime = " + C.LastExposureStartTime);
                int[] imgArray = (int[])C.ImageArray;
                

                using (Image image = Image.FromStream(new MemoryStream(imgArray)))
                {
                    image.Save("output.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);  // Or Png
                }

                
                Console.WriteLine("  Array is " + (imgArray.GetUpperBound(0) + 1) + " by " + (imgArray.GetUpperBound(1) + 1));
                C.Connected = false;
                C.Dispose();
            }
            #endregion
        }
    }
}
