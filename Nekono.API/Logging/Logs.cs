using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nekono.API.Logging
{
    public class Logs
    {
        public static void Write(string content, LogStatus status, string path)
        {
            var statusString = "";
            var filePath = $"{path}NekonoAPI-{DateTime.Now:yyyyMMdd}.txt";

            switch (status)
            {
                case LogStatus.Info:
                    statusString = "INF";
                    break;
                case LogStatus.Request:
                    statusString = "REQ";
                    break;
                case LogStatus.Response:
                    statusString = "RES";
                    break;
                case LogStatus.Error:
                    statusString = "ERR";
                    break;
                default:
                    break;
            }

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(string.Format("{0}:{1} {2}", statusString, DateTime.Now, content));
            }
        }

        public enum LogStatus
        {
            Info,
            Request,
            Response,
            Error
        }
    }
}
