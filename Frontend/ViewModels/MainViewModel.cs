using System;
using System.Collections.Generic;
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

        public ObservableCollection<MedicineStatus> Medicines { get; set; }

        public ICommand AddMedicineCommand { get; set; }
        public ICommand RefillCommand { get; set; }

        public MainViewModel(Supplies supplies)
        {
            this.supplies = supplies;

            this.AddMedicineCommand = new DelegateCommand(AddMedicine);
            this.RefillCommand = new DelegateCommand<string>(RefillMedicine);

            this.Medicines = new ObservableCollection<MedicineStatus>();

            this.DumpSuppliesStatus();
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
                    Dosage = this.supplies.CurrentDosage(medicine, DateTime.Today),
                    ExhaustionDate = this.supplies.ExhaustionOf(medicine),
                    Remaining = this.supplies.RemainingStock(medicine, DateTime.Today).Count
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
