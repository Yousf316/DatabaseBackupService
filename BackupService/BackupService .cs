using System;
using System.IO;
using System.ServiceProcess;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Timers;

namespace BackupService
{
    public partial class BackupService : ServiceBase
    {
        private Timer backupTimer;
        private string connectionString;
        private string backupFolderPath;
        private int backupIntervalMinutes;
        private string logFolderPath;



        public BackupService()
        {
            // This is where InitializeComponent() is called from the Designer file
            InitializeComponent();

            // Initialize the service-related settings (database connection, folder paths)
            connectionString = ConfigurationManager.AppSettings["SqlConnectionString"];
            backupFolderPath = ConfigurationManager.AppSettings["BackupFolderPath"];
            backupIntervalMinutes = int.Parse(ConfigurationManager.AppSettings["BackupIntervalMinutes"]);
            logFolderPath = ConfigurationManager.AppSettings["LogFolderPath"] ?? "C:\\ServiceLogs"; // Default folder if not configured
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LogDebug("Service is starting...");
                backupTimer = new Timer(backupIntervalMinutes * 60 * 1000); // Backup interval in milliseconds
                backupTimer.Elapsed += BackupTimerElapsed;
                backupTimer.Start();

                LogDebug("Service started and timer initialized.");
            }
            catch (Exception ex)
            {
                LogDebug($"Error in OnStart: {ex.Message}");
            }
        }

        protected override void OnStop()
        {
            try
            {
                LogDebug("Service is stopping...");
                
                if (backupTimer != null)
                {
                    backupTimer.Stop();
                    backupTimer.Dispose();  // Optionally dispose to free resources
                }
                LogDebug("Service stopped.");

            }
            catch (Exception ex)
            {
                LogDebug($"Error in OnStop: {ex.Message}");
            }
        }

        private void BackupTimerElapsed(object sender, ElapsedEventArgs e)
        {
            LogDebug("Backup timer triggered.");
            BackupDatabase();
        }

        private void BackupDatabase()
        {
            try
            {
                string databaseName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

                // Ensure the backup folder exists
                if (!Directory.Exists(backupFolderPath))
                {
                    Directory.CreateDirectory(backupFolderPath);
                    LogDebug($"Created backup folder at: {backupFolderPath}");
                }

                string backupFilePath = Path.Combine(backupFolderPath, $"{databaseName}_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak");

                LogDebug($"Starting backup for database: {databaseName}, saving to {backupFilePath}");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"BACKUP DATABASE [{databaseName}] TO DISK = '{backupFilePath}'", connection);
                    command.ExecuteNonQuery();
                }

                LogBackupSuccess(backupFilePath);
            }
            catch (Exception ex)
            {
                LogBackupError(ex);
            }
        }

        private void LogBackupSuccess(string backupFilePath)
        {
            LogDebug($"Backup successful: {backupFilePath}");
        }

        private void LogBackupError(Exception ex)
        {
            LogDebug($"Backup failed: {ex.Message}");
        }

        private void LogDebug(string message)
        {
            try
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - DEBUG: {message}";

                // Log to file
                if (!Directory.Exists(logFolderPath))
                {
                    Directory.CreateDirectory(logFolderPath);
                }
                string logFilePath = Path.Combine(logFolderPath, "service_log.txt");
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);

                // Log to console if in interactive mode
                if (Environment.UserInteractive)
                {
                    Console.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                LogDebug( $"Error logging to file: {ex.Message}");
            }
        }


        public void StartDebug()
        {
            OnStart(null);
        }

        public void StopDebug()
        {
            OnStop();
        }

    }
}
