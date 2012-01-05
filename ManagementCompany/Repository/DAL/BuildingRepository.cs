using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.DAL
{
    public interface IBuildingRepository : IDisposable
    {
        IEnumerable<Building> GetBuildings();
        Building GeBuildingById(int buildingId);
        void InsertBuilding(Building building);
        void DeleteBuilding(int buildingId);
        void UpdateBuilding(Building building);
        void Save();
    }

    public class BuildingRepository : IBuildingRepository
    {
        private MCDatabaseModelContainer db;

        public BuildingRepository(MCDatabaseModelContainer mcDatabase)
        {
            db = mcDatabase;
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        public Building GeBuildingById(int buildingId)
        {
            return db.Buildings.Single(building => building.Id == buildingId);
        }

        public void InsertBuilding(Building building)
        {
            db.Buildings.AddObject(building);
        }

        public void DeleteBuilding(int buildingId)
        {
            var deletingEntity = db.Buildings.Single(building => building.Id == buildingId);
            db.Buildings.DeleteObject(deletingEntity);
        }

        public void UpdateBuilding(Building building)
        {
            // TODO: refactor me
            var updatingItem = db.Buildings.Single(item => item.Id == building.Id);

            updatingItem.Name = building.Name;
            updatingItem.Description = building.Description;
            updatingItem.EstimateConsumptionHeat = building.EstimateConsumptionHeat;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
