using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAL
{
    public interface IHeatSupplierRepository : IDisposable
    {
        IEnumerable<HeatSupplier> GetHeatSuppliers();
        HeatSupplier GetHeatSupplierById(int heatsupplierId);
        void InsertHeatSupplier(HeatSupplier heatSupplier);
        void DeleteHeatSupplier(int heatSupplierId);
        void UpdateHeatSupplier(HeatSupplier heatSupplier);
        void Save();
    }

    public class HeatSupplierRepository : IHeatSupplierRepository
    {
        private MCDatabaseModelContainer db;

        public HeatSupplierRepository(MCDatabaseModelContainer mcDatabaseModel)
        {
            db = mcDatabaseModel;
        }

        public IEnumerable<HeatSupplier> GetHeatSuppliers()
        {
            return db.HeatSuppliers;
        }

        public HeatSupplier GetHeatSupplierById(int heatSupplierId)
        {
            return db.HeatSuppliers.Single(supplier => supplier.Id == heatSupplierId);
        }

        public void InsertHeatSupplier(HeatSupplier heatSupplier)
        {
            db.HeatSuppliers.AddObject(heatSupplier);
        }

        public void DeleteHeatSupplier(int heatSupplierId)
        {
            var deletingSupplier = db.HeatSuppliers.Single(supplier => supplier.Id == heatSupplierId);
            db.HeatSuppliers.DeleteObject(deletingSupplier);
        }

        public void UpdateHeatSupplier(HeatSupplier heatSupplier)
        {
            // TODO: refactor me
            var updatingEntry = db.HeatSuppliers.Single(supplier => supplier.Id == heatSupplier.Id);

            updatingEntry.Name = heatSupplier.Name;
            updatingEntry.Description = heatSupplier.Description;
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception err)
            {
                int x;

            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
