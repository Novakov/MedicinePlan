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
    public class ChangeDosageViewModel
    {
        private readonly Supplies supplies;
        private readonly INavigation navigation;

        public string Name { get; private set; }

        public DateTime Since { get; set; }

        public int CountPerDay { get; set; }

        public ICommand ChangeDosageCommand { get; private set; }

        public ChangeDosageViewModel(string name, Supplies supplies, INavigation navigation)
        {
            this.supplies = supplies;
            this.navigation = navigation;            

            this.Name = name;

            this.Since = DateTime.Today;
            this.CountPerDay = ((CountPerDayDosage)supplies.CurrentDosage(new Medicine(name), DateTime.Today)).CountPerDay;

            this.ChangeDosageCommand = new RelayCommand(ChangeDosage);
        }

        private void ChangeDosage()
        {
            this.supplies.AddDosage(new Medicine(this.Name), this.Since, new CountPerDayDosage(this.CountPerDay));

            this.navigation.GoBack();
        }        
    }
}
