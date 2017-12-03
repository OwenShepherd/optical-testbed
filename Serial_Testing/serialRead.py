"""
Author: Owen Shepherd
Date: 11-30-2017
Program Info: This program is designed to record the rate at which data is sent
from a Teensy over Serial USB to the Computer.  At the same time, the data is
saved automatically by the Python script.  Then the data is tested for errors;
note that the program is designed to check against a Teensy that is sending the
numbers 0-99 iterating, switching back to zero after hitting 99.

CU Boulder Fall 2017
ASEN Senior Projects
SWRI (AWESoMe)
"""


import serial
import time

inputs = ""
Baud = 100 # Baud rate (note on the Teensy this doesn't change Serial rates)
maxLatency = 0


testTime = 12 # Total time you desire to test the Teensy
Port = 'COM3' # USB port ID that the Teensy is plugged in to

# Directory to store the file and the filename are defined here, change for your
# own filesystem
directory = "C:\\Users\\sheph\\Documents\\Arduino\\ASEN-4018-EnvSensors\\Serial_Testing\\"
fName = "Teensy_Test_"+str(testTime)+"_seconds.txt"
fileName = directory+fName
fileName2 = directory+"Teensy_Test_Statistics_"+str(testTime)+"_seconds.txt"

f = open(fileName,'w+')

start = time.time()
end = time.time()
byteCounter = 0


# Here we read the serial port in 1000 byte blocks and writes to the above specified file
with serial.Serial(Port, Baud) as ser:
    while ((end-start)<testTime):
        startLatency = time.time()
        x = ser.read(100)
        endLatency = time.time()
        currTime = endLatency-startLatency
        if (currTime > maxLatency):
            maxLatency = currTime
        f.write(x.decode("utf-8"))
        byteCounter = byteCounter+100
        end = time.time()

f.close
timeElapsed = end-start


# Data rate statistics
print("Time Elapsed: " + str(timeElapsed))
print("Bytes Read: " + str(byteCounter))
print("Data Rate: " + str(byteCounter/timeElapsed/1000) + " kbps")
print("Filename: "+ fName)


# Opening up the file created above
with open(fileName) as f:
    data = f.read()

firstRead = True
firstDigit = "1"
secondDigit = "2"
prevDigit = 12
readFirst = False
readSecond = False
flags = 0
counter = 0
totalCounter = 0


# Iterating over each character in the collected data string
for c in data:

    # If it's the first read, want to make sure the counter is set to the first
    # whole number transferred (sometimes the second half of a number is sent,
    # these cases are ignored)
    if (firstRead):
        if (c==' '):
            readFirst = True
            continue
        elif (readFirst):
            firstDigit = c
            readFirst = False
            readSecond = True
            continue
        elif (readSecond):
            secondDigit = c
            readSecond = False
            firstRead = False
            prevDigit = int(firstDigit+secondDigit)
            counter = prevDigit
            totalCounter = totalCounter + 1

    # This is everything not including the first read.  Keeps track of whether
    # or not the upcoming byte to be read is the first or second digit of the
    # space-separated number.  Also prints some error info if a flag is
    # encountered
    else:
        if (readFirst):
            firstDigit = c
            readFirst = False
            readSecond = True
            continue
        elif (readSecond):
            secondDigit = c
            readSecond = False
            prevDigit = int(firstDigit+secondDigit)
            counter = counter+1
            totalCounter = totalCounter + 1
            if (counter != prevDigit):
                print()
                print(totalCounter)
                print(counter)
                print(prevDigit)
                input("Press Enter to continue...")
                flags = flags + 1
        else:
            readFirst = True

    # Resetting counter.  Setting to -1 works best with the above code setup
    if (counter==99):
        counter = -1

# Printing error info
print("Error Flags: "+ str(flags))
print("Count of Every Third Char: "+ str(totalCounter))

f2 = open(fileName2,'w')
f2.write("Time Elapsed,Bytes Read,Data Rate,Filename,Flags,Char Count")
f2.write("\n" + str(timeElapsed) + ",")
f2.write(str(byteCounter) + ",")
f2.write(str(byteCounter/timeElapsed/1000) + ",")
f2.write(fName+",")
f2.write(str(flags)+",")
f2.write(str(totalCounter)+",")
f2.close()
