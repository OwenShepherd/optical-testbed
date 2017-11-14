#include "temp.h"
#include <string>

String filename = "TemperatureReadings8.csv";

void setup() {
  tempSetup(filename);
  
}

void loop() {
  // nothing happens after setup
  tempLoop();
}
