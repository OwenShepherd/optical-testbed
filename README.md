# ASEN-4018-Automation
A collection of my information / attempts at working software necessary for automating different tasks of the testbed.

## Windows Documentation
Currently, interfacing via scripts has not been completed yet.  Although testing
python by connecting python to .NET via the COM platform has been successful.
Full interfacing with the ASI120MM has not been established currently.

### Installation / Setup
Please first ensure that python and pip have been installed and added to your
environment variables path.  Now, install win32com via pip using the command:
```
pip install pywin32
```
Now, try just a simple python import:
```
import win32com.client
```
If this import fails because the module "win32api" cannot be found follow the
steps [here](https://github.com/michaelgundlach/pyspeech/issues/23) to correct such issues.


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
test
