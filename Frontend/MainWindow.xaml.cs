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
            var store = IsolatedStorageFile.GetUserStoreForAssembly();

            using (var fs = store.OpenFile("MedicinePlan.json", FileMode.Create))
            {                
                var repo = new Repository();

                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(repo.DumpJson(this.supplies));
                }
            }
        }

        private static Supplies LoadSupplies()
        {
            var store = IsolatedStorageFile.GetUserStoreForAssembly();

            if (store.FileExists("MedicinePlan.json"))
            {
                using (var fs = store.OpenFile("MedicinePlan.json", FileMode.Open))
                {
                    var repo = new Repository();

                    using (var sr = new StreamReader(fs))
                    {
                        var state = sr.ReadToEnd();

                        return repo.ReadJson(state);
                    }
                }
            }

            return new Supplies();
        }
    }
}
