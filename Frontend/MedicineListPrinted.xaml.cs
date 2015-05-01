using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Frontend.ViewModels;

namespace Frontend
{
    /// <summary>
    /// Interaction logic for MedicineListPrinted.xaml
    /// </summary>
    public partial class MedicineListPrinted : FlowDocument
    {
        public MedicineListPrintedViewModel ViewModel { get; set; }

        public MedicineListPrinted(MedicineListPrintedViewModel viewModel)
        {
            this.DataContext = viewModel;
            this.ViewModel = viewModel;
            InitializeComponent();            
        }
    }
}
