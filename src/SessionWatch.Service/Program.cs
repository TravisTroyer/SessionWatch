using System;
using System.ServiceProcess;
using SessionWatch.Service.Installation;

namespace SessionWatch.Service
{
   static class Program
   {
      internal static void Main()
      {
         if (Environment.UserInteractive)
         {
            Install();
            return;
         }

         Run();
      }

      private static void Install()
      {
         var installer = new ConsoleInstaller();

         installer.Prompt();
      }

      private static void Run()
      {
         var services = new ServiceBase[] 
            { 
                new SessionWatchService() 
            };

         ServiceBase.Run(services);
      }
   }
}
