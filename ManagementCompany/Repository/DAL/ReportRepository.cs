using System.Collections.Generic;
using System.Linq;

namespace Repository.DAL
{
    public interface IReportRepository
    {
        IEnumerable<HeatSupplier> GetHeatSuppliers();
        IEnumerable<DateTimeImtervals> GetDateTimeIntervals();
        DateTimeImtervals GetDateTimeIntervalId(int dateTimeIntervalId);
        void InsertDateTimeInterval(DateTimeImtervals dateTimeImterval);
        void DeleteDateTimeInterval(int dateTimeIntervalId);
        void UpdateDateTimeInterval(DateTimeImtervals dateTimeImterval);
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

        public IEnumerable<DateTimeImtervals> GetDateTimeIntervals()
        {
            return db.DateTimeImtervals;
        }

        public DateTimeImtervals GetDateTimeIntervalId(int dateTimeIntervalId)
        {
            return db.DateTimeImtervals.Single(interval => interval.Id == dateTimeIntervalId);
        }

        public void InsertDateTimeInterval(DateTimeImtervals dateTimeImterval)
        {
            db.DateTimeImtervals.AddObject(dateTimeImterval);
        }

        public void DeleteDateTimeInterval(int dateTimeIntervalId)
        {
            var deletingItem = db.DateTimeImtervals.Single(interval => interval.Id == dateTimeIntervalId);
            db.DateTimeImtervals.DeleteObject(deletingItem);
        }

        public void UpdateDateTimeInterval(DateTimeImtervals dateTimeImterval)
        {
            // TODO refactor me

            var updatingItem = db.DateTimeImtervals.Single(interval => interval.Id == dateTimeImterval.Id);
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
