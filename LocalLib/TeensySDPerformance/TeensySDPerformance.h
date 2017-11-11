#ifndef TEENSYSDPERFORMANCE_H
#define TEENSYSDPERFORMANCE_H

bool sdBusy();

void errorHalt(const char*);

uint32_t kHzSdClk();

void yield();

void runTest();

void perfSetup();

void perfLoop();

#endif
