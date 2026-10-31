using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AEC.Tools;
using AEC.Core;

namespace SuPCodeGenerator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string mengAudio = "M-Audio";
        private DispatcherTimer timer = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtDeviceCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.txtDeviceCode.Text.Length == this.txtDeviceCode.MaxLength)
            {
                string permissionCode = Utils.ByteArrayToHexStr(HashHelper.MD5(Encoding.UTF8.GetBytes(mengAudio + this.txtDeviceCode.Text)), string.Empty);

                this.txtPermissionCode.Text = permissionCode;
            }
            else
            {
                this.txtPermissionCode.Clear();
            }
        }

        private void UpdateTimeLabel()
        {
            this.txtTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.UpdateTimeLabel();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.UpdateTimeLabel();
            if(this.timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(128);
                timer.Tick += timer_Tick;
                timer.Start();
            }
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(this.timer != null)
            {
                timer.Stop();
            }
        }
    }
}
