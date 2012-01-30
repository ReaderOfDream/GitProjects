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
    public class BuildingViewModel : INotifyPropertyChanged
    {
        private readonly IBuildingRepository _supplierRepository;
        private readonly UserControl _view;

        public BuildingViewModel(IBuildingRepository buildingRepository)
        {
            try
            {
                Buildings = new ObservableCollection<Building>(buildingRepository.GetBuildings());
                HeatSuppliers = new ObservableCollection<HeatSupplier>(buildingRepository.GetSuppliers());
                _supplierRepository = buildingRepository;
                _view = new CreateBuildingView() {DataContext = this};
            }
            catch (Exception error)
            {
                int x;

            }
        }

        public ObservableCollection<Building> Buildings { get; set; }
        public ObservableCollection<HeatSupplier> HeatSuppliers { get; set; }

        private void CreateBuilding()
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
                                       StandartOfHeat = _standartOfHeat,
                                       TotalArea = _totalArea,
                                       HeatSupplier = SelectedHeatSupplier
                                   };
                _supplierRepository.InsertBuilding(building);
                _supplierRepository.Save();
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

        private void DeleteBuilding()
        {
            if (_selectedItem == null)
                return;

            _supplierRepository.DeleteBuilding(_selectedItem.Id);
            _supplierRepository.Save();

            Buildings.Remove(_selectedItem);

        }

        private bool UserInputValid()
        {
            if (String.IsNullOrEmpty(Name))
                return false;

            if (_standartOfHeat <= 1)
                return false;

            return true;
        }

        public ICommand CreateBuildingCommand { get { return new DelegatingCommand(CreateBuilding); } }
        public ICommand DeleteBuildingCommand
        {
            get
            {
                return new DelegatingCommand(DeleteBuilding);
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        
        private double _standartOfHeat;
        public string StandartOfHeat
        {
            get { return _standartOfHeat.ToString(); }
            set
            {
                var result = Double.TryParse(value, out _standartOfHeat);
                if (!result)
                {
                    _standartOfHeat = 0.0;
                    throw new ArgumentException(String.Format("Не удалось преобразовать значение {0}", value));
                }
            }
        }

        private double _totalArea;
        public string TotalArea
        {
            get { return _totalArea.ToString(); }
            set
            {
                var result = Double.TryParse(value, out _totalArea);
                if (!result)
                {
                    _totalArea = 0.0;
                    throw new ArgumentException(String.Format("Не удалось преобразовать значение {0}", value));
                }
            }

        }

        public HeatSupplier SelectedHeatSupplier { get; set; }

        private Building _selectedItem;
        public Building SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }

        public UserControl View
        {
            get { return _view; }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}