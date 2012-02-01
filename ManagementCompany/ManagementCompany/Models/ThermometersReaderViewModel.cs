using System.Collections.Generic;
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
        
        public EnumMonth selectedMonth { get; set; }
        public int Year { get; set; }
        public int AirTemperature { get; set; }
        
        public ObservableCollection<ThermometerReading> ThermometerReadings { get; private set; }
        public ObservableCollection<EnumMonth> Months { get; set; }

        private readonly IThermometerReadingRepository repository;
        private readonly UserControl view;

        public ThermometersReaderViewModel(IThermometerReadingRepository repository)
        {
            this.repository = repository;
            ThermometerReadings = new ObservableCollection<ThermometerReading>(repository.GetThermometerReadings());
            var monthList = new List<EnumMonth>(12)
                                {
                                    EnumMonth.Январь,
                                    EnumMonth.Февраль,
                                    EnumMonth.Март,
                                    EnumMonth.Апрель,
                                    EnumMonth.Май,
                                    EnumMonth.Июнь,
                                    EnumMonth.Июль,
                                    EnumMonth.Август,
                                    EnumMonth.Сентябрь,
                                    EnumMonth.Октябрь,
                                    EnumMonth.Ноябрь,
                                    EnumMonth.Декабрь
                                };
            Months = new ObservableCollection<EnumMonth>(monthList);
            view = new ThermometerReadingView(){DataContext = this};
        }

        private void AddReading()
        {
            var newThermReading = new ThermometerReading()
                                      {
                                          AirTemperature = AirTemperature,
                                          Month = (int) selectedMonth,
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

    }
}
