using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace BackupService
{
    [RunInstaller(true)]
    public partial class BackupServiceInstaller : Installer
    {
        public BackupServiceInstaller()
        {
            InitializeComponent();

            // إنشاء وتكوين المثبتات الخاصة بالخدمة
            var serviceInstaller = new ServiceInstaller
            {
                ServiceName = "DatabaseBackupService",  // اسم الخدمة
                DisplayName = "Database Backup Service",  // الاسم المعروض
                Description = "This service performs periodic backups of a SQL Server database.",  // الوصف
                StartType = ServiceStartMode.Automatic  // نوع بدء الخدمة (تلقائي)
            };

            var processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem  // الحساب الذي سيشغل الخدمة (حساب النظام المحلي)
            };

            // إضافة المثبتات إلى مجموعة المثبتات
            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}
