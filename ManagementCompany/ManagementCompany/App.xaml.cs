using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ManagementCompany.Models;
using ManagementCompany.Views;

namespace ManagementCompany
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //var viewVM = new ManagementObjectsViewModel();
            View = new ManagementObjectsView();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow{DataContext = this};
            mainWindow.Show();
            
            base.OnStartup(e);
        }

        public UserControl View { get; set; }
    }
}
