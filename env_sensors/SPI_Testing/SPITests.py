import serial
import time
from string import whitespace
start = time.time()
end = time.time()
testTime = 60
readData = ""
readFirst = True
switchAverage = 0
readAverage = 0
switchSum = 0
readSum = 0
counter = 0
numData = 0
readCounter = 0
switchCounter = 0

directory = "C:\\Users\\sheph\\Documents\\Arduino\\ASEN-4018-EnvSensors\\SPI_Testing\\"
fileName2 = directory+"Teensy_SPITest_Statistics_"+str(testTime)+"_seconds.txt"

with serial.Serial('COM3',9600) as ser:
    while ((end-start)<testTime):
        x = ser.read()
        xchar = x.decode("utf-8")
        readData = readData + xchar
        end = time.time()

tElapsed = end-start
readData = readData.replace('\x00','')
my_data = readData.split(",")
dataLength = len(my_data)

for data in my_data:
    counter = counter + 1
    if (counter!=dataLength):
        if (int(data)>9):
            readSum = readSum + int(data)
            readFirst = False
            readCounter = readCounter+1
        elif (int(data)<9):
            switchSum = switchSum + int(data)
            readFirst = True
            switchCounter = switchCounter+1



avgRead = readSum/readCounter
avgSwitch = switchSum/switchCounter


f = open(fileName2,'w')
f.write("Avg SPIRead,Avg SPISwitch,Times Tested,Time Elapsed\n")
f.write(str(avgRead)+","+str(avgSwitch)+","+str(counter)+","+str(tElapsed))
f.close()
