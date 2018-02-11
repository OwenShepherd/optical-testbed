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
using ASEN;




namespace ASCOM
{

    class Program
    {
        
        static void Main(string[] args)
        {

            // Put the driver name here.  You can figure out the driver name by uncommenting the below code
            string driverID = "ASCOM.ASICamera2.Camera";
            double exposureTime = 0.01; // Sets the exposure time in seconds.
            bool lightImage = true; // Sets the ASI to take a light or dark image

            ASEN_RCWS C = new ASEN_RCWS(driverID);

            C.InitializeCamera();

            C.Capture(exposureTime, lightImage);

            C.Disconnect();

           }
    }
}
