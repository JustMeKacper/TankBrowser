using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBrowser.MVVM.Model
{
    internal class Logger
    {
        public static void logMessage(string message, Exception ex = null)
        {
            if (ex == null)
            {
                string allLines = "";
                var log = $"{DateTime.Now}:\t{message}";
                if (File.Exists(@"..\logdir\LogFile.txt"))
                    File.AppendAllText(@"..\logdir\LogFile.txt",$"\n{log}");
                else
                    File.WriteAllText(@"..\logdir\LogFile.txt", log);
            }
            else
            {
                var log = $"{DateTime.Now}\t\t{message}. Exceptoin data = {ex}";
                if (File.Exists(@"..\logdir\LogFile.txt"))
                    File.AppendAllText(@"..\logdir\LogFile.txt", $"\n{log}");
                else
                    File.WriteAllText(@"..\logdir\LogFile.txt", log);
            }
        }
    }
}
