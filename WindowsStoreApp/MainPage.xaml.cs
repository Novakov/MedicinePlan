using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WindowsStoreApp.ViewModels;
using MedicinePlan;

namespace WindowsStoreApp
{
    public sealed partial class MainPage : BasePage
    {       
        public MainPage()
        {
            this.InitializeComponent();                       
            
            var viewModel = new MainViewModel(App.Instance.Supplies, this);

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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            //this.DataContext = new MainViewModel((Supplies) e.Parameter, this);
        }
    }
}
