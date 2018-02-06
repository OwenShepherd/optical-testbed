# ASEN-4018-Automation
Team Website: [Team AWESoMe](https://www.colorado.edu/aerospace/current-students/undergraduates/senior-design-projects/past-senior-projects/2017-2018/apparatus)  

A collection of my information / attempts at working software necessary for automating different tasks of the testbed.

## Windows Documentation
Currently, interfacing via scripts has not been completed yet.  Although testing
python by connecting python to .NET via the COM platform has been successful.
Full interfacing with the ASI120MM has not been established currently.

### Installation / Setup
The project was created using Visual Studio and thus requires visual studio for
working on.  In addition, you must install the [ASI120MM Drivers](http://astronomy-imaging-camera.com/software/),
specifically the "ASI Cameras Driver" first.  Then install the "ASCOM Platform 6.3" from [here](https://ascom-standards.org/Downloads/Index.htm).  Then go [here](https://ascom-standards.org/Downloads/PlatDevComponents.htm) and install the "ASCOM Platform Developer Components".  Finally, install the "ASI Cameras ASCOM Driver" from [here](http://astronomy-imaging-camera.com/software/).  Now you may run the visual studio "program.cs" and connect to the ASI120MM camera.  So far the camera may take an image with a set exposure, but produces an "int[*,*]" that I am not sure how to convert to an image file.  I'm getting close though...

### ASCOM References
The two RCWS cameras (ASI120MM & QHY...) both interface with Windows using ASCOM.  Some
references on how to use ASCOM with VS:
- [ASCOM Standards](http://www.ascom-standards.org/Help/Developer/html/7d9253c2-fdfd-4c0d-8225-a96bddb49731.htm)


## Ubuntu Documentation
Some attempts have been made to interface with the ASI120MM via INDI, kstars, PyINDI,
and other methods.  Some progress has been made, but connecting to the camera and
taking photos via scripting or any GUI has not been achieved.  Other than that,
just hours of troubleshooting....


## Filesystem Organization
Here lies the overall planned file-structure for the experiment's software.  Follow
whatever current convention this README has, and be aware that the file structure
may change over time.
```
EXAMPLE_EXPERIMENT
├── 2018_02_03_13_48_41_STATE1
├── 2018_02_03_13_50_55_STATE2
├── 2018_02_03_13_52_13_STATE3
├── 2018_02_03_13_54_32_STATE4
│   ├── data_RCWS
│   │   ├── img_RCWS_aft.png
│   │   ├── img_RCWS_fore.png
│   │   └── zernikes_RCWS.csv
│   ├── data_SHA
│   │   ├── img_SHA_aft.png
│   │   ├── img_SHA_fore.png
│   │   ├── spt_SHA_aft.png
│   │   ├── spt_SHA_fore.png
│   │   ├── wft_SHA_aft.png
│   │   ├── wft_SHA_fore.png
│   │   └── zernikes_SHA.csv
│   ├── env_data.csv
│   ├── state_parameters.csv
│   └── zernikes_model.csv
└── experiment_schedule.csv
```
