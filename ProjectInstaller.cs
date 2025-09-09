using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FileMonitoringService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller _ProcessInstaller;
        private ServiceInstaller _ServiceInstaller;
        public ProjectInstaller()
        {
            InitializeComponent();

            _ProcessInstaller = new ServiceProcessInstaller
            {
              // Run the service under the local system account
              Account = ServiceAccount.LocalSystem
            };

            // Initialize ServiceInstaller
            _ServiceInstaller = new ServiceInstaller
            {
                ServiceName = "FileMonitoringService",
                DisplayName = "File Monitoring Service",
                Description = "A Windows Service that monitors added files in a direcotry, then move these files replaced by GUID.",
                StartType = ServiceStartMode.Automatic,
            };

            // Add both installers to the Installers collection
            Installers.Add(_ProcessInstaller);
            Installers.Add(_ServiceInstaller);
        }
    }
}
