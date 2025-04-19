using System;
using System.ServiceProcess;

namespace BackupService
{
    static class Program
    {
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                // Console mode (debug/testing)
                var service = new BackupService();
                service.StartDebug(); // custom method you'll define in BackupService
                Console.WriteLine("Press Enter to stop...");
                Console.ReadLine();
                service.StopDebug(); // simulate OnStop
            }
            else
            {
                // Windows Service mode
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new BackupService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
