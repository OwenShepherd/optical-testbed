#include "temp.h"
#include "TeensySDWrite.h"
#include <OneWire.h>
#include <DallasTemperature.h>

#define ONE_WIRE_BUS 1

OneWire oneWire(ONE_WIRE_BUS);

DallasTemperature sensors(&oneWire);

char dataBuff[100];
char timeBuff[100];
int counter = 0;
int retVal = 0;

void tempSetup() {
  Serial.begin(9600);
  Serial.println("Dallas Temperature IC Control Library Demo");
  sdSetup();
  sensors.begin();
}
void tempLoop(String filename) {
  Serial.print(" Requesting Temperatures... ");
  sensors.requestTemperatures();
  Serial.println("DONE");
  Serial.print(" Temperature is: ");


  float tempReading = sensors.getTempCByIndex(0);
  retVal = sprintf(dataBuff,"%d, %f",counter,tempReading);
  Serial.println(dataBuff);
  sdWrite(String(dataBuff),filename);

}
