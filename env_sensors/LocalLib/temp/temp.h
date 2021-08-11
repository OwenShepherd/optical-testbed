#ifndef TEMP_H
#define TEMP_H
#include <Arduino.h>
#include <string>
#include <TimeLib.h>

void tempSetup(String);

void tempLoop();

void printDigits(int);

time_t getTeensy3Time();

unsigned long processSyncMessage();



#endif
