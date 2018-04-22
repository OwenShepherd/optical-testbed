'''
Calculate the Signal-to-Noise Ratio (SNR) across the RCWS for each octave of exposure time.

Author: Noirot, Brandon
Created: 2018/04/18
Modified: 2018/04/18
'''
from glob import glob
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.image as img
import numpy as np
import cv2
import csv
import os
import time
import itertools

def csv2png(file_in, saving=True):
    f = open(file_in,'r')
    holderf = csv.reader(f)
    holder = [ii for ii in holderf]
    f.close()
    holder = np.array(holder)
    empties = np.where(holder == '')
    holder[empties] = '0'
    holder = holder.astype(np.uint16)
    holder = holder[:,0:-1]
    if saving:
        print(file_in[0:-3]+'png')
        cv2.imwrite(file_in[0:-3]+'png',holder.transpose())
    return holder.transpose()

if __name__ == '__main__':
    # image threshold
    threshold = 2048
    numStates = 8
    numRows = 0
    numCols = 0

    # Directories containing light dat
    lightDirs = [os.getcwd()] * 4
    lightDirs[0] += '\\SNR_LightOn'
    lightDirs[1] += '\\SNR_LightOff'
    lightDirs[2] += '\\SNR_LightOn2'
    lightDirs[3] += '\\SNR_LightOff2'

    # Creating an array to hold the test data
    testData = [[[0 for x in range(2)] for y in range(8)] for z in range(4)]
    countData = [[0 for y in range(7)] for x in range(8)]
    countDataFore = [[0 for y in range(7)] for x in range(8)]

    # Looping through every piece of test data
    for i in range(len(lightDirs)):
        # Collects all the state directories and the fore / aft files locations
        holder = glob(lightDirs[i] + "\\*\\data_RCWS\\*.csv")

        # Going to loop through the holder names.
        # Everything will be stored in the testData 3d array
        for j in range(0,len(holder),2):
            state = int(j/2)
            aftData = list(csv.reader(open(holder[j])))
            foreData = list(csv.reader(open(holder[j+1])))
            testData[i][state][0] = aftData
            numRows = len(aftData)
            numCols = len(aftData[0])
            testData[i][state][1] = foreData

    # Looping through the states to collect the exposure times
    header = []
    holder = glob(lightDirs[0] + "\\*\\parameters.csv")
    for i in range(len(holder)):
        fileName = holder[i]
        with open(fileName) as f:
            reader = csv.reader(f)
            header = next(reader)
            countData[i][0] = header[0]
            countDataFore[i][0] = header[0]

    # Now we've collected the test data as desired
    # want to get the count data to hopefully back out the SNR
    lightOn = []
    lightOn2 = []
    lightOnFore = []
    lightOnFore2 = []
    for i in range(len(lightDirs)):
        # Loop through the stages
        for state in range(numStates):
            # Collecting the fore and aft data
            aftData = testData[i][state][0]
            foreData = testData[i][state][1]

            # If the light is on
            if (i == 0 or i == 2):
                # Loop through the image
                for r in range(numRows):
                    for c in range(numCols):
                        # Ignore empty cells
                        if (aftData[r][c] != ''):
                            data = int(aftData[r][c])
                            dataFore = int(foreData[r][c])
                            # If the pixel is more intense than the threshold
                            if (i == 0 and data > threshold):
                                lightOn.append((r,c,data))
                            # If the pixel is more intense than the threshold
                            elif (i == 2 and (data > threshold)):
                                lightOn2.append((r,c,data))

                            elif (i == 0 and (dataFore > threshold)):
                                lightOnFore.append((r,c,dataFore))

                            elif (i == 2 and (dataFore > threshold)):
                                lightOnFore2.append((r,c,dataFore))


                            countData[state][i+1] += data
                            countDataFore[state][i+1] += dataFore

            # If
            elif (i == 1):
                for item in lightOn:
                    (r,c,data) = item
                    countData[state][i+1] += int(aftData[r][c])

                for item in lightOnFore:
                    (r,c,data) = item
                    countDataFore[state][i+1] += int(foreData[r][c])

            else:
                for item in lightOn2:
                    (r,c,data) = item
                    countData[state][i+1] += int(aftData[r][c])

                for item in lightOnFore2:
                    (r,c,data) = item
                    countDataFore[state][i+1] += int(foreData[r][c])

    exposures = []
    aftSNR = []
    aftSNR2 = []
    foreSNR = []
    foreSNR2 = []
    for i in range(numStates):
        exposures.append(float(countData[i][0]))

    print("SNR Results:")
    print("Aft Data:")
    for i in range(numStates):
        countData[i][5] = countData[i][1]/countData[i][2]
        countData[i][6] = countData[i][3]/countData[i][4]
        aftSNR.append(float(countData[i][5]))
        aftSNR2.append(float(countData[i][6]))
        print("Exposure Time: " + str(countData[i][0]) + "; SNR_1: " + str(countData[i][5]) + "; SNR_2: " + str(countData[i][6]))

    print("Fore Data:")
    for i in range(numStates):
        countDataFore[i][5] = countDataFore[i][1]/countDataFore[i][2]
        countDataFore[i][6] = countDataFore[i][3]/countDataFore[i][4]
        foreSNR.append(float(countDataFore[i][5]))
        foreSNR2.append(float(countDataFore[i][6]))
        print("Exposure Time: " + str(countDataFore[i][0]) + "; SNR_1: " + str(countDataFore[i][5]) + "; SNR_2: " + str(countDataFore[i][6]))


    print(type(exposures))
    print(type(aftSNR))
    input()



    fig = plt.figure()
    line1 = plt.plot(exposures,aftSNR, label='Aft Results - Test 1')
    line2 = plt.plot(exposures,aftSNR2, label='Aft Results - Test 2')
    line3 = plt.plot(exposures,foreSNR, label='Fore Results - Test 2')
    line4 = plt.plot(exposures,foreSNR2, label='Fore Results - Test 2')
    plt.legend()
    plt.xlabel("Exposure Time [us]")
    plt.ylabel("Signal-To-Noise Ratio")
    # plt.title("Results From Signal-To-Noise Ratio Testing")
    plt.savefig('SNR_Results.png')
