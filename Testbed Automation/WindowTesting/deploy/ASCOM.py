import clr
import sys
import time
sys.path.append("Lib")
sys.path.append("Lib\site-packages")
clr.AddReference("ASCOM.DriverAccess.dll")
import os


import serial
#from ASCOM.DriverAccess import *
filename = "C:\\Users\\sheph\\Documents\\hello.txt"
fh = open(filename,"w+")
fh.write("it worked....")
fh.close()
"""
a = Camera("ASCOM.ASICamera2.Camera")
a.Connected = True
a.StartExposure(1/60, True)

wait = True
start_time = time.time()
while wait:
    if a.ImageReady:
        wait = False
elapsed_time = time.time()-start_time

print elapsed_time
"""
def Simple():
	print os.path.dirname(os.path.abspath(__file__))
