# AWESoMe Automation
Team: **A**pparatus for **W**avefront **E**rror **S**ens**o**r **Me**asurement

Website: [AWESoMe](https://www.colorado.edu/aerospace/current-students/undergraduates/senior-design-projects/past-senior-projects/2017-2018/apparatus)  

![](images/awesome_logo_whitebackground.png?raw=true)

AWESoMe is a joint project between the South West Research Institute (SWRI) and a team of Aerospace Engineering students at Boulder.  The goal of the project is to create a testbed for comparing the performance of a [Shack-Hartmann Array](https://en.wikipedia.org/wiki/Shack%E2%80%93Hartmann_wavefront_sensor) (SHA) and a [Roddier Curvature Wavefront Sensor](http://www.soft.belastro.net/files/tmp/Wave-front_reconstruction_from_defocused_images_and_the_testing_of_ground-based_optical_telescopes.pdf) (RCWS).  

This repository contains .NET programs aimed at automating as much of the testbed as possible due to the large number of tests that need to be run in a timely manner.  This mostly includes: prompting the user to load in a specifically formatted CSV for scheduling tests, and then running those tests one-by-one by adjusting the parameters of the SHA, RCWS, and motorized stages.

## Getting Started
Some third-party drivers and Visual Studio 15 need to be installed.

### Pre-Requisites  
**Please download drivers as 32-bit or it may cause some compatibility issues**  

First, ensure you have at least Visual Studio 15 installed (or at least 10.0.40219.1).  Then, install the following drivers in the listed order:
- [ASI120MM Drivers](http://astronomy-imaging-camera.com/software/)
- [ASCOM Platform 6.3](https://ascom-standards.org/Downloads/Index.htm)
- [ASCOM Platform Developer Components](https://ascom-standards.org/Downloads/PlatDevComponents.htm)
- [ASI Cameras ASCOM Driver](http://astronomy-imaging-camera.com/software/)
- [QHY Camera Driver](http://www.qhyccd.com/QHY5-III-Camera.html)
- [QHY ASCOM Driver (Capture Version V0131)](http://www.qhyccd.com/ASCOM-Camera.html)

Next, in order to use the complete version of the program, the most recent version of Python (At least Python version 3.6) must be installed:
- [Python Downloads](https://www.python.org/downloads/)

Then, install pyserial for Python 3 in any fashion (reccomended to use pip); just ensure that pyserial can be called from a Python 3 program:
- [Pyserial](https://pypi.python.org/pypi/pyserial)

Next, the program requires extra DLLs in order to run properly.  These must be acquired by also installing the following software:
- [Thorlabs Kinesis (32-bit for 32-bit or 32-bit for 64-bit](https://www.thorlabs.com/software_pages/ViewSoftwarePage.cfm?Code=Motion_Control&viewtab=0)
- [Thorlabs WFS](https://www.thorlabs.com/software_pages/ViewSoftwarePage.cfm?Code=WFS)

Lastly, it is possible that the software will initially have issues connecting to the QHY due to a defect in the driver.  In this event, it is required to use sharpcap to connect to the QHY via ASCOM, and simply set the gain to, say, 1, and click accept.  It is possible that some error warnings will pop up, but ignore these.  Try running your experiment again and see if an error still pops up about some camera connection issues.
- [Sharpcap](https://www.sharpcap.co.uk/sharpcap/downloads)

## Running the Program
As of now, the program has been tested with all components of the testbed and is working in its designed fashion for data collection.  The most recent working version of the program is contained in the "TestBed_Automation_Final" Visual Studio Project.  The installer for the project builds may be found at [this link](https://drive.google.com/open?id=1Bk6WBR5vOVbcDDSYLsn6n1ccp4iNH_cr) (NOTE: Only CU Boulder Accounts may access this link).

### Using the Executable
The Visual Studio Project will automatically add the required files for the executable to the appropriate directory.  But, when running the executable outside of visual studio, ensure that the following are in the same directory as the EXE:
- ASCOM.DeviceInterfaces.dll
- ASCOM.DriverAccess.dll
- ASCOM.Exceptions.dll
- ASCOM.Utilities.dll
- SerialReader.py
- Thorlabs.MotionControl.DeviceManager.dll
- Thorlabs.MotionControl.DeviceManagerCLI.dll
- Thorlabs.MotionControl.GenericMotorCLI.dll
- Thorlabs.MotionControl.KCube.DCServo.dll
- Thorlabs.MotionControl.KCube.DCServoCLI.dll
- Thorlabs.MotionControl.Tools.Common.dll
- Thorlabs.MotionControl.Tools.Logging.dll
- Throlabs.WFS.Interop.dll

An official "release" hasn't been made yet for this program, although running the program in Visual Studio will achieve the desired results.

## Data Organization
This is how data is organized by the program.

### Filesystem Organization
Here is how data will be organized.  An experiment corresponds to a collection of data
taken during one session.  Each state corresponds to a different test (i.e. with
different de-focus distances, exposures, etc...).
```
EXAMPLE_EXPERIMENT
├── 2018_02_03_13_48_41_STATE1
├── 2018_02_03_13_50_55_STATE2
├── 2018_02_03_13_52_13_STATE3
├── 2018_02_03_13_54_32_STATE4
│   ├── data_RCWS
│   │   ├── img_RCWS_aft.png
│   │   ├── img_RCWS_fore.png
│   │   └── zernikes_RCWS.csv
│   ├── data_SHA
│   │   ├── img_SHA_aft.png
│   │   ├── img_SHA_fore.png
│   │   ├── spt_SHA_aft.png
│   │   ├── spt_SHA_fore.png
│   │   ├── wft_SHA_aft.png
│   │   ├── wft_SHA_fore.png
│   │   └── zernikes_SHA.csv
│   ├── env_data.csv
│   ├── state_parameters.csv
│   └── zernikes_model.csv
└── experiment_schedule.csv
```
The automation program will automatically create the experiment directory (as named off user input), and will automatically create the state folders based on the number of tests in the schedule file.  Each state folder will be filled with the appropriate data shown above as the tests are run.

### Schedule File Structure
The experiment_schedule.csv file will specify all the system states that are to be tested for a given experiment. For each state specified in this file the automated test control program will create the appropriate sub-folder of the test and populate it with the data that is produced while the experiment is running. In post-processing other files can be added in the created file structure. The first row will include the header:  
```
RCWS EXPT (us), SHA EXPT (us), RCWS D FORE (um), RCWS D AFT (um), M2 A X (arc sec), M2 A Y (arc sec)  
```
After that line every row of data will specify those state values at which to make a wavefront measurement with both sensors.

### Environmental Sensor Data
Data is collected from the environmental sensors and sent in an organized way to the computer.  Each set of data sent contains at most: 3-Dimensional readings from 6 accelerometers, and temperature readings from 6 IC temperature sensors.
```
| 0xA0 | 0xA1 | RT | SN | {PL} | SH | SL | MH | ML | UH | UL | CS | 0x0D | 0x0A |
```
These bytes / sets of bytes contain the following data:
- RT: (0 or 1) indicates whether the payload contains accelerometer or temperature data.
- SN: Indicates the sensor number / sensor ID
- PL: The actual sensor data; details below
- SH: MSB for seconds elapsed
- SL: LSB for seconds elapsed
- MH: MSB for milliseconds elapsed
- ML: LSB for milliseconds elapsed
- UH: MSB for microseconds elapsed
- UL: LSB for microseconds elapsed
- CS: Result of taking XOR of all payload and timestamp bytes.  AKA: A rudimentary checksum.

**Payload Organization**:  
Temperature Sensor:
```
| Temperature High Byte | Temperature Low Byte |
```

Accelerometer:
```
| Acc. Axis 1 H | Acc. Axis 1 L | Acc. Axis 2 H | Acc. Axis 2 L | Acc. Axis 3 H | Acc. Axis 3 L |
```



## Conceptual Information

### ASCOM References
The two RCWS cameras (ASI120MM & QHY...) both interface with Windows using ASCOM.  Some
references on how to use ASCOM with VS:
- [ASCOM Standards](http://www.ascom-standards.org/Help/Developer/html/7d9253c2-fdfd-4c0d-8225-a96bddb49731.htm)

### ASEN_RCWS class
The ASEN_RCWS class can be used to take images and save them in a very simple
way.  Currently, it has only been tested with the ASI, but should be able to
easily extend to the QHY if necessary.  To use the class, follow the current
example shown in the "program.cs" file for using the ASI120MM.

### ASEN_KDC Class
In order for the current test code for the KDC101 to work, you must do the
following.  Currently the class is not ready as further testing needs to be
completed.

Add these references to the program (if they are not already included):
- Thorlabs.MotionControl.KCube.DCServoCLI.dll
- Thorlabs.MotionControl.DeviceManagerCLI.dll
- Thorlabs.MotionControl.GenericMotorCLI.dll

In addition, the following native C DLLs need to be copied to the executable folder:
- ThorLabs.MotionControl.KCube.DCServo.dll
- ThorLabs.MotionControl.DeviceManager.dll

These DLL files may be found in the References_DLL/KDC101 folder.



## Authors (This Repository)
- Owen Shepherd
- Jake Crouse


## The Team
- Robert Belter
- Ali Colic
- Jake Crouse
- Lucas Droste
- Diego Gomes
- Ankit Hriday
- Owen Lyke
- Brandon Noirot
- Owen Shepherd
- Brandon Stetler

## Acknowledgements
- Eliot Young (SWRI)
- Bob Woodruff (SWRI)
- Engineering Excellence Fund (EEF)
- NASA Glenn


## ASEN 4018 - Senior Projects Environmental Sensors Code
This code was used to test the following devices and conduct experiments with
the following software as well as accelerometers and thermometers.

### Software Used
* Arduino IDE 1.8.5
* SDFat Version 1.0.3
* TeensyDuino Version 1.40
* DallasTemperature Version 3.7.6
* Python 3.6.2
* pySerial 3.4

### Software Downloads
* [Arduino IDE](https://www.arduino.cc/en/Main/Software)  
* [TeensyDuino](https://www.pjrc.com/teensy/td_download.html)  
* [SDFat Repository](https://github.com/greiman/SdFat)
* [pySerial](https://pypi.python.org/pypi/pyserial)
* [Python](https://www.python.org/downloads/)


### Installation
Download and install the Arduino IDE along with the TeensyDuino
addon.  Then, download SDFat Version 1.0.3 and DallasTemperature using the
library manager in the IDE, and then you can open "ASEN-4018-EnvSensors.ino"
located in the root directory using the Arduino IDE.  

To use the custom libraries made for the project, either
copy the folders in "LocalLib" to the Arduino libraries folder at the root of
your sketches directory or (what I did for easier git integration): create
symbolic links to the folders in "LocalLib" and place those shortcuts in the Arduino
libraries folder.  NOTE: This was mostly developed in Ubuntu; Windows users should 
note that shortcuts != symoblic links.  I have not tested development on a Apple
computer.


### Filesystem Info
Function details (.h) and their implementations (.cpp) are
located in their respective header and implementation files within "LocalLib/".
The current functions provide ADXL reading, SD Performance estimates, and SD
implementations.  

### Testing information
Please read "RateTesting.md" in the root of the git directory for information
about how to test the Teensy for various data rate performances.

### Examples Used to Develop Code
* [Speeding UP SD Writes](https://forum.arduino.cc/index.php?topic=49649.0)
* [Low-Latency Logger Example](https://github.com/greiman/SdFat/tree/master/examples/LowLatencyLogger)
* [Using an ADXL345 with the Teensy](https://gist.github.com/FuzzyWuzzie/b1903a8353dc1ec57da8)
* [Using DS18B20 Temp. Sensor with Arduino](https://create.arduino.cc/projecthub/TheGadgetBoy/ds18b20-digital-temperature-sensor-and-arduino-9cc806)

### Examples
To test Teensy SD read / write performance:
```
#include "TeensySDPerformance.h"

void setup() {
  perfSetup();
}

void loop() {
  perfLoop();
}  
```

To read temperature from pin 1 on the Teensy and save it to the SD card:
```
#include "temp.h"
#include <string>

String filename = "TemperatureReadings.csv";

void setup() {
  tempSetup();

}

void loop() {
  tempLoop(filename);
}
```
