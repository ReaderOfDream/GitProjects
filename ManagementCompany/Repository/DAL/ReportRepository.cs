using System.Collections.Generic;
using System.Linq;

namespace Repository.DAL
{
    public interface IReportRepository
    {
        IEnumerable<HeatSupplier> GetHeatSuppliers();
        IEnumerable<DateTimeInterval> GetDateTimeIntervals();
        DateTimeInterval GetDateTimeIntervalId(int dateTimeIntervalId);
        void InsertDateTimeInterval(DateTimeInterval dateTimeImterval);
        void DeleteDateTimeInterval(int dateTimeIntervalId);
        void UpdateDateTimeInterval(DateTimeInterval dateTimeImterval);
        void Save();
    }

    public class ReportRepository : IReportRepository
    {
        private MCDatabaseModelContainer db;

        public ReportRepository(MCDatabaseModelContainer container)
        {
            db = container;
        }

        public IEnumerable<HeatSupplier> GetHeatSuppliers()
        {
            return db.HeatSuppliers;
        }

        public IEnumerable<DateTimeInterval> GetDateTimeIntervals()
        {
            return db.DateTimeIntervals;
        }

        public DateTimeInterval GetDateTimeIntervalId(int dateTimeIntervalId)
        {
            return db.DateTimeIntervals.Single(interval => interval.Id == dateTimeIntervalId);
        }

        public void InsertDateTimeInterval(DateTimeInterval dateTimeImterval)
        {
            db.DateTimeIntervals.AddObject(dateTimeImterval);
        }

        public void DeleteDateTimeInterval(int dateTimeIntervalId)
        {
            var deletingItem = db.DateTimeIntervals.Single(interval => interval.Id == dateTimeIntervalId);
            db.DateTimeIntervals.DeleteObject(deletingItem);
        }

        public void UpdateDateTimeInterval(DateTimeInterval dateTimeImterval)
        {
            // TODO refactor me

            var updatingItem = db.DateTimeIntervals.Single(interval => interval.Id == dateTimeImterval.Id);
            updatingItem.StartDate = dateTimeImterval.StartDate;
            updatingItem.EndDate = dateTimeImterval.EndDate;
            updatingItem.HeatSupplier = dateTimeImterval.HeatSupplier;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
