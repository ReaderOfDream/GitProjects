using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAL
{
    public interface IContractConsumprionRepository
    {
        IEnumerable<DateTimeImtervals> GetDateTimeIntervals();
        IEnumerable<ThermometerReading> GetThermometerReadings();
        IEnumerable<Building> GetBuildings();
        IEnumerable<ContractConsumptionHeat> GetConstractConsumptions();

        ContractConsumptionHeat GetConstractConsumptionById(int id);
        void InsertConstractConsumption(ContractConsumptionHeat item);
        void DeleteConstractConsumption(ContractConsumptionHeat item);
        void UpdateConstractConsumption(ContractConsumptionHeat item);
        void Save();
    }

    public class ContractComsumptionRepository : IContractConsumprionRepository
    {
        private readonly MCDatabaseModelContainer db;

        public ContractComsumptionRepository(MCDatabaseModelContainer db)
        {
            this.db = db;
        }

        #region Implementation of IRepository<ContractConsumptionHeat>

        public IEnumerable<DateTimeImtervals> GetDateTimeIntervals()
        {
            return db.DateTimeImtervals;
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
            return db.ContractConsumptionHeatTable;
        }

        public ContractConsumptionHeat GetConstractConsumptionById(int id)
        {
            return db.ContractConsumptionHeatTable.Single(item => item.ID == id);
        }

        public void InsertConstractConsumption(ContractConsumptionHeat item)
        {
            db.ContractConsumptionHeatTable.AddObject(item);
        }

        public void DeleteConstractConsumption(ContractConsumptionHeat item)
        {
            var deletingItem = db.ContractConsumptionHeatTable.Single(x => x.ID == item.ID);
            db.ContractConsumptionHeatTable.DeleteObject(deletingItem);
        }

        public void UpdateConstractConsumption(ContractConsumptionHeat item)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #endregion
    }
}
