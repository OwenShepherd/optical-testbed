#include "temp.h"
#include <string>

String filename = "TemperatureReadings7.csv";

void setup() {
  tempSetup();
  
}

void loop() {
  // nothing happens after setup
  tempLoop(filename);
}
