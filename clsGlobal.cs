using System;
using System.IO;

namespace FileMonitoringService
{
    public static class clsGlobal
    {
        /// <summary>
        /// This procedure logs a certain message in the log file of this service.
        /// </summary>
        /// <param name="Message">The message to be logged in the log file.</param>
        public static void LogMessage(string Message, string LogFile)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {Message}\n";
            File.AppendAllText(LogFile, logMessage);
            Console.WriteLine(logMessage); // If in debug mode, the log message will be displayed on the console, for better debugging.
        }

        /// <summary>
        /// This method replaces a certain file with GUID.
        /// The file's path sould be provided to return the full path with the GUID.
        /// </summary>
        /// <param name="FilePath">The file's whole path.</param>
        /// <returns>The file's whole path replaced with the GUID.</returns>
        /// <exception cref="Exception">Throws an error if the file's path does not exists.</exception>
        public static string ReplaceFileWithGUID(string FilePath, string LogFile)
        {
            string FileName = Path.GetFileName(FilePath);
            FilePath = FilePath.Substring(0, FilePath.IndexOf(FileName) + 1); // Extracting the path without the file name.
            FileName = FileName.Replace(FileName, Guid.NewGuid().ToString()); // Replacing the file name with GUID
            return FilePath += FileName; // Returning the full path, with the new file name (replaced with GUID).
        }
    }
}
