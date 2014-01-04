using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
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
        private readonly Supplies supplies;

        public MainWindow()
        {
            InitializeComponent();

            this.supplies = LoadSupplies();

            this.DataContext = new ViewModels.MainViewModel(this.supplies);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            SaveSupplies();
        }

        private void SaveSupplies()
        {
            var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MedicinePlan");
            var file = System.IO.Path.Combine(path, "MedicinePlan.json");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var repo = new Repository();

            File.WriteAllText(file, repo.DumpJson(this.supplies));
        }

        private static Supplies LoadSupplies()
        {
            var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MedicinePlan");
            var file = System.IO.Path.Combine(path, "MedicinePlan.json");

            if (!Directory.Exists(path) || !File.Exists(file))
            {
                return new Supplies();
            }

            using (var sr = File.OpenText(file))
            {
                var repo = new Repository();

                var state = sr.ReadToEnd();

                return repo.ReadJson(state);
            }
        }
    }
}
