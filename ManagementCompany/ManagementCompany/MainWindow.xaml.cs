using System;
using System.Linq;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.Windows;
using Core;
using Core.ContractCalculation;
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

            var months = new Months();
            cmbxMonts.ItemsSource = months.AllMonth;
            cmbxMonts.DisplayMemberPath = "Name";

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                cmbxBuildings.ItemsSource = mcDatabaseModelContainer.BuildingsНабор.ToArray();
                cmbxBuildingsContract.ItemsSource = mcDatabaseModelContainer.BuildingsНабор.ToArray();
            }
        }

        private void CreateObject_Click(object sender, RoutedEventArgs e)
        {
            var buildings = new Buildings
                                {
                                    Name = tbxName.Text, 
                                    Description = tbxDescription.Text,
                                    EstimateConsumptionHeat = tbxNormativeConsumptionHeat.Text
                                    
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
            normativeCalculation.DateTimeImtervals = dateTimeIntervals;

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                DbTransaction transaction = null;
                try
                {
                    mcDatabaseModelContainer.Connection.Open();
                    transaction = mcDatabaseModelContainer.Connection.BeginTransaction();

                    mcDatabaseModelContainer.DateTimeImtervalsНабор.AddObject(dateTimeIntervals);
                    //mcDatabaseModelContainer.NormativeCalculationНабор.AddObject(normativeCalculation);
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

        private void btnAddContract_Click(object sender, RoutedEventArgs e)
        {
            double airtemperature = Double.Parse(tbxAirTemperature.Text);
            int countPeoples = int.Parse(tbxPeoplesCount.Text);
            int countDays = DateTime.DaysInMonth(DateTime.Now.Year, ((Month) cmbxMonts.SelectedItem).Index);

            var contractCalculator = new ContractCalculator();

            using (var context = new MCDatabaseModelContainer())
            {
                var datetime = from date in context.DateTimeImtervalsНабор
                               select date;

                var estimatedConsumption = from building in context.BuildingsНабор
                                           where building.Id == ((Buildings) cmbxBuildingsContract.SelectedItem).Id
                                           select building.EstimateConsumptionHeat;

                var consumptionByLoad = contractCalculator.ConsumptionByLoad(Double.Parse(estimatedConsumption.FirstOrDefault()), countDays,
                                                                             airtemperature);
                var hotWaterByNorm = contractCalculator.HotWaterByNorm(countPeoples);
                var totalHeatConsumption = contractCalculator.TotalHeatConsumption(consumptionByLoad, hotWaterByNorm);

                var contractConsumption = new ContractConsumptionHeat
                                              {
                                                  AirTemperature = airtemperature.ToString(),
                                                  BuildingsId = ((Buildings) cmbxBuildingsContract.SelectedItem).Id,
                                                  HeatByLoading = consumptionByLoad.ToString(),
                                                  PeopleCount = countPeoples.ToString(),
                                                  HotWaterByNorm = hotWaterByNorm.ToString(),
                                                  TotalHeatConsumption = totalHeatConsumption.ToString(),
                                                  DateTimeImtervals = datetime.First()
                                              };
                context.ContractConsumptionHeatTable.AddObject(contractConsumption);
                context.SaveChanges();
            }
        }
    }
}
