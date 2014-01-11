﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((GridView)sender).SelectedItem != null)
            {
                this.BottomAppBar.IsOpen = true;

                foreach (var cmd in ((CommandBar)this.BottomAppBar).SecondaryCommands)
                {
                    ((UIElement)cmd).Visibility = Visibility.Visible;
                }
            }
            else
            {
                foreach (var cmd in ((CommandBar)this.BottomAppBar).SecondaryCommands)
                {
                    ((UIElement)cmd).Visibility = Visibility.Collapsed;
                }
            }
        }

        private void BottomAppBar_Closed(object sender, object e)
        {
            this.itemsView.SelectedItem = null;
        }
    }
}
