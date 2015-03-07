using System.ServiceProcess;
using SessionWatch.Service.Properties;
using SessionWatch.Service.Session;

namespace SessionWatch.Service
{
   public partial class SessionWatchService : ServiceBase
   {
      private SessionWatcher _sessionWatcher;

      public SessionWatchService()
      {
         InitializeComponent();

         ServiceName = Resources.ServiceName;
         AutoLog = true;
      }

      protected override void OnStart(string[] args)
      {
         _sessionWatcher = new SessionWatcher();
      }

      protected override void OnStop()
      {
         _sessionWatcher = null;
      }

      protected override void OnSessionChange(SessionChangeDescription changeDescription)
      {
         base.OnSessionChange(changeDescription);

         var watcher = _sessionWatcher;

         if (watcher == null) return;

         watcher.OnSessionChanged(changeDescription);
      }
   }
}
