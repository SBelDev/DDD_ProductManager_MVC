using Core.Mail;
using Core.Services;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Core.Services
{
    public class MailService
    {
        private static Thread thread;

        public static void Start()
        {
            try
            {
                ExtensionLogger.Info("Mail Job started.");
                thread = new Thread(MailJob.Start);
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                ExtensionLogger.Error("An unhandled exception occured on start mail job", ex);
            }
        }

        public static void Stop()
        { }
    }
}
