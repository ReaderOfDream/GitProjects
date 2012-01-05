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
        private INormativeCalculationRepository db;
        private readonly IStandartCalculator standartCalculator;
        private UserControl view;

        public NormativeCalculationViewModel(INormativeCalculationRepository normativeCalculationRepository, IStandartCalculator standartCalculator)
        {
            db = normativeCalculationRepository;
            this.standartCalculator = standartCalculator;
            DateTimeIntervals = new ObservableCollection<DateTimeImtervals>(db.GetDateTimeIntervals());
            Buildings = new ObservableCollection<Building>(db.GetBuildings());
            view = new NormativeCalculationView(){DataContext = this};
        }

        public void CreateNormativeCalculation()
        {
            if (SelectedBuilding == null)
                return;

            if (SelectedInterval == null)
                return;

            double calculationArea = Double.Parse(CalculationArea);
            double standartHeat = Double.Parse(Standart);

            double totalArea = db.GetBuildings().Single(building => building.Id == SelectedBuilding.Id).StandartOfHeat;
            var consumptionByTotalArea = standartCalculator.CalculateConsumptionByArea(totalArea, standartHeat);
            var consumptionByCalculationArea = standartCalculator.CalculateConsumptionByArea(calculationArea,
                                                                                             standartHeat);

            var normativeCalculation = new NormativeCalculations();
            normativeCalculation.CalculationArea = calculationArea;
            normativeCalculation.EstimateConsumptionHeat = standartHeat;
            normativeCalculation.ConsumptionHeatByTotalArea = consumptionByTotalArea;
            normativeCalculation.ConsumptionHeatByCalculationArea = consumptionByCalculationArea;
            normativeCalculation.Buildings = SelectedBuilding;
            normativeCalculation.DateTimeImtervals = SelectedInterval;

            db.InsertNormativeCalculations(normativeCalculation);
            db.Save();
        }

        public ICommand CreateNormativeCalculationCommand
        {
            get { return new DelegatingCommand(CreateNormativeCalculation); }
        }

        public UserControl View
        {
            get { return view; }
        }

        public string CalculationArea { get; set; }
        public string Standart { get; set; }

        public ObservableCollection<Building> Buildings { get; set; }
        public ObservableCollection<DateTimeImtervals> DateTimeIntervals { get; set; }

        public Building SelectedBuilding { get; set; }
        public DateTimeImtervals SelectedInterval { get; set; }
    }
}
