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
import cv2
import csv

def csv2png(file_in, saving=True):
    f = open(file_in,'r')
    holderf = csv.reader(f)
    holder = [ii for ii in holderf]
    f.close()
    holder = np.array(holder)
    empties = np.where(holder == '')
    holder[empties] = '0'
    holder = holder.astype(np.uint16)
    if saving:
        print(file_in[0:-3]+'png')
        cv2.imwrite(file_in[0:-3]+'png',holder.transpose())
    return holder.transpose()

if __name__ == '__main__':
    # Retrieve image file locations from RCWS
    dirs0 = ['SNR_LightOn','SNR_LightOff','SNR_LightOn2','SNR_LightOff2']
    files = []
    for dir0 in dirs0:
        dirs1 = glob('./' + dir0 + '/*')
        # print('dirs1 = ',dirs1)
        for dir1 in dirs1:
            print(dir1)
            holder = glob(dir1 + 'data_RCWS/*.csv')
            # print(holder)
            files.append(holder)
    # Conglomerate the image data into a list
    imgs_array = []
    for file in files:
        print(file)
        #holder = cv2.imread(file[1],-1)
        holder = csv2png(file[1])
        imgs_array.append(holder)
        #holder = cv2.imread(file[0],-1)
        holder = csv2png(file[0])
        imgs_array.append(holder)
    # Present each image
    ii = 0
    jj = 0
    """
    for image_out in imgs_array:
        plt.figure(figsize=[16,10])
        plt.imshow(image_out,cmap='gray')
        if ii % 2 == 0:
            plt.title(files[jj][1])
        else:
            plt.title(files[jj][0])
            jj += 1
        plt.show()
        ii += 1
        # plt.figure(figsize=[6,4])
        # plt.hist(image_out.ravel(), bins=2**8, range=(0.0, 2**16-1))
        # plt.grid(b=True,which='major')
        # plt.xlim([0.0, 2**16-1])
        # plt.yscale('log')
        # plt.xlabel('Bin (16-bit)')
        # plt.ylabel('Frequency')
        # plt.title('QHY174M'+'\n'+'Resolution: 1920x1200')
        # plt.show()
        """
