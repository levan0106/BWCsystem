using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Core.Common
{
    public static class LogManager
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(LogManager));
        public static void Configurator()
        {
            XmlConfigurator.Configure();
        }
        public static void LogDebug(object ex)
        {
            log.Debug(ex);
        }
        public static void LogDebug(object message, Exception exception)
        {
            log.Debug(message, exception);
        }
        public static void LogError(object ex)
        {
            log.Error(ex);
        }
        public static void LogError(object message, Exception exception)
        {
            log.Error(message,exception);
        }

        public static void LogWarning(object ex)
        {
            log.Warn(ex);
        }
        public static void LogWarning(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public static void LogInfo(object ex)
        {
            log.Info(ex);
        }
        public static void LogInfo(object message, Exception exception)
        {
            log.Info(message, exception);
        }

        public static void LogFatal(object ex)
        {
            log.Fatal(ex);
        }
        public static void LogFatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }
    }
}
