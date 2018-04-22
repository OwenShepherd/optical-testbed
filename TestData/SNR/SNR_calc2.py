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
    # Directories containing light dat
    lightDirs = [os.getcwd()] * 4
    lightDirs[0] += '\\SNR_LightOn'
    lightDirs[1] += '\\SNR_LightOff'
    lightDirs[2] += '\\SNR_LightOn2'
    lightDirs[3] += '\\SNR_LightOff2'

    # Creating an array to hold the test data
    testData = [[[0 for x in range(4)] for y in range(8)] for z in range(2)]

    # Looping through every piece of test data
    for i in range(len(lightDirs)):
        # Collects all the state directories and the fore / aft files locations
        holder = glob(lightDirs[i] + "\\*\\data_RCWS\\*.csv")

        # Going to loop through the holder names
        for j in range(0,len(holder),2):
            state = j/2
