using System;
using System.Diagnostics;
using System.ServiceProcess;
using SessionWatch.Service.Properties;

namespace SessionWatch.Service.Session
{
   internal sealed class SessionWatcher
   {
      private string _lastUser;

      public void OnSessionChanged(SessionChangeDescription description)
      {
         // NOTE: this is obviously crude, but accomplishes my goal for now
         // TODO: load configuration and respond accordingly

         var currentUser = Machine.Current.GetUsername();
         
         switch(description.Reason)
         {
            case SessionChangeReason.ConsoleConnect:
            case SessionChangeReason.SessionLogon:
            case SessionChangeReason.SessionUnlock:
               
               if (!String.IsNullOrWhiteSpace(_lastUser) && currentUser != _lastUser)
               {
                  KillSteam();                  
               }

               break;
         }

         _lastUser = currentUser;
      }

      private static void KillSteam()
      {
         try
         {
            EventLog.WriteEntry(Resources.ServiceName, "Attempting to perform taskkill.", EventLogEntryType.Information);

            Process.Start("taskkill", "/F /IM steam*");
         }
         catch(Exception e)
         {
            EventLog.WriteEntry(Resources.ServiceName, "Error running taskkill.\n\n" + e, EventLogEntryType.Error);
         }
      }
   }
}
