#include "temp.h"
#include "TeensySDWrite.h"
#include <OneWire.h>
#include <DallasTemperature.h>
#include <TimeLib.h>

#define ONE_WIRE_BUS 1

OneWire oneWire(ONE_WIRE_BUS);

DallasTemperature sensors(&oneWire);

char dataBuff[100];
char timeBuff[100];
int counter = 0;
int retVal = 0;

void tempSetup() {
  setSyncProvider(getTeensy3Time);
  Serial.begin(9600);
  sdSetup();
  sensors.begin();
}
void tempLoop(String filename) {
  if (Serial.available()) {
    time_t t = processSyncMessage();
    if (t != 0) {
      Teensy3Clock.set(t); // set the RTC
      setTime(t);
    }
  }

  sensors.requestTemperatures();


  float tempReading = sensors.getTempCByIndex(0);
  retVal = sprintf(dataBuff,"%d:%d:%d,%d, %f",hour(),minute(),second(),counter,tempReading);
/*
  Serial.print(hour());
  printDigits(minute());
  printDigits(second());
  Serial.print(" ");
  Serial.print(day());
  Serial.print(" ");
  Serial.print(month());
  Serial.print(" ");
  Serial.print(year());
*/
  uint32_t t = micros();
  sdWrite(dataBuff,filename);
  t = micros()-t;
  Serial.printf("Temp Write Time: %d\n",t);
  counter++;
}

time_t getTeensy3Time()
{
  return Teensy3Clock.get();
}

void printDigits(int digits){
  // utility function for digital clock display: prints preceding colon and leading 0
  Serial.print(":");
  if(digits < 10)
    Serial.print('0');
  Serial.print(digits);
}

#define TIME_HEADER  "T"   // Header tag for serial time sync message

unsigned long processSyncMessage() {
  unsigned long pctime = 0L;
  const unsigned long DEFAULT_TIME = 1357041600; // Jan 1 2013

  if(Serial.find(TIME_HEADER)) {
     pctime = Serial.parseInt();
     return pctime;
     if( pctime < DEFAULT_TIME) { // check the value is a valid time (greater than Jan 1 2013)
       pctime = 0L; // return 0 to indicate that the time is not valid
     }
  }
  return pctime;
}
