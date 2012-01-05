using System;
using System.Windows;
using System.Windows.Input;
using Core;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class CreateObjectViewModel
    {
        public CreateObjectViewModel(IBuildingRepository buildingRepository)
        {
            
        }

        private void CreateObject()
        {
            MCDatabaseModelContainer context = null;
            try
            {
                context = new MCDatabaseModelContainer();
                var building = new Building
                                   {
                                       Name = Name,
                                       Description = Description,
                                       EstimateConsumptionHeat = estimatedConsumption
                                   };
                context.Buildings.AddObject(building);
                context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сохранить новую сущность", "Внимание!");
            }
            finally
            {
                if (context != null)
                    context.Dispose();
            }
        }

        private bool UserInputValid()
        {
            if (String.IsNullOrEmpty(Name))
                return false;

            if (estimatedConsumption <= 1)
                return false;

            return true;
        }

        public ICommand CreateObjectCommand { get { return new DelegatingCommand(CreateObject); } }

        public string Name { get; set; }
        public string Description { get; set; }

        private double estimatedConsumption;
        public string EstimatedConsumption
        {
            get { return estimatedConsumption.ToString(); }
            set
            {
                var result = Double.TryParse(value, out estimatedConsumption);
                if (!result)
                {
                    estimatedConsumption = 0.0;
                    throw new ArgumentException(String.Format("Не удалось преобразовать значение {0}", value));
                }
            }
        }
    }
}