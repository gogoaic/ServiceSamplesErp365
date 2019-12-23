using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataConsoleApplication.Common
{
    public static class LogHelper
    {
        const string PATH_ERR = "ErrorLog.log";

        public static void WriteErrorLog(string str)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(PATH_ERR, true))
            {
                DateTime dt = DateTime.Now;
                file.Write("[" + dt.ToString("dd.MM.yyyy HH:mm:ss") + "] ");
                file.WriteLine(str);
            }
        }

        const string PATH_DEV = "DevLog.log";
        public static void WriteDevLog(string str)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(PATH_DEV, true))
            {
                DateTime dt = DateTime.Now;
                file.Write("[" + dt.ToString("dd.MM.yyyy HH:mm:ss") + "] ");
                file.WriteLine(str);
            }
        }

        public static void WriteLog(string file_path, string str)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(file_path, true))
            {
                DateTime dt = DateTime.Now;
                file.Write("[" + dt.ToString("dd.MM.yyyy HH:mm:ss") + "] ");
                file.WriteLine(str);
            }
        }
        public static void WriteLogAndPrint(string str, string file_path)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(file_path, true))
            {
                Console.WriteLine(str);
                DateTime dt = DateTime.Now;
                file.Write("[" + dt.ToString("dd.MM.yyyy HH:mm") + "] ");
                file.WriteLine(str);
            }
        }

        public static void WriteLogAndPrint(string str)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(PATH_ERR, true))
            {
                Console.WriteLine(str);
                DateTime dt = DateTime.Now;
                file.Write("[" + dt.ToString("dd.MM.yyyy HH:mm") + "] ");
                file.WriteLine(str);
            }
        }
    }
}
