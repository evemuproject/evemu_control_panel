using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evemu_DB_Editor.src
{
	static class LogSystem
	{
        public static void addLog(string logText)
        {
            DateTime time = DateTime.Now;
            string sec = time.Second.ToString();
            string min = time.Minute.ToString();
            string hour = time.Hour.ToString();
            string day = time.Day.ToString();
            string month = time.Month.ToString();
            string year = time.Year.ToString();

            if (int.Parse(sec) < 10)
            {
                sec = "0" + sec;
            }

            if (int.Parse(min) < 10)
            {
                min = "0" + min;
            }

            if (int.Parse(hour) < 10)
            {
                hour = "0" + hour;
            }
            if (!System.IO.Directory.Exists(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\"))
            {
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\");
            }
            // Just a test, even though i think CreateDirectory checks if the directory is there anyways...
            //System.IO.Directory.CreateDirectory(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\");

            System.IO.FileInfo fi = new System.IO.FileInfo(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\log.log");
            System.IO.StreamWriter appendLog = fi.AppendText();
            string logInfo = "[Log][" + day + "/" + month + "/" + year + " :: " + hour + ":" + min + ":" + sec + "]: \"" + logText + "\"\n";
            appendLog.WriteLine(logInfo);
            appendLog.Close();
        }
	}
}
