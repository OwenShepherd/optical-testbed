using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thorlabs.WFS.Interop;
using ASEN;


namespace ASEN
{
    class TestSHA
    {

        static void Main(string[] args)
        {
            //This program is here to test the functionality of the SHA with the new ASEN_SHA class.
            //NOTE: -- The SHA manual says it needs 15 minutes of warmup time to achieve it's rated accuracy, so we need to keep this in mind when it comes to testing.


            double exposureTime = 1;//milliseconds, This test value is well within the operating range of 79 microseconds - 65 milliseconds, as given in the manual for the (WFS150-7AR). 
            int expectedImageSize = 1280 * 1024; 
            byte[] spotfieldImage = new byte[expectedImageSize];//This is set to WFS.ImageBufferSize in the original code.
            float[] zernikeCoeffs = new float[67];//The maximum number of Zernike Modes that can be returned, although only the first 10 entries should be non-zero. Entry 1 is ZCF1, 2, ..., 66.


            ASEN_SHA C = new ASEN_SHA();

            C.CameraConnectionAndSetup();

            spotfieldImage = C.GatherCameraData(exposureTime);
            
            zernikeCoeffs = C.ProcessCameraData();

            C.CloseCamera();
        }
    }
}
