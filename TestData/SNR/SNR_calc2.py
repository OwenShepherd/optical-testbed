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

    # Now we've collected the test data as desired
    # want to get the count data to hopefully back out the SNR
    lightOn = []
    lightOn2 = []
    for i in range(len(lightDirs)):
        # If we're dealing with a light on
        stateint = time.time()
        for state in range(numStates):
            aftData = testData[i][state][0]
            foreData = testData[i][state][1]
            if (i == 0 or i == 2):
                for r in range(numRows):
                    for c in range(numCols):
                        if (aftData[r][c] != ''):
                            data = int(aftData[r][c])
                            if (i == 0 and data > threshold):
                                lightOn.append((r,c,data))
                            elif (i == 2 and (data > threshold)):
                                lightOn2.append((r,c,data))
                            countData[state][i+1] += data

            elif (i == 1):
                for item in lightOn:
                    (r,c,data) = item
                    countData[state][i+1] += int(aftData[r][c])

            else:
                for item in lightOn2:
                    (r,c,data) = item
                    countData[state][i+1] += int(aftData[r][c])

            stateend = time.time()

    print("SNR Results:")
    for i in range(numStates):
        countData[i][5] = countData[i][1]/countData[i][2]
        countData[i][6] = countData[i][3]/countData[i][4]
        print("Exposure Time: " + str(countData[i][0]) + "; SNR_1: " + str(countData[i][5]) + "; SNR_2: " + str(countData[i][6]))
