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
    public class RefillAllViewModel
    {
        private readonly Supplies supplies;
        private readonly INavigation navigation;

        public DateTime Date { get; set; }

        public List<Item> Medicines { get; set; }

        public ICommand RefillAllCommand { get; set; }

        public RefillAllViewModel(Supplies supplies, INavigation navigation)
        {            
            this.supplies = supplies;
            this.navigation = navigation;

            this.Date = DateTime.Today;

            this.Medicines = this.supplies.GetMedicines().Select(x => new Item { Name = x.Name }).ToList();

            this.RefillAllCommand = new RelayCommand(RefillAll);
        }

        private void RefillAll()
        {
            var data = this.Medicines.ToDictionary(x => new Medicine(x.Name), x => new Stock(x.Count, this.Date));

            this.supplies.Refill(data);

            this.navigation.GoBack();
        }

        public class Item
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }
    }
}
