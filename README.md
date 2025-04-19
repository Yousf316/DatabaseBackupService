# Database Backup Service - Installation Guide

## Introduction
The **Database Backup Service** automatically performs periodic backups of a SQL Server database and stores them in a specified folder. This guide will walk you through the installation process.

---

## Prerequisites
- **.NET Framework** should be installed on your machine.
- A **SQL Server database** must be running and accessible.
- You must have **administrator privileges** to install and run the service.

---

## Installation Steps

### 1. **Build the Project**
   - Ensure you have built the project in **Release** mode in Visual Studio.
   - After building, verify that all necessary files (e.g., `BackupService.exe`) are present in your project output directory.

### 2. **Open Command Prompt as Administrator**
   - Press `Windows + X`, then select **Command Prompt (Admin)** or **Windows PowerShell (Admin)**.

### 3. **Navigate to the Service Folder**
   - Use the `cd` command to navigate to the folder containing your `BackupService.exe` file. Example:
     ```bash
     cd C:\path\to\your\BackupService
     ```

### 4. **Install the Service Using `InstallUtil`**
   - Use the `InstallUtil.exe` tool to install the service:
     ```bash
     C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe BackupService.exe
     ```
   - **Note**: If you're on a 64-bit Windows system, use the 64-bit version of `InstallUtil.exe`:
     ```bash
     C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe BackupService.exe
     ```

### 5. **Verify Service Installation**
   - Open **Services**:
     - Press `Windows + R`, type `services.msc`, and hit Enter.
     - Look for the service named **Database Backup Service** in the list.

---

## Starting and Stopping the Service

### Start the Service:
   - To start the service from the command prompt, type:
     ```bash
     net start DatabaseBackupService
     ```

### Stop the Service:
   - To stop the service, type:
     ```bash
     net stop DatabaseBackupService
     ```

---

## Uninstalling the Service

### 1. **Open Command Prompt as Administrator**
   - Open **Command Prompt (Admin)** or **Windows PowerShell (Admin)**.

### 2. **Navigate to the Service Folder**
   - Use the `cd` command to navigate to the folder where `BackupService.exe` is located.

### 3. **Uninstall the Service**
   - Run the following command to uninstall the service:
     ```bash
     C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u BackupService.exe
     ```
   - If you're on a 64-bit system, use:
     ```bash
     C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe /u BackupService.exe
     ```

---

## Configuration
Before running the service, make sure that the **connection string** and **backup folder paths** are properly configured in the `App.config` file:
- **SqlConnectionString**: The connection string for your SQL Server.
- **BackupFolderPath**: The folder where the backups will be stored.
- **LogFolderPath**: The folder where logs will be written.

If the backup folder does not exist, it will be created automatically by the service.

---

## Important Notes

- Ensure that your **SQL Server** instance is accessible by the service.
- The service can be run in **Interactive Mode** during development using `StartDebug()`, but it should be run as a regular service in production environments.
- The service logs errors and information to a **log file** located in the configured `LogFolderPath`. Make sure the log folder exists and the service has permission to write to it.

---

## Conclusion
Once the service is installed, it will start automatically according to the schedule defined in the `App.config` file. The service will periodically back up the SQL Server database and store the backups in the specified location.

---

## Troubleshooting
- **Service not starting**: Check the service log file for any startup errors.
- **Backups not happening**: Ensure that the SQL Server database is accessible and the backup folder has proper permissions.
