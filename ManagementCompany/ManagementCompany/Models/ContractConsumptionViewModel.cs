using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using Core.ContractCalculation;
using ManagementCompany.Views;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class ContractConsumptionViewModel
    {
        private readonly IContractConsumptionRepository repository;
        private readonly IContractCalculator contractCalculator;
        private UserControl view;

        public string PeopleCount { get; set; }
        public ObservableCollection<ContractConsumptionHeat> ContractConsumptions { get; private set; }
        public ObservableCollection<Building> Buildings { get; private set; }
        public ObservableCollection<ThermometerReading> ThermometerReadings { get; private set; }
        public ObservableCollection<DateTimeInterval> DateTimeIntervals { get; private set; }

        public ThermometerReading SelectedThermometerReading { get; set; }
        public Building SelectedBuilding { get; set; }
        public DateTimeInterval SelectedInterval { get; set; }

        public ContractConsumptionViewModel(IContractConsumptionRepository repository, IContractCalculator contractCalculator)
        {
            this.repository = repository;
            this.contractCalculator = contractCalculator;
            Buildings = new ObservableCollection<Building>(repository.GetBuildings());
            DateTimeIntervals = new ObservableCollection<DateTimeInterval>(repository.GetDateTimeIntervals());
            ContractConsumptions = new ObservableCollection<ContractConsumptionHeat>(repository.GetConstractConsumptions());
            ThermometerReadings = new ObservableCollection<ThermometerReading>(repository.GetThermometerReadings());

            view = new ContractConsumptionView(){DataContext = this};
        }

        public UserControl View
        {
            get { return view; }
        }

        public ICommand CalculateCommand
        {
            get{return new DelegatingCommand(Calculate);}
        }

        private void Calculate()
        {
            if (SelectedBuilding == null || SelectedThermometerReading == null)
                return;

            var month = (int)SelectedThermometerReading.Month;
            var year = SelectedThermometerReading.Year;
            var consumptionByLoad = contractCalculator.ConsumptionByLoad(SelectedBuilding.StandartOfHeat, DateTime.DaysInMonth(year, month), SelectedThermometerReading.AirTemperature);
            var hotWaterByNorm = contractCalculator.HotWaterByNorm(int.Parse(PeopleCount));
            var totalHeatConsumption = contractCalculator.TotalHeatConsumption(consumptionByLoad, hotWaterByNorm);
            var contractConsumption = new ContractConsumptionHeat
                                          {
                                              Building = SelectedBuilding,
                                              HeatByLoading = consumptionByLoad,
                                              HotWaterByNorm = hotWaterByNorm,
                                              TotalHeatConsumption = totalHeatConsumption,
                                              DateTimeInterval = SelectedInterval,
                                              ThermometerReading = SelectedThermometerReading
                                          };
            repository.InsertConstractConsumption(contractConsumption);
            repository.Save();
            ContractConsumptions.Add(contractConsumption);
        }
    }
}

