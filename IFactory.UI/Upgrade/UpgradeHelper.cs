using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace IFactory.UI.Upgrade
{
    public class UpgradeHelper
    {
        public static void CheckNewVersion()
        {
            try
            {
                string strB = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater\\version.xml")) 
                    && XDocument.Load(
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater\\version.xml")
                        ).Root.Element("versionCode").Value.CompareTo(strB) > 0)
                {
                    ProcessStartInfo info = new ProcessStartInfo
                    {
                        FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Updater\IFactory.UI.Updater.exe"),
                        Arguments = Process.GetCurrentProcess().Id.ToString()
                    };
                    new Process { StartInfo = info }.Start();
                    Environment.Exit(0);

                }
            }
            catch (Exception)
            {
            }
        }
    }
}
