using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.DAL
{
    public interface IClearingRepository
    {
        IEnumerable<ContractConsumptionHeat> GetContractConsumptions();
        IEnumerable<Clearing> GetClearings();
        IEnumerable<DateTimeImtervals> GetDateTimeIntervals();
        IEnumerable<Building> GetBuildings();

        void InsertMeterReading(MeterReadings readings);
        Clearing GetClearingById(int id);
        void InsertClearing(Clearing clearing);
        void DeleteClearing(Clearing clearing);
        void UpdateClearing(Clearing clearing);
        void Save();

    }
    
    public class ClearingRepository : IClearingRepository
    {
        private readonly MCDatabaseModelContainer db;

        public ClearingRepository(MCDatabaseModelContainer db)
        {
            this.db = db;
        }

        #region Implementation of IClearingRepository

        public IEnumerable<ContractConsumptionHeat> GetContractConsumptions()
        {
            return db.ContractConsumptionHeatTable;
        }

        public IEnumerable<Clearing> GetClearings()
        {
            return db.ClearingTable;
        }

        public IEnumerable<DateTimeImtervals> GetDateTimeIntervals()
        {
            return db.DateTimeImtervals;
        }

        public IEnumerable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        public void InsertMeterReading(MeterReadings readings)
        {
            db.MeterReadingsTable.AddObject(readings);
        }

        public Clearing GetClearingById(int id)
        {
            return db.ClearingTable.Single(item => item.Id == id);
        }

        public void InsertClearing(Clearing clearing)
        {
            db.ClearingTable.AddObject(clearing);
        }

        public void DeleteClearing(Clearing clearing)
        {
            var deletingItem = db.ClearingTable.Single(item => item.Id == clearing.Id);
            db.ClearingTable.DeleteObject(deletingItem);
        }

        public void UpdateClearing(Clearing clearing)
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
