// This implements a console application that can be used to test an ASCOM driver
//

// This is used to define code in the template that is specific to one class implementation
// unused code can be deleted and this definition removed.

#define Camera
// remove this to bypass the code that uses the chooser to select the driver
//#define UseChooser

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ASCOM.DriverAccess;


namespace ASCOM
{
    public class ASEN_RCWS : Camera
    {

    }


    class Program
    {
        
        static void Main(string[] args)
        {

            // Put the driver name here.  You can figure out the driver name by uncommenting the below code
            string driverID = "ASCOM.ASICamera2.Camera";
            double exposureTime = 0.01; // Sets the exposure time in seconds.
            bool lightImage = true; // Sets the ASI to take a light or dark image

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
                
                C.StartExposure(exposureTime, lightImage);

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
                //int[] imgArray = (int[])C.ImageArray;
                int[,] arr = new int[C.CameraXSize, C.CameraYSize];
                arr = (int[,])C.ImageArray;
                Type type = arr.GetType();
                Console.WriteLine(type.Name);
                

                int width = C.CameraXSize; // read from file
                int height = C.CameraYSize; // read from file
                /*
                using (StreamWriter outfile = new StreamWriter(@"C:\Users\sheph\Documents\Classes\output.csv"))
                {
                    for (int x = 0; x < width; x++)
                    {
                        string content = "";
                        for (int y = 0; y < height; y++)
                        {
                            content += arr[x,y].ToString() + ",";
                        }
                        //trying to write data to csv
                        outfile.WriteLine(content);
                    }


                }
                */ 
                /*
                var bitmap = new Bitmap(width, height, PixelFormat.Format16bppGrayScale);
                
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        double temp = arr[x,y] *255.0/65535; // read from array
                        int red = (int)temp;
                        int green = red; // read from array
                        int blue = green; // read from array
                        bitmap.SetPixel(x, y, Color.FromArgb(0, red, green, blue));
                    }
                
                */
                Bitmap theImage = new Bitmap(width,height);

                var data = theImage.LockBits(
                    new Rectangle(0, 0, theImage.Width, theImage.Height),
                    ImageLockMode.ReadWrite,
                    theImage.PixelFormat
                    );
                
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Marshal.WriteInt16(data.Scan0, 0, (short)arr[x, y]);
                    }
                }
                string filename = "C:\\Users\\sheph\\Documents\\Arduino\\ASEN-4018-Automation\\test.jpg";
                theImage.UnlockBits(data);
                theImage.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);



                C.Connected = false;
                C.Dispose();
            }
            #endregion
        }
    }
}
