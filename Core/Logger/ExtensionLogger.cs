using NLog;
using System;

namespace Core
{
    public class ExtensionLogger
    {
        private static Logger logger = LogManager.GetLogger("Application");

        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public static void Fatal(string message, string description)
        {
            logger.Fatal(message + ": " + description);
        }

        public static void Fatal(string message, Exception exception)
        {
            logger.Fatal(message + ": " + exception);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Error(string message, string description)
        {
            logger.Error(message + ": " + description);
        }

        public static void Error(string message, Exception exception)
        {
            logger.Error(message + ": " + exception);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Warn(string message, string description)
        {
            logger.Warn(message + ": " + description);
        }

        public static void Warn(string message, Exception exception)
        {
            logger.Warn(message + ": " + exception);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Info(string message, string description)
        {
            logger.Info(message + ": " + description);
        }

        public static void Info(string message, Exception exception)
        {
            logger.Info(message + ": " + exception);
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Debug(string message, string description)
        {
            logger.Debug(message + ": " + description);
        }

        public static void Debug(string message, Exception exception)
        {
            logger.Debug(message + ": " + exception);
        }

        public static void Trace(string message)
        {
            logger.Trace(message);
        }

        public static void Trace(string message, string description)
        {
            logger.Trace(message + ": " + description);
        }

        public static void Trace(string message, Exception exception)
        {
            logger.Trace(message + ": " + exception);
        }
    }
}
