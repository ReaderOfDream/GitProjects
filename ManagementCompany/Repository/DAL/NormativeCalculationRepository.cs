using System.Collections.Generic;
using System.Linq;

namespace Repository.DAL
{
    public interface INormativeCalculationRepository
    {
        IEnumerable<Building> GetBuildings();
        IEnumerable<DateTimeImtervals> GetDateTimeIntervals();
        IEnumerable<NormativeCalculations> GetNormativeCalculations();
        NormativeCalculations GetNormativeCalculationsId(int normativeCalculationsId);
        void InsertNormativeCalculations(NormativeCalculations normativeCalculations);
        void DeleteNormativeCalculations(int normativeCalculationsId);
        void UpdateNormativeCalculations(NormativeCalculations normativeCalculations);
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

        public IEnumerable<DateTimeImtervals> GetDateTimeIntervals()
        {
            return db.DateTimeImtervals;
        }

        public IEnumerable<NormativeCalculations> GetNormativeCalculations()
        {
            return db.NormativeCalculations;
        }

        public NormativeCalculations GetNormativeCalculationsId(int normativeCalculationsId)
        {
            return db.NormativeCalculations.Single(item => item.Id == normativeCalculationsId);
        }

        public void InsertNormativeCalculations(NormativeCalculations normativeCalculations)
        {
            db.NormativeCalculations.AddObject(normativeCalculations);
        }

        public void DeleteNormativeCalculations(int normativeCalculationsId)
        {
            var deletingItem = db.NormativeCalculations.Single(item => item.Id == normativeCalculationsId);
            db.NormativeCalculations.DeleteObject(deletingItem);
        }

        public void UpdateNormativeCalculations(NormativeCalculations normativeCalculations)
        {
            var updatingItem = db.NormativeCalculations.Single(item => item.Id == normativeCalculations.Id);

            updatingItem.CalculationArea = normativeCalculations.CalculationArea;
            updatingItem.EstimateConsumptionHeat = normativeCalculations.EstimateConsumptionHeat;
            updatingItem.ConsumptionHeatByCalculationArea = normativeCalculations.ConsumptionHeatByCalculationArea;
            updatingItem.ConsumptionHeatByTotalArea = normativeCalculations.ConsumptionHeatByTotalArea;
            updatingItem.BuildingsId = normativeCalculations.BuildingsId;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #endregion
    }
}
