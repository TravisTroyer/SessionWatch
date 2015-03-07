using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using SessionWatch.Service.Properties;

namespace SessionWatch.Service.Session
{
   internal sealed class SessionWatcher
   {
      private int _lastSessionId;

      public void OnSessionChanged(SessionChangeDescription description)
      {
         // NOTE: this is obviously crude, but accomplishes my goal for now
         // TODO: load configuration and respond accordingly

         LogSessionChanged(description, description.SessionId, _lastSessionId);

         switch (description.Reason)
         {
            case SessionChangeReason.SessionLogon:
               // This is a new session, so any other Steam instances
               // must belong to a different user.
               KillSteam();
               break;

            case SessionChangeReason.ConsoleConnect:
            case SessionChangeReason.RemoteConnect:
            case SessionChangeReason.SessionUnlock:
               if (_lastSessionId != description.SessionId)
               {
                  KillSteam();
               }
               break;
         }

         _lastSessionId = description.SessionId;
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

      private void LogSessionChanged(SessionChangeDescription description, int currentionSessionId, int lastSessionId)
      {
         var machine = new Machine();

         var message = new StringBuilder("Session change detected.\n");
         message.Append(string.Format("Reason:         \t{0}\n", description.Reason));
         message.Append(string.Format("New Session ID: \t{0}\n", currentionSessionId));
         message.Append(string.Format("Last Session ID:\t{0}\n", lastSessionId));
         message.Append(string.Format("Current User   :\t{0}\n", machine.GetUsername()));

         EventLog.WriteEntry(Resources.ServiceName, message.ToString(), EventLogEntryType.Information);
      }
   }
}
