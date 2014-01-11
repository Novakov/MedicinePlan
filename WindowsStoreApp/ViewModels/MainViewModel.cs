using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MedicinePlan;

namespace WindowsStoreApp.ViewModels
{
    public class MainViewModel
    {        
        private readonly Supplies supplies;
        private readonly DateTime asOfDate;

        public ObservableCollection<MedicineStatus> Medicines { get; set; }       

        public MainViewModel(Supplies supplies)
        {
            this.supplies = supplies;

            this.asOfDate = DateTime.Today;
            this.Medicines = new ObservableCollection<MedicineStatus>();

            this.DumpSuppliesStatus();
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
