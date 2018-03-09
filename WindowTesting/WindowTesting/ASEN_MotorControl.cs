using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Thorlabs.MotionControl.DeviceManagerCLI;
using Thorlabs.MotionControl.GenericMotorCLI;
using Thorlabs.MotionControl.GenericMotorCLI.ControlParameters;
using Thorlabs.MotionControl.GenericMotorCLI.AdvancedMotor;
using Thorlabs.MotionControl.GenericMotorCLI.KCubeMotor;
using Thorlabs.MotionControl.GenericMotorCLI.Settings;
using Thorlabs.MotionControl.KCube.DCServoCLI;

namespace ASEN
{
    class ASEN_MotorControl
    {
        public string serialNo;
        public int position;
        public int velocity;
        private KCubeDCServo currentMotor;





        public ASEN_MotorControl()
        {
            Console.WriteLine("Usage = KDC_Console_net_managed [serial_no] [position: optional (0 - 50)] [velocity: optional (0 - 5)]");
            Console.Write("Serial No.: ");
            serialNo = Console.ReadLine();
            Console.Write("\n");
            Console.Write("\n");
            Console.Write("Velocity: ");
            velocity = Int32.Parse(Console.ReadLine());
            Console.Write("\n");
        }

        public void InitializeMotor()
        {
            try
            {
                // Tell the device manager to get the list of all devices connected to the computer
                DeviceManagerCLI.BuildDeviceList();
            }
            catch (Exception ex)
            {
                // an error occurred - see ex for details
                Console.WriteLine("Exception raised by BuildDeviceList {0}", ex);
                Console.ReadKey();
                return;
            }

            // get available KCube DC Servos and check our serial number is correct
            List<string> serialNumbers = DeviceManagerCLI.GetDeviceList(KCubeDCServo.DevicePrefix);
            if (!serialNumbers.Contains(this.serialNo))
            {
                // the requested serial number is not a KDC101 or is not connected
                Console.WriteLine("{0} is not a valid serial number", this.serialNo);
                Console.ReadKey();
                return;
            }

            // Have to create the motor device based on the serial number (assuming the serial number is accurate)
            this.CreateDevice();

            // Have to open a connection to the device to begin using it
            this.OpenConnection();

            // Collect the motor settings to display some information
            this.CreateConfigs();
        }

        private void CreateDevice()
        {
            // create the device
            this.currentMotor = KCubeDCServo.CreateKCubeDCServo(this.serialNo);
            if (this.currentMotor == null)
            {
                // an error occured
                Console.WriteLine("{0} is not a KCubeDCServo", this.serialNo);
                Console.ReadKey();
                return;
            }
        }

        private void OpenConnection()
        {
            // open a connection to the device.
            try
            {
                Console.WriteLine("Opening device {0}", this.serialNo);
                this.currentMotor.Connect(this.serialNo);
            }
            catch (Exception)
            {
                // connection failed
                Console.WriteLine("Failed to open device {0}", this.serialNo);
                Console.ReadKey();
                return;
            }

            // wait for the device settings to initialize
            if (!this.currentMotor.IsSettingsInitialized())
            {
                try
                {
                    this.currentMotor.WaitForSettingsInitialized(5000);
                }
                catch (Exception)
                {
                    Console.WriteLine("Settings failed to initialize");
                }
            }
        }

        private void CreateConfigs()
        {
            // start the device polling.
            // Polling requests a status update every specified number of milliseconds.
            this.currentMotor.StartPolling(250);

            // needs a delay so that the current enabled state can be obtained
            // ????
            Thread.Sleep(500);

            // enable the channel otherwise any move is ignored 
            this.currentMotor.EnableDevice();

            // needs a delay to give time for the device to be enabled
            Thread.Sleep(500);

            // call GetMotorConfiguration on the device to initialize the DeviceUnitConverter object required for real world unit parameters
            // Sets up proper unit conversion for the correct device.  Only call this function ONCE
            MotorConfiguration motorSettings = this.currentMotor.LoadMotorConfiguration(this.serialNo);

            // Simply retrieves the "motor device settings"
            KCubeDCMotorSettings currentDeviceSettings = this.currentMotor.MotorDeviceSettings as KCubeDCMotorSettings;

            // display info about device

            // Retrieves a device info block
            DeviceInfo deviceInfo = this.currentMotor.GetDeviceInfo();
            Console.WriteLine("Device {0} = {1}", deviceInfo.SerialNumber, deviceInfo.Name);

            // After the device is opened we want to save the velocity
            // Retrieves the "velocity parameters" in real world units
            VelocityParameters velPars = this.currentMotor.GetVelocityParams();

            // Restricts the velocity allowed to be the user-defined velocity
            decimal dVelocity = this.velocity;
            velPars.MaxVelocity = dVelocity;
            this.currentMotor.SetVelocityParams(velPars);

        }

        private decimal ConvertToDeviceUnits(decimal position)
        {
            // Need to extract proper conversion from the device itself
            DeviceUnitConverter currConverter;
            currConverter = currentMotor.UnitConverter;

            // Enuming stuff
            
            // Now we shall do the actual conversion
            return currConverter.RealToDeviceUnit(position, DeviceUnitConverter.UnitType.Length);
        }

        public void HomeMotor()
        {
            try
            {
                Console.WriteLine("Homing device");
                this.currentMotor.Home(60000);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to home device");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Device Homed");
        }

        public void MoveMotor(decimal position)
        {
            decimal devicePosition = ConvertToDeviceUnits(position);

            try
            {
                Console.WriteLine("Moving Device to {0}", position);
                this.currentMotor.MoveTo(devicePosition, 5000);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to move to position");
                Console.ReadKey();
                return;
            }
            Decimal newPos = this.currentMotor.Position;
            Console.WriteLine("Device Moved to {0}", newPos);
        }

        public void DisconnectMotor()
        {
            this.currentMotor.StopPolling();
            this.currentMotor.Disconnect(true);
        }       
    }
}
