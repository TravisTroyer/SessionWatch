using System.ComponentModel;
using System.Configuration.Install;
using System.Reflection;

namespace SessionWatch.Service.Installation
{
   [RunInstaller(true)]
   public partial class SessionWatchInstaller : Installer
   {
      public SessionWatchInstaller()
      {
         InitializeComponent();
      }

      public static void Install()
      {
         var installer = GetAssemblyInstaller();

         installer.Install(null);
         installer.Commit(null);
      }

      public static void Uninstall()
      {
         var installer = GetAssemblyInstaller();
         installer.Uninstall(null);
      }

      private static AssemblyInstaller GetAssemblyInstaller()
      {
         var assembly = Assembly.GetExecutingAssembly().Location;
         var installer = new AssemblyInstaller(assembly, null);

         installer.UseNewContext = true;

         return installer;
      }
   }
}