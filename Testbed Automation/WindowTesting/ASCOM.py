import clr
import sys
import time
import os
import csv
sys.path.append("C:\\Program Files (x86)\\ASCOM\\Platform 6 Developer Components\\Components\\Platform6")
clr.AddReference("ASCOM.DriverAccess.dll")

from ASCOM.DriverAccess import *

filename = sys.argv[1]
a = Camera("ASCOM.ASICamera2.Camera")
a.Connected = True
a.StartExposure(float(sys.argv[2]), True)

wait = True
start_time = time.time()
while wait:
    if a.ImageReady:
        wait = False
elapsed_time = time.time()-start_time
ypixels = a.CameraYSize
xpixels = a.CameraXSize

imageList = list(a.ImageArray)
count = 0



with open(filename,"w+") as myfile:
    wr= csv.writer(myfile)
    for x in range(xpixels):
        wr.writerow(imageList[count:(count+ypixels)])
        count = count + ypixels
