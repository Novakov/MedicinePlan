using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for RefillMedicineWindow.xaml
    /// </summary>
    public partial class RefillMedicineWindow : Window
    {
        public RefillMedicineWindow(RefillMedicineViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void RefillClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
