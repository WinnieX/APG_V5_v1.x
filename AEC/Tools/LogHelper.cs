using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;

namespace AEC.Tools
{
    public class LogHelper
    {
        private const string process_id = "pid";

        private const string LOG_NAME = "AEC_Log";

        private const string LOG_PATTERN = "%date [p: %property{pid}] [t: %thread] %-5level - %message%newline";
       
        private static readonly ILog log = null;

        static LogHelper()
        {
            GlobalContext.Properties[process_id] = System.Diagnostics.Process.GetCurrentProcess().Id;

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Name = LOG_NAME;

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = LOG_PATTERN;
            patternLayout.ActivateOptions();

            if(AppConfig.isLogOutputToConsole)
            {
                ColoredConsoleAppender coloredConsoleAppender = new ColoredConsoleAppender();
                coloredConsoleAppender.Layout = patternLayout;
                coloredConsoleAppender.ActivateOptions();
                hierarchy.Root.AddAppender(coloredConsoleAppender);
            }

            RollingFileAppender rollingFileAppender = new RollingFileAppender();
            rollingFileAppender.AppendToFile = true;
            rollingFileAppender.File = @"Logs\\AEC.log";
            //roller.File = @"Logs\\";
            //roller.DatePattern = "yyyy-MM-dd HHmmss'.log'";
            rollingFileAppender.Layout = patternLayout;
            //roller.StaticLogFileName = false;
            rollingFileAppender.StaticLogFileName = true;
            rollingFileAppender.MaxSizeRollBackups = 8;
            //roller.MaxSizeRollBackups = 4;
            rollingFileAppender.MaximumFileSize = "16MB";
            //roller.MaximumFileSize = "16KB";
            rollingFileAppender.RollingStyle = RollingFileAppender.RollingMode.Size;
            rollingFileAppender.ActivateOptions();
            hierarchy.Root.AddAppender(rollingFileAppender);

            hierarchy.Root.Level = AppConfig.isOutputDebugLog ? Level.Debug : Level.Info;
            hierarchy.Configured = true;

            log = log4net.LogManager.GetLogger(LOG_NAME);
        }

        public static void FlushBuffers()
        {
            try
            {
                log4net.LogManager.Flush(128);
                //if(rollingFileAppender != null)
                //{
                //    rollingFileAppender.Flush(128);
                //}
            }
            catch(Exception )
            {

            }
        }

        public static void WriteDebug(string message)
        {
            if(log != null)
            {
                log.Debug(message);
            }
        }

        public static void WriteDebug(string format, params object[] arg)
        {
            if (log != null)
            {
                log.DebugFormat(format, arg);
            }
        }

        public static void WriteDebug(string message, Exception ex)
        {
            if (log != null)
            {
                log.Debug(message, ex);
            }
        }

        public static void WriteInfo(string message)
        {
            if (log != null)
            {
                log.Info(message);
            }
        }

        public static void WriteInfo(string format, params object[] arg)
        {
            if (log != null)
            {
                log.InfoFormat(format, arg);
            }
        }

        public static void WriteInfo(string message, Exception ex)
        {
            if (log != null)
            {
                log.Info(message, ex);
            }
        }
    }
}
