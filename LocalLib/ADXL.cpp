//Add the SPI library so we can communicate with the ADXL345 sensor
#include "ADXL.h"


ACCELS ADXLRead () {

  //Reading 6 bytes of data starting at register DATAX0 will retrieve the x,y and z acceleration values from the ADXL345.
  //The results of the read operation will get stored to the values[] buffer.
  readRegister(DATAX0, 6, values);

  //The ADXL345 gives 10-bit acceleration values, but they are stored as bytes (8-bits). To get the full value, two bytes must be combined for each axis.
  //The X value is stored in values[0] and values[1].
  x = tenBitTwosComplementToDecimal((((uint16_t)values[1]<<8)|(uint16_t)values[0]) & 1023);
  //The Y value is stored in values[2] and values[3].
  y = tenBitTwosComplementToDecimal((((uint16_t)values[3]<<8)|(uint16_t)values[2]) & 1023);
  //The Z value is stored in values[4] and values[5].
  z = tenBitTwosComplementToDecimal((((uint16_t)values[5]<<8)|(uint16_t)values[4]) & 1023);

  //Convert the accelerometer value to G's.
  //With 10 bits measuring over a +/-4g range we can find how to convert by using the equation:
  // Gs = Measurement Value * (G-range/(2^10)) or Gs = Measurement Value * (8/1024)
  double xg = x * 0.00390625;
  double yg = y * 0.00390625;
  double zg = z * 0.00390625;

  ACCELS gReadings = {xg, yg, zg};

  return ACCELS;
}


void ADXLSetup() {
  //Initiate an SPI communication instance.
  SPI.begin();
  //Configure the SPI connection for the ADXL345.
  SPI.setDataMode(SPI_MODE3);
  //Create a serial connection to display the data on the terminal.
  Serial.begin(9600);

  //Set up the Chip Select pin to be an output from the Arduino.
  pinMode(CS, OUTPUT);
  //Before communication starts, the Chip Select pin needs to be set high.
  digitalWrite(CS, HIGH);

  //Put the ADXL345 into +/- 2G range by writing the value 0x00 to the DATA_FORMAT register.
  writeRegister(DATA_FORMAT, 0x00);

  // disable interrupts
  writeRegister(INT_ENABLE, 0x00);

  //Put the ADXL345 into Measurement Mode by writing 0x08 to the POWER_CTL register.
  writeRegister(POWER_CTL, 0x08); //Measurement mode
  readRegister(INT_SOURCE, 1, values); //Clear the interrupts from the INT_SOURCE register.
}


int16_t tenBitTwosComplementToDecimal(uint16_t x)
{
  boolean negative = (x & (1 << 9)) != 0;
  if(negative)
    return x | ~((1 << 10) - 1);
   return (int16_t)x;
}

void ADXLLoop () {
  // Read accelerations from specified register
  ACCELS gReadings = ADXLRead();

  Serial.print((float)gReadings.xg,2);
  Serial.print("g,");
  Serial.print((float)gReadings.yg,2);
  Serial.print("g,");
  Serial.print((float)gReadings.zg,2);
  Serial.println("g");
  /*for(unsigned char i = 0; i < 6; i++)
  {
    Serial.print(values[i]);
    Serial.print(",");
  }
  Serial.println();*/
  delay(100);
}

//This function will write a value to a register on the ADXL345.
//Parameters:
//  char registerAddress - The register to write a value to
//  char value - The value to be written to the specified register.
void writeRegister(char registerAddress, unsigned char value){
  //Set Chip Select pin low to signal the beginning of an SPI packet.
  digitalWrite(CS, LOW);
  //Transfer the register address over SPI.
  SPI.transfer(registerAddress);
  //Transfer the desired register value over SPI.
  SPI.transfer(value);
  //Set the Chip Select pin high to signal the end of an SPI packet.
  digitalWrite(CS, HIGH);
}



//This function will read a certain number of registers starting from a specified address and store their values in a buffer.
//Parameters:
//  char registerAddress - The register addresse to start the read sequence from.
//  int numBytes - The number of registers that should be read.
//  char * values - A pointer to a buffer where the results of the operation should be stored.
void readRegister(char registerAddress, int numBytes, unsigned char * values){
  //Since we're performing a read operation, the most significant bit of the register address should be set.
  char address = 0x80 | registerAddress;
  //If we're doing a multi-byte read, bit 6 needs to be set as well.
  if(numBytes > 1)address = address | 0x40;

  //Set the Chip select pin low to start an SPI packet.
  digitalWrite(CS, LOW);
  //Transfer the starting register address that needs to be read.
  SPI.transfer(address);
  //Continue to read registers until we've read the number specified, storing the results to the input buffer.
  for(int i=0; i<numBytes; i++){
    values[i] = SPI.transfer(0x00);
  }
  //Set the Chips Select pin high to end the SPI packet.
  digitalWrite(CS, HIGH);
}
