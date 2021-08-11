#ifndef ADXL_H
#define ADXL_H

// Creating an accelerometer struct for storing acceleration readings
struct ACCELS {
  double xg;
  double yg;
  double zg;
};


// ADXL Functions ---------------------------------------------------
ACCELS ADXLRead();

void ADXLSetup();

void ADXLLoop();

int16_t tenBitTwosComplementToDecimal(uint16_t);

void writeRegister(char, unsigned char);

void readRegister(char, int, unsigned char);

#endif
