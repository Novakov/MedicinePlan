using System;
using System.Collections.ObjectModel;
using MedicinePlan;

namespace Frontend.ViewModels
{
    public class DesignMainViewModel : MainViewModel
    {
        public DesignMainViewModel() : base(new Supplies())
        {
            this.Medicines = new ObservableCollection<MedicineStatus>
            {
                new MedicineStatus {Name = "DEXAMETHASON", Remaining = 205, Dosage = new CountPerDayDosage(3), ExhaustionDate = new DateTime(2014, 2, 11)},
                new MedicineStatus {Name = "Polprazol", Remaining = 60, Dosage = new CountPerDayDosage(1), ExhaustionDate = new DateTime(2014, 2, 18)},
                new MedicineStatus {Name = "Depakine Chrono 500", Remaining = 181, Dosage = new CountPerDayDosage(2), ExhaustionDate = new DateTime(2014, 3, 20)},
                new MedicineStatus {Name = "Depakine Chrono 300", Remaining = 100, Dosage = new CountPerDayDosage(1), ExhaustionDate = new DateTime(2014, 3, 6)},
            };
        }
    }
}