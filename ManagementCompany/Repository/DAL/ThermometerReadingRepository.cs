using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.DAL
{
    public interface IThermometerReadingRepository
    {
        IEnumerable<ThermometerReading> GetThermometerReadings();

        ThermometerReading GetThermometerReadingById(int id);
        void InsertThermometerReading(ThermometerReading thermReading);
        void DeleteThermometerReading(ThermometerReading thermReading);
        void UpdateThermometerReading(ThermometerReading thermReading);
        void Save();
    }

    class ThermometerReadingRepository : IThermometerReadingRepository
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
            throw new NotImplementedException();
        }

        public void InsertThermometerReading(ThermometerReading thermReading)
        {
            throw new NotImplementedException();
        }

        public void DeleteThermometerReading(ThermometerReading thermReading)
        {
            throw new NotImplementedException();
        }

        public void UpdateThermometerReading(ThermometerReading thermReading)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
