using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using Core.ContractCalculation;
using Core.StandartCalculation;
using ManagementCompany.Views;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class NormativeAndProjectModel
    {
        private readonly INormativeCalculationRepository _normativeRepository;
        private readonly IStandartCalculator _standartCalculator;
        private readonly IContractConsumptionRepository _projectRepository;
        private readonly IContractCalculator _projectCalculator;
        private readonly IBuildingMonthVariablesRepository _variablesRepository;

        private readonly UserControl _view;

        public string CalculationArea { get; set; }
        public string Standart { get; set; }
        public ThermometerReading SelectedThermometerReading { get; set; }
        public string PeopleCount { get; set; }
        public Building SelectedBuilding { get; set; }
        public DateTimeInterval SelectedInterval { get; set; }

        public ObservableCollection<Building> Buildings { get; set; }
        public ObservableCollection<DateTimeInterval> DateTimeIntervals { get; set; }
        public ObservableCollection<ThermometerReading> ThermometerReadings { get; private set; }
        public ObservableCollection<ContractConsumptionHeat> ContractConsumptions { get; private set; }
        public ObservableCollection<NormativeCalculation> NormativeCalculations { get; private set; }

        public NormativeAndProjectModel(INormativeCalculationRepository normativeCalculationRepository, 
                                        IStandartCalculator standartCalculator,
                                        IContractConsumptionRepository contractConsumptionRepository,
                                        IContractCalculator projectCalculator,
                                        IBuildingMonthVariablesRepository variablesRepository)
        {
            _normativeRepository = normativeCalculationRepository;
            _standartCalculator = standartCalculator;
            _projectRepository = contractConsumptionRepository;
            _projectCalculator = projectCalculator;
            _variablesRepository = variablesRepository;

            Buildings = new ObservableCollection<Building>(_normativeRepository.GetBuildings());
            DateTimeIntervals = new ObservableCollection<DateTimeInterval>(_normativeRepository.GetDateTimeIntervals());
            ThermometerReadings = new ObservableCollection<ThermometerReading>(_projectRepository.GetThermometerReadings());
            NormativeCalculations = new ObservableCollection<NormativeCalculation>(_normativeRepository.GetNormativeCalculations());
            ContractConsumptions = new ObservableCollection<ContractConsumptionHeat>(_projectRepository.GetConstractConsumptions());


            _view = new NormativeAndProjectView() { DataContext = this };
        }

        public void CreateNormativeAndProjectCalculation()
        {
            if (SelectedBuilding == null)
                return;

            if (SelectedInterval == null)
                return;

            var totalArea = Buildings.Single(building => building.Id == SelectedBuilding.Id).TotalArea;
            var calculationArea = Double.Parse(CalculationArea);
            var standartHeat = Double.Parse(Standart);
            var consumptionByTotalArea = _standartCalculator.CalculateConsumptionByArea(totalArea, standartHeat);
            var consumptionByCalculationArea = _standartCalculator.CalculateConsumptionByArea(calculationArea, standartHeat);

            
            var month = SelectedThermometerReading.Month;
            var year = SelectedThermometerReading.Year;
            var countOfPeople = int.Parse(PeopleCount);
            var consumptionByLoad = _projectCalculator.ConsumptionByLoad(SelectedBuilding.StandartOfHeat, DateTime.DaysInMonth(year, month), SelectedThermometerReading.AirTemperature);
            var hotWaterByNorm = _projectCalculator.HotWaterByNorm(countOfPeople);
            var totalHeatConsumption = _projectCalculator.TotalHeatConsumption(consumptionByLoad, hotWaterByNorm);

            var normativeCalculation = new NormativeCalculation
                                           {
                                               EstimateConsumptionHeat = standartHeat,
                                               ConsumptionHeatByTotalArea = consumptionByTotalArea,
                                               ConsumptionHeatByCalculationArea = consumptionByCalculationArea,
                                               TotalHeatConsumption = totalHeatConsumption,
                                               Building = SelectedBuilding,
                                               DateTimeInterval = SelectedInterval
                                           };

            _normativeRepository.InsertNormativeCalculations(normativeCalculation);
            _normativeRepository.Save();

            var projectCalculation = new ContractConsumptionHeat
                                          {
                                              HeatByLoading = consumptionByLoad,
                                              HotWaterByNorm = hotWaterByNorm,
                                              TotalHeatConsumption = totalHeatConsumption,
                                              Building = SelectedBuilding,
                                              DateTimeInterval = SelectedInterval,
                                              ThermometerReading = SelectedThermometerReading
                                          };

            _projectRepository.InsertConstractConsumption(projectCalculation);
            _projectRepository.Save();
            //Consumptions.Add(contractConsumption);

            var variables = new BuildingMonthVariables
                                {
                                    CalculationArea = calculationArea,
                                    CountOfPeople = countOfPeople,
                                    Building = SelectedBuilding,
                                    DateTimeInterval = SelectedInterval
                                };
            _variablesRepository.InsertBuildingMonthVariables(variables);
            _variablesRepository.Save();

        }

        public ICommand CreateNormativeAndProjectCalculationCommand
        {
            get { return new DelegatingCommand(CreateNormativeAndProjectCalculation); }
        }

        public UserControl View
        {
            get { return _view; }
        }

    }
}
