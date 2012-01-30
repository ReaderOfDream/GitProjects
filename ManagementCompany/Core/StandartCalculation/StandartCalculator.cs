namespace Core.StandartCalculation
{
    public class StandartCalculator : IStandartCalculator
    {
        #region Implementation of IStandartCalculator

        public double CalculateConsumptionByArea(double area, double standartHeat)
        {
            return area * standartHeat;
        }

        public double CalculateTotalConsumption(double consumptionbyarea,double volumebystandart,double someconstant)
        {
            return consumptionbyarea + volumebystandart*someconstant;
        }

        #endregion
    }
}