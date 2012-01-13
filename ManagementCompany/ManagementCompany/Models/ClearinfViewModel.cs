using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using Core.TotalCalculation;
using ManagementCompany.Views;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class ClearinfViewModel
    {
        private readonly IClearingRepository clearingRepository;
        private readonly ITotalCalculator totalCalculator;
        private readonly UserControl view;

        public ClearinfViewModel(IClearingRepository clearingRepository, ITotalCalculator totalCalculator)
        {
            this.clearingRepository = clearingRepository;
            this.totalCalculator = totalCalculator;

            Buildings = new ObservableCollection<Building>(clearingRepository.GetBuildings());
            DateTimeIntervals = new ObservableCollection<DateTimeImtervals>(clearingRepository.GetDateTimeIntervals());
            Clearings = new ObservableCollection<Clearing>(clearingRepository.GetClearings());

            view = new ClearingView(){DataContext = this};
        }


        public ICommand CalculateCommand
        {
            get{return new DelegatingCommand(Calculate);}
        }

        private void Calculate()
        {
            if (SelectedInterval == null || SelectedBuilding == null)
                return;

            var heatMeterReadings = Double.Parse(HeatMeterReadings);
            var waterMeterReadings = Double.Parse(WaterMeterReadings);

            var meterReadings = new MeterReadings
                                    {
                                        CurrentHeatMeterReader = heatMeterReadings,
                                        CurrentWaterHeatReader = waterMeterReadings,
                                        BuildingsId = SelectedBuilding.Id,
                                        DateTimeImtervals = SelectedInterval
                                    };
            clearingRepository.InsertMeterReading(meterReadings);
            
            var clearing = new Clearing
                               {
                                   Requirements = Double.Parse(RequirementHeat),
                                   CalculationHotWater = Double.Parse(WaterBuxgalter),
                                   BuildingsId = SelectedBuilding.Id
                               };

            var totalHeatConsumption =
                clearingRepository.GetContractConsumptions().Where(item => item.BuildingsId == SelectedBuilding.Id &&
                                                                           item.DateTimeImtervals == SelectedInterval).Select(x => x.TotalHeatConsumption).Single();

            clearing.CalculationHot = totalCalculator.TotalHeatConsumption(totalHeatConsumption, Double.Parse(WaterBuxgalter));
            clearing.DateTimeImtervals = SelectedInterval;
            clearingRepository.InsertClearing(clearing);
            clearingRepository.Save();
            Clearings.Add(clearing);
        }

        public UserControl View{get { return view; }}

        public string WaterMeterReadings { get; set; }
        public string HeatMeterReadings { get; set; }
        public string RequirementHeat { get; set; }
        public string WaterBuxgalter { get; set; }

        public Building SelectedBuilding { get; set; }
        public ObservableCollection<Building> Buildings { get; private set; }

        public DateTimeImtervals SelectedInterval { get; set; }
        public ObservableCollection<DateTimeImtervals> DateTimeIntervals { get; private set; }

        public ObservableCollection<Clearing> Clearings { get; private set; }
    }
}
