using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
namespace WindowTesting.Device_Classes
{
    class ASEN_QHY
    {
        private uint QHYCCD_SUCCESS;
        private uint cambinx;
        private uint cambiny;
        private int length;
        private IntPtr camhandle;
        private double chipw;
        private double chiph;
        private double pixelw;
        private double pixelh;
        private uint x;
        private uint h;
        private uint bpp;
        private uint c;



        public ASEN_QHY()
        {
            // Initializing some variables, including some to be used as pointers
            QHYCCD_SUCCESS = 0;
            cambinx = 1;
            cambiny = 1;
            chipw = 0;
            chiph = 0;
            pixelw = 0;
            pixelh = 0;
            x = 0;
            h = 0;
            bpp = 0;
            c = 0;



            // Collecting the number of cameras connected
            int numCameras = Convert.ToInt32(ASCOM.QHYCCD.libqhyccd.ScanQHYCCD());

            // Camera id of the first camera (assuming the testbed won't every simultaneously use 2xQHY)
            StringBuilder id = new StringBuilder(0);
            uint ret = ASCOM.QHYCCD.libqhyccd.GetQHYCCDId(0, id);

            // Getting the "camhandle", which is a pointer to the camera
            this.camhandle = ASCOM.QHYCCD.libqhyccd.OpenQHYCCD(id);
            if (camhandle == null)
            {
                Console.WriteLine("No QHY camera connected");
            }

            // Setting the "stream mode", I do not know what this function does
            ret = ASCOM.QHYCCD.libqhyccd.SetQHYCCDStreamMode(camhandle, 0);

            // Initializes the QHY
            ret = ASCOM.QHYCCD.libqhyccd.InitQHYCCD(camhandle);

            // Retrives the chip info
            ret = ASCOM.QHYCCD.libqhyccd.GetQHYCCDChipInfo(camhandle, ref chipw, ref chiph, ref x, ref h, ref pixelw, ref pixelh, ref bpp);

        }

        public void Initialize()
        {
            uint ret;

            ret = ASCOM.QHYCCD.libqhyccd.IsQHYCCDControlAvailable(camhandle, StructModel.CONTROL_ID.CONTROL_USBTRAFFIC);
            if (ret == QHYCCD_SUCCESS)
            {
                ret = ASCOM.QHYCCD.libqhyccd.SetQHYCCDParam(camhandle, StructModel.CONTROL_ID.CONTROL_USBTRAFFIC, 50);
                if (ret != QHYCCD_SUCCESS)
                {
                    Console.WriteLine("SetQHYCCDParam CONTROL_USBTRAFFIC failed\n");
                    //getchar();
                    Console.ReadLine();
                    return;
                }
            }

            // Default gain
            ret = ASCOM.QHYCCD.libqhyccd.IsQHYCCDControlAvailable(camhandle, StructModel.CONTROL_ID.CONTROL_GAIN);
            if (ret == QHYCCD_SUCCESS)
            {
                ret = ASCOM.QHYCCD.libqhyccd.SetQHYCCDParam(camhandle, StructModel.CONTROL_ID.CONTROL_GAIN, 1);
                if (ret != QHYCCD_SUCCESS)
                {
                    Console.WriteLine("SetQHYCCDParam CONTROL_GAIN failed\n");
                    //getchar();
                    Console.ReadLine();
                    return;
                }
            }

            // Default offset
            ret = ASCOM.QHYCCD.libqhyccd.IsQHYCCDControlAvailable(camhandle, StructModel.CONTROL_ID.CONTROL_OFFSET);
            if (ret == QHYCCD_SUCCESS)
            {
                ret = ASCOM.QHYCCD.libqhyccd.SetQHYCCDParam(camhandle, StructModel.CONTROL_ID.CONTROL_OFFSET, 0);
                if (ret != QHYCCD_SUCCESS)
                {
                    Console.WriteLine("SetQHYCCDParam CONTROL_GAIN failed\n");
                    //getchar();
                    Console.ReadLine();
                    return;
                }
            }

            
            // Default USB Traffic
            ret = ASCOM.QHYCCD.libqhyccd.SetQHYCCDParam(camhandle, StructModel.CONTROL_ID.CONTROL_USBTRAFFIC, 50);
            if (ret != QHYCCD_SUCCESS)
            {
                Console.WriteLine("SetQHYCCDParam USB Traffic failed\n");
                //getchar();
                Console.ReadLine();
                return;
            }

            // Setting resolution to the maximum camera resolution
            ASCOM.QHYCCD.libqhyccd.SetQHYCCDResolution(camhandle, 0, 0, x, h);

            // Setting Default Bin Mode
            ASCOM.QHYCCD.libqhyccd.SetQHYCCDBinMode(camhandle, cambinx, cambiny);

            // Setting default to 16 bit input
            ASCOM.QHYCCD.libqhyccd.SetQHYCCDBitsMode(camhandle, 16);
        }

        public void Capture(int exposure)
        {
            uint ret;
            // User input exposure
            ret = ASCOM.QHYCCD.libqhyccd.SetQHYCCDParam(camhandle, StructModel.CONTROL_ID.CONTROL_EXPOSURE, exposureTime);
            if (ret != QHYCCD_SUCCESS)
            {
                Console.WriteLine("SetQHYCCDParam CONTROL_EXPOSURE failed\n");
                //getchar();
                Console.ReadLine();
                return;
            }



        }
    }
}

namespace StructModel
{
    public enum CONTROL_ID
    {
        CONTROL_BRIGHTNESS = 0, //!< image brightness
        CONTROL_CONTRAST,       //!< image contrast
        CONTROL_WBR,            //!< red of white balance
        CONTROL_WBB,            //!< blue of white balance
        CONTROL_WBG,            //!< the green of white balance
        CONTROL_GAMMA,          //!< screen gamma
        CONTROL_GAIN,           //!< camera gain
        CONTROL_OFFSET,         //!< camera offset
        CONTROL_EXPOSURE,       //!< expose time (us)
        CONTROL_SPEED,          //!< transfer speed
        CONTROL_TRANSFERBIT,    //!< image depth bits
        CONTROL_CHANNELS,       //!< image channels
        CONTROL_USBTRAFFIC,     //!< hblank
        CONTROL_ROWNOISERE,     //!< row denoise
        CONTROL_CURTEMP,        //!< current cmos or ccd temprature
        CONTROL_CURPWM,         //!< current cool pwm
        CONTROL_MANULPWM,       //!< set the cool pwm
        CONTROL_CFWPORT,        //!< control camera color filter wheel port
        CONTROL_COOLER,         //!< check if camera has cooler
        CONTROL_ST4PORT,        //!< check if camera has st4port
        CAM_COLOR,
        CAM_BIN1X1MODE,         //!< check if camera has bin1x1 mode
        CAM_BIN2X2MODE,         //!< check if camera has bin2x2 mode
        CAM_BIN3X3MODE,         //!< check if camera has bin3x3 mode
        CAM_BIN4X4MODE,         //!< check if camera has bin4x4 mode
        CAM_MECHANICALSHUTTER,                   //!< mechanical shutter
        CAM_TRIGER_INTERFACE,                    //!< triger
        CAM_TECOVERPROTECT_INTERFACE,            //!< tec overprotect
        CAM_SINGNALCLAMP_INTERFACE,              //!< singnal clamp
        CAM_FINETONE_INTERFACE,                  //!< fine tone
        CAM_SHUTTERMOTORHEATING_INTERFACE,       //!< shutter motor heating
        CAM_CALIBRATEFPN_INTERFACE,              //!< calibrated frame
        CAM_CHIPTEMPERATURESENSOR_INTERFACE,     //!< chip temperaure sensor
        CAM_USBREADOUTSLOWEST_INTERFACE,         //!< usb readout slowest

        CAM_8BITS,                               //!< 8bit depth
        CAM_16BITS,                              //!< 16bit depth
        CAM_GPS,                                 //!< check if camera has gps

        CAM_IGNOREOVERSCAN_INTERFACE,            //!< ignore overscan area

        QHYCCD_3A_AUTOBALANCE,
        QHYCCD_3A_AUTOEXPOSURE,
        QHYCCD_3A_AUTOFOCUS,
        CONTROL_AMPV,                            //!< ccd or cmos ampv
        CONTROL_VCAM,                            //!< Virtual Camera on off
        CAM_VIEW_MODE,

        CONTROL_CFWSLOTSNUM,         //!< check CFW slots number
        IS_EXPOSING_DONE,
        ScreenStretchB,
        ScreenStretchW,
        CONTROL_DDR,
        CAM_LIGHT_PERFORMANCE_MODE,

        CAM_QHY5II_GUIDE_MODE,
        DDR_BUFFER_CAPACITY,
        DDR_BUFFER_READ_THRESHOLD
    };

    public enum BAYER_ID
    {
        BAYER_GB = 1,
        BAYER_GR,
        BAYER_BG,
        BAYER_RG
    };
}


namespace ASCOM.QHYCCD
{

    class libqhyccd
    {
        [DllImport("qhyccd.dll", EntryPoint = "InitQHYCCDResource")]
        public static extern UInt32 InitQHYCCDResource();

        [DllImport("qhyccd.dll", EntryPoint = "ReleaseQHYCCDResource")]
        public static extern UInt32 ReleaseQHYCCDResource();

        [DllImport("qhyccd.dll", EntryPoint = "ScanQHYCCD")]
        public static extern UInt32 ScanQHYCCD();

        [DllImport("qhyccd.dll", EntryPoint = "GetQHYCCDId")]
        public static extern UInt32 GetQHYCCDId(int index, StringBuilder id);

        [DllImport("qhyccd.dll", EntryPoint = "OpenQHYCCD")]
        public static extern IntPtr OpenQHYCCD(StringBuilder id);

        [DllImport("qhyccd.dll", EntryPoint = "InitQHYCCD")]
        public static extern UInt32 InitQHYCCD(IntPtr handle);

        [DllImport("qhyccd.dll", EntryPoint = "CloseQHYCCD")]
        public static extern UInt32 CloseQHYCCD(IntPtr handle);

        [DllImport("qhyccd.dll", EntryPoint = "SetQHYCCDBinMode")]
        public static extern UInt32 SetQHYCCDBinMode(IntPtr handle, UInt32 wbin, UInt32 hbin);

        [DllImport("qhyccd.dll", EntryPoint = "SetQHYCCDParam")]
        public static extern UInt32 SetQHYCCDParam(IntPtr handle, StructModel.CONTROL_ID controlid, double value);

        [DllImport("qhyccd.dll", EntryPoint = "GetQHYCCDMemLength")]
        public static extern UInt32 GetQHYCCDMemLength(IntPtr handle);

        [DllImport("qhyccd.dll", EntryPoint = "ExpQHYCCDSingleFrame")]
        public static extern UInt32 ExpQHYCCDSingleFrame(IntPtr handle);

        [DllImport("qhyccd.dll", EntryPoint = "GetQHYCCDSingleFrame")]
        public static extern UInt32 GetQHYCCDSingleFrame(IntPtr handle, ref UInt32 w, ref UInt32 h, ref UInt32 bpp, ref UInt32 channels, byte[] rawArray);

        [DllImport("qhyccd.dll", EntryPoint = "GetQHYCCDChipInfo", CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 GetQHYCCDChipInfo(IntPtr handle, ref double chipw, ref double chiph, ref UInt32 imagew, ref UInt32 imageh, ref double pixelw, ref double pixelh, ref UInt32 bpp);

        [DllImport("qhyccd.dll", EntryPoint = "GetQHYCCDParam")]
        public static extern double GetQHYCCDParam(IntPtr handle, StructModel.CONTROL_ID controlid);

        [DllImport("qhyccd.dll", EntryPoint = "IsQHYCCDControlAvailable")]
        public static extern UInt32 IsQHYCCDControlAvailable(IntPtr handle, StructModel.CONTROL_ID controlid);

        [DllImport("qhyccd.dll", EntryPoint = "SetQHYCCDResolution")]
        public static extern UInt32 SetQHYCCDResolution(IntPtr handle, UInt32 startx, UInt32 starty, UInt32 sizex, UInt32 sizey);

        [DllImport("qhyccd.dll", EntryPoint = "SetQHYCCDStreamMode")]
        public static extern UInt32 SetQHYCCDStreamMode(IntPtr handle, UInt32 mode);

        [DllImport("qhyccd.dll", EntryPoint = "SetQHYCCDBitsMode")]
        public static extern UInt32 SetQHYCCDBitsMode(IntPtr handle, UInt32 bits);

    }
}

