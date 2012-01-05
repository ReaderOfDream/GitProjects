using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using ManagementCompany.Views;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class HeatSupplierViewModel : INotifyPropertyChanged
    {
        private readonly IHeatSupplierRepository supplierRepository;
        private readonly UserControl view;

        public HeatSupplierViewModel(IHeatSupplierRepository heatSupplierRepository)
        {
            HeatSuppliers = new ObservableCollection<HeatSupplier>(heatSupplierRepository.GetHeatSuppliers());
            supplierRepository = heatSupplierRepository;
            view = new CreateHeatSupplier(){DataContext = this};
        }

        public void CreateSupplier()
        {
            if(string.IsNullOrEmpty(Name))
                return;

            var supplier = new HeatSupplier()
                               {
                                   Name = Name,
                                   Description = Description
                               };

            supplierRepository.InsertHeatSupplier(supplier);
            supplierRepository.Save();
            HeatSuppliers.Add(supplier);

            Name = string.Empty;
            Description = string.Empty;
        }

        public void DeleteSupplier()
        {
            if (selectedItem == null)
                return;

            supplierRepository.DeleteHeatSupplier(selectedItem.Id);
            supplierRepository.Save();

            HeatSuppliers.Remove(selectedItem);
        }

        public ICommand DeleteSupplierCommand
        {
            get
            {
                return new DelegatingCommand(DeleteSupplier);
            }
        }

        public ICommand CreateSupplierCommand
        {
            get { return new DelegatingCommand(CreateSupplier); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }

        private HeatSupplier selectedItem;
        public HeatSupplier SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }

        public ObservableCollection<HeatSupplier> HeatSuppliers { get; set; }

        public UserControl View
        {
            get { return view; }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate{};
    }
}
