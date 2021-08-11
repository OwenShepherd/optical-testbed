/*
  SD card read/write

 This example shows how to read and write data to and from an SD card file
 The circuit:
  * SD card attached to SPI bus as follows:
 ** MOSI - pin 11, pin 7 on Teensy with audio board
 ** MISO - pin 12
 ** CLK - pin 13, pin 14 on Teensy with audio board
 ** CS - pin 4, pin 10 on Teensy with audio board

 created   Nov 2010
 by David A. Mellis
 modified 9 Apr 2012
 by Tom Igoe
 modified 9 Nov 2017

 This example code is in the public domain.

 */

#ifndef TEENSYSDWRITE_H
#define TEENSYSDWRITE_H
#include <Arduino.h>
#include <string>

//void sdSetup();

void sdInitialize(String);

void sdWrite(String, bool);

#endif
