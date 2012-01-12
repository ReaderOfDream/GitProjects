using System;
using System.Linq;
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
        private readonly IHeatSupplierRepository heatSupplierRepository;
        private readonly HeatSupplierViewModel heatSupplierViewModel;

        private readonly IBuildingRepository buildingRepository;
        private readonly BuildingViewModel buildingViewModel;

        private readonly IReportRepository reportRepository;
        private readonly CreateReportViewModel createReportViewModel;

        private readonly INormativeCalculationRepository normativeCalculationRepository;
        private readonly NormativeCalculationViewModel normativeCalculationViewModel;

        private readonly IThermometerReadingRepository thermometerReadingRepository;
        private readonly ThermometersReaderViewModel thermometerReadingViewModel;

        private IContractConsumprionRepository contractConsumprionRepository;
        private ContractConsumptionViewModel contractConsumptionViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var db = new MCDatabaseModelContainer();

            heatSupplierRepository = new HeatSupplierRepository(db);
            buildingRepository = new BuildingRepository(db);
            reportRepository = new ReportRepository(db);
            normativeCalculationRepository = new NormativeCalculationRepository(db);
            thermometerReadingRepository = new ThermometerReadingRepository(db);
            contractConsumprionRepository = new ContractComsumptionRepository(db);

            heatSupplierViewModel = new HeatSupplierViewModel(heatSupplierRepository);
            buildingViewModel = new BuildingViewModel(buildingRepository);
            createReportViewModel = new CreateReportViewModel(reportRepository);
            normativeCalculationViewModel = new NormativeCalculationViewModel(normativeCalculationRepository, new StandartCalculator());
            thermometerReadingViewModel = new ThermometersReaderViewModel(thermometerReadingRepository);
            contractConsumptionViewModel = new ContractConsumptionViewModel(contractConsumprionRepository, new ContractCalculator());

            cmbxMontsClearing.DisplayMemberPath = "Name";

            using (var mcDatabaseModelContainer = new MCDatabaseModelContainer())
            {
                cmbxBuildingsClearing.ItemsSource = mcDatabaseModelContainer.Buildings.ToArray();
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
        public BuildingViewModel BuildingViewModel { get { return buildingViewModel; } }
        public CreateReportViewModel CreateReportViewModel { get { return createReportViewModel; } }
        public ThermometersReaderViewModel ThermometersReaderViewModel{get { return thermometerReadingViewModel; }}
        public ContractConsumptionViewModel ContractConsumptionViewModel
        {
            get { return contractConsumptionViewModel; }
        }
    }
}
