using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Repository;

namespace ManagementCompany.Views
{
    /// <summary>
    /// Логика взаимодействия для ManagementObjectsView.xaml
    /// </summary>
    public partial class ManagementObjectsView : UserControl
    {
        public ManagementObjectsView()
        {
            InitializeComponent();

            using (var context = new MCDatabaseModelContainer())
            {
                var query = context.Buildings.Where(buid => buid.Id == 1);
                //dataGrid.ItemsSource = context.Buildings.ToList();
            }
        }
    }
}
