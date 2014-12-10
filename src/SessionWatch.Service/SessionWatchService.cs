using System.ServiceProcess;
using SessionWatch.Service.Properties;
using SessionWatch.Service.Session;

namespace SessionWatch.Service
{
   public partial class SessionWatchService : ServiceBase
   {
      public SessionWatchService()
      {
         InitializeComponent();

         ServiceName = Resources.ServiceName;
         AutoLog = true;
      }

      protected override void OnStart(string[] args)
      {
      }

      protected override void OnStop()
      {
      }

      protected override void OnSessionChange(SessionChangeDescription changeDescription)
      {
         base.OnSessionChange(changeDescription);
         
         var watcher = new SessionWatcher();
         watcher.OnSessionChanged(changeDescription);
      }
   }
}
