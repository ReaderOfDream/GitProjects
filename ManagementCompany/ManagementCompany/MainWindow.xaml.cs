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
using ManagementCompany.Models;
using Repository;
using Repository.DAL;

namespace ManagementCompany
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IHeatSupplierRepository heatSupplierRepository;
        private HeatSupplierViewModel heatSupplierViewModel;

        private IBuildingRepository createobjectRepository;
        private CreateObjectViewModel createobjectViewModel;

        private IReportRepository reportRepository;
        private CreateReportViewModel createReportViewModel;

        private INormativeCalculationRepository normativeCalculationRepository;
        private NormativeCalculationViewModel normativeCalculationViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            heatSupplierRepository = new HeatSupplierRepository(new MCDatabaseModelContainer());
            heatSupplierViewModel = new HeatSupplierViewModel(heatSupplierRepository);

            createobjectRepository = new BuildingRepository(new MCDatabaseModelContainer());
            createobjectViewModel = new CreateObjectViewModel(createobjectRepository);

            reportRepository = new ReportRepository(new MCDatabaseModelContainer());
            createReportViewModel = new CreateReportViewModel(reportRepository);

            normativeCalculationRepository = new NormativeCalculationRepository(new MCDatabaseModelContainer());
            normativeCalculationViewModel = new NormativeCalculationViewModel(normativeCalculationRepository, new StandartCalculator());

            var months = new Months();
            cmbxMonts.ItemsSource = months.AllMonth;
            cmbxMonts.DisplayMemberPath = "Name";
            cmbxMontsClearing.ItemsSource = months.AllMonth;
            cmbxMontsClearing.DisplayMemberPath = "Name";

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                cmbxBuildingsContract.ItemsSource = mcDatabaseModelContainer.Buildings.ToArray();
                cmbxBuildingsClearing.ItemsSource = mcDatabaseModelContainer.Buildings.ToArray();
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
                                           select building.StandartOfHeat;

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

        public NormativeCalculationViewModel NormativeCalculationViewModel { get { return normativeCalculationViewModel; } }
        public HeatSupplierViewModel HeatSupplierViewModel { get { return heatSupplierViewModel; }}
        public CreateObjectViewModel CreateObjectViewModel { get { return createobjectViewModel; } }
        public CreateReportViewModel CreateReportViewModel { get { return createReportViewModel; } }
    }
}
