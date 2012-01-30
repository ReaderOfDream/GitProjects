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
        private MCDatabaseModelContainer db;

        public NormativeCalculationRepository(MCDatabaseModelContainer container)
        {
            db = container;
        }

        #region Implementation of INormativeCalculationRepository

        public IEnumerable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        public IEnumerable<DateTimeInterval> GetDateTimeIntervals()
        {
            return db.DateTimeIntervals;
        }

        public IEnumerable<NormativeCalculation> GetNormativeCalculations()
        {
            return db.NormativeCalculations;
        }

        public NormativeCalculation GetNormativeCalculationsId(int normativeCalculationsId)
        {
            return db.NormativeCalculations.Single(item => item.Id == normativeCalculationsId);
        }

        public void InsertNormativeCalculations(NormativeCalculation normativeCalculations)
        {
            db.NormativeCalculations.AddObject(normativeCalculations);
        }

        public void DeleteNormativeCalculations(int normativeCalculationsId)
        {
            var deletingItem = db.NormativeCalculations.Single(item => item.Id == normativeCalculationsId);
            db.NormativeCalculations.DeleteObject(deletingItem);
        }

        public void UpdateNormativeCalculations(NormativeCalculation normativeCalculations)
        {
            var updatingItem = db.NormativeCalculations.Single(item => item.Id == normativeCalculations.Id);

            updatingItem.CalculationArea = normativeCalculations.CalculationArea;
            updatingItem.EstimateConsumptionHeat = normativeCalculations.EstimateConsumptionHeat;
            updatingItem.ConsumptionHeatByCalculationArea = normativeCalculations.ConsumptionHeatByCalculationArea;
            updatingItem.ConsumptionHeatByTotalArea = normativeCalculations.ConsumptionHeatByTotalArea;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #endregion
    }
}
