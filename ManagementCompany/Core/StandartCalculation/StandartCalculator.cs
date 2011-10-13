namespace Core.StandartCalculation
{
    public class StandartCalculator : IStandartCalculator
    {
        #region Implementation of IStandartCalculator

        public double CalculateConsumptionByArea(double area, double standartHeat)
        {
            return area * standartHeat;
        }

        #endregion
    }
}