using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.TotalCalculation
{
    public interface ITotalCalculator
    {
        double TotalHeatConsumption(double totalHeatConsumption, double accountantWater);
    }

    public class TotalCalculation : ITotalCalculator
    {
        #region Implementation of ITotalCalculator

        public double TotalHeatConsumption(double totalHeatConsumption, double accountantWater)
        {
            return totalHeatConsumption - accountantWater * 0.0586;
        }

        #endregion
    }
}
