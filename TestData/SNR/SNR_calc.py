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
    testData = [[0 for x in range(0,6)] for y in range(0,8)]
    # Retrieve image file locations from RCWS
    dirs0 = ['SNR_LightOn','SNR_LightOff','SNR_LightOn2','SNR_LightOff2']
    stateDirs = ['STATE1','STATE2','STATE3','STATE4','STATE5','STATE6','STATE7','STATE8']
    files = []
    print(os.getcwd())
    for dir0 in dirs0:
        dirs1 = glob('.\\' + dir0 + '\\*')
        # print('dirs1 = ',dirs1)
        for dir1 in dirs1:
            holder = glob(dir1 + '\\data_RCWS\\*.csv')
            holder2 = glob(dir1 + '\\*.csv')
            #print(holder)
            holder.append(holder2[1])
            files.append(holder)
    # Conglomerate the image data into a list
    imgs_array = []
    localData = [[0 for x in range(2)] for y in range(8)]
    fileIter = 0
    for file in files:
        print(file)
        input()
        dirCounter = 0
        for dirs in stateDirs:
            currString = file[0]
            if (currString.find(dirs) != -1):
                fileIter = dirCounter
            dirCounter = dirCounter + 1
        data = list(csv.reader(open(file[0])))
        numrow = len(data)
        numcol = len(data[0])
        with open(file[2]) as f:
            reader = csv.reader(f)
            header = next(reader)


        localData[fileIter][0] = header[0]
        sum = 0
        tempData = []
        tempR = []
        tempC = []
        for r in range(0,numrow):
            for c in range(0,numcol):
                if (data[r][c] == ''):
                    sum = sum + 0
                else:
                    sum = sum + int(data[r][c])
                    if (int(data[r][c]) > 4000):
                        tempData.append(int(data[r][c]))
                        tempR.append(r)
                        tempC.append(c)

        z = list(zip(tempData,tempR,tempC))
        counter = 0
        for dirs in dirs0:
            if (file[0].find(dirs) != -1):
                testData[fileIter][counter + 1] = sum
            counter = counter + 1

        # holder = cv2.imread(file[1],-1)
        # holder = csv2png(file[1])
        # imgs_array.append(holder)
        # holder = cv2.imread(file[0],-1)
        # holder = csv2png(file[0])
        # imgs_array.append(holder)
    # Present each image

    with open(os.getcwd() + "\\Counts.csv","w+") as my_csv:
        csvWriter = csv.writer(my_csv,delimiter=',')
        csvWriter.writerows(testData)

    input()

    ii = 0
    jj = 0
    for image_out in imgs_array:
        """
        plt.figure(figsize=[16,10])
        plt.imshow(image_out,cmap='gray')
        if ii % 2 == 0:
            plt.title(files[jj][1])
        else:
            plt.title(files[jj][0])
            jj += 1
        plt.show()
        ii += 1
        plt.figure(figsize=[6,4])
        """


        hist = cv2.calcHist([image_out], [0], None, [2**16], [0.0, 2**16-1])
        x = list(range(2**16))
        """
        plt.bar(tuple(x),tuple(hist),align = 'center')
        plt.xlabel("Bins")
        plot.ylabel("Frequency")
        plt.show()
        # hist,bins = np.histogram(image_out.ravel(), 2**16-1, [0, 2**16-1])
        """
        counter = 0
        imageSum = 0
        newHist = []
        newX = []
        for entry in hist:
            if (entry != 0):
                newHist.append(int(entry))
                newX.append(counter)
                #plt.bar(counter,entry,align='center')

            counter = counter + 1

        #plt.bar(newX,newHist,align = 'center')
        """
        print(newHist)
        print(newX)
        plt.plot(newX,newHist)
        plt.xlim([0,len(newX)-1])
        plt.xlabel("Bins")
        plt.ylabel("Frequency")
        plt.show()
        # hist,bins = np.histogram(image_out.ravel(), 2**16-1, [0, 2**16-1])
        """

        """
        plt.plot(hist)
        plt.grid(b=True, which='major')
        plt.xlim([0.0, 2**16-1])
        plt.yscale('log')
        plt.xlabel('Bin (16-bit)')
        plt.ylabel('Frequency')
        plt.title('QHY174M'+'\n'+'Resolution: 1920x1200')
        plt.show()
        """
