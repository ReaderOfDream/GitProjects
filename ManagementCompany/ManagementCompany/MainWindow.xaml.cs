using System;
using System.Data;
using System.Data.Objects;
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
        private readonly IBuildingRepository buildingRepository;
        private readonly BuildingViewModel buildingViewModel;

        private readonly IHeatSupplierRepository heatSupplierRepository;
        private readonly HeatSupplierViewModel heatSupplierViewModel;

        private readonly IReportRepository reportRepository;
        private readonly CreateReportViewModel createReportViewModel;

        private readonly INormativeCalculationRepository normativeCalculationRepository;
        private readonly NormativeCalculationViewModel normativeCalculationViewModel;
        private readonly IContractConsumptionRepository contractConsumprionRepository;
        private readonly ContractConsumptionViewModel contractConsumptionViewModel;
        private readonly IBuildingMonthVariablesRepository variablesRepository;
        private readonly NormativeAndProjectModel normativeAndProjectModel;

        private readonly IThermometerReadingRepository thermometerReadingRepository;
        private readonly ThermometersReaderViewModel thermometerReadingViewModel;

        private IClearingRepository clearingRepository;
        private ClearingViewModel clearinViewModel;

        private bool _isReportViewerLoaded;

        private readonly MCDatabaseModelContainer db = new MCDatabaseModelContainer();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            heatSupplierRepository = new HeatSupplierRepository(db);
            buildingRepository = new BuildingRepository(db);
            reportRepository = new ReportRepository(db);
            normativeCalculationRepository = new NormativeCalculationRepository(db);
            thermometerReadingRepository = new ThermometerReadingRepository(db);
            contractConsumprionRepository = new ContractComsumptionRepository(db);
            clearingRepository = new ClearingRepository(db);
            variablesRepository = new BuildingMonthVariablesRepository(db);

            heatSupplierViewModel = new HeatSupplierViewModel(heatSupplierRepository);
            buildingViewModel = new BuildingViewModel(buildingRepository);
            createReportViewModel = new CreateReportViewModel(reportRepository);
            normativeAndProjectModel = new NormativeAndProjectModel(normativeCalculationRepository, new StandartCalculator(),
                                                               contractConsumprionRepository, new ContractCalculator(),
                                                               variablesRepository);
            //normativeCalculationViewModel = new NormativeCalculationViewModel(normativeCalculationRepository, new StandartCalculator());
            thermometerReadingViewModel = new ThermometersReaderViewModel(thermometerReadingRepository);
            contractConsumptionViewModel = new ContractConsumptionViewModel(contractConsumprionRepository, new ContractCalculator());

            clearinViewModel = new ClearingViewModel(clearingRepository, new TotalCalculation());

            _reportViewer.Load += ReportViewerLoad;
        }

        private void ReportViewerLoad(object sender, EventArgs e)
        {
               

            foreach (var objectWholeTableSet in db.WholeTableSet)
            {
                db.WholeTableSet.DeleteObject(objectWholeTableSet);
            }
            db.SaveChanges();

                var tableQuery = from cl in db.Clearings
                                 join bs in db.Buildings on cl.Building.Id equals  bs.Id
                                join hs in db.HeatSuppliers on bs.HeatSupplier.Id equals hs.Id
                                join dt in db.DateTimeIntervals on hs.Id equals dt.HeatSupplier.Id
                                join mr in db.MeterReadings on dt.MeterReading.ID equals mr.ID
                                join ch in db.ContractConsumptionHeats on dt.ContractConsumptionHeat.ID equals ch.ID
                                join nc in db.NormativeCalculations on dt.NormativeCalculation.Id equals nc.Id
                                select new {cl, bs, hs, dt , mr , nc , ch};

                foreach (var building in tableQuery)
                {
                    db.WholeTableSet.AddObject(new WholeTable() {BuildingName = building.bs.Name,
                        BuildingDescription = building.bs.Description,
                    BuildingStandartOfHeat = building.bs.StandartOfHeat,
                    BuildingTotalArea = building.bs.TotalArea,
                    HeatSupplierName = building.hs.Name,
                    HeatSupplierDescription = building.hs.Description,
                    DateTimeIntervalName = building.dt.Name,
                    DateTimeIntervalStartDate = building.dt.StartDate,
                    DateTimeIntervalEndDate = building.dt.EndDate,
                    ClearingRequirements = building.cl.Requirements,
                    ClearingCalculationHot = building.cl.CalculationHot,
                    ClearingCalculationByBuhgaltery =  building.cl.CalculationByBughaltery,
                    MeterReadingHeat =  building.mr.CurrentHeatMeterReader,
                    MeterReadingWater = building.mr.CurrentWaterHeatReader,
                    ContractHeatByLoading = building.ch.HeatByLoading,
                    //ContractPeopleCount = building.ch.PeopleCount,
                    ContractHotWaterByNorm = building.ch.HotWaterByNorm,
                    ContractTotalHeatConsumption = building.ch.TotalHeatConsumption,
                    //NormativeCalculationArea = building.nc.CalculationArea,
                    NormativeEstimateConsumptionHeat = building.nc.EstimateConsumptionHeat,
                    NormativeConsumptionHeatByCalculationArea = building.nc.ConsumptionHeatByCalculationArea,
                    NormativConsumptionHeatByTotalArea = building.nc.ConsumptionHeatByTotalArea
                    });
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
                

            

            if (!_isReportViewerLoaded)
            {
                var reportDataSource1 = new
                    Microsoft.Reporting.WinForms.ReportDataSource {Name = "TableSet"};

                var masterdataset = new masterDataSet();

                masterdataset.BeginInit();

                //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = masterdataset.WholeTableSet;

                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer.LocalReport.ReportPath = "../../Report.rdlc";

                masterdataset.EndInit();

                //fill data into WpfApplication4DataSet
                var wholeTableAdapter = new
                    masterDataSetTableAdapters.WholeTableSetTableAdapter {ClearBeforeFill = true};
                try
                {
                    wholeTableAdapter.Fill(masterdataset.WholeTableSet);
                }
                catch(Exception er)
                {
                    int x;
                }

                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }
        public NormativeCalculationViewModel NormativeCalculationViewModel { get { return normativeCalculationViewModel; } }
        public HeatSupplierViewModel HeatSupplierViewModel { get { return heatSupplierViewModel; }}
        public BuildingViewModel BuildingViewModel { get { return buildingViewModel; } }
        public CreateReportViewModel CreateReportViewModel { get { return createReportViewModel; } }
        public ThermometersReaderViewModel ThermometersReaderViewModel{get { return thermometerReadingViewModel; }}
        public ContractConsumptionViewModel ContractConsumptionViewModel{get { return contractConsumptionViewModel; }}
        public ClearingViewModel ClearinViewModel{ get { return clearinViewModel; }}
        public NormativeAndProjectModel NormativeAndProjectModel { get { return normativeAndProjectModel; }}
    }
}
