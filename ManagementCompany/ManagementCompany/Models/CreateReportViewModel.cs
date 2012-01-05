using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using ManagementCompany.Views;
using Repository;
using Repository.DAL;

namespace ManagementCompany.Models
{
    public class CreateReportViewModel
    {
        private readonly UserControl view;
        private IReportRepository db;

        public CreateReportViewModel(IReportRepository repository)
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            view = new CreateReportView(){DataContext = this};
            db = repository;
            HeatSuppliers = new ObservableCollection<HeatSupplier>(repository.GetHeatSuppliers());
            DateTimeIntervals = new ObservableCollection<DateTimeImtervals>(repository.GetDateTimeIntervals());
        }

        public void CreateReport()
        {
            if (string.IsNullOrEmpty(Name))
                return;

            if (SelectedHeatSupplier == null)
                return;

            var interval = new DateTimeImtervals()
                               {
                                   Name = Name,
                                   StartDate = StartDate,
                                   EndDate = EndDate,
                                   HeatSupplier = SelectedHeatSupplier
                               };
            db.InsertDateTimeInterval(interval);
            db.Save();
            DateTimeIntervals.Add(interval);
        }

        public ObservableCollection<DateTimeImtervals> DateTimeIntervals { get; set; }
        public ObservableCollection<HeatSupplier> HeatSuppliers { get; set; }

        public UserControl View
        {
            get { return view; }
        }

        public ICommand CreateReportCommand
        {
            get { return new DelegatingCommand(CreateReport); }
        }

        public HeatSupplier SelectedHeatSupplier { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
    }
}
