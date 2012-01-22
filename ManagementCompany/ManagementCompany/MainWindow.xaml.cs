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

        private IClearingRepository clearingRepository;
        private ClearinfViewModel clearinfViewModel;

        private bool _isReportViewerLoaded;

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
            clearingRepository = new ClearingRepository(db);

            heatSupplierViewModel = new HeatSupplierViewModel(heatSupplierRepository);
            buildingViewModel = new BuildingViewModel(buildingRepository);
            createReportViewModel = new CreateReportViewModel(reportRepository);
            normativeCalculationViewModel = new NormativeCalculationViewModel(normativeCalculationRepository, new StandartCalculator());
            thermometerReadingViewModel = new ThermometersReaderViewModel(thermometerReadingRepository);
            contractConsumptionViewModel = new ContractConsumptionViewModel(contractConsumprionRepository, new ContractCalculator());

            clearinfViewModel = new ClearinfViewModel(clearingRepository, new TotalCalculation());
            _reportViewer.Load += ReportViewerLoad;
        }

        private void ReportViewerLoad(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                    Microsoft.Reporting.WinForms.ReportDataSource();
                DataSet dataset = new DataSet();
                dataset.BeginInit();
                reportDataSource1.Name = "BuildingsSet";
                //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = dataset.Buildings;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer.LocalReport.ReportPath = "../../Report.rdlc";
                dataset.EndInit();
                //fill data into WpfApplication4DataSet
                DataSetTableAdapters.BuildingsTableAdapter
                    accountsTableAdapter = new
                        DataSetTableAdapters.BuildingsTableAdapter();

                accountsTableAdapter.ClearBeforeFill = true;
                accountsTableAdapter.Fill(dataset.Buildings);
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
        public ClearinfViewModel ClearinfViewModel{ get { return clearinfViewModel; }}
    }
}
