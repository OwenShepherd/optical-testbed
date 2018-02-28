namespace SHA_Control
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Thorlabs.WFS.Interop64;



    public class Program
    {
        #region Defines
        private static int sampleCameraResolWfs = 2; // CAM_RES_768 = 768x768 pixels
        //private static int sampleCameraResolWfs10 = 2; // CAM_RES_WFS10_360 = 360x360 pixels, commented out, not used
        //private static int sampleCameraResolWfs20 = 3; // CAM_RES_WFS20_512 = 512x512 pixels, commented out, not used
        private static int pixelFormat = 0; // PIXEL_FORMAT_MONO8 = 0
        private static int sampleRefPlane = 0; // WFS_REF_INTERNAL = 0
        private static double samplePupilCentroidX = 0.0; // in mm
        private static double samplePupilCentroidY = 0.0;
        private static double samplePupilDiameterX = 2.0; // in mm, needs to fit to selected camera resolution
        private static double samplePupilDiameterY = 2.0;
        private static int sampleImageReadings = 10; // trials to read a exposed spotfield image
        private static int sampleOptionDynNoiseCut = 1; // use dynamic noise cut features
        private static int sampleOptionCalcSpotDias = 0; // don't calculate spot diameters
        private static int sampleOptionCancelTilt = 0; // don't cancel average wavefront tip and tilt, ALTERED BY JAKE
        private static int sampleOptionLimitToPupil = 0; // don't limit wavefront calculation to pupil interior
        private static int sampleZernikeOrders = 3; // calculate up to 3rd Zernike order
        private static int maxZernikeModes = 66; // allocate Zernike array of 67 because index is 1..66
        private static int sampleOptionHighspeed = 1; // use highspeed mode (only for WFS10 and WFS20 instruments)
        private static int sampleOptionHsAdaptCentr = 1; // adapt centroids in highspeed mode to previously measured centroids
        private static int sampleHsNoiseLevel = 30; // cut lower 30 digits in highspeed mode
        private static int sampleOptionHsAllowAutoexpos = 1; // allow autoexposure in highspeed mode (runs somewhat slower)
        private static int sampleWavefrontType = 0; // WAVEFRONT_MEAS = 0
        private static int samplePrintoutSpots = 5; // printout results for first 5 x 5 spots only
        #endregion

        private static WFS instrument = new WFS(IntPtr.Zero);//This calls the WFS constructor to create a new WFS object, called "instrument".



        static void Main(string[] args)
        {
            //----------------------------------- DRIVER CHECKS ---------------------------------------
            //Note: A lot of the functions used from the driver software won't work in highspeed mode.
            //Note: I believe there needs to be additional code to initially calibrate the MLAs, as (I believe) this code assumes the user has previously calibrated the MLAs.
            //      This could most likely be automated because (I believe) there is a way to save the calibration over time, or else we can just calibrate before each batch of testing.

            ConsoleKeyInfo waitKey; //This key allows one to read from the console (when user input in necessary).

            int selectedInstrId = 0;//I believe this is a handler index in the situation that there would be mulitple SHA's connected to the computer.
            string resourceName = default(string);//Just creating a string initially set to null.

            Console.WriteLine("=================================================================");
            Console.WriteLine("Jake's Attempt at Creating a SHA API to Take Images, Set Exposure");
            Console.WriteLine("=================================================================");

            //These two lines are also constructors, but creating StringBuilders. They create strings up to a specified size (WFS.BufferSize, in this case).
            StringBuilder camDriverRev = new StringBuilder(WFS.BufferSize);
            StringBuilder wfsDriverRev = new StringBuilder(WFS.BufferSize);

            //This function comes from the WFS class, which means this structure can be used in the future as a reference (of how to write the code).
            instrument.revision_query(wfsDriverRev, camDriverRev);//Checks for the current revision of computer (installed) and camera (installed) drivers.

            //This chuck writes the driver information to the console.
            Console.WriteLine("");
            Console.Write("WFS instrument driver version : ");
            Console.WriteLine(wfsDriverRev.ToString(), camDriverRev.ToString());
            Console.WriteLine();

            //----------------------------------- CAMERA CONNECTION ---------------------------------------

            //Calls the helper function, which uses functions from the namespace find connected WFS, get information on the connected WFS, allow the user to select one.
            SelectInstrument(out selectedInstrId, out resourceName);

            //Error Handling, checking that the selected WFS isn't a negative index.
            if (0 > selectedInstrId)
                return;

            //Yields resource name of connected sensor.
            Console.Write("\nResource name of selected WFS: ");
            Console.WriteLine(resourceName.ToString());
            Console.WriteLine();

            Console.WriteLine("\n-----------------------------------------------------------\n");
            Console.WriteLine(">> Initialize Device <<");

            //Not totally sure what is going on here. Having trouble gaining insight into this constructor.
            instrument = new WFS(resourceName, false, false);

            //Fairly straightforward. Getting information about the selected WFS. From the ThorLabs namespace.
            Console.WriteLine(">> Get Device Info <<");
            StringBuilder manufacturerName = new StringBuilder(WFS.BufferSize);
            StringBuilder instrumentName = new StringBuilder(WFS.BufferSize);
            StringBuilder serialNumberWfs = new StringBuilder(WFS.BufferSize);
            StringBuilder serialNumberCam = new StringBuilder(WFS.BufferSize);

            //If this line succeeds, the connection was successful, as we access unit-specific information.
            instrument.GetInstrumentInfo(manufacturerName, instrumentName, serialNumberWfs, serialNumberCam);

            //Writing unit-specific information to the console for verification.
            Console.WriteLine(">> Opened Instrument <<");
            Console.Write("Manufacturer           : ");
            Console.WriteLine(manufacturerName);
            Console.Write("Instrument Name        : ");
            Console.WriteLine(instrumentName);
            Console.Write("Serial Number WFS      : ");
            Console.WriteLine(serialNumberWfs);
