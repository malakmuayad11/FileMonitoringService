using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace FileMonitoringService
{
    public partial class FileMonitoringService : ServiceBase
    {
        private string _SourceFolder = ConfigurationManager.AppSettings["SourceFolder"];
        private string _DestinationFolder = ConfigurationManager.AppSettings["DestinationFolder"];
        private string _LogFile = ConfigurationManager.AppSettings["LogFile"];
        private FileSystemWatcher _Watcher;

        public FileMonitoringService()
        {
            InitializeComponent();
            _Watcher = new FileSystemWatcher(_SourceFolder);
            _Watcher.NotifyFilter = NotifyFilters.FileName;
            _Watcher.EnableRaisingEvents = true;
            _Watcher.Created += _Watcher_Created;
        }
        /// <summary>
        /// If a new file is added to the source folder, the file
        /// will be replaced with GUID, and then moved to the destination folder.
        /// </summary>
        /// <param name="AddedFilePath">The file of the deticted added file.</param>
        private void _MoveAddedFile(string AddedFilePath)
        {
            // Maintain the added file's path without renaming it, for logging messages and moving the renamed file.
            string OriginalFilePath = AddedFilePath; 
            clsGlobal.LogMessage($"File detected: {AddedFilePath}", _LogFile); // Log detecting the added file.
            AddedFilePath = clsGlobal.ReplaceFileWithGUID(AddedFilePath, _LogFile); // The added file's path now is replaced with GUID.
            clsGlobal.LogMessage($"File renmaed: {OriginalFilePath} -> {AddedFilePath}", _LogFile); // Log renaming the file with GUID.

            // Replacing the path with the destination folder, so it can be moved to the destination folder.
            AddedFilePath = AddedFilePath.Replace(_SourceFolder, _DestinationFolder);
            try
            {
                File.Move(OriginalFilePath, AddedFilePath); // Move the GUID file to the destination folder.
                File.Delete(AddedFilePath.Replace(_DestinationFolder, _SourceFolder)); // Delete the detected file from the source folder.
                clsGlobal.LogMessage($"File moved: {OriginalFilePath} -> {AddedFilePath}", _LogFile); // Log file moving success.
            }
            catch (Exception ex)
            {
                clsGlobal.LogMessage($"Failed to move {OriginalFilePath} to {_DestinationFolder}", _LogFile); // Log file moving failure.
            }
        }

        /// <summary>
        /// This procedure monitors the source folder. If a new file is added to the source folder, the file
        /// will be replaced with GUID, and then moved to the destination folder.
        /// </summary>
        private void _Watcher_Created(object sender, FileSystemEventArgs e) =>
            _MoveAddedFile(e.FullPath);

        protected override void OnStart(string[] args) => 
            clsGlobal.LogMessage("File Monitoring Service started...", _LogFile);

        protected override void OnStop() =>
            clsGlobal.LogMessage("File Monitoring Service stopped.", _LogFile);

        /// <summary>
        /// This procedure works only in debugging mode to test the service before deployment.
        /// It starts the service, then stops it when the user presses enter.
        /// </summary>
        [Conditional("DEBUG")]
        public void StartInConsole()
        {
            OnStart(null);
            Console.WriteLine("Please press enter to stop this service...");
            Console.ReadLine();
            OnStop();
            Console.ReadKey();
        }
    }
}
