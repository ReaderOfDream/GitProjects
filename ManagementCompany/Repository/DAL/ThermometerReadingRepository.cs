using System.Collections.Generic;
using System.Linq;

namespace Repository.DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetItems();

        T GetThermometerReadingById(int id);
        void InsertThermometerReading(T item);
        void DeleteThermometerReading(T item);
        void UpdateThermometerReading(T item);
        void Save();
    }

    public interface IThermometerReadingRepository
    {
        IEnumerable<ThermometerReading> GetThermometerReadings();

        ThermometerReading GetThermometerReadingById(int id);
        void InsertThermometerReading(ThermometerReading thermReading);
        void DeleteThermometerReading(ThermometerReading thermReading);
        void UpdateThermometerReading(ThermometerReading thermReading);
        void Save();
    }

    public class ThermometerReadingRepository : IThermometerReadingRepository
    {
        private readonly MCDatabaseModelContainer db;

        public ThermometerReadingRepository(MCDatabaseModelContainer db)
        {
            this.db = db;
        }

        #region Implementation of IThermometerReadingRepository

        public IEnumerable<ThermometerReading> GetThermometerReadings()
        {
            return db.ThermometerReadings;
        }

        public ThermometerReading GetThermometerReadingById(int id)
        {
            return db.ThermometerReadings.Single(item => item.Id == id);
        }

        public void InsertThermometerReading(ThermometerReading thermReading)
        {
            db.ThermometerReadings.AddObject(thermReading);
        }

        public void DeleteThermometerReading(ThermometerReading thermReading)
        {
            var deletingItem = db.ThermometerReadings.Single(item => item.Id == thermReading.Id);
            db.ThermometerReadings.DeleteObject(deletingItem);
        }

        public void UpdateThermometerReading(ThermometerReading thermReading)
        {
            var updatingItem = db.ThermometerReadings.Single(item => item.Id == thermReading.Id);
            updatingItem.Month = thermReading.Month;
            updatingItem.Year = thermReading.Year;
            updatingItem.AirTemperature = thermReading.AirTemperature;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #endregion
    }
}
