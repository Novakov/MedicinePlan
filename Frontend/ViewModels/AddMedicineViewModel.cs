using System;

namespace Frontend.ViewModels
{
    public class AddMedicineViewModel
    {
        public string Name { get; set; }
        public int CountPerDay { get; set; }

        public DateTime Date { get; set; }


        public AddMedicineViewModel()
        {
            this.Date = DateTime.Today;
            this.CountPerDay = 1;
        }
    }
}