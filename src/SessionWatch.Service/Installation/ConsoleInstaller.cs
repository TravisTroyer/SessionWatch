using System;

namespace SessionWatch.Service.Installation
{
   internal class ConsoleInstaller
   {
      public void Prompt()
      {
         try
         {
            do
            {
               Console.Clear();
               WriteOptions();
            } while (!TryHandleInput());
         }
         catch (Exception ex)
         {
            var message = string.Concat("Error attempting to run installer:", Environment.NewLine, ex);
            Write(message);
            PromptToContinue();
         }
      }

      private void WriteOptions()
      {
         // TODO: detect whether service is installed
         // TODO: detect whether user is admin

         Write("Select an option to continue (install/uninstall requires administrator privileges):");
         Write("[I]nstall Service");
         Write("[U]ninstall Service");
         Write("[Q]uit");
      }

      private bool TryHandleInput()
      {
         var key = ReadKey();

         switch(key)
         {
            case ConsoleKey.I:
               SessionWatchInstaller.Install();
               PromptToContinue();
               return true;

            case ConsoleKey.U:
               SessionWatchInstaller.Uninstall();
               PromptToContinue();
               return true;

            case ConsoleKey.Q:
               return true;
         }

         return false;
      }

      private void PromptToContinue()
      {
         Write("Press any key to continue.");
         ReadKey();
      }

      private ConsoleKey ReadKey()
      {
         return Console.ReadKey(true).Key;
      }

      private void Write(string format, params object[] args)
      {
         Console.WriteLine(format, args);
      }
   }
}
