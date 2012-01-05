using System;
using System.Linq;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.Windows;
using Core;
using Core.ContractCalculation;
using Core.StandartCalculation;
using Core.TotalCalculation;
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
            cmbxMontsClearing.ItemsSource = months.AllMonth;
            cmbxMontsClearing.DisplayMemberPath = "Name";

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                cmbxBuildings.ItemsSource = mcDatabaseModelContainer.Buildings.ToArray();
                cmbxBuildingsContract.ItemsSource = mcDatabaseModelContainer.Buildings.ToArray();
                cmbxBuildingsClearing.ItemsSource = mcDatabaseModelContainer.Buildings.ToArray();
            }
        }

        private void CreateObject_Click(object sender, RoutedEventArgs e)
        {
            var buildings = new Building
                                {
                                    Name = tbxName.Text,
                                    Description = tbxDescription.Text,
                                    EstimateConsumptionHeat = Double.Parse(tbxNormativeConsumptionHeat.Text)

                                };

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                mcDatabaseModelContainer.Buildings.AddObject(buildings);
                mcDatabaseModelContainer.SaveChanges();
            }

        }

        private void btnSaveStandartCalculation_Click(object sender, RoutedEventArgs e)
        {
            var standartCalculator = new StandartCalculator();

            var startDate = DateTime.Parse(dtStartDate.Text);
            var endDate = DateTime.Parse(dtEndDate.Text);

            var dateTimeIntervals = new DateTimeImtervals();
            dateTimeIntervals.EndDate = endDate;
            dateTimeIntervals.StartDate = startDate;
            dateTimeIntervals.BuildingsId = 1;

            double totalArea = Double.Parse(tbxTotalArea.Text);
            double calculationArea = Double.Parse(tbxCalculationArea.Text);
            double standartHeat = Double.Parse(tbxStandart.Text);

            var consumptionByTotalArea = standartCalculator.CalculateConsumptionByArea(totalArea, standartHeat);
            var consumptionByCalculationArea = standartCalculator.CalculateConsumptionByArea(calculationArea,
                                                                                             standartHeat);
            var normativeCalculation = new NormativeCalculations();

            if (cmbxBuildings.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать здание");
                return;
            }
            normativeCalculation.BuildingsId = ((Building)cmbxBuildings.SelectedItem).Id;
            normativeCalculation.CalculationArea = calculationArea;
            normativeCalculation.TotalArea = totalArea;
            normativeCalculation.StandartOfHeat = standartHeat;
            normativeCalculation.ConsumptionHeatByTotalArea = consumptionByTotalArea;
            normativeCalculation.ConsumptionHeatByCalculationArea = consumptionByCalculationArea;
            normativeCalculation.TotalNormativeHeat = Double.Parse(tbxStandart.Text);
            normativeCalculation.DateTimeImtervals = dateTimeIntervals;

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                DbTransaction transaction = null;
                try
                {
                    mcDatabaseModelContainer.Connection.Open();
                    transaction = mcDatabaseModelContainer.Connection.BeginTransaction();

                    mcDatabaseModelContainer.DateTimeImtervals.AddObject(dateTimeIntervals);
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
            int countDays = DateTime.DaysInMonth(DateTime.Now.Year, ((Month)cmbxMonts.SelectedItem).Index);

            var contractCalculator = new ContractCalculator();

            using (var context = new MCDatabaseModelContainer())
            {
                var datetime = from date in context.DateTimeImtervals
                               select date;

                var estimatedConsumption = from building in context.Buildings
                                           where building.Id == ((Building)cmbxBuildingsContract.SelectedItem).Id
                                           select building.EstimateConsumptionHeat;

                var consumptionByLoad = contractCalculator.ConsumptionByLoad(estimatedConsumption.FirstOrDefault(), countDays,
                                                                             airtemperature);
                var hotWaterByNorm = contractCalculator.HotWaterByNorm(countPeoples);
                var totalHeatConsumption = contractCalculator.TotalHeatConsumption(consumptionByLoad, hotWaterByNorm);

                var contractConsumption = new ContractConsumptionHeat
                                              {
                                                  AirTemperature = airtemperature,
                                                  BuildingsId = ((Building)cmbxBuildingsContract.SelectedItem).Id,
                                                  HeatByLoading = consumptionByLoad,
                                                  PeopleCount = countPeoples,
                                                  HotWaterByNorm = hotWaterByNorm,
                                                  TotalHeatConsumption = totalHeatConsumption,
                                                  DateTimeImtervals = datetime.First()
                                              };
                context.ContractConsumptionHeatTable.AddObject(contractConsumption);
                context.SaveChanges();
            }
        }

        private void btnAddClearingInfo_Click(object sender, RoutedEventArgs e)
        {
            var heatMeterReadings = Double.Parse(tbxHeatMeterReading.Text);
            var waterMeterReadings = Double.Parse(tbxWaterMeterReading.Text);

            using (var context = new MCDatabaseModelContainer())
            {
                var datetime = from date in context.DateTimeImtervals
                               select date;

                var meterReadings = context.MeterReadingsTable.CreateObject();
                meterReadings.CurrentHeatMeterReader = heatMeterReadings;
                meterReadings.CurrentWaterHeatReader = waterMeterReadings;
                meterReadings.BuildingsId = ((Building)cmbxBuildingsClearing.SelectedItem).Id;
                meterReadings.DateTimeImtervals = datetime.First();
                context.MeterReadingsTable.AddObject(meterReadings);

                var clearing = context.ClearingTable.CreateObject();
                clearing.Requirements = Double.Parse(tbxRequirementHeat.Text);
                clearing.CalculationHotWater = Double.Parse(tbxWaterBuxgalter.Text);
                clearing.BuildingsId = ((Building)cmbxBuildingsClearing.SelectedItem).Id;

                var totalHeatConsumption = from contract in context.ContractConsumptionHeatTable
                                           where
                                               contract.BuildingsId == clearing.BuildingsId &&
                                               contract.DateTimeImtervals == datetime.FirstOrDefault()
                                           select contract.TotalHeatConsumption;

                var totalCalculator = new TotalCalculation();
                clearing.CalculationHot = totalCalculator.TotalHeatConsumption(totalHeatConsumption.First(), Double.Parse(tbxWaterBuxgalter.Text));
                clearing.DateTimeImtervals = datetime.First();
                context.ClearingTable.AddObject(clearing);
                context.SaveChanges();
            }
        }
    }
}
