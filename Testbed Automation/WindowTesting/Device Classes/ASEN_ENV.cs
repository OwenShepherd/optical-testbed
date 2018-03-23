using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

namespace ASEN
{
    class ASEN_ENV
    {
        private bool READ;
        private FileStream saveFile;
        private SerialPort teensy;
        private byte[] readData = new byte[1];
        public string path;

        // Let's think of some things that we want...
        // Should be a relatively simple class
        public ASEN_ENV(string COMport, string directory)
        {
            int baud = 9600; // We can change this to anything.  Check out error rates and such for the Teensy 3.6 here: https://www.pjrc.com/teensy/td_uart.html
            this.teensy = new SerialPort(COMport,baud);
            this.teensy.Open();

            // Appned the correct env csv path to the directory
            this.path = directory + "\\env_data.csv";
        }

        // I need to set read separately from everything else for parallelization
        public void PrepRead()
        {
            this.READ = true;
        }

        public void EndRead()
        {
            this.READ = false;
        }


        public void StartListening()
        {
            // Subscribe to event and open serial port
            this.teensy.DataReceived += new SerialDataReceivedEventHandler(Serial_DataReceived);
            this.teensy.Open();
        }

        public void StopListening()
        {
            this.teensy.Close();
        }

        void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int dataLength = this.teensy.BytesToRead;
            byte[] data = new byte[dataLength];
            int nbrDataRead = this.teensy.Read(data, 0, dataLength);
            if (nbrDataRead == 0)
                return;

            File.WriteAllBytes(path, data);
        }


        
        public void BeginTeensyRead(ref int dataCount)
        {

            this.teensy.DataReceived += (sender, e) =>
            {
                if (e.EventType == SerialData.Chars)
                {

                }
            };
            File.WriteAllBytes(path, readData);

            //return dataCount;
        }


        


        
    }
}
