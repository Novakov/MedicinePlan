using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MedicinePlan;

namespace Frontend.ViewModels
{
    public class MainViewModel
    {
        private readonly Supplies supplies;
        private DateTime asOfDate;

        public ObservableCollection<MedicineStatus> Medicines { get; set; }

        public DateTime AsOfDate
        {
            get { return this.asOfDate; }
            set { this.asOfDate = value; this.DumpSuppliesStatus(); }
        }

        public ICommand AddMedicineCommand { get; set; }
        public ICommand RefillCommand { get; set; }
        public ICommand RefillAllCommand { get; set; }

        public MainViewModel(Supplies supplies)
        {
            this.supplies = supplies;

            this.asOfDate = DateTime.Today;

            this.AddMedicineCommand = new DelegateCommand(AddMedicine);
            this.RefillCommand = new DelegateCommand<string>(RefillMedicine);
            this.RefillAllCommand = new DelegateCommand(RefillAllMedicine);

            this.Medicines = new ObservableCollection<MedicineStatus>();

            this.DumpSuppliesStatus();
        }

        private void RefillAllMedicine()
        {
            var viewModel = new RefillAllMedicinesViewModel(this.supplies.GetMedicines().Select(x => x.Name));

            var window = new RefillAllMedicinesWindow(viewModel);

            if (window.ShowDialog() == true)
            {
                this.supplies.Refill(viewModel.Stocks.ToDictionary(x => new Medicine(x.MedicineName), x => new Stock(x.Count, viewModel.Date)));
                
                this.DumpSuppliesStatus();                
            }
        }

        private void RefillMedicine(string medicineName)
        {
            var viewModel = new RefillMedicineViewModel {MedicineName = medicineName};

            var window = new RefillMedicineWindow(viewModel);

            if (window.ShowDialog() == true)
            {
                this.supplies.Refill(new Medicine(medicineName), new Stock(viewModel.Count, viewModel.Date));

                this.DumpSuppliesStatus();
            }
        }

        private void DumpSuppliesStatus()
        {
            this.Medicines.Clear();

            foreach (var medicine in this.supplies.GetMedicines())
            {
                this.Medicines.Add(new MedicineStatus
                {
                    Name = medicine.Name,
                    Dosage = this.supplies.CurrentDosage(medicine, this.asOfDate),
                    ExhaustionDate = this.supplies.ExhaustionOf(medicine),
                    Remaining = this.supplies.RemainingStock(medicine, this.asOfDate).Count
                });
            }
        }

        private void AddMedicine()
        {
            var viewModel = new AddMedicineViewModel();

            var addDosage = new AddMedicineWindow(viewModel);

            if (addDosage.ShowDialog() == true)
            {
                this.supplies.AddDosage(new Medicine(viewModel.Name), DateTime.Today, new CountPerDayDosage(viewModel.CountPerDay));

                this.DumpSuppliesStatus();
            }
        }
    }
}
