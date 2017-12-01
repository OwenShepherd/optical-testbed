# Data Rate Testing Procedure And Results
## Team: AWESoME (SWRI)

### Required / Used Software
* Python 3.6
* pySerial

### Serial Data Testing
First, we begin with writing some simple code to the Teensy for testing the
performance of the Serial USB connection.  This will simply have the teensy
write the numbers 0-99 repeating to the serial connection.
```
#define MYSERIAL Serial
int i = 0,j=0;

void setup() {
  MYSERIAL.begin(9600);

}

void loop() {

  MYSERIAL.write('0' + j);
  MYSERIAL.write('0' + i);
  MYSERIAL.write(' ');
  //delayMicroseconds(2);
  i++;

  if(i >= 10){ i = 0; j++;};
  if(j >= 10) j = 0;
}
```

Then we must have some code to run for the computer to capture the serial data.
This is done by executing the script "serialRead.py" kept in the
"Serial_Testing" folder.  Note that changes will have to be made in the sections
 of the script detailed below:

First change the port and desired test length here:
```
testTime = 1 # Total time you desire to test the Teensy
Port = 'COM3' # USB port ID that the Teensy is plugged in to
```
Note that changing the baud rate does nothing to change the communication speed,
as the Teensy will always operate at max USB Serial speed.

Then you can edit where python will log the data to by changing the directory
location here:
```
# Directory to store the file and the filename are defined here, change for your
# own filesystem
directory = "C:\\Users\\sheph\\Documents\\Arduino\\ASEN-4018-EnvSensors\\"
fName = "Teensy_Test_"+str(testTime)+"_seconds.txt"
```

After uploading the code to the Teensy and ensuring the COM port is correct and
open, run the python code.  The results are defaulted to save as
"Teensy_Test_XX_Seconds.txt" and "Teensy_Test_Statistics_XX_seconds.txt"; the
former holds the raw data output from the serial communication link, while the
latter holds some statistics about the data transfer.

After the raw file has been created, it is also error-checked to ensure that
the Teensy and Python are indeed capable of correctly handling the speeds
required.
