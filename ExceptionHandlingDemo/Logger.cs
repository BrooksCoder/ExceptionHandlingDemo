using System;
using System.IO;

namespace ExceptionHandlingDemo
{
    public class Logger
    {
        private static Logger _instance;
        private static readonly object _lock = new object();
        private string logFilePath = "log.txt"; // File where logs are written

        private Logger() { }

        // Get the Singleton instance
        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }

        // Method to log exceptions
        public void LogException(Exception ex)
        {
            lock (_lock)
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"Date: {DateTime.Now}");
                    writer.WriteLine($"Exception: {ex.Message}");
                    writer.WriteLine($"Stack Trace: {ex.StackTrace}");
                    writer.WriteLine("-----------------------------------");
                }
            }
        }
    }

}
