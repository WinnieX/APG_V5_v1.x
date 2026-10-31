using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using System.IO;

namespace AEC.Comm.CommDevices.HID
{
    public class HIDDevice
    {
        #region constants
        private const int DIGCF_DEFAULT = 0x1;
        private const int DIGCF_PRESENT = 0x2;
        private const int DIGCF_ALLCLASSES = 0x4;
        private const int DIGCF_PROFILE = 0x8;
        private const int DIGCF_DEVICEINTERFACE = 0x10;

        private const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        //private const short INVALID_HANDLE_VALUE = -1;
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint FILE_SHARE_READ = 0x00000001;
        private const uint FILE_SHARE_WRITE = 0x00000002;
        private const uint CREATE_NEW = 1;
        private const uint CREATE_ALWAYS = 2;
        private const uint OPEN_EXISTING = 3;

        private const uint FILE_FLAG_OVERLAPPED = 0x40000000;

        private const int BUFFER_SIZE = 0x100;

        static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public const int DefaultWriteTimeout = 0x400;

        enum SetupDiGetDeviceRegistryPropertyEnum : uint
        {
            SPDRP_DEVICEDESC = 0x00000000, // DeviceDesc (R/W)
            SPDRP_HARDWAREID = 0x00000001, // HardwareID (R/W)
            SPDRP_COMPATIBLEIDS = 0x00000002, // CompatibleIDs (R/W)
            SPDRP_UNUSED0 = 0x00000003, // unused
            SPDRP_SERVICE = 0x00000004, // Service (R/W)
            SPDRP_UNUSED1 = 0x00000005, // unused
            SPDRP_UNUSED2 = 0x00000006, // unused
            SPDRP_CLASS = 0x00000007, // Class (R--tied to ClassGUID)
            SPDRP_CLASSGUID = 0x00000008, // ClassGUID (R/W)
            SPDRP_DRIVER = 0x00000009, // Driver (R/W)
            SPDRP_CONFIGFLAGS = 0x0000000A, // ConfigFlags (R/W)
            SPDRP_MFG = 0x0000000B, // Mfg (R/W)
            SPDRP_FRIENDLYNAME = 0x0000000C, // FriendlyName (R/W)
            SPDRP_LOCATION_INFORMATION = 0x0000000D, // LocationInformation (R/W)
            SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E, // PhysicalDeviceObjectName (R)
            SPDRP_CAPABILITIES = 0x0000000F, // Capabilities (R)
            SPDRP_UI_NUMBER = 0x00000010, // UiNumber (R)
            SPDRP_UPPERFILTERS = 0x00000011, // UpperFilters (R/W)
            SPDRP_LOWERFILTERS = 0x00000012, // LowerFilters (R/W)
            SPDRP_BUSTYPEGUID = 0x00000013, // BusTypeGUID (R)
            SPDRP_LEGACYBUSTYPE = 0x00000014, // LegacyBusType (R)
            SPDRP_BUSNUMBER = 0x00000015, // BusNumber (R)
            SPDRP_ENUMERATOR_NAME = 0x00000016, // Enumerator Name (R)
            SPDRP_SECURITY = 0x00000017, // Security (R/W, binary form)
            SPDRP_SECURITY_SDS = 0x00000018, // Security (W, SDS form)
            SPDRP_DEVTYPE = 0x00000019, // Device Type (R/W)
            SPDRP_EXCLUSIVE = 0x0000001A, // Device is exclusive-access (R/W)
            SPDRP_CHARACTERISTICS = 0x0000001B, // Device Characteristics (R/W)
            SPDRP_ADDRESS = 0x0000001C, // Device Address (R)
            SPDRP_UI_NUMBER_DESC_FORMAT = 0X0000001D, // UiNumberDescFormat (R/W)
            SPDRP_DEVICE_POWER_DATA = 0x0000001E, // Device Power Data (R)
            SPDRP_REMOVAL_POLICY = 0x0000001F, // Removal Policy (R)
            SPDRP_REMOVAL_POLICY_HW_DEFAULT = 0x00000020, // Hardware Removal Policy (R)
            SPDRP_REMOVAL_POLICY_OVERRIDE = 0x00000021, // Removal Policy Override (RW)
            SPDRP_INSTALL_STATE = 0x00000022, // Device Install State (R)
            SPDRP_LOCATION_PATHS = 0x00000023, // Device Location Paths (R)
            SPDRP_BASE_CONTAINERID = 0x00000024  // Base ContainerID (R)
        }

        #endregion

        #region win32_API_declarations
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid,
                                                      IntPtr Enumerator,
                                                      IntPtr hwndParent,
                                                      uint Flags);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern void HidD_GetHidGuid(ref Guid hidGuid);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean SetupDiEnumDeviceInterfaces(
           IntPtr hDevInfo,
            //ref SP_DEVINFO_DATA devInfo,
           IntPtr devInfo,
           ref Guid interfaceClassGuid,
           UInt32 memberIndex,
           ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData
        );

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean SetupDiGetDeviceInterfaceDetail(
           IntPtr hDevInfo,
           ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
           ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
           UInt32 deviceInterfaceDetailDataSize,
           out UInt32 requiredSize,
           ref SP_DEVINFO_DATA deviceInfoData
        );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr deviceInfoSet,
            ref SP_DEVINFO_DATA deviceInfoData,
            uint property,
            out UInt32 propertyRegDataType,
            byte[] propertyBuffer,
            uint propertyBufferSize,
            out UInt32 requiredSize
            );


        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess,
            uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition,
            uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadFile(SafeFileHandle hFile, byte[] lpBuffer,
           uint nNumberOfBytesToRead, ref uint lpNumberOfBytesRead, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteFile(SafeFileHandle hFile, byte[] lpBuffer,
           uint nNumberOfBytesToWrite, ref uint lpNumberOfBytesWritten, IntPtr lpOverlapped);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern bool HidD_GetPreparsedData(
            SafeFileHandle hObject,
            ref IntPtr PreparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern Boolean HidD_FreePreparsedData(ref IntPtr PreparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern int HidP_GetCaps(
            IntPtr pPHIDP_PREPARSED_DATA,					// IN PHIDP_PREPARSED_DATA  PreparsedData,
            ref HIDP_CAPS myPHIDP_CAPS);				// OUT PHIDP_CAPS  Capabilities

        [DllImport("hid.dll", SetLastError = true)]
        private static extern Boolean HidD_GetAttributes(SafeFileHandle hObject, ref HIDD_ATTRIBUTES Attributes);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool HidD_GetFeature(
           IntPtr hDevice,
           IntPtr hReportBuffer,
           uint ReportBufferLength);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool HidD_SetFeature(
           IntPtr hDevice,
           IntPtr ReportBuffer,
           uint ReportBufferLength);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool HidD_GetProductString(
           SafeFileHandle hDevice,
           IntPtr Buffer,
           uint BufferLength);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool HidD_GetSerialNumberString(
           SafeFileHandle hDevice,
           IntPtr Buffer,
           uint BufferLength);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern Boolean HidD_GetManufacturerString(
            SafeFileHandle hDevice,
            IntPtr Buffer,
            uint BufferLength);

        [DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool HidD_GetNumInputBuffers(
            SafeFileHandle HidDeviceObject, 
            ref uint NumberBuffers);        

        #endregion

        #region structs

        public struct InterfaceDetails
        {
            public string manufacturer;
            public string product;
            public string serialNumber;
            public ushort VID;
            public ushort PID;
            public string devicePath;
            public string deviceDesc;
            public int IN_reportByteLength;
            public int OUT_reportByteLength;
            public ushort versionNumber;
        }

        // HIDP_CAPS
        [StructLayout(LayoutKind.Sequential)]
        private struct HIDP_CAPS
        {
            public System.UInt16 Usage;					// USHORT
            public System.UInt16 UsagePage;				// USHORT
            public System.UInt16 InputReportByteLength;
            public System.UInt16 OutputReportByteLength;
            public System.UInt16 FeatureReportByteLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
            public System.UInt16[] Reserved;				// USHORT  Reserved[17];			
            public System.UInt16 NumberLinkCollectionNodes;
            public System.UInt16 NumberInputButtonCaps;
            public System.UInt16 NumberInputValueCaps;
            public System.UInt16 NumberInputDataIndices;
            public System.UInt16 NumberOutputButtonCaps;
            public System.UInt16 NumberOutputValueCaps;
            public System.UInt16 NumberOutputDataIndices;
            public System.UInt16 NumberFeatureButtonCaps;
            public System.UInt16 NumberFeatureValueCaps;
            public System.UInt16 NumberFeatureDataIndices;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SP_DEVINFO_DATA
        {
            public uint cbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public IntPtr Reserved;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct SP_DEVICE_INTERFACE_DATA
        {
            public uint cbSize;
            public Guid InterfaceClassGuid;
            public uint Flags;
            public IntPtr Reserved;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public int cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HIDD_ATTRIBUTES 
        { 
            public Int32 Size; 
            public Int16 VendorID; 
            public Int16 ProductID; 
            public Int16 VersionNumber; 
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public UInt32 ReadIntervalTimeout;
            public UInt32 ReadTotalTimeoutMultiplier;
            public UInt32 ReadTotalTimeoutConstant;
            public UInt32 WriteTotalTimeoutMultiplier;
            public UInt32 WriteTotalTimeoutConstant;
        }

        #endregion

        public delegate void DataReceivedEvent(byte[] data);

        #region globals
        public string devicePath { get; set; }
        public bool deviceConnected { get; private set; }
        private SafeFileHandle handleRead;
        private SafeFileHandle handleWrite;
        private FileStream fsRead;
        private FileStream fsWrite;
        private HIDP_CAPS capabilities;
        public InterfaceDetails productInfo;
        public event DataReceivedEvent DataReceived;    //The calling class can subscribe to this event        
        public byte[] readData;

        public int WriteTimeout
        {
            get;
            set;
        }

        #endregion



        #region static_methods

        public static InterfaceDetails[] GetConnectedDevices(ushort vid, ushort pid)
        {
            InterfaceDetails[] devices = new InterfaceDetails[0];

            try
            {
                //Create structs to hold interface information
                SP_DEVINFO_DATA dd = new SP_DEVINFO_DATA();
                dd.cbSize = (uint)Marshal.SizeOf(dd);

                SP_DEVICE_INTERFACE_DATA dia = new SP_DEVICE_INTERFACE_DATA();            
                dia.cbSize = (uint)(Marshal.SizeOf(dia));

                Guid G = new Guid();
                HidD_GetHidGuid(ref G); //Get the guid of the HID device class

                IntPtr deviceInfoSet = SetupDiGetClassDevs(ref G, IntPtr.Zero, IntPtr.Zero, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);

                if(deviceInfoSet.ToInt32() == INVALID_HANDLE_VALUE.ToInt32())
                {
                    return devices;
                }

                //Loop through all available entries in the device list, until false
                SP_DEVICE_INTERFACE_DETAIL_DATA didd = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                if (IntPtr.Size == 8) // for 64 bit operating systems
                    didd.cbSize = 8;
                else
                    didd.cbSize = 4 + Marshal.SystemDefaultCharSize; // for 32 bit systems

                uint deviceIndex = 0;
                SafeFileHandle tempHandle;

                while (SetupDiEnumDeviceInterfaces(deviceInfoSet, IntPtr.Zero, ref G, deviceIndex++, ref dia))
                {
                    uint requiredSize = 0;
                    if(!SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref dia, ref didd, BUFFER_SIZE, out requiredSize, ref dd))
                    {
                        continue;
                    }

                    string controllerDeviceDesc = string.Empty;

                    UInt32 regType;
                    byte[] propertyBuffer = new byte[BUFFER_SIZE];
                    if (SetupDiGetDeviceRegistryProperty(deviceInfoSet, ref dd, (uint)SetupDiGetDeviceRegistryPropertyEnum.SPDRP_DEVICEDESC, out regType, propertyBuffer, BUFFER_SIZE, out requiredSize))
                    {
                        controllerDeviceDesc = Encoding.Unicode.GetString(propertyBuffer).TrimEnd('\0');
                    }

                    string devicePath = didd.DevicePath;

                    //create file handles using CT_CreateFile
                    tempHandle = CreateFile(devicePath, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE,
                        IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

                    if (tempHandle.IsInvalid)
                    {
                        tempHandle.Close();
                        continue;
                    }

                    //get capabilites - use getPreParsedData, and getCaps
                    //store the reportlengths
                    IntPtr ptrToPreParsedData = new IntPtr();
                    if (!HidD_GetPreparsedData(tempHandle, ref ptrToPreParsedData))
                    {
                        tempHandle.Close();
                        continue;
                    } 

                    HIDP_CAPS capabilities = new HIDP_CAPS();
                    HidP_GetCaps(ptrToPreParsedData, ref capabilities);

                    HIDD_ATTRIBUTES attributes = new HIDD_ATTRIBUTES();
                    bool hidAttribSucsess = HidD_GetAttributes(tempHandle, ref attributes);

                    if (!hidAttribSucsess || attributes.VendorID != vid || attributes.ProductID != pid)
                    {
                        HidD_FreePreparsedData(ref ptrToPreParsedData);
                        tempHandle.Close();
                        continue;
                    }

                    string productName = "";
                    string SN = "";
                    string manfString = "";
                    IntPtr buffer = Marshal.AllocHGlobal(126);//max alloc for string; 
                    if (HidD_GetProductString(tempHandle, buffer, 126)) productName = Marshal.PtrToStringAuto(buffer);
                    if (HidD_GetSerialNumberString(tempHandle, buffer, 126)) SN = Marshal.PtrToStringAuto(buffer);
                    if (HidD_GetManufacturerString(tempHandle, buffer, 126)) manfString = Marshal.PtrToStringAuto(buffer);
                    Marshal.FreeHGlobal(buffer);

                    //Call freePreParsedData to release some stuff
                    HidD_FreePreparsedData(ref ptrToPreParsedData);

                    //If connection was sucsessful, record the values in a global struct
                    InterfaceDetails productInfo = new InterfaceDetails();
                    productInfo.devicePath = devicePath;
                    productInfo.deviceDesc = controllerDeviceDesc;
                    productInfo.manufacturer = manfString;
                    productInfo.product = productName;
                    productInfo.serialNumber = SN;
                    productInfo.PID = (ushort)attributes.ProductID;
                    productInfo.VID = (ushort)attributes.VendorID;
                    productInfo.versionNumber = (ushort)attributes.VersionNumber;
                    productInfo.IN_reportByteLength = (int)capabilities.InputReportByteLength;
                    productInfo.OUT_reportByteLength = (int)capabilities.OutputReportByteLength;
                
                    int newSize = devices.Length + 1;
                    Array.Resize(ref devices, newSize);
                    devices[newSize - 1] = productInfo;

                    tempHandle.Close();
                }

                SetupDiDestroyDeviceInfoList(deviceInfoSet);
            }
            catch(Exception ex)
            {
                Tools.LogHelper.WriteDebug("HIDDevice.GetConnectedDevices()...Exception", ex);
            }

            return devices;
        }
        
        #endregion

        #region constructors
        /// <summary>
        /// Creates an object to handle read/write functionality for a USB HID device
        /// Uses one filestream for each of read/write to allow for a write to occur during a blocking
        /// asnychronous read
        /// </summary>
        /// <param name="devicePath">The USB device path - from getConnectedDevices</param>
        public HIDDevice()
        {
            this.devicePath = string.Empty;
            this.deviceConnected = false;
            this.WriteTimeout = DefaultWriteTimeout;
        }
        #endregion

        #region functions
        public bool InitDevice()
        {
            if (deviceConnected)
            {
                this.Close();
            }

            handleRead = CreateFile(devicePath, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE,
                IntPtr.Zero, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);

            handleWrite = CreateFile(devicePath, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE,
                IntPtr.Zero, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);

            bool isFailed = handleRead.IsInvalid || handleWrite.IsInvalid;

            if (isFailed)
            {
                if (!handleRead.IsInvalid)
                {
                    handleRead.Close();
                }

                if (!handleWrite.IsInvalid)
                {
                    handleWrite.Close();
                }

                return false;
            }

            //get capabilites - use getPreParsedData, and getCaps
            //store the reportlengths
            IntPtr ptrToPreParsedData = new IntPtr();
            bool ppdSucsess = HidD_GetPreparsedData(handleRead, ref ptrToPreParsedData);

            capabilities = new HIDP_CAPS();
            int hidCapsSucsess = HidP_GetCaps(ptrToPreParsedData, ref capabilities);

            HIDD_ATTRIBUTES attributes = new HIDD_ATTRIBUTES();
            bool hidAttribSucsess = HidD_GetAttributes(handleRead, ref attributes);

            string productName = string.Empty;
            string SN = string.Empty;
            string manfString = string.Empty;            
            IntPtr buffer = Marshal.AllocHGlobal(126);//max alloc for string; 
            if (HidD_GetProductString(handleRead, buffer, 126)) productName = Marshal.PtrToStringAuto(buffer);
            if (HidD_GetSerialNumberString(handleRead, buffer, 126)) SN = Marshal.PtrToStringAuto(buffer);
            if (HidD_GetManufacturerString(handleRead, buffer, 126)) manfString = Marshal.PtrToStringAuto(buffer);            
            Marshal.FreeHGlobal(buffer);

            //Call freePreParsedData to release some stuff
            HidD_FreePreparsedData(ref ptrToPreParsedData);
            //SetupDiDestroyDeviceInfoList(i);

            deviceConnected = true;

            //If connection was sucsessful, record the values in a global struct
            productInfo = new InterfaceDetails();
            productInfo.devicePath = devicePath;
            productInfo.manufacturer = manfString;
            productInfo.product = productName;
            productInfo.serialNumber = SN;
            productInfo.PID = (ushort)attributes.ProductID;
            productInfo.VID = (ushort)attributes.VendorID;
            productInfo.versionNumber = (ushort)attributes.VersionNumber;
            productInfo.IN_reportByteLength = (int)capabilities.InputReportByteLength;
            productInfo.OUT_reportByteLength = (int)capabilities.OutputReportByteLength;

            //use a filestream object to bring this stuff into .NET
            fsRead = new FileStream(handleRead, FileAccess.ReadWrite, capabilities.InputReportByteLength, false);
            fsWrite = new FileStream(handleWrite, FileAccess.ReadWrite, capabilities.OutputReportByteLength, false);

            BeginRead();

            return true;
        }

        public void Close()
        {
            if (fsRead != null)
            {
                fsRead.Close();
            }
                
            if (fsWrite != null)
            {
                fsWrite.Close();
            }
                
            
            if ((handleRead != null) && (!(handleRead.IsInvalid)))
            {
                handleRead.Close();
            }
                
            if ((handleWrite != null) && (!(handleWrite.IsInvalid)))
            {
                handleWrite.Close();
            }

            this.deviceConnected = false;
        }

        public void Write(byte[] data)  
        {
            if (data.Length > capabilities.OutputReportByteLength)
                throw new Exception("Output report must not exceed " + (capabilities.OutputReportByteLength - 1).ToString() + " bytes");

            //uint numBytesWritten = 0;
            byte[] packet = new byte[capabilities.OutputReportByteLength];
            Array.Copy(data, 0, packet, 1, data.Length);            //start at 1, as the first byte must be zero for HID report
            packet[0] = 0;

            try
            {
                if (fsWrite.CanWrite)
                {
                    IAsyncResult result = fsWrite.BeginWrite(packet, 0, packet.Length, OutputReportDataHandler, packet);
                    result.AsyncWaitHandle.WaitOne(WriteTimeout, false);
                    if(!result.IsCompleted)
                    {
                        fsWrite.Close();
                    }
                    //fsWrite.Write(packet, 0, packet.Length);
                }
            }
            catch(Exception ex)
            {
                Tools.LogHelper.WriteDebug("HIDDevice.Write()...Exception", ex);
            }
        }

        //This read function will be used with asychronous operation, called by the constructor if async reads are used
        private void BeginRead()
        {
            if (readData == null || readData.Length != capabilities.InputReportByteLength)
            {
                readData = new byte[capabilities.InputReportByteLength];
            }
            
            if (fsRead.CanRead)
                fsRead.BeginRead(readData, 0, readData.Length, new AsyncCallback(InputReportDataHandler), readData);
            else
                throw new Exception("Device is unable to read");
        }

        private void InputReportDataHandler(IAsyncResult ar)
        {
            try
            {
                if (!fsRead.CanRead)
                {
                    return;
                }

                int numberOfReadBytes = fsRead.EndRead(ar); //must call an endread before starting another one
                //TODO handle exception with PCB is reaet

                //Reset the read thread to read the next report
                byte[] buffer = (byte[])ar.AsyncState;
                byte[] payloadData = new byte[buffer.Length - 1];
                Buffer.BlockCopy(buffer, 1, payloadData, 0, payloadData.Length);

                //Log.OutputLine("InputReportDataHandler() thread id:{0}", Thread.CurrentThread.ManagedThreadId);

                if (DataReceived != null)
                {
                    DataReceived(payloadData);
                }

                if (fsRead.CanRead)
                {
                    fsRead.BeginRead(readData, 0, readData.Length, new AsyncCallback(InputReportDataHandler), readData);
                }
            }
            catch (Exception ex)
            {
                Tools.LogHelper.WriteDebug("HIDDevice.InputReportDataHandler()...Exception", ex);
                //Log.OutputLine(ex.Message);
            }
        }

        // 处理客户端连接的函数
        private void OutputReportDataHandler(IAsyncResult ar)
        {
            try
            {
                fsWrite.EndWrite(ar);
            }
            catch (Exception ex)
            {
                //Log.OutputLine("HandleTcpServerConnected:" + ex.Message);
                Tools.LogHelper.WriteDebug("HIDDevice.OutputReportDataHandler()...Exception", ex);
                return;
            }
        }
        #endregion
    }
}