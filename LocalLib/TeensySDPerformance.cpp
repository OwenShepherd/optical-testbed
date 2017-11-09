#include "SdFat.h"
#include "TeensySDPerformance.h"

// 32 KiB buffer.
const size_t BUF_DIM = 32768;

// 8 MiB file.
const uint32_t FILE_SIZE = 256UL*BUF_DIM;

SdFatSdio sd;

SdFatSdioEX sdEx;

File file;

uint8_t buf[BUF_DIM];

// buffer as uint32_t
uint32_t* buf32 = (uint32_t*)buf;

// Total usec in read/write calls.
uint32_t totalMicros = 0;
// Time in yield() function.
uint32_t yieldMicros = 0;
// Number of yield calls.
uint32_t yieldCalls = 0;
// Max busy time for single yield call.
uint32_t yieldMaxUsec = 0;
// Control access to the two versions of SdFat.
bool useEx = false;




// ---------------------------SD Performance Setup and Loop---------------------
void perfSetup() {
  Serial.begin(9600);
  while (!Serial) {
  }
  Serial.println("SdFatSdioEX uses extended multi-block transfers without DMA.");
  Serial.println("SdFatSdio uses a traditional DMA SDIO implementation.");
  Serial.println("Note the difference is speed and busy yield time.\n");
}


void perfLoop() {
  do {
    delay(10);
  } while (Serial.available() && Serial.read());

  Serial.println("Type '1' for SdFatSdioEX or '2' for SdFatSdio");
  while (!Serial.available()) {
  }
  char c = Serial.read();
  if (c != '1' && c != '2') {
    Serial.println("Invalid input");
    return;
  }
  if (c =='1') {
    useEx = true;
    if (!sdEx.begin()) {
      sd.initErrorHalt("SdFatSdioEX begin() failed");
    }
    // make sdEx the current volume.
    sdEx.chvol();
  } else {
    useEx = false;
    if (!sd.begin()) {
      sd.initErrorHalt("SdFatSdio begin() failed");
    }
    // make sd the current volume.
    sd.chvol();
  }
  runTest();
}


//-----------------------SD Performance Additional Functions--------------------
void runTest() {
  // Zero Stats
  totalMicros = 0;
  yieldMicros = 0;
  yieldCalls = 0;
  yieldMaxUsec = 0;
  if (!file.open("TeensyDemo.bin", O_RDWR | O_CREAT)) {
    errorHalt("open failed");
  }
  Serial.println("\nsize,write,read");
  Serial.println("bytes,KB/sec,KB/sec");
  for (size_t nb = 512; nb <= BUF_DIM; nb *= 2) {
    file.truncate(0);
    uint32_t nRdWr = FILE_SIZE/nb;
    Serial.print(nb);
    Serial.print(',');
    uint32_t t = micros();
    for (uint32_t n = 0; n < nRdWr; n++) {
      // Set start and end of buffer.
      buf32[0] = n;
      buf32[nb/4 - 1] = n;
      if (nb != file.write(buf, nb)) {
        errorHalt("write failed");
      }
    }
    t = micros() - t;
    totalMicros += t;
    Serial.print(1000.0*FILE_SIZE/t);
    Serial.print(',');
    file.rewind();
    t = micros();

    for (uint32_t n = 0; n < nRdWr; n++) {
      if ((int)nb != file.read(buf, nb)) {
        errorHalt("read failed");
      }
      // crude check of data.
      if (buf32[0] != n || buf32[nb/4 - 1] != n) {
        errorHalt("data check");
      }
    }
    t = micros() - t;
    totalMicros += t;
    Serial.println(1000.0*FILE_SIZE/t);
  }
  file.close();
  Serial.print("\ntotalMicros  ");
  Serial.println(totalMicros);
  Serial.print("yieldMicros  ");
  Serial.println(yieldMicros);
  Serial.print("yieldCalls   ");
  Serial.println(yieldCalls);
  Serial.print("yieldMaxUsec ");
  Serial.println(yieldMaxUsec);
  Serial.print("kHzSdClk     ");
  Serial.println(kHzSdClk());
  Serial.println("Done");
}


void yield() {
  // Only count cardBusy time.
  if (!sdBusy()) {
    return;
  }
  uint32_t m = micros();
  yieldCalls++;
  while (sdBusy()) {
    // Do something here.
  }
  m = micros() - m;
  if (m > yieldMaxUsec) {
    yieldMaxUsec = m;
  }
  yieldMicros += m;
}


uint32_t kHzSdClk() {
  return useEx ? sdEx.card()->kHzSdClk() : sd.card()->kHzSdClk();
}


void errorHalt(const char* msg) {
  if (useEx) {
    sdEx.errorHalt(msg);
  } else {
    sd.errorHalt(msg);
  }
}

void sdBusy() {
  return useEx ? sdEx.card()->isBusy() : sd.card()->isBusy();
}
