using System.Collections.Generic;
using System.Linq;

namespace Repository.DAL
{
    public interface INormativeCalculationRepository
    {
        IEnumerable<Building> GetBuildings();
        IEnumerable<DateTimeInterval> GetDateTimeIntervals();
        IEnumerable<NormativeCalculation> GetNormativeCalculations();
        NormativeCalculation GetNormativeCalculationsId(int normativeCalculationsId);
        void InsertNormativeCalculations(NormativeCalculation normativeCalculations);
        void DeleteNormativeCalculations(int normativeCalculationsId);
        void UpdateNormativeCalculations(NormativeCalculation normativeCalculations);
        void Save();
    }

    public class NormativeCalculationRepository : INormativeCalculationRepository
    {
        private readonly MCDatabaseModelContainer _db;

        public NormativeCalculationRepository(MCDatabaseModelContainer container)
        {
            _db = container;
        }

        #region Implementation of INormativeCalculationRepository

        public IEnumerable<Building> GetBuildings()
        {
            return _db.Buildings;
        }

        public IEnumerable<DateTimeInterval> GetDateTimeIntervals()
        {
            return _db.DateTimeIntervals;
        }

        public IEnumerable<NormativeCalculation> GetNormativeCalculations()
        {
            return _db.NormativeCalculations;
        }

        public NormativeCalculation GetNormativeCalculationsId(int normativeCalculationsId)
        {
            return _db.NormativeCalculations.Single(item => item.Id == normativeCalculationsId);
        }

        public void InsertNormativeCalculations(NormativeCalculation normativeCalculations)
        {
            _db.NormativeCalculations.AddObject(normativeCalculations);
        }

        public void DeleteNormativeCalculations(int normativeCalculationsId)
        {
            var deletingItem = _db.NormativeCalculations.Single(item => item.Id == normativeCalculationsId);
            _db.NormativeCalculations.DeleteObject(deletingItem);
        }

        public void UpdateNormativeCalculations(NormativeCalculation normativeCalculations)
        {
            var updatingItem = _db.NormativeCalculations.Single(item => item.Id == normativeCalculations.Id);

            updatingItem.EstimateConsumptionHeat = normativeCalculations.EstimateConsumptionHeat;
            updatingItem.ConsumptionHeatByCalculationArea = normativeCalculations.ConsumptionHeatByCalculationArea;
            updatingItem.ConsumptionHeatByTotalArea = normativeCalculations.ConsumptionHeatByTotalArea;
            updatingItem.TotalHeatConsumption = normativeCalculations.TotalHeatConsumption;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        #endregion
    }
}
