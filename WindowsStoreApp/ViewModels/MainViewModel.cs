using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using WindowsStoreApp.Common;
using MedicinePlan;

namespace WindowsStoreApp.ViewModels
{
    public class MainViewModel
    {        
        private readonly Supplies supplies;
        private readonly INavigation navigation;
        private readonly DateTime asOfDate;

        public ObservableCollection<MedicineStatus> Medicines { get; private set; }

        public ICommand AddMedicineCommand { get; private set; }
        public ICommand RefillCommand { get; private set; }

        public object SelectedMedicine { get; set; }

        public MainViewModel(Supplies supplies, INavigation navigation)
        {
            this.supplies = supplies;
            this.navigation = navigation;

            this.asOfDate = DateTime.Today;
            this.Medicines = new ObservableCollection<MedicineStatus>();

            this.AddMedicineCommand = new RelayCommand(AddMedicine);

            this.RefillCommand = new RelayCommand<string>(Refill);

            this.DumpSuppliesStatus();
        }

        private void Refill(string medicine)
        {
            this.navigation.NavigateTo<RefillPage>(medicine);
        }

        private void AddMedicine()
        {
            this.navigation.NavigateTo<AddMedicinePage>();
        }

        private void DumpSuppliesStatus()
        {
            this.Medicines.Clear();

            foreach (var medicine in this.supplies.GetMedicines())
            {
                this.Medicines.Add(new MedicineStatus
                {
                    AsOfDate = this.asOfDate,
                    Name = medicine.Name,
                    Dosage = this.supplies.CurrentDosage(medicine, this.asOfDate),
                    ExhaustionDate = this.supplies.ExhaustionOf(medicine),
                    Remaining = this.supplies.RemainingStock(medicine, this.asOfDate).Count,
                });
            }
        }        
    }
}
