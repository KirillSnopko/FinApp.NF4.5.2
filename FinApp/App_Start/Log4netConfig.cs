using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinApp.App_Start
{
    public class Log4netConfig
    {
        public static void configure()
        {
            //output view
            log4net.Appender.TraceAppender trace = new log4net.Appender.TraceAppender();
            trace.Layout = new log4net.Layout.PatternLayout("%-5level %date{HH:mm:ss,fff} " +
                                                          "[%thread] %logger (%file:%line) " +
                                                          "%message%newline%newline");
            log4net.Config.BasicConfigurator.Configure(trace);
            trace.ActivateOptions();

            //log to file in runtime info level
            log4net.Appender.RollingFileAppender info = new log4net.Appender.RollingFileAppender();
            info.AppendToFile = true;
            info.File = "LogRuntimeInfo.log";
            info.MaxFileSize = 4096000;
            info.MaxSizeRollBackups = 2;
            info.Threshold = log4net.Core.Level.Info;
            info.Layout = new log4net.Layout.PatternLayout("%-5level %date{HH:mm:ss,fff} " +
                                   "[%thread] %logger%newline%message%newline%newline");
            log4net.Config.BasicConfigurator.Configure(info);
            info.ActivateOptions();

            //log to file in runtime error level
            log4net.Appender.RollingFileAppender error = new log4net.Appender.RollingFileAppender();
            error.AppendToFile = true;
            error.File = "LogRuntimeError.log";
            error.MaxFileSize = 4096000;
            error.MaxSizeRollBackups = 2;
            error.Threshold = log4net.Core.Level.Error;
            error.Threshold = log4net.Core.Level.Fatal;
            error.Threshold = log4net.Core.Level.Warn;
            error.Layout = new log4net.Layout.PatternLayout("%-5level %date{HH:mm:ss,fff} " +
                                               "[%thread] %logger" +
                                               "%newline%message%newline%newline");
            log4net.Config.BasicConfigurator.Configure(error);
            error.ActivateOptions();

            log4net.Repository.Hierarchy.Hierarchy h =
        (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
            log4net.Repository.Hierarchy.Logger rootLogger = h.Root;
            rootLogger.Level = log4net.Core.Level.All;
        }
    }
}