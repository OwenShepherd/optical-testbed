import matplotlib.pyplot as plt
from numpy import genfromtxt

counter = 0

directory = "C:\\Users\\sheph\\Documents\\Arduino\\ASEN-4018-EnvSensors\\SPI_Testing\\"
preFileName = "Teensy_SPITest_Statistics_"
postFileName = "_seconds.txt"
testTimes = [1,5,8,10,12,20,30,40,50,60]

readData = []
switchData = []
counterData = []
timeData = []

for ttime in testTimes:
    fileName = directory + preFileName + str(ttime) + postFileName

    # Opening up the file created above
    with open(fileName) as f:
        data = f.readlines()

    counter = 0

    for line in data:
        if (counter != 0):
            my_data = line.split(",")
            readData.append(float(my_data[0]))
            switchData.append(float(my_data[1]))
            counterData.append(float(my_data[2]))
            timeData.append(float(my_data[3]))


        counter = counter + 1



plt.plot(timeData,readData,color='red')
plt.plot(timeData,switchData,color='blue')
plt.title("Data Rate vs. Sampling Time")
plt.ylabel('Execution Time [us]')
plt.xlabel('Time Elapsed [s]')
axes = plt.gca()
axes.set_xlim([0,65])
axes.set_ylim([0,15])
plt.show()
