# AWESoMe Automation
Team: **A**pparatus for **W**avefront **E**rror **S**ens**o**r **Me**asurement

Website: [AWESoMe](https://www.colorado.edu/aerospace/current-students/undergraduates/senior-design-projects/past-senior-projects/2017-2018/apparatus)  

![](images/awesome_logo_whitebackground.png?raw=true)

AWESoMe is a joint project between the South West Research Institute (SWRI) and a team of Aerospace Engineering students at Boulder.  The goal of the project is to create a testbed for comparing the performance of a [Shack-Hartmann Array](https://en.wikipedia.org/wiki/Shack%E2%80%93Hartmann_wavefront_sensor) (SHA) and a [Roddier Curvature Wavefront Sensor](http://www.soft.belastro.net/files/tmp/Wave-front_reconstruction_from_defocused_images_and_the_testing_of_ground-based_optical_telescopes.pdf) (RCWS).  

This repository contains .NET programs aimed at automating as much of the testbed as possible due to the large number of tests that need to be run in a timely manner.  This mostly includes: prompting the user to load in a specifically formatted CSV for scheduling tests, and then running those tests one-by-one by adjusting the parameters of the SHA, RCWS, and motorized stages.

## Getting Started
Some third-party drivers and Visual Studio 15 need to be installed.

### Pre-Requisites  
**Please download drivers as 32-bit or it may cause some compatibility issues**  

First, ensure you have at least Visual Studio 15 installed (or at least 10.0.40219.1).  Then, install the following:
- [ASI120MM Drivers](http://astronomy-imaging-camera.com/software/)
- [ASCOM Platform 6.3](https://ascom-standards.org/Downloads/Index.htm)
- [ASCOM Platform Developer Components](https://ascom-standards.org/Downloads/PlatDevComponents.htm)
- [ASI Cameras ASCOM Driver](http://astronomy-imaging-camera.com/software/)

## Running the Program
Currently, the program is unfinished, although several components have been completed.  Other than the aforementioned ASI drivers, the necessary DLL references should already be installed in the corresponding VS solutions.  Details on each solution are as follows:
- WFS150-7AR: Solution is capable of interfacing with the Thorlabs SHA.
- KDC101: Solution is capable of interfacing with the Thorlabs motor controllers.
- ASI120MM: Solution is capable of interfacing with ASI120MM image sensor, and soon the QHY as well.
- WindowTesting: This will eventually be the complete overall program to replace the four separate solutions.  Currently it has a GUI for prompting the user for some necessary inputs.

## Conceptual Information

### ASCOM References
The two RCWS cameras (ASI120MM & QHY...) both interface with Windows using ASCOM.  Some
references on how to use ASCOM with VS:
- [ASCOM Standards](http://www.ascom-standards.org/Help/Developer/html/7d9253c2-fdfd-4c0d-8225-a96bddb49731.htm)

### ASEN_RCWS class
The ASEN_RCWS class can be used to take images and save them in a very simple
way.  Currently, it has only been tested with the ASI, but should be able to
easily extend to the QHY if necessary.  To use the class, follow the current
example shown in the "program.cs" file for using the ASI120MM.

### ASEN_KDC Class
In order for the current test code for the KDC101 to work, you must do the
following.  Currently the class is not ready as further testing needs to be
completed.

Add these references to the program (if they are not already included):
- Thorlabs.MotionControl.KCube.DCServoCLI.dll
- Thorlabs.MotionControl.DeviceManagerCLI.dll
- Thorlabs.MotionControl.GenericMotorCLI.dll

In addition, the following native C DLLs need to be copied to the executable folder:
- ThorLabs.MotionControl.KCube.DCServo.dll
- ThorLabs.MotionControl.DeviceManager.dll

These DLL files may be found in the References_DLL/KDC101 folder.


### Filesystem Organization
Here is how data will be organized.  An experiment corresponds to a collection of data
taken during one session.  Each state corresponds to a different test (i.e. with 
different de-focus distances, exposures, etc...).
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

### Schedule File Structure
The experiment_schedule.csv file will specify all the system states that are to be tested for a given experiment. For each state specified in this file the automated test control program will create the appropriate sub-folder of the test and populate it with the data that is produced while the experiment is running. In post-processing other files can be added in the created file structure. The first row will include the header:  
```
RCWS EXPT (us), SHA EXPT (us), RCWS D FORE (um), RCWS D AFT (um), M2 A X (arc sec), M2 A Y (arc sec)  
```
After that line every row of data will specify those state values at which to make a wavefront measurement with both sensors.

## Authors (This Repository)
- Owen Shepherd
- Jake Crouse


## The Team
- Robert Belter
- Ali Colic
- Jake Crouse
- Lucas Droste
- Diego Gomes
- Ankit Hriday
- Owen Lyke
- Brandon Noirot
- Owen Shepherd
- Brandon Stetler

## Acknowledgements
- Eliot Young (SWRI)
- Bob Woodruff (SWRI)
- Engineering Excellence Fund (EEF)
- NASA Glenn
