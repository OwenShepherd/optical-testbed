# ASEN 4018 - Senior Projects Microcontroller Code
## Team: AWESoME (SWRI)

### Software Used
* Arduino IDE 1.8.5
* SDFat Version 1.0.3
* TeensyDuino Version 1.40
* DallasTemperature Version 3.7.6

### Software Downloads
* [Arduino IDE](https://www.arduino.cc/en/Main/Software)  
* [TeensyDuino](https://www.pjrc.com/teensy/td_download.html)  
* [SDFat Repository](https://github.com/greiman/SdFat)  


### Installation
Download and install the Arduino IDE along with the TeensyDuino
addon.  Then, download SDFat Version 1.0.3 and DallasTemperature using the
library manager in the IDE, and then you can open "ASEN-4018-EnvSensors.ino"
located in the root directory using the Arduino IDE.  

To use the custom libraries made for the project, either
copy the folders in "LocalLib" to the Arduino libraries folder at the root of
your sketches directory or (what I did for easier git integration): create
shortcuts to the folders in "LocalLib" and place those shortcuts in the Arduino
libraries folder.  


### Filesystem Info
Function details (.h) and their implementations (.cpp) are
located in their respective header and implementation files within "LocalLib/".
The current functions provide ADXL reading, SD Performance estimates, and SD
implementations.  

### External Resources
* [Speeding UP SD Writes](https://forum.arduino.cc/index.php?topic=49649.0)

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
