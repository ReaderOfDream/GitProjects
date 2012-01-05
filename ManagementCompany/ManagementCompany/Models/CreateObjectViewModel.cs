using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using ManagementCompany.Views;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class CreateObjectViewModel : INotifyPropertyChanged
    {
        private readonly IBuildingRepository supplierRepository;
        private readonly UserControl view;

        public CreateObjectViewModel(IBuildingRepository buildingRepository)
        {
            Buildings = new ObservableCollection<Building>(buildingRepository.GetBuildings());
            HeatSuppliers = new ObservableCollection<HeatSupplier>(buildingRepository.GetSuppliers());
            supplierRepository = buildingRepository;
            view = new CreateObjectView() { DataContext = this };
        }

        public ObservableCollection<Building> Buildings { get; set; }
        public ObservableCollection<HeatSupplier> HeatSuppliers { get; set; }

        private void CreateObject()
        {
            if (string.IsNullOrEmpty(Name))
                return;

            if (SelectedHeatSupplier == null)
                return;
            try
            {
                var building = new Building
                                   {
                                       Name = Name,
                                       Description = Description,
                                       EstimateConsumptionHeat = estimatedConsumption,
                                       TotalArea = TotalArea,
                                       HeatSupplier = SelectedHeatSupplier
                                   };
                supplierRepository.InsertBuilding(building);
                supplierRepository.Save();
                Buildings.Add(building);

                //Clean Gui's text
                Name = string.Empty;
                Description = string.Empty;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Внимание!");
            }
        }

        private void DeleteObject()
        {
            if (selectedItem == null)
                return;

            supplierRepository.DeleteBuilding(selectedItem.Id);
            supplierRepository.Save();

            Buildings.Remove(selectedItem);

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
        public ICommand DeleteObjectCommand
        {
            get
            {
                return new DelegatingCommand(DeleteObject);
            }
        }

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
        public string TotalArea { get; set; }
        public HeatSupplier SelectedHeatSupplier { get; set; }

        private Building selectedItem;
        public Building SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }
        
        public UserControl View
        {
            get { return view; }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}