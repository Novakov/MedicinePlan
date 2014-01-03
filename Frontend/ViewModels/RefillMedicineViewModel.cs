using System;

namespace Frontend.ViewModels
{
    public class RefillMedicineViewModel
    {
        public string MedicineName { get; set; }

        public int Count { get; set; }
        public DateTime Date { get; set; }

        public RefillMedicineViewModel()
        {
            this.MedicineName = "Vitaminum C";
            this.Date = DateTime.Today;
            this.Count = 20;
        }
    }
}