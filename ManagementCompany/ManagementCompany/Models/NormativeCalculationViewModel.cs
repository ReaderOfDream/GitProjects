using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using Core.StandartCalculation;
using ManagementCompany.Views;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class NormativeCalculationViewModel
    {
        private readonly INormativeCalculationRepository _db;
        private readonly IStandartCalculator _standartCalculator;
        private readonly UserControl _view;

        public string CalculationArea { get; set; }
        public string Standart { get; set; }

        public ObservableCollection<Building> Buildings { get; set; }
        public ObservableCollection<DateTimeInterval> DateTimeIntervals { get; set; }

        public Building SelectedBuilding { get; set; }
        public DateTimeInterval SelectedInterval { get; set; }

        private const double _constForCalculationTotalConsumption = 0.0586;

        public NormativeCalculationViewModel(INormativeCalculationRepository normativeCalculationRepository, IStandartCalculator standartCalculator)
        {
            _db = normativeCalculationRepository;
            _standartCalculator = standartCalculator;
            DateTimeIntervals = new ObservableCollection<DateTimeInterval>(_db.GetDateTimeIntervals());
            Buildings = new ObservableCollection<Building>(_db.GetBuildings());
            _view = new NormativeCalculationView(){DataContext = this};
        }

        public void CreateNormativeCalculation()
        {
            if (SelectedBuilding == null)
                return;

            if (SelectedInterval == null)
                return;

            var calculationArea = Double.Parse(CalculationArea);
            var standartHeat = Double.Parse(Standart);

            var totalArea = Buildings.Single(building => building.Id == SelectedBuilding.Id).StandartOfHeat;
            var consumptionByCalculationArea = _standartCalculator.CalculateConsumptionByArea(totalArea, standartHeat);
            var consumptionByTotalArea = _standartCalculator.CalculateTotalConsumption(calculationArea,
                                                                                             standartHeat,
                                                                                             _constForCalculationTotalConsumption);

            var normativeCalculation = new NormativeCalculation
                                           {
                                               EstimateConsumptionHeat = standartHeat,
                                               ConsumptionHeatByTotalArea = consumptionByTotalArea,
                                               ConsumptionHeatByCalculationArea = consumptionByCalculationArea,
                                               Building = SelectedBuilding,
                                               DateTimeInterval = SelectedInterval
                                           };


            _db.InsertNormativeCalculations(normativeCalculation);
            _db.Save();
        }

        public ICommand CreateNormativeCalculationCommand
        {
            get { return new DelegatingCommand(CreateNormativeCalculation); }
        }

        public UserControl View
        {
            get { return _view; }
        }

    }
}
