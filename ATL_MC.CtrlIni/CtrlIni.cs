using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.CtrlIni
{
    public class CtrlIni
    {
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string GetANDONIni(string section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", temp, 1024, @"D:/ANDON/ANDON.INI");
            return temp.ToString();
        }

        public void SetStopTypeIni(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, @"D:/ANDON/StopType.ini");
        }

        public void SetANDONIni(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, @"D:/ANDON/ANDON.INI");
        }


        public string GetIni(string path, string section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", temp, 1024, path);
            return temp.ToString();
        }

        public void SetIni(string path, string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
        }
    }
}
