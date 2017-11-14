#include "SdFat.h"
#include "TeensySDWrite.h"


File myFile;
SdFatSdio SD;


void sdSetup() {
  sdInitialize();
}

void sdInitialize() {

  // Open serial communications and wait for port to open:
    Serial.begin(9600);
    while (!Serial) {
      ; // wait for serial port to connect. Needed for native USB port only
    }

    if (!SD.begin()) {
        SD.initErrorHalt("SdFatSdio begin() failed");
      }
      // make sd the current volume.
    SD.chvol();

}

void sdWrite(String data, String FILENAME) {
  // open the file. note that only one file can be open at a time,
  // so you have to close this one before opening another.
  myFile = SD.open(FILENAME, FILE_WRITE);

  // if the file opened okay, write to it:
  if (myFile) {
    //Serial.print("Writing to test.txt...");
    myFile.println(data);
    // close the file:
    myFile.close();
    Serial.println("done.");
  } else {
    // if the file didn't open, print an error:
    Serial.println("error opening");
  }
}
