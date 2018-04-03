import clr
import sys
import time
import os
import csv
clr.AddReferenceToFileAndPath("C:\\Program Files (x86)\\ASCOM\\Platform 6 Developer Components\\Components\\Platform6\\ASCOM.DriverAccess.dll")

from ASCOM.DriverAccess import *

filename = "elapsedtime.txt"
directory = "C:\\Users\\sheph\\Desktop\\"
a = Camera("ASCOM.ASICamera2.Camera")
a.Connected = True
a.StartExposure(1/60, True)

wait = True
start_time = time.time()
while wait:
    if a.ImageReady:
        wait = False
elapsed_time = time.time()-start_time
ypixels = a.CameraYSize
xpixels = a.CameraXSize

imageList = list(a.ImageArray)

imageMat = [[0 for x in range(xpixels)] for y in range(ypixels)]
count = 0
count2 = 0



with open(directory+filename,"w+") as myfile:
    wr= csv.writer(myfile)
    for x in range(xpixels):
        wr.writerow(imageList[count:(count+ypixels)])
        count = count + ypixels
