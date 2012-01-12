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
        private readonly IContractConsumprionRepository repository;
        private readonly IContractCalculator contractCalculator;
        private UserControl view;

        public ContractConsumptionViewModel(IContractConsumprionRepository repository, IContractCalculator contractCalculator)
        {
            this.repository = repository;
            this.contractCalculator = contractCalculator;
            Buildings = new ObservableCollection<Building>(repository.GetBuildings());
            DateTimeIntervals = new ObservableCollection<DateTimeImtervals>(repository.GetDateTimeIntervals());
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
            var month = int.Parse(SelectedThermometerReading.Month);
            var year = int.Parse(SelectedThermometerReading.Year);
            var consumptionByLoad = contractCalculator.ConsumptionByLoad(SelectedBuilding.StandartOfHeat, DateTime.DaysInMonth(year, month), int.Parse(SelectedThermometerReading.AirTemperature));
            var hotWaterByNorm = contractCalculator.HotWaterByNorm(int.Parse(PeopleCount));
            var totalHeatConsumption = contractCalculator.TotalHeatConsumption(consumptionByLoad, hotWaterByNorm);
            var contractConsumption = new ContractConsumptionHeat
                                          {
                                              BuildingsId = SelectedBuilding.Id,
                                              HeatByLoading = consumptionByLoad,
                                              PeopleCount = int.Parse(PeopleCount),
                                              HotWaterByNorm = hotWaterByNorm,
                                              TotalHeatConsumption = totalHeatConsumption,
                                              DateTimeImtervals = SelectedInterval,
                                              ThermometerReading = SelectedThermometerReading
                                          };
            repository.InsertConstractConsumption(contractConsumption);
            repository.Save();
            ContractConsumptions.Add(contractConsumption);
        }

        public string PeopleCount { get; set; }
        public ObservableCollection<ContractConsumptionHeat> ContractConsumptions { get; private set; }
        public ObservableCollection<Building> Buildings { get; private set; }
        public ObservableCollection<ThermometerReading> ThermometerReadings { get; private set; }
        public ThermometerReading SelectedThermometerReading { get; set; }
        public Building SelectedBuilding { get; set; }
        public ObservableCollection<DateTimeImtervals> DateTimeIntervals { get; private set; }
        public DateTimeImtervals SelectedInterval { get; set; }
    }
}

