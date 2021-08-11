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
 Serial.begin(9600);

 pinMode(CS,OUTPUT);
 pinMode(CS2,OUTPUT);
 SPI.begin();
 SPI.beginTransaction(SPISettings(5000000, MSBFIRST, SPI_MODE3));
}
void loop(void)
{
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

sprintf(readChar,"%d,",readTime);
sprintf(switchChar,"%d,",switchTime);


Serial.write(readChar,20);
Serial.write(switchChar,20);
   //delay(1000);
}
