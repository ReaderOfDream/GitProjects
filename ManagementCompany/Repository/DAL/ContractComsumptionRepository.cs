using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAL
{
    public interface IContractConsumptionRepository
    {
        IEnumerable<DateTimeInterval> GetDateTimeIntervals();
        IEnumerable<ThermometerReading> GetThermometerReadings();
        IEnumerable<Building> GetBuildings();
        IEnumerable<ContractConsumptionHeat> GetConstractConsumptions();

        ContractConsumptionHeat GetConstractConsumptionById(int id);
        void InsertConstractConsumption(ContractConsumptionHeat item);
        void DeleteConstractConsumption(ContractConsumptionHeat item);
        void UpdateConstractConsumption(ContractConsumptionHeat item);
        void Save();
    }

    public class ContractComsumptionRepository : IContractConsumptionRepository
    {
        private readonly MCDatabaseModelContainer db;

        public ContractComsumptionRepository(MCDatabaseModelContainer db)
        {
            this.db = db;
        }

        #region Implementation of IRepository<ContractConsumptionHeat>

        public IEnumerable<DateTimeInterval> GetDateTimeIntervals()
        {
            return db.DateTimeIntervals;
        }

        public IEnumerable<ThermometerReading> GetThermometerReadings()
        {
            return db.ThermometerReadings;
        }

        public IEnumerable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        public IEnumerable<ContractConsumptionHeat> GetConstractConsumptions()
        {
            return db.ContractConsumptionHeats;
        }

        public ContractConsumptionHeat GetConstractConsumptionById(int id)
        {
            return db.ContractConsumptionHeats.Single(item => item.ID == id);
        }

        public void InsertConstractConsumption(ContractConsumptionHeat item)
        {
            db.ContractConsumptionHeats.AddObject(item);
        }

        public void DeleteConstractConsumption(ContractConsumptionHeat item)
        {
            var deletingItem = db.ContractConsumptionHeats.Single(x => x.ID == item.ID);
            db.ContractConsumptionHeats.DeleteObject(deletingItem);
        }

        public void UpdateConstractConsumption(ContractConsumptionHeat item)
        {
            var updatingItem = db.ContractConsumptionHeats.Single(x => x.ID == item.ID);
            updatingItem.HeatByLoading = item.HeatByLoading;
            updatingItem.HotWaterByNorm = item.HotWaterByNorm;
            updatingItem.TotalHeatConsumption = item.TotalHeatConsumption;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #endregion
    }
}