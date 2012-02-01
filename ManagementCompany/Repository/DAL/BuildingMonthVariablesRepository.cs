using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.DAL
{
    public interface IBuildingMonthVariablesRepository
    {
        IEnumerable<Building> GetBuildings();
        IEnumerable<DateTimeInterval> GetDateTimeIntervals();

        BuildingMonthVariables GetBuildingMonthVariablesById(int buildingMonthVariablesId);
        void InsertBuildingMonthVariables(BuildingMonthVariables buildingMonthVariables);
        void DeleteBuildingMonthVariables(int buildingMonthVariablesId);
        void UpdateBuildingMonthVariables(BuildingMonthVariables buildingMonthVariables);
        void Save();
    }

    public class BuildingMonthVariablesRepository : IBuildingMonthVariablesRepository
    {
        private readonly MCDatabaseModelContainer _db;

        public BuildingMonthVariablesRepository(MCDatabaseModelContainer db)
        {
            _db = db;
        }

        public IEnumerable<Building> GetBuildings()
        {
            return _db.Buildings;
        }

        public IEnumerable<DateTimeInterval> GetDateTimeIntervals()
        {
            return _db.DateTimeIntervals;
        }

        public BuildingMonthVariables GetBuildingMonthVariablesById(int buildingMonthVariablesId)
        {
            return _db.BuildingMonthVariablesTable.Single(variables => variables.Id == buildingMonthVariablesId);
        }

        public void InsertBuildingMonthVariables(BuildingMonthVariables buildingMonthVariables)
        {
            _db.BuildingMonthVariablesTable.AddObject(buildingMonthVariables);
        }

        public void DeleteBuildingMonthVariables(int buildingMonthVariablesId)
        {
            var deletingEntity =
                _db.BuildingMonthVariablesTable.Single(variables => variables.Id == buildingMonthVariablesId);
            _db.BuildingMonthVariablesTable.DeleteObject(deletingEntity);
        }

        public void UpdateBuildingMonthVariables(BuildingMonthVariables buildingMonthVariables)
        {
            var updatingItem =
                _db.BuildingMonthVariablesTable.Single(variables => variables.Id == buildingMonthVariables.Id);

            updatingItem.CalculationArea = buildingMonthVariables.CalculationArea;
            updatingItem.CountOfPeople = buildingMonthVariables.CountOfPeople;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
