/********************************************************************/
// First we include the libraries
#include <OneWire.h> 
#include <DallasTemperature.h>
#include <SPI.h>
/********************************************************************/
// Data wire is plugged into pin 2 on the Arduino 
#define ONE_WIRE_BUS 1 
#define registerAddress 0x32
/********************************************************************/
// Setup a oneWire instance to communicate with any OneWire devices  
// (not just Maxim/Dallas temperature ICs) 
OneWire oneWire(ONE_WIRE_BUS); 
/********************************************************************/
// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature sensors(&oneWire);
/********************************************************************/ 
const int CS = 10;
const int CS2 = 7;
byte values[6];
byte values2[6];
float t1;
float t2;


void setup(void) 
{ 
 // start serial port 
 Serial.begin(9600); 
 Serial.println("Dallas Temperature IC Control Library Demo"); 
 // Start up the library 
 sensors.begin(); 
 pinMode(CS,OUTPUT);
 pinMode(CS2,OUTPUT);
 SPI.begin();
 SPI.beginTransaction(SPISettings(5000000, MSBFIRST, SPI_MODE3)); 
} 
void loop(void) 
{ 
 // call sensors.requestTemperatures() to issue a global temperature 
 // request to all devices on the bus 
/********************************************************************/
 Serial.print(" Requesting temperatures..."); 
 sensors.requestTemperatures(); // Send the command to get temperature readings 
 Serial.println("DONE"); 
/********************************************************************/
 Serial.print("Temperature is: "); 
 Serial.print(sensors.getTempCByIndex(0)); // Why "byIndex"?  
   // You can have more than one DS18B20 on the same bus.  
   // 0 refers to the first IC on the wire 

 Serial.println("\nRequesting Accelerometer data");
 char address = 0xC0 | registerAddress;
 digitalWrite(CS,LOW);
 SPI.transfer(address);

 for (int i = 0; i<6; i++) {
  values[i] = SPI.transfer(0x00);
 }
 t1 = micros();
 digitalWrite(CS,HIGH);
// -------------------------------------------------------------Space Where SPI Sensors are Switched---------------------------------------------------
 digitalWrite(CS2,LOW);
 t2 = micros();
 SPI.transfer(address);

 for (int i = 0; i<6; i++) {
  values[i] = SPI.transfer(0x00);
 }
 digitalWrite(CS2,HIGH);


 Serial.print("Time in Microseconds Taken for Teensy to Switch:\n");
 Serial.print(t2-t1);
 
 
  values[0] = values[1]<<8|values[0];
  values[2] = values[3]<<8|values[2];
  values[4] = values[5]<<8|values[4];
  /*
  Serial.print("XRAW ");
  Serial.print(values[0]);
  Serial.print("\n");
  Serial.print("YRAW ");
  Serial.print(values[2]);
  Serial.print("\n");
  Serial.print("ZRAW ");
  Serial.print(values[4]);
  Serial.print("\n");
  

  Serial.print("2nd Accelerometer Data\n");
  values2[0] = values2[1]<<8|values2[0];
  values2[2] = values2[3]<<8|values2[2];
  values2[4] = values2[5]<<8|values2[4];
  /*
  Serial.print("XRAW ");
  Serial.print(values[0]);
  Serial.print("\n");
  Serial.print("YRAW ");
  Serial.print(values[2]);
  Serial.print("\n");
  Serial.print("ZRAW ");
  Serial.print(values[4]);
  Serial.print("\n");
  */

 
   
   delay(1000); 
} 
