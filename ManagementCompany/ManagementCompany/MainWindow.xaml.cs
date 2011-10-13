using System;
using System.Data.Common;
using System.Windows;
using Core.StandartCalculation;
using Repository;

namespace ManagementCompany
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                cmbxBuildings.ItemsSource = mcDatabaseModelContainer.BuildingsНабор;
                mcDatabaseModelContainer.SaveChanges();
            }
        }

        private void CreateObject_Click(object sender, RoutedEventArgs e)
        {
            var buildings = new Buildings
                                {
                                    Name = tbxName.Text, 
                                    Description = tbxDescription.Text
                                };

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                mcDatabaseModelContainer.BuildingsНабор.AddObject(buildings);
                mcDatabaseModelContainer.SaveChanges();
            }

        }

        private void btnSaveStandartCalculation_Click(object sender, RoutedEventArgs e)
        {
            var standartCalculator = new StandartCalculator();

            var startDate = DateTime.Parse(dtStartDate.Text);
            var endDate = DateTime.Parse(dtEndDate.Text);

            var dateTimeIntervals = new DateTimeImtervals();
            dateTimeIntervals.EndDate = endDate.ToString();
            dateTimeIntervals.StartDate = startDate.ToString();
            dateTimeIntervals.BuildingsId = 1;

            double totalArea = Double.Parse(tbxTotalArea.Text);
            double calculationArea = Double.Parse(tbxCalculationArea.Text);
            double standartHeat = Double.Parse(tbxStandart.Text);

            var consumptionByTotalArea = standartCalculator.CalculateConsumptionByArea(totalArea, standartHeat);
            var consumptionByCalculationArea = standartCalculator.CalculateConsumptionByArea(calculationArea,
                                                                                             standartHeat);
            var normativeCalculation = new NormativeCalculation();

            if (cmbxBuildings.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать здание");
                return;
            }
            normativeCalculation.BuildingsId = ((Buildings)cmbxBuildings.SelectedItem).Id;
            normativeCalculation.CalculationArea = calculationArea.ToString();
            normativeCalculation.TotalArea = totalArea.ToString();
            normativeCalculation.StandartOfHeat = standartHeat.ToString();
            normativeCalculation.ConsumptionHeatByTotalArea = consumptionByTotalArea.ToString();
            normativeCalculation.ConsumptionHeatByCalculationArea = consumptionByCalculationArea.ToString();
            normativeCalculation.TotalNormativeHeat = tbxStandart.Text;

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                DbTransaction transaction = null;
                try
                {
                    mcDatabaseModelContainer.Connection.Open();
                    transaction = mcDatabaseModelContainer.Connection.BeginTransaction();

                    mcDatabaseModelContainer.DateTimeImtervalsНабор.AddObject(dateTimeIntervals);
                    mcDatabaseModelContainer.NormativeCalculationНабор.AddObject(normativeCalculation);
                    mcDatabaseModelContainer.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    MessageBox.Show(exception.ToString());
                }
            }
        }
    }
}
