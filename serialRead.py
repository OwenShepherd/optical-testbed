import serial
import time

inputs = ""
testTime = 1 # Total time you desire to test the Teensy
Baud = 9600 # Baud rate (note on the Teensy this doesn't change Serial rates)
Port = 'COM3' # USB port ID that the Teensy is plugged in to

directory = "C:\\Users\\sheph\\Documents\\Arduino\\ASEN-4018-EnvSensors\\"
fName = "Teensy_"+str(Baud)+"_Baud_"+str(testTime)+"_seconds.txt"
fileName = directory+fName

f = open(fileName,'w+')



start = time.time()
end = time.time()
byteCounter = 0



with serial.Serial(Port, Baud) as ser:
    while ((end-start)<testTime):
        x = ser.read(1000)
        f.write(x.decode("utf-8"))
        byteCounter = byteCounter+1000
        end = time.time()

f.close
timeElapsed = end-start

print("Time Elapsed: " + str(timeElapsed))
print("Bytes Read: " + str(byteCounter))
print("Data Rate: " + str(byteCounter/timeElapsed/1000) + " kbps")
print("Filename: "+ fName)



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

for c in data:

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


    if (counter==99):
        counter = -1

print("Error Flags: "+ str(flags))
print("Count of Every Third Char: "+ str(totalCounter))
