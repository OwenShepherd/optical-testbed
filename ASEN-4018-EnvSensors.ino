#include "temp.h"
#include <string>

String filename = "TemperatureReadings5.csv";

void setup() {
  tempSetup();
  
}

void loop() {
  // nothing happens after setup
  tempLoop(filename);
}
