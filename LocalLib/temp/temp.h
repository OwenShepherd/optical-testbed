#ifndef TEMP_H
#define TEMP_H
#include <Arduino.h>
#include <string>
#include <TimeLib.h>

void tempSetup();

void tempLoop(String);

void printDigits(int);

time_t getTeensy3Time();

unsigned long processSyncMessage();



#endif
