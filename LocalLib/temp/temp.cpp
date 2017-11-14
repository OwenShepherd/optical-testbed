#include "temp.h"
#include <OneWire.h>
#include <DallasTemperature.h>

#define ONE_WIRE_BUS 1


OneWire oneWire(ONE_WIRE_BUS);

DallasTemperature sensors(&oneWire);


void tempSetup() {
  Serial.begin(9600);
  Serial.println("Dallas Temperature IC Control Library Demo");
  sensors.begin();
}
void tempLoop() {
  Serial.print(" Requesting Temperatures... ");
  sensors.requestTemperatures();
  Serial.println("DONE");
  Serial.print(" Temperature is: ");
  Serial.print(sensors.getTempCByIndex(0));
  //delay(1000);
}
