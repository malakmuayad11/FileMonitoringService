using System;
using System.ServiceProcess;

namespace FileMonitoringService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Handle debugging mode of this service.
            if (Environment.UserInteractive)
            {
                Console.WriteLine("Running in console mode.");
                FileMonitoringService service = new FileMonitoringService();
                service.StartInConsole();
            }
            else
            {
                // Service is deployed.
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new FileMonitoringService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
