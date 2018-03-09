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
        public string position;
        public string velocity;
        private MotorConfigurations motorSettings;
        private KCubeDCServo currentMotor;





        public ASEN_MotorControl()
        {
            Console.WriteLine("Usage = KDC_Console_net_managed [serial_no] [position: optional (0 - 50)] [velocity: optional (0 - 5)]");
            Console.Write("Serial No.: ");
            serialNo = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Position: ");
            position = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Velocity: ");
            velocity = Console.ReadLine();
            Console.Write("\n");
        }

        public void initializeMotor()
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
            if (device == null)
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
                this.CurrentMotor.Connect(this.serialNo);
            }
            catch (Exception)
            {
                // connection failed
                Console.WriteLine("Failed to open device {0}", this.serialNo);
                Console.ReadKey();
                return;
            }

            // wait for the device settings to initialize
            if (!CurrentMotor.IsSettingsInitialized())
            {
                try
                {
                    CurrentMotor.WaitForSettingsInitialized(5000);
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
            this.motorSettings = device.LoadMotorConfiguration(this.serialNo);

            // Simply retrieves the "motor device settings"
            KCubeDCMotorSettings currentDeviceSettings = this.currentMotor.MotorDeviceSettings as KCubeDCMotorSettings;

            // display info about device

            // Retrieves a device info block
            DeviceInfo deviceInfo = device.GetDeviceInfo();
            Console.WriteLine("Device {0} = {1}", deviceInfo.SerialNumber, deviceInfo.Name);

        }


        static void Main(string[] args)
        {
            
            
            Home_Method1(device);
            // or 
            //Home_Method2(device);

            // This command I'm not really sure about
            bool homed = device.Status.IsHomed;


            // Some other stuff for testing
            Decimal test = device.GetHomingVelocity();
            Decimal currPos = device.Position;

            // if a position is requested
            if (position != 0)
            {
                // update velocity if required using real world methods
                if (velocity != 0)
                {
                    // Retrieves the "velocity parameters" in real world units
                    VelocityParameters velPars = device.GetVelocityParams();

                    // Restricts the velocity allowed to be the user-defined velocity
                    velPars.MaxVelocity = velocity;
                    device.SetVelocityParams(velPars);
                }

                // Moves the device to the desired "real world" position
                Move_Method1(device, position);
                // or
                // Move_Method2(device, position);

                // Retrieves the position actually achieved by the motor
                Decimal newPos = device.Position;
                Console.WriteLine("Device Moved to {0}", newPos);
            }

            device.StopPolling();
            device.Disconnect(true);

            //Console.ReadKey();
        }






        // ------------------------------ Helper Functions ---------------------------------------------------------
        // Simply sets the motor to its "home" position
        public static void Home_Method1(IGenericAdvancedMotor device)
        {
            try
            {
                Console.WriteLine("Homing device");
                device.Home(60000);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to home device");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Device Homed");
        }

        public static void Move_Method1(IGenericAdvancedMotor device, decimal position)
        {
            try
            {
                Console.WriteLine("Moving Device to {0}", position);
                device.MoveTo(position, 5000);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to move to position");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Device Moved");
        }

        private static bool _taskComplete;
        private static ulong _taskID;

        public static void CommandCompleteFunction(ulong taskID)
        {
            if ((_taskID > 0) && (_taskID == taskID))
            {
                _taskComplete = true;
            }
        }

        // Same as Home_Method2, but returns extra information about the current status of the homing request
        public static void Home_Method2(IGenericAdvancedMotor device)
        {
            Console.WriteLine("Homing device");
            _taskComplete = false;
            _taskID = device.Home(CommandCompleteFunction);
            while (!_taskComplete)
            {
                Thread.Sleep(500);
                StatusBase status = device.Status;
                Console.WriteLine("Device Homing {0}", status.Position);

                // will need some timeout functionality;
            }
            Console.WriteLine("Device Homed");
        }

        // Same as Move_Method1, but returns extra information about the current status of the move request
        public static void Move_Method2(IGenericAdvancedMotor device, decimal position)
        {
            Console.WriteLine("Moving Device to {0}", position);
            _taskComplete = false;
            _taskID = device.MoveTo(position, CommandCompleteFunction);
            while (!_taskComplete)
            {
                Thread.Sleep(500);
                StatusBase status = device.Status;
                Console.WriteLine("Device Moving {0}", status.Position);

                // will need some timeout functionality;
            }
            Console.WriteLine("Device Moved");
        }
    }
}
