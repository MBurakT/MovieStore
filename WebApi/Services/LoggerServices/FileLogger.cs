using System;
using System.IO;
using System.Text;

namespace WebApi.Services.LoggerServices;

public class FileLogger : IFileLoggerService
{
    public void Log(string message)
    {
        string time = DateTime.Now.ToString("yyyyMMdd");

        string path = $"{Environment.CurrentDirectory}\\Logs\\{time}";

        Directory.CreateDirectory(path);

        path = $"{path}\\{time}.txt";

        using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None))
        {
            byte[] text = new UTF8Encoding().GetBytes(message);

            fs.Flush();
            fs.Write(text);
            fs.Flush();
        }
    }
}