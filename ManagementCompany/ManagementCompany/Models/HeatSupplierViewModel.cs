using System.Windows.Input;
using Core;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class HeatSupplierViewModel
    {

        public HeatSupplierViewModel(IHeatSupplierRepository heatSupplierRepository)
        {
            
        }

        public void CreateSupplier()
        {
            

        }


        public ICommand CreateSupplierCommand
        {
            get { return new DelegatingCommand(CreateSupplier, () => true); }
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
