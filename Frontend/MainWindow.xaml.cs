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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MedicinePlan;

namespace Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var supplies = new Supplies();
            supplies.AddDosage(new Medicine("VitaminumC"), DateTime.Today.AddDays(-7), new CountPerDayDosage(5));

            this.DataContext = new ViewModels.MainViewModel(supplies);
        }
    }
}
