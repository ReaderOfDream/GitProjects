using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using ManagementCompany.Views;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class ThermometersReaderViewModel
    {
        private readonly IThermometerReadingRepository repository;
        private readonly UserControl view;

        public ThermometersReaderViewModel(IThermometerReadingRepository repository)
        {
            this.repository = repository;
            ThermometerReadings = new ObservableCollection<ThermometerReading>(repository.GetThermometerReadings());
            view = new ThermometerReadingView(){DataContext = this};
        }

        private void AddReading()
        {
            var newThermReading = new ThermometerReading()
                                      {
                                          AirTemperature = AirTemperature,
                                          Month = Month,
                                          Year = Year
                                      };
            repository.InsertThermometerReading(newThermReading);
            repository.Save();
            ThermometerReadings.Add(newThermReading);
        }

        public UserControl View { get { return view; }}

        public ICommand AddReadingCommand
        {
            get { return new DelegatingCommand(AddReading);}
        }

        public string Month { get; set; }
        public string Year { get; set; }
        public string AirTemperature { get; set; }
        public ObservableCollection<ThermometerReading> ThermometerReadings { get; private set; }
    }
}
