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
        //private static int sampleImageReadings = 10; // trials to read a exposed spotfield image, commented out, not used
        private static int sampleOptionDynNoiseCut = 1; // use dynamic noise cut features
        private static int sampleOptionCalcSpotDias = 0; // don't calculate spot diameters
        private static int sampleOptionCancelTilt = 0; // don't cancel average wavefront tip and tilt, ALTERED BY JAKE
        private static int sampleOptionLimitToPupil = 0; // don't limit wavefront calculation to pupil interior
        private static int sampleZernikeOrders = 3; // calculate up to 3rd Zernike order
        private static int maxZernikeModes = 66; // allocate Zernike array of 67 because index is 1..66
        //private static int sampleOptionHighspeed = 1; // use highspeed mode (only for WFS10 and WFS20 instruments), commented out, not used
        //private static int sampleOptionHsAdaptCentr = 1; // adapt centroids in highspeed mode to previously measured centroids, commented out, not used
        //private static int sampleHsNoiseLevel = 30; // cut lower 30 digits in highspeed mode, commented out, not used
        //private static int sampleOptionHsAllowAutoexpos = 1; // allow autoexposure in highspeed mode (runs somewhat slower), commented out, not used
        private static int sampleWavefrontType = 0; // WAVEFRONT_MEAS = 0
        private static int samplePrintoutSpots = 5; // printout results for first 5 x 5 spots only

        //JAKE DEFINED VALUES
        public static int BufferSize = 255;//Just defined this to try and see what would happen. Previously, these entries were "WFS.BufferSize", which isn't defined in the dll. The manual seemed to mention 255 fairly often, so I'll try it for now.
        public static int ImageBufferSize = 255;//Similar to the above line, there wasn't an ImageBufferSize value defined in WFS, so I had to create my own. Since it is of type byte, it made sense to limit the value to 255.
        #endregion

        private static WFS instrument = new WFS(IntPtr.Zero);//This calls the WFS constructor to create a new WFS object, called "instrument".



        static void Main(string[] args)
        {
            //----------------------------------- DRIVER CHECKS ---------------------------------------
            //Note: A lot of the functions used from the driver software won't work in highspeed mode.
            //Note: I believe there needs to be additional code to initially calibrate the MLAs, as (I believe) this code assumes the user has previously calibrated the MLAs.
            //      This could most likely be automated because (I believe) there is a way to save the calibration over time, or else we can just calibrate before each batch of testing.

            ConsoleKeyInfo waitKey; //This key allows one to read from the console (when user input in necessary).

            int BufferSize = 256;//Just defined this to try and see what would happen. Previously, these entries were "WFS.BufferSize", which isn't defined in the dll. The manual seemed to mention 256 fairly often, so I'll try it for now.

            int selectedInstrId = 0;//I believe this is a handler index in the situation that there would be mulitple SHA's connected to the computer.
            string resourceName = default(string);//Just creating a string initially set to null.

            Console.WriteLine("=================================================================");
            Console.WriteLine("Jake's Attempt at Creating a SHA API to Take Images, Set Exposure");
            Console.WriteLine("=================================================================");

            //These two lines are also constructors, but creating StringBuilders. They create strings up to a specified size (BufferSize, in this case).
            StringBuilder camDriverRev = new StringBuilder(BufferSize);
            StringBuilder wfsDriverRev = new StringBuilder(BufferSize);

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
            StringBuilder manufacturerName = new StringBuilder(BufferSize);
            StringBuilder instrumentName = new StringBuilder(BufferSize);
            StringBuilder serialNumberWfs = new StringBuilder(BufferSize);
            StringBuilder serialNumberCam = new StringBuilder(BufferSize);

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

            //----------------------------------- CAMERA CONFIGURATION ---------------------------------------
            //In the future, I believe this is where a loop would go, varying the exposure of the SHA, using the information from the experimental variables

            //I believe you need to predefine these variables before you use them for the first time as "out" variables, which are essentially pointers.
            double exposureTimeMin;
            double exposureTimeMax;
            double exposureTimeIncr;
            double exposureTimeAct;

            double masterGainMin;
            double masterGainMax;
            double masterGainAct;

            //SelectMla aids with the selection of the MLA from the list of pre-calibrated MLAs (helper function).
            SelectMla();

            Console.WriteLine("\n>> Configure Device, use pre-defined settings <<");
            //ConfigureDevice(selectedInstrId);//ConfigureDevice selects the resolution and bit width per pixel (currently only 8 bit supported) of the WFS.
            //COMMENTED THIS LINE OUT -- didn't seem like the helper function was configuring anything for the WFS150, which we are using.

            Console.WriteLine(">> Setting Exposure <<");

            //This function finds the maximum and minimum exposure times possible with the selected resolution. (these inputs supposedly need to be passed by reference, which seems to be what "out" does).
            instrument.GetExposureTimeRange(out exposureTimeMin, out exposureTimeMax, out exposureTimeIncr);
            Console.Write("Minimum Exposure (ms)       : ");
            Console.WriteLine(exposureTimeMin);
            Console.Write("Maximum Exposure (ms)       : ");
            Console.WriteLine(exposureTimeMax);
            Console.Write("Exposure Time Increment (ms): ");
            Console.WriteLine(exposureTimeIncr);

            Console.Write("\nInput the desired Exposure (ms): ");
            string tempstr = Console.ReadLine();
            double exposureTimeSet = Convert.ToDouble(tempstr);

            //This function sets the exposure time of the camera, and returns the actual exposure time set.
            instrument.SetExposureTime(exposureTimeSet, out exposureTimeAct);
            Console.Write("\nActual Exposure Set: ");
            Console.WriteLine(exposureTimeAct);


            Console.WriteLine(">> Setting Master Gain <<");

            //This function finds the max and min linear master gain values for the WFS.
            instrument.GetMasterGainRange(out masterGainMin, out masterGainMax);
            Console.Write("Minimum Linear Master Gain: ");
            Console.WriteLine(masterGainMin);
            Console.Write("Maximum Linear Master Gain: ");
            Console.WriteLine(masterGainMax);

            //Prompt the user to provide the master gain value.
            Console.Write("\nInput the desired master Gain: ");
            tempstr = Console.ReadLine();
            double masterGainSet = Convert.ToDouble(tempstr);
            //Set the master gain.
            instrument.SetMasterGain(masterGainSet, out masterGainAct);
            Console.Write("\nActual Master Gain Set: ");
            Console.WriteLine(masterGainAct);

            //Set WFS internal reference plane
            Console.WriteLine("\nSet WFS to internal reference plane.\n");
            instrument.SetReferencePlane(sampleRefPlane);

            // define pupil size and position, Zernike results are related to pupil (not sure what to do if we aren't in the pupil. Maybe define Radius of curvature?
            DefinePupil();

            //Here, removed AdjustImageBrightness(), since I am setting the exposure and gain manually.

            //----------------------------------- GATHER CAMERA DATA ---------------------------------------

            //Helper function. The camera image can be retrieved for later display.
            GetSpotfieldImage();

            //----------------------------------- PROCESS CAMERA DATA ---------------------------------------

            //Helper function. Calculate and display the centroid positions of the spots.
            CalcSpotCentroids();
            Console.WriteLine("Calculating Spot Centroids...");
            //Console.WriteLine("\nPress <ANY_KEY> to proceed...");
            //waitKey = Console.ReadKey(true);

            //Calculate and display the beam parameters, derived from the centroid intensities
            CalcBeamCentroid();
            Console.WriteLine("Calculating Beam Centroids...");
            //Console.WriteLine("\nPress <ANY_KEY> to proceed...");
            //waitKey = Console.ReadKey(true);

            //Calculate spot deviations to internal reference positions
            CalcSpotDeviations();
            Console.WriteLine("Calculating Spot Deviations...");
            //Console.WriteLine("\nPress <ANY_KEY> to proceed...");
            //waitKey = Console.ReadKey(true);

            // calculate and display the wavefront
            CalcWavefront();
            Console.WriteLine("Calculating Wavefront...");
            //Console.WriteLine("\nPress <ANY_KEY> to proceed...");
            //waitKey = Console.ReadKey(true);

            // calculate and display a pre-defined number of Zernike results
            CalcZernikes();
            Console.WriteLine("Calculating Zernikes...");
            //Console.WriteLine("\nPress <ANY_KEY> to proceed...");
            //waitKey = Console.ReadKey(true);

            // enter highspeed mode and read calculated spot centroids directly from the camera
            // this enables faster measurements
            //I got rid of this. Highspeed mode is only available for WFS10 and WFS20, and we have the WFS150.
            //HighspeedMode(selectedInstrId);
            Console.WriteLine("\nEnd of Sample Program, press <ANY_KEY> to exit.");
            waitKey = Console.ReadKey(false);

            // Close instrument, important to release allocated driver data!
            instrument.Dispose();

            //----------------------------------- END PROGRAM ---------------------------------------


        }


        //----------------------------------- BEGIN HELPER FUNCTIONS ---------------------------------------

        //SelectInstrument aids with the initial selection of attached WFS, as well as connection.
        private static void SelectInstrument(out int selectedInstrId, out string resourceName)
        {
            resourceName = null;
            int instrCount;
            instrument.GetInstrumentListLen(out instrCount);//This function reads all the connected WFS to the computer and returns the number of sensors.
            if (0 == instrCount)//If GetInstrumentListLen doesn't find any connected Wavefront Sensors.
            {
                Console.WriteLine("No Wavefront Sensor instrument found!");
                selectedInstrId = -1;//Error return value.
                ConsoleKeyInfo waitKey = Console.ReadKey(true);//Waits for a key to be pressed to exit the program.
                return;
            }

            //This block lists all the connected wavefront sensors (includes the following for loop).
            Console.WriteLine("Available Wavefront Sensor instruments:\n");
            int deviceID;
            int inUse;

            //Initialize some new strings.
            StringBuilder instrumentName = new StringBuilder(BufferSize);
            StringBuilder instrumentSN = new StringBuilder(BufferSize);
            StringBuilder resourceNameTemp = new StringBuilder(BufferSize);

            //This loop is used to find the characteristics of all connected WFS and print to console.
            //It's a loop because one could have connected many WFS to the computer.
            for (int i = 0; i < instrCount; ++i)
            {
                instrument.GetInstrumentListInfo(i, out deviceID, out inUse, instrumentName, instrumentSN, resourceNameTemp);//Finds information about the instrument based on index.
                resourceName = resourceNameTemp.ToString();
                Console.Write(deviceID.ToString() + "  " + instrumentName.ToString() + "  " + instrumentSN.ToString() + ((0 == inUse) ? "" : "  (inUse)") + "\n\n");
            }

            //After outputting information on all the WFS connected, prompts to select the WFS to use.
            Console.Write("Select a Wavefront Sensor instrument: ");

            string input = Console.ReadLine();
            bool chk = Int32.TryParse(input.ToString(), out selectedInstrId);//Error handling.
            if (!chk)
            {
                throw new Exception("Invalid selection", new Exception(input.ToString()));
            }

            // get selected resource name
            int deviceIDtemp = 0;
            for (int i = 0; (i < instrCount) && (deviceIDtemp != selectedInstrId); ++i)
            {
                instrument.GetInstrumentListInfo(i, out deviceID, out inUse, instrumentName, instrumentSN, resourceNameTemp);
                deviceIDtemp = deviceID;
                resourceName = resourceNameTemp.ToString();
            }

            // selectedInstrId available?
            if (deviceIDtemp != selectedInstrId)
            {
                throw new Exception("Invalid selection", new Exception(selectedInstrId.ToString()));
            }
        }

        //SelectMla aids with the selection of the MLA from the list of pre-calibrated MLAs.
        private static void SelectMla()
        {
            //Create variables.
            int selectedMla;
            int mlaCount;

            instrument.GetMlaCount(out mlaCount);//This function returns the number of calibrated MLAs.

            //Preparing for determining information on all calibrated MLAs.
            //Note that once an MLA is calibrated, it receives an MLA index, very similar to the method for WFsensors.
            Console.WriteLine("\nAvailable Microlens Arrays:\n");
            StringBuilder mlaName = new StringBuilder(BufferSize);
            double camPitch;
            double lensletPitch;
            double spotOffsetX;
            double spotOffsetY;
            double lensletFum;
            double grdCorr0;
            double grdCorr45;

            //Loops through all calibrated MLAs, outputting their respective calibration characteristics.
            for (int i = 0; i < mlaCount; ++i)
            {
                instrument.GetMlaData(i, mlaName, out camPitch, out lensletPitch, out spotOffsetX, out spotOffsetY, out lensletFum, out grdCorr0, out grdCorr45);
                Console.Write(i.ToString() + "  " + mlaName.ToString() + "  " + camPitch.ToString() + "  " + lensletPitch.ToString() + "\n");
            }

            //After all the information is written to the console, select an MLA.
            Console.Write("\nSelect a Microlens Array: ");
            string input = Console.ReadLine();
            bool chk = Int32.TryParse(input.ToString(), out selectedMla);
            if (!chk)//Error Handling.
            {
                throw new Exception("Invalid selection", new Exception(input.ToString()));
            }
            else
            {
                if ((0 > selectedMla) || (mlaCount <= selectedMla))
                {
                    throw new Exception("Invalid selection", new Exception(selectedMla.ToString()));
                }
                else
                {
                    //Selects one of the MLAs based on the calibrated index.
                    instrument.SelectMla(selectedMla);
                }
            }
        }

        //ConfigureDevice, selects the resolution and bit width per pixel (currently only 8 bit supported) of the WFS.
        //Note that I removed half of this function, as in pertains only to the WFS10 and WFS20 line, whereas we use the WFS150.
        /*private static void ConfigureDevice(int selectedInstrId)
        {
            int spotsX = 0;//Number of spots which can be detected in the X direction.
            int spotsY = 0;//Number of spots which can be detected in the Y direction.

            //This if statement is used to determine if the connected sensor is either the WFS10 and WFS20. We won't use these, so I've commented it out.

            /*if ((0 == (selectedInstrId & WFS.DeviceOffsetWFS10)) &&
                (0 == (selectedInstrId & WFS.DeviceOffsetWFS20)))
            {
                Console.Write("Configure WFS camera with resolution index: " + sampleCameraResolWfs.ToString() + " (" + WFS.CamWFSXPixel[sampleCameraResolWfs].ToString() + " x " + WFS.CamWFSYPixel[sampleCameraResolWfs].ToString() + " pixels)\n");

                //This function configures the camera's resolution, and the max. number of detectable spots in X and Y direction.
                //We will most likely want to use the maximum of each of these values, for maximum information to detect the Zernikes.
                instrument.ConfigureCam(pixelFormat, sampleCameraResolWfs, out spotsX, out spotsY);
            }
            else
                Console.WriteLine("Error in ConfigureDevice function.");

            Console.WriteLine("Camera is configured to detect " + spotsX.ToString() + " x " + spotsY.ToString() + " lenslet spots.\n", spotsX, spotsY);
        }*/

        /// Set the pupil to pre-defined values, the pupil needs to fit the selected camera resolution and the beam diameter
        /// Zernike results depend and relate to the pupil size!
        private static void DefinePupil()
        {
            Console.WriteLine("\nDefine pupil to:");
            Console.WriteLine("Centroid_x = " + samplePupilCentroidX.ToString("F3"));
            Console.WriteLine("Centroid_y = " + samplePupilCentroidY.ToString("F3"));
            Console.WriteLine("Diameter_x = " + samplePupilDiameterX.ToString("F3"));
            Console.WriteLine("Diameter_y = " + samplePupilDiameterY.ToString("F3") + "\n");

            //This line sets up the pupil, although I'm not sure what we do, since we don't have a pupil.
            instrument.SetPupil(samplePupilCentroidX, samplePupilCentroidY, samplePupilDiameterX, samplePupilDiameterY);
        }

        //Creates the 2d array to store the spotfield information.
        private static void GetSpotfieldImage()
        {
            //Create the 2d array to store the spotfield information.
            //byte[] imageBuffer = new byte[WFS.ImageBufferSize]; Jake removed this, since WFS.ImageBufferSize wasn't defined.
            byte[] imageBuffer = new byte[ImageBufferSize];

            int rows;
            int cols;
            //This function retrieves the spotfield information 
            instrument.GetSpotfieldImage(imageBuffer, out rows, out cols);
        }

        //Calculate all spot centroid positions using dynamic noise cut option.
        private static void CalcSpotCentroids()
        {
            //Calculates the centroids, diameters, and intensities of all spots generated by the lenslets. (Note: calculates, not saves. Save data is to follow).
            instrument.CalcSpotsCentrDiaIntens(sampleOptionDynNoiseCut, sampleOptionCalcSpotDias);

            //Get centroid result arrays. 
            float[] centroidX = new float[WFS.MaxSpotX];
            float[] centroidY = new float[WFS.MaxSpotY];
            instrument.GetSpotCentroids(centroidX, centroidY);

            //Print out some centroid positions
            Console.WriteLine("\nCentroid X Positions in pixels (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
                
                    Console.Write(" " + centroidX[i].ToString("F3"));
                
                Console.WriteLine("");
            }

            Console.WriteLine("\nCentroid Y Positions in pixels (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
                    Console.Write("  " + centroidY[i].ToString("F3"));
                
                Console.WriteLine("");
            }
        }

        //Calculate centroid and diameter of the optical beam
        //You may use this beam data to define a pupil variable in position and size
        private static void CalcBeamCentroid()
        {
            //Define variables.
            double beamCentroidX, beamCentroidY, beamDiameterX, beamDiameterY;

            //This function calculates and returns the beam centroid and diameter data based on the intensity distribution of the WFS camera image in mm.
            instrument.CalcBeamCentroidDia(out beamCentroidX, out beamCentroidY, out beamDiameterX, out beamDiameterY);

            Console.WriteLine("\nInput beam is measured to:");
            Console.WriteLine("CentroidX = " + beamCentroidX.ToString("F3") + " mm");
            Console.WriteLine("CentroidY = " + beamCentroidY.ToString("F3") + " mm");
            Console.WriteLine("DiameterX = " + beamDiameterX.ToString("F3") + " mm");
            Console.WriteLine("DiameterY = " + beamDiameterY.ToString("F3") + " mm");
        }

        //Calculate and printout spot deviations to reference positions
        private static void CalcSpotDeviations()
        {
            //This function calculates reference positions and deviations for all spots depending on the setting ref_idx(internal/user) set by SetWavefrontReference.
            instrument.CalcSpotToReferenceDeviations(sampleOptionCancelTilt);

            // get spot deviations
            float[] deviationX = new float[WFS.MaxSpotX];
            float[] deviationY = new float[WFS.MaxSpotY];
            instrument.GetSpotDeviations(deviationX, deviationY);

            // print out some spot deviations
            Console.WriteLine("\nSpot Deviation X in pixels (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
            
                Console.Write(" " + deviationX[i].ToString("F3"));
                
                Console.WriteLine("");
            }
            Console.WriteLine("\nSpot Deviation Y in pixels (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
                
                    Console.Write(" " + deviationY[i].ToString("F3"));
                
                Console.WriteLine("");
            }
        }

        //Calculate and printout measured wavefront
        private static void CalcWavefront()
        {
            ConsoleKeyInfo waitKey;
            //Create a new 2d array.
            float[] wavefront = new float[WFS.MaxSpotY*WFS.MaxSpotX];

            //This function calculates the wavefront based on spot deviations.
            instrument.CalcWavefront(sampleWavefrontType, sampleOptionLimitToPupil, wavefront);

            // print out some wavefront points
            Console.WriteLine("\nWavefront in microns (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
                    Console.Write(" " + wavefront[i].ToString("F3"));
                
                Console.WriteLine("");
            }
            Console.WriteLine("\nPress <ANY_KEY> to proceed...");
            waitKey = Console.ReadKey(true);

            // calculate wavefront statistics within defined pupil
            double wavefrontMin, wavefrontMax, wavefrontDiff, wavefrontMean, wavefrontRms, wavefrontWeightedRms;
            instrument.CalcWavefrontStatistics(out wavefrontMin, out wavefrontMax, out wavefrontDiff, out wavefrontMean, out wavefrontRms, out wavefrontWeightedRms);
            Console.WriteLine("\nWavefront Statistics in microns:");
            Console.WriteLine("Min          : " + wavefrontMin.ToString("F3"));
            Console.WriteLine("Max          : " + wavefrontMax.ToString("F3"));
            Console.WriteLine("Diff         : " + wavefrontDiff.ToString("F3"));
            Console.WriteLine("Mean         : " + wavefrontMean.ToString("F3"));
            Console.WriteLine("RMS          : " + wavefrontRms.ToString("F3"));
            Console.WriteLine("Weigthed RMS : " + wavefrontWeightedRms.ToString("F3"));
        }

        // calculate Zernike results in microns, related to defined pupil size
        private static void CalcZernikes()
        {
            Console.WriteLine("\nZernike fit up to order " + sampleZernikeOrders.ToString());
            int zernike_order = sampleZernikeOrders;//Specifies up to what order to calculate the Zernikes. sampleZernikeOrders is specified before main.
            float[] zernikeUm = new float[maxZernikeModes + 1];
            float[] zernikeOrdersRmsUm = new float[maxZernikeModes + 1];
            double rocMm;

            //This function caluclates the spot deviations (centroid with respect to its reference) and performs a least square fit to the desired number of Zernike functions.
            instrument.ZernikeLsf(out zernike_order, zernikeUm, zernikeOrdersRmsUm, out rocMm); // also calculates deviation from centroid data for wavefront integration

            //Write the Zernike results to the console.
            Console.WriteLine("\nZernike Mode    Coefficient");
            for (int i = 0; i < (maxZernikeModes + 1); ++i)
            {
                Console.WriteLine("  " + i.ToString() + "             " + zernikeUm[i].ToString("F3"));
            }
        }
    }

    //----------------------------------- END HELPER FUNCTIONS ---------------------------------------
}

