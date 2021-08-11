import matplotlib.pyplot as plt
from numpy import genfromtxt

counter = 0

directory = "C:\\Users\\sheph\\Documents\\Arduino\\ASEN-4018-EnvSensors\\Serial_Testing\\"
preFileName = "Teensy_Test_Statistics_"
postFileName = "_seconds.txt"
testTimes = [1,5,8,10,12,20,30,40,50,60]

timeData = []
byteData = []
rateData = []
fileNames = []
flagData = []
charData = []

for ttime in testTimes:
    fileName = directory + preFileName + str(ttime) + postFileName

    # Opening up the file created above
    with open(fileName) as f:
        data = f.readlines()

    counter = 0

    for line in data:
        if (counter != 0):
            my_data = line.split(",")
            timeData.append(float(my_data[0]))
            byteData.append(float(my_data[1]))
            rateData.append(float(my_data[2]))
            fileNames.append(str(my_data[3]))
            flagData.append(float(my_data[4]))
            charData.append(float(my_data[5]))

        counter = counter + 1



plt.plot(timeData,rateData,color='red')
plt.title("Average Data Rate vs. Elapsed Time")
plt.ylabel('Average Data Rate [kBps]')
plt.xlabel('Elapsed Time [s]')
axes = plt.gca()
axes.set_xlim([0,65])
axes.set_ylim([0,1200])
plt.show()
