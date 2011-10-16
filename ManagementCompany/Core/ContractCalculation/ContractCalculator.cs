using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ContractCalculation
{
    public interface IContractCalculator
    {
        double ConsumptionByLoad(double estimatedConsumption, int countDays, double airTemperature);
        double HotWaterByNorm(int countPeople);
        double TotalHeatConsumption(double consumptionByLoad, double hotWaterByNorm);
    }


    public class ContractCalculator : IContractCalculator
    {
        #region Implementation of IContractCalculator

        public double ConsumptionByLoad(double estimatedConsumption, int countDays, double airTemperature)
        {
            return estimatedConsumption*countDays*24*(18 - airTemperature)/44;
        }

        public double HotWaterByNorm(int countPeople)
        {
            return 3.49*countPeople;
        }

        public double TotalHeatConsumption(double consumptionByLoad, double hotWaterByNorm)
        {
            return consumptionByLoad + hotWaterByNorm*0.0586;
        }

        #endregion
    }
}
