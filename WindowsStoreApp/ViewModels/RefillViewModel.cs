using MedicinePlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsStoreApp.Common;

namespace WindowsStoreApp.ViewModels
{
    public class RefillViewModel
    {
        private readonly Supplies supplies;
        private readonly INavigation navigation;

        public string MedicineName { get; private set; }

        public DateTime Date { get; set; }

        public int Count { get; set; }

        public ICommand RefillCommand { get; set; }

        public RefillViewModel(string medicineName, Supplies supplies, INavigation navigation)
        {
            this.supplies = supplies;
            this.navigation = navigation;

            this.MedicineName = medicineName;
            this.Count = 20;
            this.Date = DateTime.Today;

            this.RefillCommand = new RelayCommand(Refill);
        }

        private void Refill()
        {
            this.supplies.Refill(new Medicine(this.MedicineName), new Stock(this.Count, this.Date));

            this.navigation.GoBack();
        }        
    }
}
