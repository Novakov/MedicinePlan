using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsStoreApp.ViewModels;
using MedicinePlan;

namespace WindowsStoreApp
{
    public sealed partial class MainPage : BasePage
    {
        private Supplies supplies;

        public MainPage()
        {
            this.InitializeComponent();

            this.supplies = new Supplies();

            var dt = new DateTime(2014, 1, 1);
            var dx = new Medicine("Dexamethason");
            var p = new Medicine("Polprazol");
            var dc500 = new Medicine("Depakine Chrono 500");
            var dc300 = new Medicine("Depakine Chrono 300");

            this.supplies.AddDosage(dx, dt, new CountPerDayDosage(2));
            this.supplies.AddDosage(p, dt, new CountPerDayDosage(1));
            this.supplies.AddDosage(dc500, dt, new CountPerDayDosage(2));
            this.supplies.AddDosage(dc300, dt, new CountPerDayDosage(1));

            this.supplies.Refill(new Dictionary<Medicine, Stock>
            {
                {dx, new Stock(100, dt)},
                {p, new Stock(100, dt)},
                {dc500, new Stock(100, dt)},
                {dc300, new Stock(100, dt)},
            });

            var viewModel = new MainViewModel(this.supplies);

            this.DataContext = viewModel;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.PageTitle = DateTime.Now.ToString();
        }
    }
}
