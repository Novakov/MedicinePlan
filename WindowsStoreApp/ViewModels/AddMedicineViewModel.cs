using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsStoreApp.Common;
using MedicinePlan;

namespace WindowsStoreApp.ViewModels
{
    public class AddMedicineViewModel
    {
        private readonly Supplies supplies;
        private readonly INavigation navigation;

        public string MedicineName { get; set; }

        public DateTime Since { get; set; }

        public int CountPerDay { get; set; }

        public int Remaining { get; set; }

        public ICommand AddCommand { get; private set; }

        public AddMedicineViewModel(Supplies supplies, INavigation navigation)
        {
            this.supplies = supplies;
            this.navigation = navigation;
            this.CountPerDay = 1;
            this.Remaining = 10;
            this.Since = DateTime.Today;

            this.AddCommand = new RelayCommand(Add);
        }

        private void Add()
        {
            var medicine = new Medicine(this.MedicineName);

            this.supplies.AddDosage(medicine, this.Since, new CountPerDayDosage(this.CountPerDay));
            this.supplies.Refill(medicine, new Stock(this.Remaining, this.Since));

            this.navigation.GoBack();
        }
    }
}
