/********************************************************************/
// First we include the libraries
#include <OneWire.h> 
#include <DallasTemperature.h>
#include <SPI.h>
#include <string.h>
/********************************************************************/
// Data wire is plugged into pin 2 on the Arduino 
#define ONE_WIRE_BUS 1 
#define registerAddress 0x32
/********************************************************************/
// Setup a oneWire instance to communicate with any OneWire devices  
// (not just Maxim/Dallas temperature ICs) 
//OneWire oneWire(ONE_WIRE_BUS); 
/********************************************************************/
// Pass our oneWire reference to Dallas Temperature. 
//DallasTemperature sensors(&oneWire);
/********************************************************************/ 
const int CS = 10;
const int CS2 = 17;
char readChar[20];
char switchChar[20];
byte values[6];
byte values2[6];
int readTime1;
int readTime2;
int switchTime1;
int switchTime2;
int readTime;
int switchTime;


void setup(void) 
{ 
 // start serial port 
 Serial.begin(9600); 
 //Serial.println("Dallas Temperature IC Control Library Demo"); 
 // Start up the library 
 //sensors.begin(); 
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
 //Serial.print(" Requesting temperatures..."); 
 //readTime1 = micros();
 //sensors.requestTemperatures(); // Send the command to get temperature readings 
 //readTime2 = micros();
 //Serial.println("DONE"); 
/********************************************************************/
 //Serial.print("Temperature is: "); 
 //switchTime1 = micros();
 //sensors.getTempCByIndex(0); // Why "byIndex"?  
 //switchTime2 = micros();
   // You can have more than one DS18B20 on the same bus.  
   // 0 refers to the first IC on the wire 

 //Serial.println("\nRequesting Accelerometer data");
 char address = 0xC0 | registerAddress;
 readTime1 = micros();
 digitalWrite(CS,LOW);
 SPI.transfer(address);

 for (int i = 0; i<6; i++) {
  values[i] = SPI.transfer(0x00);
 }
 readTime2 = micros();
 switchTime1 = micros();
 digitalWrite(CS,HIGH);
// -------------------------------------------------------------Space Where SPI Sensors are Switched---------------------------------------------------
 digitalWrite(CS2,LOW);
 switchTime2 = micros();
 
 SPI.transfer(address);

 for (int i = 0; i<6; i++) {
  values[i] = SPI.transfer(0x00);
 }
 digitalWrite(CS2,HIGH);

 readTime = readTime2-readTime1;
 switchTime = switchTime2-switchTime1;
 //Serial.print("Time in Microseconds Taken for Teensy to Switch:\n");
 //float timeResult = t2-t1;
 //sprintf(readChar,"%d,",readTime);
 //sprintf(switchChar,"%d,",switchTime);
 //Serial.print(readChar);
 //Serial.println((readTime2-readTime1)+(switchTime2-switchTime1));
 
 /*
  values[0] = values[1]<<8|values[0];
  values[2] = values[3]<<8|values[2];
  values[4] = values[5]<<8|values[4];
  
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
  /*
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

 
   //delay(1);
   sprintf(readChar,"%d,",readTime);
   sprintf(switchChar,"%d,",switchTime);
  
  
   Serial.write(readChar,20);
   Serial.write(switchChar,20); 
   //delay(1000);
} 
