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
        IEnumerable<DateTimeInterval> GetDateTimeIntervals();
        IEnumerable<Building> GetBuildings();

        void InsertMeterReading(MeterReading readings);
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
            return db.ContractConsumptionHeats;
        }

        public IEnumerable<Clearing> GetClearings()
        {
            return db.Clearings;
        }

        public IEnumerable<DateTimeInterval> GetDateTimeIntervals()
        {
            return db.DateTimeIntervals;
        }

        public IEnumerable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        public void InsertMeterReading(MeterReading readings)
        {
            db.MeterReadings.AddObject(readings);
        }

        public Clearing GetClearingById(int id)
        {
            return db.Clearings.Single(item => item.Id == id);
        }

        public void InsertClearing(Clearing clearing)
        {
            db.Clearings.AddObject(clearing);
        }

        public void DeleteClearing(Clearing clearing)
        {
            var deletingItem = db.Clearings.Single(item => item.Id == clearing.Id);
            db.Clearings.DeleteObject(deletingItem);
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
