using System;
using System.Linq;
using System.Management;

namespace SessionWatch.Service
{
   internal class Machine
   {      
      private static readonly Lazy<Machine> _machine;

      static Machine()
      {
         _machine = new Lazy<Machine>(() => new Machine(), true);
      }

      private Machine()
      {
      }

      public static Machine Current
      {
         get { return _machine.Value;}
      } 

      public String GetUsername()
      {         
         try
         {
            var scope = new ManagementScope("\\\\.\\root\\cimv2");

            scope.Connect();

            var query = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            var searcher = new ManagementObjectSearcher(scope, query);

            var results = searcher.Get()
               .Cast<ManagementObject>()
               .First();

            var result = results["UserName"]
               .ToString();
         
            return result;
            
         }
         catch (Exception)
         {            
            return null;
         }
      }
   }
}
