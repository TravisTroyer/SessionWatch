using System;
using System.Diagnostics;
using System.ServiceProcess;
using SessionWatch.Service.Properties;

namespace SessionWatch.Service.Session
{
   internal sealed class SessionWatcher
   {
      public void OnSessionChanged(SessionChangeDescription changeDescription)
      {
         // NOTE: this is obviously crude, but accomplishes my goal for now
         // TODO: load configuration and respond accordingly
         switch(changeDescription.Reason)
         {
            case SessionChangeReason.ConsoleConnect:
            case SessionChangeReason.SessionLogon:
            case SessionChangeReason.SessionUnlock:
               KillSteam();
               break;
         }
      }

      private void KillSteam()
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
