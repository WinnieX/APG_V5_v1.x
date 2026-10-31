using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using AEC.Core;

namespace AEC
{
    class Program
    {
        public static System.Threading.Mutex mutex = null;
        public static string startupPath = string.Empty;

        [STAThread]
        static void Main(string[] args)
        {
            startupPath = string.Join(" ", args);
            //Console.WriteLine(startupPath);
            if (!System.IO.Directory.Exists(startupPath))
            {
                return;
            }
            mutex = new System.Threading.Mutex(true, AEC.ProductsConfig.AppName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Tools.LogHelper.WriteInfo("M-Audio AEC Startup.");
            //AEC.Test.PrintTest.PointNow("启动");

            AEC.MController mController = new MController(startupPath);

            //AEC.Test.PrintTest.PointNow("初始化完成");
            //UI.V5.FormProMode form = new UI.V5.FormProMode(v5);
            //UI.V5.FormEasyMode form = new UI.V5.FormEasyMode(v5);
            //UI.FormMain form = new UI.FormMain(mController);

            Form form = null;
            //form = new UI.Forms.FormFirmwareUpdate();
            //Form form = new UI.FormCentralControl();
            //form = new UI.Forms.FormOnlineHelp();
            //form = new AEC.UI.Forms.FormDspProgrammer();
            //form = new AEC.UI.Forms.FormExportDspFirmwarePackage();
            //form = new UITest.FormTest();
            //form = new AEC.UI.Forms.FormExportFirmwarePackage();
            //form = new AEC.UI.UsageLimitLocks.FormUsageLimitLockManagement();

            if (AppConfig.isShowMainForm)
            {
                form = new UI.FormMain(mController);
            }
            else
            {
                form = new UI.FormProMode(mController);
            }
            Application.Run(form);
            //RemoteControl.Protocol.Test.TestFrameEncoderAndFrameDecoder();
            //Application.Run(new AEC.UITest.FormTest());

            mutex.Close();

            Tools.LogHelper.WriteInfo("M-Audio AEC Exit.");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            Tools.LogHelper.WriteInfo(e.ExceptionObject.ToString());
            MessageBox.Show(e.ExceptionObject.ToString(), "M-Audio AEC");
        }
    }
}
