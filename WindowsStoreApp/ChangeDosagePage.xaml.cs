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

namespace WindowsStoreApp
{   
    public sealed partial class ChangeDosagePage : BasePage
    {
        public ChangeDosagePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var vm = new ChangeDosageViewModel((string)e.Parameter, App.Instance.Supplies, this);

            this.PageTitle = string.Format("Change dosage of {0}", vm.Name);

            this.DataContext = vm;
        }
    }
}
