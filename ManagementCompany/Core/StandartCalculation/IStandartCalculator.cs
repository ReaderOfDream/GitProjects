using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.StandartCalculation
{
    public interface IStandartCalculator
    {
        double CalculateConsumptionByArea(double area, double standartHeat);
    }
}
