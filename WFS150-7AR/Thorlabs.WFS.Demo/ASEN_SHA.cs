using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thorlabs.WFS.Interop;


namespace ASEN
{
    class ASEN_SHA : WFS
    {
        public ASEN_SHA(IntPtr Instrument_Handle) : base(IntPtr Instrument_Handle)
        {

        }

        //NOTE: All below functions (excluding the helper functions) will need to specify what they're returning, as well as what inputs they will need.
        //The current state of the functions work, but need to be altered to allow for functions to be 

        public void DriverChecks()
        {
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
            this.revision_query(wfsDriverRev, camDriverRev);//Checks for the current revision of computer (installed) and camera (installed) drivers.

            //This chuck writes the driver information to the console.
            Console.WriteLine("");
            Console.Write("WFS instrument driver version : ");
            Console.WriteLine(wfsDriverRev.ToString(), camDriverRev.ToString());
            Console.WriteLine();
        }

        public void CameraConnection()
        {
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
            this.GetInstrumentInfo(manufacturerName, instrumentName, serialNumberWfs, serialNumberCam);

            //Writing unit-specific information to the console for verification.
            Console.WriteLine(">> Opened Instrument <<");
            Console.Write("Manufacturer           : ");
            Console.WriteLine(manufacturerName);
            Console.Write("Instrument Name        : ");
            Console.WriteLine(instrumentName);
            Console.Write("Serial Number WFS      : ");
            Console.WriteLine(serialNumberWfs);
        }

        public void CameraConfiguration()
        {
            //---NOTE--- This will need to be configured such that it can take in the exposure time and set this (as well as the gain) without console input (for automation).
            // select a microlens array, WFS kits can operate 2 or more MLAs
            SelectMla();

            // Configure the camera resolution to a pre-defined setting
            Console.WriteLine("\n>> Configure Device, use pre-defined settings <<");
            ConfigureDevice(selectedInstrId);

            // set camera exposure time and gain if you don't want to use auto exposure
            // use functions GetExposureTimeRange, SetExposureTime, GetMasterGainRange, SetMasterGain

            // set WFS internal reference plane
            Console.WriteLine("\nSet WFS to internal reference plane.\n");
            this.SetReferencePlane(sampleRefPlane);

            // define pupil size and position, Zernike results are related to pupil
            DefinePupil();

            // use the autoexposure feature to find the optimal exposure time and gain
            //AdjustImageBrightness();

            Console.WriteLine(">> Setting Exposure <<");

            double exposureTimeMin;
            double exposureTimeMax;
            double exposureTimeIncr;
            double exposureTimeAct;

            double masterGainMin;
            double masterGainMax;
            double masterGainAct;

            //This function finds the maximum and minimum exposure times possible with the selected resolution. (these inputs supposedly need to be passed by reference, which seems to be what "out" does).
            this.GetExposureTimeRange(out exposureTimeMin, out exposureTimeMax, out exposureTimeIncr);
            Console.Write("Minimum Exposure (ms)       : ");
            Console.WriteLine(exposureTimeMin);
            Console.Write("Maximum Exposure (ms)       : ");
            Console.WriteLine(exposureTimeMax);
            Console.Write("Exposure Time Increment (ms): ");
            Console.WriteLine(exposureTimeIncr);

            Console.Write("\nInput the desired Exposure (ms): ");
            string temp = Console.ReadLine();
            double exposureTimeSet = Convert.ToDouble(temp);

            //This function sets the exposure time of the camera, and returns the actual exposure time set.
            this.SetExposureTime(exposureTimeSet, out exposureTimeAct);
            Console.Write("\nActual Exposure Set: ");
            Console.WriteLine(exposureTimeAct);


            Console.WriteLine(">> Setting Master Gain <<");

            //This function finds the max and min linear master gain values for the WFS.
            this.GetMasterGainRange(out masterGainMin, out masterGainMax);
            Console.Write("Minimum Linear Master Gain: ");
            Console.WriteLine(masterGainMin);
            Console.Write("Maximum Linear Master Gain: ");
            Console.WriteLine(masterGainMax);

            //Prompt the user to provide the master gain value.
            Console.Write("\nInput the desired master Gain: ");
            temp = Console.ReadLine();
            double masterGainSet = Convert.ToDouble(temp);

            //Set the master gain.
            this.SetMasterGain(masterGainSet, out masterGainAct);
            Console.Write("\nActual Master Gain Set: ");
            Console.WriteLine(masterGainAct);
        }

        public void GatherCameraData()
        {
            // the camera image can be retrieved for later display
            GetSpotfieldImage();
        }

        public void ProcessCameraData()
        {
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
        }

        public void CloseCamera()
        {
            Console.WriteLine("\nEnd of Sample Program, press <ANY_KEY> to exit.");
            waitKey = Console.ReadKey(false);

            // Close instrument, important to release allocated driver data!
            this.Dispose();
        }



        //--------------------------- HELPER FUNCTIONS ---------------------------
        //These helper functions shorten the above class functions, but are not expected to be called explicitly by the user.




        #region Helper Methods
        private void SelectInstrument(out int selectedInstrId, out string resourceName)
        {
            resourceName = null;
            int instrCount;
            this.GetInstrumentListLen(out instrCount);
            if (0 == instrCount)
            {
                Console.WriteLine("No Wavefront Sensor instrument found!");
                selectedInstrId = -1;
                ConsoleKeyInfo waitKey = Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Available Wavefront Sensor instruments:\n");
            int deviceID;
            int inUse;

            StringBuilder instrumentName = new StringBuilder(WFS.BufferSize);
            StringBuilder instrumentSN = new StringBuilder(WFS.BufferSize);
            StringBuilder resourceNameTemp = new StringBuilder(WFS.BufferSize);

            for (int i = 0; i < instrCount; ++i)
            {
                this.GetInstrumentListInfo(i, out deviceID, out inUse, instrumentName, instrumentSN, resourceNameTemp);
                resourceName = resourceNameTemp.ToString();
                Console.Write(deviceID.ToString() + "  " + instrumentName.ToString() + "  " + instrumentSN.ToString() + ((0 == inUse) ? "" : "  (inUse)") + "\n\n");
            }

            Console.Write("Select a Wavefront Sensor instrument: ");

            string input = Console.ReadLine();
            bool chk = Int32.TryParse(input.ToString(), out selectedInstrId);
            if (!chk)
            {
                throw new Exception("Invalid selection", new Exception(input.ToString()));
            }

            // get selected resource name
            int deviceIDtemp = 0;
            for (int i = 0; (i < instrCount) && (deviceIDtemp != selectedInstrId); ++i)
            {
                this.GetInstrumentListInfo(i, out deviceID, out inUse, instrumentName, instrumentSN, resourceNameTemp);
                deviceIDtemp = deviceID;
                resourceName = resourceNameTemp.ToString();
            }

            // selectedInstrId available?
            if (deviceIDtemp != selectedInstrId)
            {
                throw new Exception("Invalid selection", new Exception(selectedInstrId.ToString()));
            }
        }


        /// <summary>
        ///  select a microlens array, WFS kits can operate 2 or more MLAs
        ///  you need to type in the MLA index number
        /// </summary>
        private void SelectMla()
        {
            int selectedMla;
            int mlaCount;
            this.GetMlaCount(out mlaCount);

            Console.WriteLine("\nAvailable Microlens Arrays:\n");
            StringBuilder mlaName = new StringBuilder(WFS.BufferSize);
            double camPitch;
            double lensletPitch;
            double spotOffsetX;
            double spotOffsetY;
            double lensletFum;
            double grdCorr0;
            double grdCorr45;
            for (int i = 0; i < mlaCount; ++i)
            {
                this.GetMlaData(i, mlaName, out camPitch, out lensletPitch, out spotOffsetX, out spotOffsetY, out lensletFum, out grdCorr0, out grdCorr45);
                Console.Write(i.ToString() + "  " + mlaName.ToString() + "  " + camPitch.ToString() + "  " + lensletPitch.ToString() + "\n");
            }

            Console.Write("\nSelect a Microlens Array: ");
            string input = Console.ReadLine();
            bool chk = Int32.TryParse(input.ToString(), out selectedMla);
            if (!chk)
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
                    this.SelectMla(selectedMla);
                }
            }
        }

        /// <summary>
        /// set the camera to a pre-defined resolution (pixels x pixels)
        /// this image size needs to fit the beam size and pupil size
        /// </summary>
        private void ConfigureDevice(int selectedInstrId)
        {
            int spotsX = 0;
            int spotsY = 0;
            if ((0 == (selectedInstrId & WFS.DeviceOffsetWFS10)) &&
                (0 == (selectedInstrId & WFS.DeviceOffsetWFS20)))
            {
                Console.Write("Configure WFS camera with resolution index: " + sampleCameraResolWfs.ToString() + " (" + WFS.CamWFSXPixel[sampleCameraResolWfs].ToString() + " x " + WFS.CamWFSYPixel[sampleCameraResolWfs].ToString() + " pixels)\n");
                this.ConfigureCam(pixelFormat, sampleCameraResolWfs, out spotsX, out spotsY);
            }
            else
            {
                if (0 != (selectedInstrId & WFS.DeviceOffsetWFS10)) // WFS10 instrument
                {
                    Console.Write("Configure WFS10 camera with resolution index: " + sampleCameraResolWfs10.ToString() + " (" + WFS.CamWFSXPixel[sampleCameraResolWfs10].ToString() + " x " + WFS.CamWFSYPixel[sampleCameraResolWfs10].ToString() + " pixels)\n");
                    this.ConfigureCam(pixelFormat, sampleCameraResolWfs10, out spotsX, out spotsY);
                }
                else // WFS20 instrument
                {
                    Console.Write("Configure WFS20 camera with resolution index: " + sampleCameraResolWfs20.ToString() + " (" + WFS.CamWFSXPixel[sampleCameraResolWfs20].ToString() + " x " + WFS.CamWFSYPixel[sampleCameraResolWfs20].ToString() + " pixels)\n");
                    this.ConfigureCam(pixelFormat, sampleCameraResolWfs20, out spotsX, out spotsY);
                }
            }
            Console.WriteLine("Camera is configured to detect " + spotsX.ToString() + " x " + spotsY.ToString() + " lenslet spots.\n", spotsX, spotsY);
        }

        /// <summary>
        /// Set the pupil to pre-defined values, the pupil needs to fit the selected camera resolution and the beam diameter
        /// Zernike results depend and relate to the pupil size!
        /// </summary>
        private void DefinePupil()
        {
            Console.WriteLine("\nDefine pupil to:");
            Console.WriteLine("Centroid_x = " + samplePupilCentroidX.ToString("F3"));
            Console.WriteLine("Centroid_y = " + samplePupilCentroidY.ToString("F3"));
            Console.WriteLine("Diameter_x = " + samplePupilDiameterX.ToString("F3"));
            Console.WriteLine("Diameter_y = " + samplePupilDiameterY.ToString("F3") + "\n");
            this.SetPupil(samplePupilCentroidX, samplePupilCentroidY, samplePupilDiameterX, samplePupilDiameterY);
        }


        /// <summary>
        /// use the autoexposure feature to get a well exposed camera image
        /// repeate image reading in case of badly saturated image
        /// suited exposure time and gain settings are adjusted within the function TakeSpotfieldImageAutoExpos()
        /// </summary>
        private void AdjustImageBrightness()
        {
            ConsoleKeyInfo waitKey;
            int status;
            int expos_ok = 0;
            double exposAct;
            double masterGainAct;

            Console.WriteLine("\nRead camera images:\n");
            Console.WriteLine("Image No.     Status     ->   newExposure[ms]   newGainFactor");

            for (int cnt = 0; cnt < sampleImageReadings; ++cnt)
            {
                // take a camera image with auto exposure, note that there may several function calls required to get an optimal exposed image
                this.TakeSpotfieldImageAutoExpos(out exposAct, out masterGainAct);
                Console.Write("    " + cnt.ToString() + "     ");

                // check instrument status for non-optimal image exposure
                this.GetStatus(out status);

                if (0 != (status & WFS.StatBitHighPower))
                {
                    Console.Write("Power too high!    ");
                }
                else if (0 != (status & WFS.StatBitLowPower))
                {
                    Console.Write("Power too low!     ");
                }
                else if (0 != (status & WFS.StatBitHighAmbientLight))
                {
                    Console.Write("High ambient light!");
                }
                else
                {
                    Console.Write("OK                 ");
                }
                Console.WriteLine("     " + exposAct.ToString("F3") + "          " + masterGainAct.ToString("F3"));

                if ((0 == (status & WFS.StatBitHighPower)) &&
                    (0 == (status & WFS.StatBitLowPower)) &&
                    (0 == (status & WFS.StatBitHighAmbientLight)))
                {
                    expos_ok = 1;
                    break; // image well exposed and is usable
                }
            }

            // close program if no well exposed image is feasible
            if (0 == expos_ok)
            {
                Console.Write("\nSample program will be closed because of unusable image quality, press <ANY_KEY>.");
                this.Dispose(); // required to release allocated driver data
                waitKey = Console.ReadKey(true);
                throw new Exception("Unusable Image");
            }
        }


        /// <summary>
        /// get last camera image called 'spotfield'
        /// this is only required for later display
        /// </summary>
        private void GetSpotfieldImage()
        {
            byte[] imageBuffer = new byte[WFS.ImageBufferSize];
            int rows;
            int cols;
            this.GetSpotfieldImage(imageBuffer, out rows, out cols);
        }


        /// <summary>
        /// calculate all spot centroid positions using dynamic noise cut option
        /// </summary>
        private void CalcSpotCentroids()
        {
            this.CalcSpotsCentrDiaIntens(sampleOptionDynNoiseCut, sampleOptionCalcSpotDias);

            // get centroid result arrays
            float[,] centroidX = new float[WFS.MaxSpotY, WFS.MaxSpotX];
            float[,] centroidY = new float[WFS.MaxSpotY, WFS.MaxSpotX];
            this.GetSpotCentroids(centroidX, centroidY);

            // print out some centroid positions
            Console.WriteLine("\nCentroid X Positions in pixels (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
                for (int j = 0; j < samplePrintoutSpots; ++j)
                {
                    Console.Write(" " + centroidX[i, j].ToString("F3"));
                }
                Console.WriteLine("");
            }

            Console.WriteLine("\nCentroid Y Positions in pixels (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
                for (int j = 0; j < samplePrintoutSpots; ++j)
                {
                    Console.Write("  " + centroidY[i, j].ToString("F3"));
                }
                Console.WriteLine("");
            }
        }


        /// <summary>
        /// calculate centroid and diameter of the optical beam
        /// you may use this beam data to define a pupil variable in position and size
        /// for WFS20: calculation is based on centroid intensties calculated by WFS_CalcSpotsCentrDiaIntens()
        /// </summary>
        private void CalcBeamCentroid()
        {
            double beamCentroidX, beamCentroidY, beamDiameterX, beamDiameterY;
            this.CalcBeamCentroidDia(out beamCentroidX, out beamCentroidY, out beamDiameterX, out beamDiameterY);

            Console.WriteLine("\nInput beam is measured to:");
            Console.WriteLine("CentroidX = " + beamCentroidX.ToString("F3") + " mm");
            Console.WriteLine("CentroidY = " + beamCentroidY.ToString("F3") + " mm");
            Console.WriteLine("DiameterX = " + beamDiameterX.ToString("F3") + " mm");
            Console.WriteLine("DiameterY = " + beamDiameterY.ToString("F3") + " mm");
        }


        /// <summary>
        /// calculate and printout spot deviations to reference positions
        /// </summary>
        private void CalcSpotDeviations()
        {
            this.CalcSpotToReferenceDeviations(sampleOptionCancelTilt);

            // get spot deviations
            float[,] deviationX = new float[WFS.MaxSpotY, WFS.MaxSpotX];
            float[,] deviationY = new float[WFS.MaxSpotY, WFS.MaxSpotX];
            this.GetSpotDeviations(deviationX, deviationY);

            // print out some spot deviations
            Console.WriteLine("\nSpot Deviation X in pixels (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
                for (int j = 0; j < samplePrintoutSpots; ++j)
                {
                    Console.Write(" " + deviationX[i, j].ToString("F3"));
                }
                Console.WriteLine("");
            }
            Console.WriteLine("\nSpot Deviation Y in pixels (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
                for (int j = 0; j < samplePrintoutSpots; ++j)
                {
                    Console.Write(" " + deviationY[i, j].ToString("F3"));
                }
                Console.WriteLine("");
            }
        }


        /// <summary>
        /// calculate and printout measured wavefront
        /// </summary>
        private void CalcWavefront()
        {
            ConsoleKeyInfo waitKey;
            float[,] wavefront = new float[WFS.MaxSpotY, WFS.MaxSpotX];
            this.CalcWavefront(sampleWavefrontType, sampleOptionLimitToPupil, wavefront);

            // print out some wavefront points
            Console.WriteLine("\nWavefront in microns (first 5x5 elements)\n");
            for (int i = 0; i < samplePrintoutSpots; ++i)
            {
                for (int j = 0; j < samplePrintoutSpots; ++j)
                {
                    Console.Write(" " + wavefront[i, j].ToString("F3"));
                }
                Console.WriteLine("");
            }
            Console.WriteLine("\nPress <ANY_KEY> to proceed...");
            waitKey = Console.ReadKey(true);

            // calculate wavefront statistics within defined pupil
            double wavefrontMin, wavefrontMax, wavefrontDiff, wavefrontMean, wavefrontRms, wavefrontWeightedRms;
            this.CalcWavefrontStatistics(out wavefrontMin, out wavefrontMax, out wavefrontDiff, out wavefrontMean, out wavefrontRms, out wavefrontWeightedRms);
            Console.WriteLine("\nWavefront Statistics in microns:");
            Console.WriteLine("Min          : " + wavefrontMin.ToString("F3"));
            Console.WriteLine("Max          : " + wavefrontMax.ToString("F3"));
            Console.WriteLine("Diff         : " + wavefrontDiff.ToString("F3"));
            Console.WriteLine("Mean         : " + wavefrontMean.ToString("F3"));
            Console.WriteLine("RMS          : " + wavefrontRms.ToString("F3"));
            Console.WriteLine("Weigthed RMS : " + wavefrontWeightedRms.ToString("F3"));
        }


        /// <summary>
        /// calculate Zernike results in microns, related to defined pupil size
        /// </summary>
        private void CalcZernikes()
        {
            Console.WriteLine("\nZernike fit up to order " + sampleZernikeOrders.ToString());
            int zernike_order = sampleZernikeOrders;
            float[] zernikeUm = new float[maxZernikeModes + 1];
            float[] zernikeOrdersRmsUm = new float[maxZernikeModes + 1];
            double rocMm;
            this.ZernikeLsf(out zernike_order, zernikeUm, zernikeOrdersRmsUm, out rocMm); // also calculates deviation from centroid data for wavefront integration

            Console.WriteLine("\nZernike Mode    Coefficient");
            for (int i = 0; i < WFS.ZernikeModes[sampleZernikeOrders]; ++i)
            {
                Console.WriteLine("  " + i.ToString() + "             " + zernikeUm[i].ToString("F3"));
            }
        }


        /// <summary>
        /// enter highspeed mode for WFS10 and WFS20 instruments only
        /// in this mode the camera itself calculates the spot centroids
        /// this enables much faster measurements
        /// </summary>
        private void HighspeedMode(int selectedInstrId)
        {
            ConsoleKeyInfo waitKey;
            if ((0 != (selectedInstrId & WFS.DeviceOffsetWFS10)) ||
                (0 != (selectedInstrId & WFS.DeviceOffsetWFS20))) // for WFS10 or WFS20 instrument only
            {
                Console.Write("\nEnter Highspeed Mode 0/1? ");
                string input = Console.ReadLine();
                int selection;
                bool chk = Int32.TryParse(input.ToString(), out selection);
                if (!chk)
                {
                    throw new Exception("Invalid selection", new Exception(input.ToString()));
                }
                else
                {
                    if ((0 != selection) && (1 != selection))
                    {
                        throw new Exception("Invalid selection", new Exception(selection.ToString()));
                    }
                }

                if (1 == selection)
                {
                    // set highspeed mode active, use pre-defined options, refere to WFS_SetHighspeedMode() function help
                    this.SetHighspeedMode(sampleOptionHighspeed, sampleOptionHsAdaptCentr, sampleHsNoiseLevel, sampleOptionHsAllowAutoexpos);
                    int hsWinCountX, hsWinCountY, hsWinSizeX, hsWinSizeY;
                    int[] hsWinStartX = new int[WFS.MaxSpotX];
                    int[] hsWinStartY = new int[WFS.MaxSpotY];

                    this.GetHighspeedWindows(out hsWinCountX, out hsWinCountY, out hsWinSizeX, out hsWinSizeY, hsWinStartX, hsWinStartY);

                    Console.WriteLine("\nCentroid detection windows are defined as follows:\n"); // refere to WFS_GetHighspeedWindows function help
                    Console.WriteLine("CountX = " + hsWinCountX.ToString() + ", CountY = " + hsWinCountY.ToString());
                    Console.WriteLine("SizeX  = " + hsWinSizeX.ToString() + ", SizeY  = " + hsWinSizeY.ToString());
                    Console.WriteLine("Start coordinates x: ");
                    for (int i = 0; i < hsWinCountX; ++i)
                    {
                        Console.Write(hsWinStartX[i].ToString() + " ");
                    }
                    Console.WriteLine("\n");
                    Console.WriteLine("Start coordinates y: ");
                    for (int i = 0; i < hsWinCountY; ++i)
                    {
                        Console.Write(hsWinStartY[i].ToString() + " ");
                    }
                    Console.WriteLine("\n");

                    Console.WriteLine("\nPress <ANY_KEY> to proceed...");
                    waitKey = Console.ReadKey(false);

                    double exposAct;
                    double masterGainAct;
                    // take a camera image with auto exposure, this is also supported in highspeed-mode
                    this.TakeSpotfieldImageAutoExpos(out exposAct, out masterGainAct);
                    Console.WriteLine("\nexposure = " + exposAct.ToString("F3") + " ms, gain =  " + masterGainAct.ToString("F3") + "\n");

                    double beamCentroidX;
                    double beamCentroidY;
                    double beamDiameterX;
                    double beamDiameterY;
                    // get centroid and diameter of the optical beam, these data are based on the detected centroids
                    this.CalcBeamCentroidDia(out beamCentroidX, out beamCentroidY, out beamDiameterX, out beamDiameterY);
                    Console.WriteLine("\nInput beam is measured to:\n");
                    Console.WriteLine("CentroidX = " + beamCentroidX.ToString("F3") + " mm");
                    Console.WriteLine("CentroidY = " + beamCentroidY.ToString("F3") + " mm");
                    Console.WriteLine("DiameterX = " + beamDiameterX.ToString("F3") + " mm");
                    Console.WriteLine("DiameterY = " + beamDiameterY.ToString("F3") + " mm");

                    Console.WriteLine("\nPress <ANY_KEY> to proceed...");
                    waitKey = Console.ReadKey(false);

                    // Info: calling WFS_CalcSpotsCentrDiaIntens() is not required because the WFS10/WFS20 camera itself already did the calculation

                    float[,] centroidX = new float[WFS.MaxSpotY, WFS.MaxSpotX];
                    float[,] centroidY = new float[WFS.MaxSpotY, WFS.MaxSpotX];
                    // get centroid result arrays
                    this.GetSpotCentroids(centroidX, centroidY);

                    // print out some centroid positions
                    Console.WriteLine("\nCentroid X Positions in pixels (first 5x5 elements)\n");
                    for (int i = 0; i < samplePrintoutSpots; ++i)
                    {
                        for (int j = 0; j < samplePrintoutSpots; ++j)
                        {
                            Console.Write(" " + centroidX[i, j].ToString("F3"));
                        }
                        Console.WriteLine("");
                    }

                    Console.WriteLine("\nCentroid Y Positions in pixels (first 5x5 elements)\n");
                    for (int i = 0; i < samplePrintoutSpots; ++i)
                    {
                        for (int j = 0; j < samplePrintoutSpots; ++j)
                        {
                            Console.Write(" " + centroidY[i, j].ToString("F3"));
                        }
                        Console.WriteLine("");
                    }

                    Console.WriteLine("\nThe following wavefront and Zernike calculations can be done identical to normal mode.\n");
                }
            }
        }
        #endregion Helper Methods
    }
}
