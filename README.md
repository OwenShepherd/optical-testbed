# ASEN-4018-Automation
A collection of my information / attempts at working software necessary for automating different tasks of the testbed.

## Windows Documentation
Currently, interfacing via scripts has not been completed yet.  Although testing
python by connecting python to .NET via the COM platform has been successful.
Full interfacing with the ASI120MM has not been established currently.

### Installation / Setup
Please first ensure that python and pip have been installed and added to your
environment variables path.  Now, try just a simple python import:
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
