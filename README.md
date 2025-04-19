Installation Guide: Database Backup Service
How to Install the Database Backup Service
Introduction
The Database Backup Service periodically takes backups of a SQL Server database and stores them in the specified folder. In this guide, you will learn how to install this service on a Windows machine.

Prerequisites
.NET Framework must be installed on your machine.

A SQL Server database must be running properly.

You must have administrator privileges to install and run the service.

Installation Steps
Build the Project:

Make sure you've built the project in Release mode in Visual Studio.

After building, make sure all necessary files (such as the BackupService.exe and installation files) are located in the project folder.

Open Command Prompt as Administrator:

Open Command Prompt with Administrator privileges. You can do this by searching for "cmd" in the Start menu, then right-clicking and selecting Run as Administrator.

Navigate to the Service Folder:

Use the cd command to navigate to the folder containing your BackupService.exe file:

bash
نسخ
تحرير
cd C:\path\to\your\BackupService
Install the Service using InstallUtil:

Use the InstallUtil.exe tool from the .NET Framework to install the service. Type the following command in Command Prompt:

bash
نسخ
تحرير
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe BackupService.exe
Note: Make sure to adjust the path to InstallUtil.exe based on your .NET version, and if you're on a 64-bit Windows system, use the 64-bit version:

bash
نسخ
تحرير
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe BackupService.exe
Verify Service Installation:

After the command has completed successfully, you can verify that the service has been installed by opening Services:

Press Windows + R, type services.msc, and press Enter.

Look for Database Backup Service in the list of services.

Starting the Service
Start the Service:

You can start the service from the Services management console or use the following command in Command Prompt:

bash
نسخ
تحرير
net start DatabaseBackupService
Stop the Service:

To stop the service, use the following command:

bash
نسخ
تحرير
net stop DatabaseBackupService
Uninstall the Service
If you need to uninstall the service, you can use InstallUtil with the /u flag as follows:

Open Command Prompt as Administrator.

Navigate to the folder containing BackupService.exe.

Type the following command to uninstall the service:

bash
نسخ
تحرير
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u BackupService.exe
Or if you're using a 64-bit system:

bash
نسخ
تحرير
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe /u BackupService.exe
Important Notes
Ensure that the database connection is correctly set in the App.config file with the correct connection string.

Make sure the backup folders are configured correctly. If they don't exist, they will be created automatically during the backup process.

If you're in a development environment, you can test the service in Interactive Mode using StartDebug(), but in production, it should run as a regular Windows service.

Conclusion
The service is now installed! It will perform periodic backups based on the settings in the App.config file. If you encounter any issues, you can refer to the service logs for potential errors.
