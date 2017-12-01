import serial
import time

inputs = ""
testTime = 1
Baud = 9600
fileName = "C:\\Users\\sheph\\Documents\\Arduino\\ASEN-4018-EnvSensors" + "\\Teensy_"+str(Baud)+"_Baud_"+str(testTime)+"_seconds.txt"

f = open(fileName,'w+')



start = time.time()
end = time.time()
byteCounter = 0



with serial.Serial('COM3', Baud) as ser:
    while ((end-start)<testTime):
        x = ser.read(1000)
        f.write(x.decode("utf-8"))
        byteCounter = byteCounter+1000
        #print(str(x))
        end = time.time()

f.close
timeElapsed = end-start

print("Time Elapsed: " + str(timeElapsed))
print("Bytes Read: " + str(byteCounter))
print("Data Rate: " + str(byteCounter/timeElapsed/1000) + " kbps")
print(fileName)
