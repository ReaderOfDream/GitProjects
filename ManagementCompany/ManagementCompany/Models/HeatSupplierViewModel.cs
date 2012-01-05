using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using ManagementCompany.Views;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class HeatSupplierViewModel
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


        public ICommand CreateSupplierCommand
        {
            get { return new DelegatingCommand(CreateSupplier, () => true); }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<HeatSupplier> HeatSuppliers { get; set; }
        public UserControl View
        {
            get { return view; }
        }
    }
}
