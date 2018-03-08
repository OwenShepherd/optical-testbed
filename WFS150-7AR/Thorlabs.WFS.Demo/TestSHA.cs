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
            //CONFIRMED ON 3/8/18 TO WORK WITH THE SHA -- Tested at SwRI to work, no errors thrown, measured the beamwidth an everything was taken.
            Console.WriteLine("Currently running TestSHA.cs");

            double exposureTime = 1;//milliseconds, This test value is well within the operating range of 79 microseconds - 65 milliseconds, as given in the manual for the (WFS150-7AR). 
            int expectedImageSize = 1280 * 1024; 
            byte[] spotfieldImage = new byte[expectedImageSize];//This is set to WFS.ImageBufferSize in the original code.
            float[] zernikeCoeffs = new float[67];//The maximum number of Zernike Modes that can be returned, although only the first 10 entries should be non-zero. Entry 1 is ZCF1, 2, ..., 66.


            ASEN_SHA C = new ASEN_SHA();

            C.CameraConnectionAndSetup();

            spotfieldImage = C.GatherCameraData(exposureTime);

            /*for(int i=0; i < expectedImageSize; i++)
            {
                Console.WriteLine(spotfieldImage[i]);
            }*/
            
            zernikeCoeffs = C.ProcessCameraData();
            //These are confirmed to work. Note that for whatever reason, the first entry in zernikeCoeffs will always be 0, as Thorlabs decided to store the zernike coeffs in their respective index. So, ZCF1 in in entry 1, not entry 0 and so on.

            /*for (int i = 0; i < 67; i++)
            {
                Console.WriteLine(zernikeCoeffs[i]);
            }*/

            C.CloseCamera();
        }
    }
}
