import serial
import time
import sys

inputs = ""
Baud = 9600 # Baud rate (note on the Teensy this doesn't change Serial rates)
maxLatency = 0

Port = sys.argv[2] # USB port ID that the Teensy is plugged in to

# Directory to store the file and the filename are defined here, change for your
# own filesystem
directory = sys.argv[1]
fname = "env_data.csv"
maxLatency = 0

fileName = directory + fname

byteCounter = 0

# Here we read the serial port in 1000 byte blocks and writes to the above specified file
with open(fileName,"w+") as f:
    with serial.Serial(Port, Baud) as ser:
        bytesToRead = ser.inWaiting()
        startLatency = time.time()
        x = ser.read(100)
        endLatency = time.time()
        currTime = endLatency-startLatency
        if (currTime > maxLatency):
            maxLatency = currTime
        f.write(x.decode("utf-8"))
        byteCounter = byteCounter+100
        end = time.time()



"""
# Data rate statistics
print "Time Elapsed: " + str(timeElapsed)
print "Bytes Read: " + str(byteCounter)
print "Data Rate: " + str(byteCounter/timeElapsed/1000) + " kbps"
print "Filename: "+ fName


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
"""
