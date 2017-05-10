using Core.Services;
using DAL;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DbProductModelContextInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // logger settings
            LoggingConfiguration loggingConfig = new LoggingConfiguration();

            var logDirPath = ConfigurationManager.AppSettings["logDirPath"];
            // create the directory if it doesn't exist
            if (!Directory.Exists(logDirPath))
            {
                Directory.CreateDirectory(logDirPath);
            }

            // file logging - always on
            FileTarget fileTarget = new FileTarget();
            fileTarget.Layout = "${longdate} ${logger} ${level} ${message} ${exception}";
            fileTarget.FileName = Path.Combine(logDirPath, "${logger}-${shortdate}.log");
            fileTarget.AutoFlush = true;
            fileTarget.ConcurrentWrites = true;
            fileTarget.MaxArchiveFiles = 0;
            fileTarget.DeleteOldFileOnStartup = false;
            fileTarget.EnableFileDelete = false;
            loggingConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, fileTarget));
            loggingConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Warn, fileTarget));
            loggingConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, fileTarget));

            LogManager.Configuration = loggingConfig;

            MailService.Start();
        }
    }
}
